using RDB.Data.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using RDB.Data.Extensions;

namespace RDB.UI.ImpExps
{
    public class Import : ImpExpBase
    {
        #region Constructors 

        public Import(DefaultContext defaultContext, ComboBox tables_cb, List<String> tableNames) : base(defaultContext, tables_cb, tableNames) { }

        #endregion

        #region Public methods

        public void OpenFile(RadioButton od_car_rad, RadioButton od_str_rad, RadioButton od_tab_rad, TextBox cesta_in_tb, Button insert_bt, ListView preview)
        {
            var fileContent = string.Empty;
            
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Textové soubory (*.txt)|*.txt|csv soubory (*.csv)|*.csv|xsl soubory (*.xsl)|*.xsl|Všechny soubory (*.*)|*.*";
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
                catch (SqlException exp)
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
                Stream fileStream = openFileDialog.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    bool prvni = true;
                    //fileContent = reader.ReadToEnd();
                    preview.View = View.Details;

                    for (int i = 0; i < 10; i++)
                    {
                        try
                        {
                            var line = reader.ReadLine();
                            if (line != null)
                            {
                                string[] values = line.Split(Separator);
                                string[] arr = new string[values.Length];
                                //var items = preview.Items;
                                //ListViewItem lvi1 = new ListViewItem();
                                ListViewItem item1 = new ListViewItem();
                                if (prvni)
                                {
                                    for (int j = 0; j < values.Length; j++)
                                    {
                                        preview.Columns.Add("Sloupec " + j);
                                        prvni = false;
                                    }
                                }
                                preview.Items.Add(new ListViewItem(values));
                                //preview.Items.Add(values);

                            }
                        }
                        catch { }

                    }

                    preview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    preview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
            }
            catch
            {
                MessageBox.Show("SOubor nemohl být otevřen, může být otevřen v jiné aplikaci.");
            }
        }

        /// <summary>
        /// Vložení hodnot ze souboru
        /// </summary>
        /// <param name="columns"></param>
        private void InsertColumns(List<String> columns)
        {
            StreamReader file = new StreamReader(@FilePath, Encoding.Default);

            InsertIntoTable(file, columns);

            MessageBox.Show("Hodnoty vloženy.");
            file.Close();
        }

        private void InsertIntoTable(StreamReader file, List<string> columns)
        {
            int counter = 0;
            string line;
            while ((line = file.ReadLine()) != null)
            {
                try
                {
                    string command = "INSERT INTO " + TableName + " (";
                    string[] values = line.Split(Separator);

                    command += String.Join(", ", columns);
                    command += ") VALUES (";
                    for (int i = 0; i < columns.Count; i++)
                    {
                        if (i < values.Length)
                            command += "'" + values[i] + "'";
                        else
                            command += DBNull.Value;

                        if (i < values.Length - 1)
                            command += ", ";
                    }
                    command += ")";

                    defaultContext.Database.ExecuteSqlCommand(command);
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Chyba: " + e);
                }
                counter++;
            }
        }

        #endregion
    }
}
