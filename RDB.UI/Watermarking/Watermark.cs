using RDB.Data.DAL;
using System;
using System.Drawing;
using System.Text;
using System.IO;
using RDB.Data.Models;
using System.Collections;
using System.Security.Cryptography;

namespace RDB.UI.Watermarking
{
    // http://iranarze.ir/wp-content/uploads/2017/08/7338-English-IranArze.pdf
    public class Watermark
    {
        private readonly DefaultContext defaultContext;
        private int imageRowSize;
        private int imageColumnSize;
        // Fraction of items that can be marked
        private int fraction;
        // Number of LSB candidate bits to be modified
        private int lsbCandidates;
        // Secrete key
        private int secretKey;

        private BitArray[] imageBitMatrix;


        public Watermark(DefaultContext defaultContext)
        {
            this.imageBitMatrix = this.ProcessImage();
            this.defaultContext = defaultContext;
            this.fraction = 30; 
            this.lsbCandidates = 3; 
            this.secretKey = 123;
        }


        public BitArray[] ProcessImage()
        { 
            var image_bits = this.LoadImage(Image.FromFile("Watermarking/random_image.jpg"));
            BitArray bits = new BitArray(image_bits);

            // Should be from image variable
            this.imageRowSize = 71;
            this.imageColumnSize = 3479;

            return this.BitsToMatrix(bits, imageRowSize, imageColumnSize);

        }


        public byte[] LoadImage(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }


        public BitArray[] BitsToMatrix(BitArray Input, int rowSize, int columnSize)
        {

            BitArray[] Output = new BitArray[rowSize];

            //Environment.Exit(1);
            for (int i = 0; i < rowSize; i += 1)
            {
                BitArray bitsRow = new BitArray(columnSize);
                for (int j = 0; j < columnSize; j++)
                {
                    bitsRow.Set(j, Input.Get(i + j));
                }

                Output.SetValue(bitsRow, i);
            }
            return Output;
        }


        public String GetPKString(Drive drive)
        {
            // Join all primary key attributes
            string[] primaryKeys = new String[] { drive.RouteNumber, drive.Time.ToString() };
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


        public void Watermarking()
        {

            foreach (Drive drive in this.defaultContext.Drives)
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

                    drive.Time = drive.Time.AddSeconds(newSeconds - seconds);
                }

            }

            //this.changeData();
            this.checkWatermark();

        }


        public void checkWatermark()
        {
 
            int totalCount = 0;
            int matchCount = 0;

            foreach (Drive drive in defaultContext.Drives)
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

            if ((float) matchCount / (float)totalCount >= 0.9) {
                Console.WriteLine("Watermarked " + ((float)matchCount / (float)totalCount));

            }
        }


        public int CreateHash(string text)
        {
            MD5 md5Hash = MD5.Create();
            var hash = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
            return Math.Abs(BitConverter.ToInt32(hash, 0)); // Must be positive

        }


        public void changeData()
        {
            var drives = defaultContext.Drives;

            var index = 0;
            foreach (Drive drive in drives)
            {
                if (index % 5 == 0)
                {
                    //drive.Time = drive.Time.AddSeconds(2);
                    drive.RouteNumber = "adlfj";
                }

                index++;
            }

        }


        public string GetColumnValue(BitArray[] bitMatrix, int index)
        {
            BitArray columnBits = new BitArray(bitMatrix.Length);
            for (int i = 0; i < bitMatrix.Length; i++)
            {
                var row = (BitArray)bitMatrix.GetValue(i);
                columnBits[i] = row[index];
            }

            return this.BitArrayToString(columnBits);
        }


        public string BitArrayToString(BitArray bits)
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
    }
}
