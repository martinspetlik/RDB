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
    public class Export
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
        public Export(DefaultContext defaultContext, BindingSource bs, ComboBox tables_cb)
        {
            data = new Data(defaultContext, bs, tables_cb);
            data.GetScheme();
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

        public void OpenFile(RadioButton od_car_rad, RadioButton od_str_rad, RadioButton od_tab_rad, TextBox cesta_in_tb, Button insert_bt, ListView preview)
        {
            var fileContent = string.Empty;
            //filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Textové soubory (*.txt)|*.txt|csv soubory (*.csv)|*.csv|xsl soubory (*.xsl)|*.xsl|Všechny soubory (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (od_car_rad.Checked)
                    Oddelovac = ',';
                else if (od_str_rad.Checked)
                    Oddelovac = ';';
                else if (od_tab_rad.Checked)
                    Oddelovac = '\t';

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

        public void Insert()
        {
            if (filePath.Length > 0 && ((tabulka.Length > 0 && !all_tables) || all_tables))
            {
                List<string> sloupce_list = new List<string>();
                try                                                 //získání názvů sloupců pro vkládání dat
                {
                    String[] columnRestrictions = new String[4];
                    columnRestrictions[2] = tabulka;
                    DataTable sloupce = data.defaultContext.Database.Connection.GetSchema("Columns", columnRestrictions);
                    foreach (DataRow row in sloupce.Rows)
                    {
                        string column = (string)row[3];
                        sloupce_list.Add(column);
                    }

                    InsertColumns(sloupce_list);    //volání vkládání
                }
                catch (SqlException exp)
                {
                    MessageBox.Show("Chyba:" + exp);
                }
            }
        }

        private void InsertColumns(List<string> sloupce_list) //vložení hodnot ze souboru
        {
            System.IO.StreamReader file =
            new System.IO.StreamReader(@FilePath, Encoding.Default);

            if (!all_tables)
            {
                InsertIntoTable(file, tabulka, sloupce_list);
            }
            else
            {
                /*
                 *      Zde bude vložení všech tabulek naráz  
                 */
            }
            MessageBox.Show("Hodnoty vloženy.");
            file.Close();
        }

        private void InsertIntoTable(StreamReader file, string tabulka, List<string> sloupce_list)
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
    }
}
