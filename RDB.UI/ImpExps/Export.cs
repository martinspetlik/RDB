using RDB.Data.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using RDB.Data.Extensions;

namespace RDB.UI.ImpExps
{
    public class Export : ImpExpBase
    {
        #region Fields

        List<string> tables = new List<string>();
        
        #endregion

        #region Constructors 

        public Export(DefaultContext defaultContext, ComboBox tables_cb, List<String> tableNames) : base(defaultContext, tables_cb, tableNames) { }

        #endregion


        #region Public methods

        public void ShowPreview(ListView preview)
        {
            if (!String.IsNullOrEmpty(TableName) && !AllTables)
                PreviewTable(defaultContext.GetTableColumns(TableName));
            else if (AllTables)
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
                System.Data.Entity.DbSet a = defaultContext.Contacts;
            }
        }

        #endregion

        #region Private methods

        private void PreviewTable(List<string> sloupce_list)
        {
            //int counter = 0;
            //string line;
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
            command += " FROM " + TableName;
            return command;
        }

        private void ExportData()
        {
            if (!String.IsNullOrEmpty(FilePath) && (AllTables || !String.IsNullOrEmpty(TableName)))
            {
                List<string> sloupce_list = new List<string>();
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

        private void InsertColumns(List<string> sloupce_list) //vložení hodnot ze souboru
        {
            StreamReader file = new StreamReader(@FilePath, Encoding.UTF8);

            if (!AllTables)
            {
                InsertIntoTable(file, TableName, sloupce_list);
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
                    string[] values = line.Split(Separator);
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
