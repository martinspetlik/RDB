﻿using RDB.Data.DAL;
using RDB.Data.Models;
using System;
using System.Drawing;
using System.Text;
using System.IO;
using System.Collections;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Linq;
using RDB.Data.Extensions;
using MySql.Data.MySqlClient;
using System.Configuration;
using RDB.UI.Watermarking.Models;

namespace RDB.UI.Watermarking
{
    // http://iranarze.ir/wp-content/uploads/2017/08/7338-English-IranArze.pdf
    public class Watermark
    {
        #region Constants

        private static double MINIMUM_PROBABILITY = 0.65;

        #endregion

        #region Fields

        private readonly DefaultContext defaultContext;

        /// <summary>
        /// Fraction of items that can be marked
        /// </summary>
        private readonly int fraction;

        /// <summary>
        /// Number of LSB candidate bits to be modified
        /// </summary>
        private readonly int lsbCandidates;

        /// <summary>
        /// Secrete key
        /// </summary>
        private readonly int secretKey;

        private readonly BitArray[] imageBitMatrix;

        #endregion

        #region Private Fields

        private int imageRowSize;

        private int imageColumnSize;

        #endregion

        #region Constructors

        public Watermark(DefaultContext defaultContext)
        {
            this.imageBitMatrix = this.ProcessImage("Watermarking/random_image.jpg");
            this.defaultContext = defaultContext;
            this.fraction = 1;
            this.lsbCandidates = 3;
            this.secretKey = 5674932;
        }

        #endregion

        #region Public methods

        public void MarkData()
        {
            if (CheckWatermark() <= MINIMUM_PROBABILITY)
            {
                try
                {
                    String updateCommand = String.Empty;

                    Drive[] drives = defaultContext.Drives.AsNoTracking().ToArray();
                    foreach (Drive drive in drives)
                    {
                        string primaryKey = this.GetPKString(drive);
                        // Hash from primary key + our 'secret' key
                        var hash = this.CreateHash(String.Concat(this.secretKey, primaryKey));

                        if (hash % this.fraction == 0)
                        {
                            var bitIndex = hash % this.lsbCandidates;
                            bool watermarkBit = this.GetWatermarkBit(hash);
                            Int32 seconds = (int)((DateTimeOffset)drive.Time).ToUnixTimeSeconds();

                            BitArray secondBits = new BitArray(new int[] { seconds });
                            secondBits[bitIndex] = watermarkBit;

                            int[] array = new int[4];
                            secondBits.CopyTo(array, 0);
                            int newSeconds = array[0];

                            updateCommand += $"UPDATE Jizda SET cas = FROM_UNIXTIME({drive.Time.AddSeconds(newSeconds - seconds).ToTimestamp()}) WHERE linka = '{drive.RouteNumber}' AND cas = FROM_UNIXTIME({drive.Time.ToTimestamp()}); ";
                        }
                    }

                    Int32 rowsUpdatedCount = 0;
                    using (MySqlConnection mySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultContext"].ConnectionString))
                    {
                        MySqlCommand sqlCommand = new MySqlCommand();
                        sqlCommand.Connection = mySqlConnection;
                        sqlCommand.CommandText = updateCommand;
                        mySqlConnection.Open();

                        rowsUpdatedCount = sqlCommand.ExecuteNonQuery();
                        sqlCommand.Dispose();

                        mySqlConnection.Close();
                        mySqlConnection.Dispose();
                    }

                    MessageBox.Show($"Data označena (celkem označeno {rowsUpdatedCount} zaznamu)!");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Označení dat se nepodařilo!");
                }
            }
            else
                MessageBox.Show("Data již byla označena!");
            //this.changeData();
            //this.CheckWatermark();
        }

        public ResultModel IsDataOurs()
        {
            return new ResultModel(CheckWatermark(), MINIMUM_PROBABILITY);
        }

        #endregion

        #region Private methods

        private float CheckWatermark()
        {
            int totalCount = 0;
            int matchCount = 0;

            Drive[] drives = defaultContext.Drives.AsNoTracking().ToArray();
            foreach (Drive drive in drives)
            {
                string primaryKey = this.GetPKString(drive);

                // Hash from primary key + our 'secret' key
                var hash = this.CreateHash(String.Concat(this.secretKey, primaryKey));

                if (hash % this.fraction == 0)
                {
                    var bitIndex = hash % this.lsbCandidates;
                    bool watermarkBit = this.GetWatermarkBit(hash);

                    Int32 seconds = (int)((DateTimeOffset)drive.Time).ToUnixTimeSeconds();
                    BitArray secondBits = new BitArray(new int[] { seconds });

                    totalCount++;
                    if (secondBits[bitIndex] == watermarkBit)
                    {
                        matchCount++;
                    }
                }

            }
            var ratio = (float)matchCount / (float)totalCount;
            return ratio;
        }

        private BitArray[] ProcessImage(String imagePath)
        {

            BitArray bits = new BitArray(File.ReadAllBytes(imagePath));
            // Should be from image variable
            this.imageRowSize = 1176;
            this.imageColumnSize = 71;


            return this.BitsToMatrix(bits, imageRowSize, imageColumnSize);

        }

        private byte[] LoadImage(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();

            }
        }

        private BitArray[] BitsToMatrix(BitArray Input, int rowSize, int columnSize)
        {
            BitArray[] Output = new BitArray[rowSize];

            for (int i = 0; i < rowSize; i += 1)
            {
                BitArray bitsRow = new BitArray(columnSize);
                for (int j = 0; j < columnSize; j++)
                {
                    bitsRow.Set(j, Input.Get((i * columnSize) + j));
                }

                Output.SetValue(bitsRow, i);
            }
            return Output;
        }

        private String GetPKString(Drive drive)
        {
            // Join all primary key attributes
            string[] primaryKeys = new String[] { drive.RouteNumber, drive.BusPlate };
            return String.Join("", primaryKeys);
        }

        private bool GetWatermarkBit(int hash)
        {
            // New indicies to image matrix
            int imageRowIndex = hash % this.imageRowSize;
            int imageColumnIndex = hash % this.imageColumnSize;

            // Row in bit matrix
            BitArray row = (BitArray)this.imageBitMatrix.GetValue(imageRowIndex);
            // String from bits in selected row
            string rowValue = this.BitArrayToString(row);
            // String from bits in selected column
            string columnValue = this.GetColumnValue(this.imageBitMatrix, imageColumnIndex);

            int rIndex = this.CreateHash(String.Concat(hash, rowValue)) % imageRowSize;
            int cIndex = this.CreateHash(String.Concat(hash, columnValue)) % imageColumnSize;

            BitArray finalRow = (BitArray)this.imageBitMatrix.GetValue(rIndex);

            return finalRow[cIndex];
        }

        private int CreateHash(string text)
        {
            MD5 md5Hash = MD5.Create();
            var hash = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
            return Math.Abs(BitConverter.ToInt32(hash, 0)); // Must be positive
        }


        //public void changeData()
        //{
        //    var drives = defaultContext.Drives;

        //    var index = 0;
        //    foreach (Drive drive in drives)
        //    {

        //        //if (index % 5 == 0)
        //        //{

        //        //DateTime startDate = new DateTime(2000, 1, 1, 10, 0, 0);
        //        //DateTime endDate = new DateTime(2020, 1, 1, 17, 0, 0);

        //        ////drive.Time = drive.Time.AddSeconds(2);
        //        //TimeSpan timeSpan = endDate - startDate;
        //        //var randomTest = new Random();
        //        //TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
        //        //DateTime newDate = startDate + newSpan;

        //            //Console.WriteLine("drive time " + drive.Time);
        //            drive.Time = drive.Time.AddHours(2);

        //            //Console.WriteLine("new drive time " + drive.Time);

        //            Random randObj = new Random(0);
        //            //drive.BusPlate = randObj.Next().ToString();

        //            //drive.RouteNumber = randObj.Next().ToString();

        //            index++;
        //       // }
        //    }

        //}


        private string GetColumnValue(BitArray[] bitMatrix, int index)
        {
            BitArray columnBits = new BitArray(bitMatrix.Length);
            for (int i = 0; i < bitMatrix.Length; i++)
            {
                var row = (BitArray)bitMatrix.GetValue(i);
                columnBits[i] = row[index];
            }

            return this.BitArrayToString(columnBits);
        }

        private string BitArrayToString(BitArray bits)
        {
            char[] charBits = new char[bits.Count];

            for (int i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                {
                    charBits[i] = '1';
                }
                else
                {
                    charBits[i] = '0';
                }

            }

            return new string(charBits);
        }

        #endregion
    }
}
