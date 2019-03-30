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

        public void ShowPreview(ListView preview)
        {
            if (Tabulka.Length > 0 && !All_tables)
            {
                PreviewTable(data.GetColumns(Tabulka));
            }
            else if(All_tables)
            {
                /*
                 *  Podle struktury dat bude vypis nahledu
                 */
            }
        }

        public void SaveFile(RadioButton od_car_rad, RadioButton od_str_rad, RadioButton od_tab_rad)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Textové soubory (*.txt)|*.txt|csv soubory (*.csv)|*.csv|xsl soubory (*.xsl)|*.xsl|Všechny soubory (*.*)|*.*";
            saveFileDialog1.Title = "Uložit data z databáze";
            saveFileDialog1.ShowDialog();
            StreamWriter writer = null;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != "")
            {
                SetSeparator(od_car_rad, od_str_rad, od_tab_rad);
               
                ExportData();
                /*
                 *  Zde bude write z db do souboru
                 *  Popřípadě v jiné metodě
                 */
                writer = new StreamWriter(saveFileDialog1.Filter);
                writer.Close();
                System.Data.Entity.DbSet a = data.defaultContext.Contacts;
            }
        }

        #endregion

        #region Private methods

        private void PreviewTable(List<string> sloupce_list)
        {
            int counter = 0;
            string line;
            string command = TableSelect(sloupce_list);
        }

        private string TableSelect(List<string> sloupce_list)
        {
            string command = "SELECT ";
            for (int i = 0; i < sloupce_list.Count; i++)
            {
                command += sloupce_list[i];
                if (i < sloupce_list.Count - 1)
                    command += ", ";
            }
            command += " FROM " + Tabulka;
            return command;
        }

        private void ExportData()
        {
            if (filePath.Length > 0 && ((tabulka.Length > 0 && !all_tables) || all_tables))
            {
                
                List<string> sloupce_list = new List<string>();
                try                                                
                {
                    InsertColumns(data.GetColumns(Tabulka));    //volání vkládání
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
