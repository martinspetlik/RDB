using RDB.Data.DAL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using RDB.Data.Extensions;
using CsvHelper;
using RDB.Data.Models;
using System.Linq;
using RDB.Data.Models.Scheme;
using RDB.UI.ImpExps.ClassMaps;
using CsvHelper.Configuration;
using Ionic.Zip;

namespace RDB.UI.ImpExps
{
    public class Export : ImpExpBase
    {
        #region Fields
        String baseDirectory = "/temp_zip";
        List<string> tableNames;
        #endregion
        #region Constructors 

        public Export(DefaultContext defaultContext, ComboBox tables_cb, List<String> tableNames) : base(defaultContext, tables_cb, tableNames) { this.tableNames = tableNames; }

        #endregion

        #region Public methods

        public void ShowPreview(ListView preview)
        {
            if (!String.IsNullOrEmpty(TableName))
                PreviewTable(defaultContext.GetTableColumns(TableName), preview);
        }

        public void SaveFile(RadioButton od_car_rad, RadioButton od_str_rad, RadioButton od_tab_rad, CheckBox zip_ch)
        {
            SetSeparator(od_car_rad, od_str_rad, od_tab_rad);
            if (!zip_ch.Checked)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "CSV soubory (*.csv)|*.csv";
                saveFileDialog1.Title = "Uložit data z databáze";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != "")
                {
                    FilePath = saveFileDialog1.FileName;
                    
                    ExportData();
                }
            }
            else
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "ZIP soubory (*.zip)|*.zip";
                saveFileDialog1.Title = "Uložit databázi do ZIPu";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != "")
                {
                    using (ZipFile zipFile = new ZipFile())
                    {
                        foreach(string table in tableNames)
                        {
                            TableName = table;
                            FilePath = baseDirectory + "/" + table + ".csv";
                            ExportData();
                            zipFile.AddFile(FilePath, "/");
                        }
                        zipFile.Save(saveFileDialog1.FileName);
                    }
                    FilePath = saveFileDialog1.FileName;
                }
            }
        }

        #endregion

        #region Private methods

        private void PreviewTable(List<Column> columns, ListView preview)
        {
            //int counter = 0;
            //string line;
            Preview p = new Preview();
            p.Select(defaultContext, TableName, preview);

        }

        private string TableSelect(List<Column> columns)
        {
            string command = "SELECT ";
            for (int i = 0; i < columns.Count; i++)
            {
                command += columns[i];
                if (i < columns.Count - 1)
                    command += ", ";
            }
            command += " FROM " + TableName;
            return command;
        }

        private void ExportData()
        {
            if (!String.IsNullOrEmpty(FilePath) && !String.IsNullOrEmpty(TableName))
            {
                List<string> sloupce_list = new List<string>();
                try
                {
                    DataFromTable();    //volání exportu podle zvolené tabulky
                }
                catch (SqlException exp)
                {
                    MessageBox.Show("Chyba:" + exp);
                }
            }
        }

        /// <summary>
        /// Vložení hodnot ze souboru
        /// </summary>
        /// <param name="sloupce_list"></param>
        private void DataFromTable()
        {

            switch (TableName.ToLower())
            {
                case "autobus": EntityExport<Bus,BusClassMap>(); break;
                case "jizda": EntityExport<Drive, DriveClassMap>(); break;
                case "jizdenka": EntityExport<Ticket, TicketClassMap>(); break;
                case "klient": EntityExport<Client, ClientClassMap>(); break;
                case "kontakt": EntityExport<Contact, ContactClassMap>(); break;
                case "lokalita": EntityExport<Location, LocationClassMap>(); break;
                case "mezizastavka": EntityExport<Station, StationClassMap>(); break;
                case "ridic": EntityExport<Driver, DriverClassMap>(); break;
                case "trasy": EntityExport<Route, RouteClassMap>(); break;
                case "typkontaktu": EntityExport<ContactType, ContactTypeClassMap>(); break;
                case "znacka": EntityExport<Model, ModelClassMap>(); break;
                default: break;
            }
        }

        private void EntityExport<TEntity, TClassMapper>()
            where TEntity : class
            where TClassMapper : ClassMap<TEntity>
        {
            List<TEntity> entities = defaultContext.Set<TEntity>().AsNoTracking().ToList();

            using (StreamWriter writer = new StreamWriter(FilePath))
            using (CsvWriter csvWriter = new CsvWriter(writer))
            {
                csvWriter.Configuration.Delimiter = Separator + "";
                csvWriter.Configuration.HasHeaderRecord = false;
                csvWriter.Configuration.RegisterClassMap<TClassMapper>();
                csvWriter.WriteRecords(entities);

                writer.Flush();
            }
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
