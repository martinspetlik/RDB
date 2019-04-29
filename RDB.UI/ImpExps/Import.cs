using RDB.Data.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RDB.Data.Extensions;
using MySql.Data.MySqlClient;
using System.Linq;

namespace RDB.UI.ImpExps
{
    public class Import : ImpExpBase
    {
        #region Constants

        private Int32 BATCH_SIZE = 500;

        #endregion

        #region Constructors 

        public Import(DefaultContext defaultContext, ComboBox tables_cb, List<String> tableNames) : base(defaultContext, tables_cb, tableNames) { }

        #endregion

        #region Public methods

        public void OpenFile(RadioButton od_car_rad, RadioButton od_str_rad, RadioButton od_tab_rad, TextBox cesta_in_tb, Button insert_bt, ListView preview)
        {
            var fileContent = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV soubory (*.csv)|*.csv";
                openFileDialog.Title = "Otevřít soubor s daty";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                SetSeparator(od_car_rad, od_str_rad, od_tab_rad);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    preview.Items.Clear();
                    OpenCSV(openFileDialog, preview);
                    cesta_in_tb.Text = FilePath;
                    insert_bt.Enabled = true;
                }
            }
        }

        public void Insert()
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
        private void InsertColumns(List<String> columns)
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

        private Int32 InsertIntoTable(String[] rows, List<String> columns)
        {
            Int32 batchCount = (rows.Length + BATCH_SIZE - 1) / BATCH_SIZE;
           
            for (Int32 i = 0; i < batchCount; i++)
            {
                String command = GetCommandHeader(columns);
                IEnumerable<String> batchRows = rows.Skip(i * BATCH_SIZE).Take(BATCH_SIZE);
                foreach (String row in batchRows)
                {
                    command += "(" + String.Join(", ", row.Split(Separator).Select(item => "'" + item + "'")) + "), ";
                }

                defaultContext.Database.ExecuteSqlCommand(command.Substring(0, command.Length - 2));
            }
            
            return rows.Length;
        }

        private String GetCommandHeader(List<String> columns)
        {
            String command = "INSERT INTO " + TableName + " (";
            command += String.Join(", ", columns);
            command += ") VALUES ";

            return command;
        }

        #endregion
    }
}
