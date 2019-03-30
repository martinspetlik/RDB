using RDB.Data.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace RDB.UI
{
    class Import
    {
        #region Fields

        Data data;
        List<string> tables = new List<string>();
        private char oddelovac;
        private bool all_tables;
        private string filePath;
        private string tabulka;

        #endregion

        #region Constructors 

        public Import(DefaultContext defaultContext, BindingSource bs, ComboBox tables_cb)
        {
            data = new Data(defaultContext, bs, tables_cb);
        }

        #endregion

        #region Properties

        public char Oddelovac
        {
            get
            {
                return oddelovac;
            }
            set
            {
                oddelovac = value;
            }
        }
        public bool All_tables
        {
            get
            {
                return all_tables;
            }
            set
            {
                all_tables = value;
            }
        }
        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
            }
        }
        public string Tabulka
        {
            get
            {
                return tabulka;
            }
            set
            {
                tabulka = value;
            }
        }

        #endregion

        #region Public methods

        public void OpenFile(RadioButton od_car_rad, RadioButton od_str_rad, RadioButton od_tab_rad, TextBox cesta_in_tb, Button insert_bt, ListView preview)
        {
            var fileContent = string.Empty;
            //filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Textové soubory (*.txt)|*.txt|csv soubory (*.csv)|*.csv|xsl soubory (*.xsl)|*.xsl|Všechny soubory (*.*)|*.*";
                openFileDialog.Title = "Otevřít soubor s daty";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                SetSeparator(od_car_rad, od_str_rad, od_tab_rad);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    preview.Items.Clear();
                    OpenCSV(openFileDialog, preview);
                    cesta_in_tb.Text = FilePath;
                    insert_bt.Enabled = true;
                }
            }
        }

        public void Insert()
        {
            if (filePath.Length > 0 && ((tabulka.Length > 0 && !all_tables) || all_tables))
            {
                try                                                 
                {
                    InsertColumns(data.GetColumns(Tabulka));    //volání vkládání
                }
                catch (SqlException exp)
                {
                    MessageBox.Show("Chyba:" + exp);
                }
            }
            else if(all_tables && filePath.Length > 0)
            {

            }
        }

        #endregion

        #region Private methods

        private void OpenCSV(OpenFileDialog openFileDialog, ListView preview)
        {
            filePath = openFileDialog.FileName;

            //Read the contents of the file into a stream
            //var fileStream;
            try
            {
                var fileStream = openFileDialog.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream, Encoding.Default))
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
                                string[] values = line.Split(oddelovac);
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

        private void InsertColumns(List<string> sloupce_list) //vložení hodnot ze souboru
        {
            System.IO.StreamReader file =
            new System.IO.StreamReader(@FilePath, Encoding.Default);

            if (!all_tables)
            {
                InsertIntoTable(file, sloupce_list);
            }
            else
            {
                /*
                 *      Zde bude vložení všech tabulek naráz  
                 */
                InsertIntoAll(file);
            }
            MessageBox.Show("Hodnoty vloženy.");
            file.Close();
        }

        private void InsertIntoTable(StreamReader file, List<string> sloupce_list)
        {
            int counter = 0;
            string line;
            while ((line = file.ReadLine()) != null)
            {
                try
                {

                    string command = "INSERT INTO " + tabulka + " (";
                    string[] values = line.Split(oddelovac);
                    String[] columnRestrictions = new String[4];
                    columnRestrictions[2] = tabulka;

                    for (int i = 0; i < sloupce_list.Count; i++)
                    {
                        command += sloupce_list[i];
                        if (i < sloupce_list.Count - 1)
                            command += ", ";
                    }
                    command += ") VALUES (";
                    for (int i = 0; i < sloupce_list.Count; i++)
                    {
                        if (i < values.Length)
                            command += values[i];
                        else
                            command += DBNull.Value;
                        if (i < values.Length - 1)
                            command += ", ";
                    }
                    command += ")";
                    data.defaultContext.Database.ExecuteSqlCommand(command);

                }
                catch (SqlException e)
                {
                    MessageBox.Show("Chyba: " + e);
                }
                counter++;
            }
        }

        private void InsertIntoAll(StreamReader file)
        {

        }

        private void SetSeparator(RadioButton od_car_rad, RadioButton od_str_rad, RadioButton od_tab_rad)
        {
            if (od_car_rad.Checked)
                Oddelovac = ',';
            else if (od_str_rad.Checked)
                Oddelovac = ';';
            else if (od_tab_rad.Checked)
                Oddelovac = '\t';
        }

        #endregion
    }
}
