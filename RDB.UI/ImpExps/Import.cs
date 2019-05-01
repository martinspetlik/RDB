using RDB.Data.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RDB.Data.Extensions;
using MySql.Data.MySqlClient;
using System.Linq;
using Ionic.Zip;
using RDB.Data.Models.Scheme;

namespace RDB.UI.ImpExps
{
    public class Import : ImpExpBase
    {
        #region
        String baseDirectory = "/temp_zip";
        bool zip = false;
        #endregion
        #region Constants

        private Int32 BATCH_SIZE = 500;

        #endregion

        #region Constructors 

        public Import(DefaultContext defaultContext, ComboBox tables_cb, List<String> tableNames) : base(defaultContext, tables_cb, tableNames) { }

        #endregion

        #region Public methods
        
        public void OpenFile(RadioButton od_car_rad, RadioButton od_str_rad, RadioButton od_tab_rad, TextBox cesta_in_tb, Button insert_bt, ListView preview, CheckBox zip_ch)
        {
            zip = false;
            var fileContent = string.Empty;
            SetSeparator(od_car_rad, od_str_rad, od_tab_rad);
            if (!zip_ch.Checked)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "CSV soubory (*.csv)|*.csv";
                    openFileDialog.Title = "Otevřít soubor s daty";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        preview.Items.Clear();
                        OpenCSV(openFileDialog, preview);
                        cesta_in_tb.Text = FilePath;
                        insert_bt.Enabled = true;
                    }
                }
            }
            else
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "ZIP soubory (*.zip)|*.zip";
                    openFileDialog.Title = "Otevřít soubor se všemi tabulkami";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        cesta_in_tb.Text = openFileDialog.FileName;
                        DirectoryInfo di = Directory.CreateDirectory(baseDirectory);
                        using (ZipFile zip = ZipFile.Read(openFileDialog.FileName))
                        {
                            string[] files = Directory.GetFiles(baseDirectory);
                            foreach (string file in files)
                            {
                                File.Delete(file);
                            }
                            try
                            {
                                ZipEntry e = zip["znacka.csv"];
                                e.Extract(baseDirectory);
                                e = zip["autobus.csv"];
                                e.Extract(baseDirectory);
                                e = zip["typkontaktu.csv"];
                                e.Extract(baseDirectory);
                                e = zip["ridic.csv"];
                                e.Extract(baseDirectory);
                                e = zip["lokalita.csv"];
                                e.Extract(baseDirectory);
                                e = zip["kontakt.csv"];
                                e.Extract(baseDirectory);
                                e = zip["jizdenka.csv"];
                                e.Extract(baseDirectory);
                                e = zip["klient.csv"];
                                e.Extract(baseDirectory);
                                e = zip["trasy.csv"];
                                e.Extract(baseDirectory);
                                e = zip["jizda.csv"];
                                e.Extract(baseDirectory);
                            }
                            catch (ZipException e)
                            {
                                MessageBox.Show("Chyba: " + e);
                            }
                        }
                        preview.Items.Clear();
                        insert_bt.Enabled = true;
                        zip = true;
                    }
                }
            }
        }

        public void Insert()
        {
            if (!zip)
            {
                if (!String.IsNullOrEmpty(FilePath) && !String.IsNullOrEmpty(TableName))
                {
                    try
                    {
                        InsertColumns(defaultContext.GetTableColumns(TableName));    //volání vkládání
                    }
                    catch (MySqlException exp)
                    {
                        MessageBox.Show("Chyba:" + exp);
                    }
                }
            }
            else
            {
                TableName = "znacka";
                FilePath = baseDirectory + "/" + TableName + ".csv";
                InsertColumns(defaultContext.GetTableColumns(TableName));

                TableName = "autobus";
                FilePath = baseDirectory + "/" + TableName + ".csv";
                InsertColumns(defaultContext.GetTableColumns(TableName));

                TableName = "lokalita";
                FilePath = baseDirectory + "/" + TableName + ".csv";
                InsertColumns(defaultContext.GetTableColumns(TableName));

                TableName = "typkontaktu";
                FilePath = baseDirectory + "/" + TableName + ".csv";
                InsertColumns(defaultContext.GetTableColumns(TableName));

                TableName = "ridic";
                FilePath = baseDirectory + "/" + TableName + ".csv";
                InsertColumns(defaultContext.GetTableColumns(TableName));

                TableName = "kontakt";
                FilePath = baseDirectory + "/" + TableName + ".csv";
                InsertColumns(defaultContext.GetTableColumns(TableName));

                TableName = "klient";
                FilePath = baseDirectory + "/" + TableName + ".csv";
                InsertColumns(defaultContext.GetTableColumns(TableName));

                TableName = "jizdenka";
                FilePath = baseDirectory + "/" + TableName + ".csv";
                InsertColumns(defaultContext.GetTableColumns(TableName));

                TableName = "trasy";
                FilePath = baseDirectory + "/" + TableName + ".csv";
                InsertColumns(defaultContext.GetTableColumns(TableName));

                TableName = "jizda";
                FilePath = baseDirectory + "/" + TableName + ".csv";
                InsertColumns(defaultContext.GetTableColumns(TableName));

            }
        }

        #endregion

        #region Private methods

        private void OpenCSV(OpenFileDialog openFileDialog, ListView preview)
        {
            FilePath = openFileDialog.FileName;

            //Read the contents of the file into a stream
            try
            {
                using (StreamReader reader = new StreamReader(openFileDialog.OpenFile(), Encoding.UTF8))
                {
                    preview.View = View.Details;
                    preview.Columns.Clear();

                    for (int i = 0; i < 10; i++)
                    {
                        try
                        {
                            String line = reader.ReadLine();
                            if (!String.IsNullOrEmpty(line))
                            {
                                String[] values = line.Split(Separator);

                                if (i == 0)
                                {
                                    for (int j = 0; j < values.Length; j++)
                                    {
                                        preview.Columns.Add("Sloupec " + (j + 1));
                                    }
                                }

                                preview.Items.Add(new ListViewItem(values));
                            }
                            else
                                break;
                        }
                        catch { }
                    }

                    preview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    preview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
            }
            catch
            {
                MessageBox.Show("Soubor nemohl být otevřen, může být otevřen v jiné aplikaci.");
            }
        }

        /// <summary>
        /// Vložení hodnot ze souboru
        /// </summary>
        /// <param name="columns"></param>
        private void InsertColumns(List<Column> columns)
        {
            try
            {
                Int32 count = InsertIntoTable(File.ReadAllLines(FilePath, Encoding.UTF8), columns);

                MessageBox.Show($"Úspěšně vloženo {count} záznamů...");
            }
            catch (Exception e)
            {
                MessageBox.Show("Data se nepodařilo naimportovat...");
            }
        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }


        private Int32 InsertIntoTable(String[] rows, List<Column> columns)
        {
            Int32 batchCount = (rows.Length + BATCH_SIZE - 1) / BATCH_SIZE;

            for (Int32 i = 0; i < batchCount; i++)
            {
                String command = GetCommandHeader(columns);
                IEnumerable<String> batchRows = rows.Skip(i * BATCH_SIZE).Take(BATCH_SIZE);
                foreach (String row in batchRows)
                {
                    String[] values = row.Split(Separator);
                    if (values.Length > 0)
                    {
                        command += "(";
                        for (Int32 j = 0; j < values.Length; j++)
                        {
                            if (columns.ElementAt(j).Type == "timestamp")
                            {
                                command += "FROM_UNIXTIME(" + values[j] + ")";
                            }
                            else if (columns.ElementAt(j).IsString)
                                command += $"'{values[j]}'";
                            else
                                command += values[j];

                            command += ", ";
                        }
                        command = command.Substring(0, command.Length - 2) + "), ";
                    }
                }

                defaultContext.Database.ExecuteSqlCommand(command.Substring(0, command.Length - 2));
            }

            return rows.Length;
        }

        private String GetCommandHeader(List<Column> columns)
        {
            String command = "INSERT INTO " + TableName + " (";
            command += String.Join(", ", columns.Select(c => c.Name));
            command += ") VALUES ";

            return command;
        }

        #endregion
    }
}
