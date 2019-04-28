using RDB.Data.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using RDB.Data.Extensions;
using CsvHelper;
using RDB.Data.Models;
using System.Linq;

namespace RDB.UI.ImpExps
{
    public class Export : ImpExpBase
    {
        #region Constructors 

        public Export(DefaultContext defaultContext, ComboBox tables_cb, List<String> tableNames) : base(defaultContext, tables_cb, tableNames) { }

        #endregion

        #region Public methods

        public void ShowPreview(ListView preview)
        {
            if (!String.IsNullOrEmpty(TableName))
                PreviewTable(defaultContext.GetTableColumns(TableName));
        }

        public void SaveFile(RadioButton od_car_rad, RadioButton od_str_rad, RadioButton od_tab_rad)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "CSV soubory (*.csv)|*.csv";
            saveFileDialog1.Title = "Uložit data z databáze";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != "")
            {
                FilePath = saveFileDialog1.FileName;
                SetSeparator(od_car_rad, od_str_rad, od_tab_rad);

                ExportData();
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
            
            switch(TableName.ToLower())
            {
                case "autobus": AutobusExport();  break;
                case "jizda": JizdaExport(); break;
                case "jizdenka": JizdenkaExport(); break;
                case "klient": KlientExport(); break;
                case "kontakt": KontaktExport();  break;
                case "lokalita": LokalitaExport(); break;
                case "mezizastavka": MezizastavkaExport(); break;
                case "ridic": RidicExport(); break;
                case "trasy": TrasyExport(); break;
                case "typkontaktu": TypKontaktuExport(); break;
                case "znacka": ZnackaExport(); break;
                default: break;
            }
        }

        private void AutobusExport()
        {
            List<Bus> buses = defaultContext.Buses.AsNoTracking().ToList();
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(FilePath))
            using (var csvWriter = new CsvWriter(writer))
            {
                csvWriter.Configuration.Delimiter = Separator + "";
                csvWriter.Configuration.HasHeaderRecord = false;
                csvWriter.Configuration.AutoMap<Bus>();
                csvWriter.WriteRecords(buses);
                writer.Flush();
            }
        }

        private void JizdaExport()
        {
            List<Drive> driv = defaultContext.Drives.AsNoTracking().ToList();
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(FilePath))
            using (var csvWriter = new CsvWriter(writer))
            {
                csvWriter.Configuration.Delimiter = Separator + "";
                csvWriter.Configuration.HasHeaderRecord = false;
                csvWriter.Configuration.AutoMap<Drive>();
                csvWriter.WriteRecords(driv);
                writer.Flush();
            }
        }

        private void JizdenkaExport()
        {
            List<Ticket> tic = defaultContext.Tickets.AsNoTracking().ToList();
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(FilePath))
            using (var csvWriter = new CsvWriter(writer))
            {
                csvWriter.Configuration.Delimiter = Separator + "";
                csvWriter.Configuration.HasHeaderRecord = false;
                csvWriter.Configuration.AutoMap<Ticket>();
                csvWriter.WriteRecords(tic);
                writer.Flush();
            }
        }

        private void KlientExport()
        {
            List<Client> cli = defaultContext.Clients.AsNoTracking().ToList();
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(FilePath))
            using (var csvWriter = new CsvWriter(writer))
            {
                csvWriter.Configuration.Delimiter = Separator + "";
                csvWriter.Configuration.HasHeaderRecord = false;
                csvWriter.Configuration.AutoMap<Client>();
                csvWriter.WriteRecords(cli);
                writer.Flush();
            }
        }

        private void KontaktExport()
        {
            List<Contact> cont = defaultContext.Contacts.AsNoTracking().ToList();
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(FilePath))
            using (var csvWriter = new CsvWriter(writer))
            {
                csvWriter.Configuration.Delimiter = Separator + "";
                csvWriter.Configuration.HasHeaderRecord = false;
                csvWriter.Configuration.AutoMap<Contact>();
                csvWriter.WriteRecords(cont);
                writer.Flush();
            }
        }

        private void LokalitaExport()
        {
            List<Location> loca = defaultContext.Locations.AsNoTracking().ToList();
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(FilePath))
            using (var csvWriter = new CsvWriter(writer))
            {
                csvWriter.Configuration.Delimiter = Separator + "";
                csvWriter.Configuration.HasHeaderRecord = false;
                csvWriter.Configuration.AutoMap<Location>();
                csvWriter.WriteRecords(loca);
                writer.Flush();
            }
        }

        private void MezizastavkaExport()
        {
            List<Station> stat = defaultContext.Stations.AsNoTracking().ToList();
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(FilePath))
            using (var csvWriter = new CsvWriter(writer))
            {
                csvWriter.Configuration.Delimiter = Separator + "";
                csvWriter.Configuration.HasHeaderRecord = false;
                csvWriter.Configuration.AutoMap<Station>();
                csvWriter.WriteRecords(stat);
                writer.Flush();
            }
        }

        private void RidicExport()
        {
            List<Driver> driver = defaultContext.Drivers.AsNoTracking().ToList();
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(FilePath))
            using (var csvWriter = new CsvWriter(writer))
            {
                csvWriter.Configuration.Delimiter = Separator + "";
                csvWriter.Configuration.HasHeaderRecord = false;
                csvWriter.Configuration.AutoMap<Driver>();
                csvWriter.WriteRecords(driver);
                writer.Flush();
            }
        }

        private void TrasyExport()
        {
            List<Route> routes = defaultContext.Routes.AsNoTracking().ToList();
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(FilePath))
            using (var csvWriter = new CsvWriter(writer))
            {
                csvWriter.Configuration.Delimiter = Separator + "";
                csvWriter.Configuration.HasHeaderRecord = false;
                csvWriter.Configuration.AutoMap<Route>();
                csvWriter.WriteRecords(routes);
                writer.Flush();
            }
        }

        private void TypKontaktuExport()
        {
            List<ContactType> types = defaultContext.ContactTypes.AsNoTracking().ToList();
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(FilePath))
            using (var csvWriter = new CsvWriter(writer))
            {
                csvWriter.Configuration.Delimiter = Separator + "";
                csvWriter.Configuration.HasHeaderRecord = false;
                csvWriter.Configuration.AutoMap<ContactType>();
                csvWriter.WriteRecords(types);
                writer.Flush();
            }
        }

        private void ZnackaExport()
        {
            List<Model> model = defaultContext.Models.AsNoTracking().ToList();
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(FilePath))
            using (var csvWriter = new CsvWriter(writer))
            {
                csvWriter.Configuration.Delimiter = Separator + "";
                csvWriter.Configuration.HasHeaderRecord = false;
                csvWriter.Configuration.AutoMap<Model>();
                csvWriter.WriteRecords(model);
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
