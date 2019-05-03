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

namespace RDB.UI.ImpExps
{
    class Preview
    {
        public Preview() { }

        public void Select(DefaultContext defaultContext, string TableName, ListView preview)
        {
            preview.Items.Clear();
            preview.View = View.Details;
            preview.Columns.Clear();
            switch (TableName.ToLower())
            {
                case "autobus": Buses(defaultContext, preview); break;
                case "jizda": Drives(defaultContext, preview); break;
                case "jizdenka": Tickets(defaultContext, preview); break;
                case "klient": Clients(defaultContext, preview); break;
                case "kontakt": Contacts(defaultContext, preview); break;
                case "lokalita": Locations(defaultContext, preview); break;
                case "mezizastavka": Stations(defaultContext, preview); break;
                case "ridic": Drivers(defaultContext, preview); break;
                case "trasy": Routes(defaultContext, preview); break;
                case "typkontaktu": ContactTypes(defaultContext, preview); break;
                case "znacka": Models(defaultContext, preview); break;
                default: break;
            }
            preview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            preview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void Buses(DefaultContext defaultContext, ListView preview)
        {
            List<Bus> l = defaultContext.Buses.ToList();
            int pocet = 10;
            if (l.Count < 10)
                pocet = l.Count;
            for (int i = 0; i < pocet; i++)
            {
                String[] data = { l[i].ModelName.ToString(), l[i].Plate.ToString() };
                if (i == 0)
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        preview.Columns.Add("Sloupec " + (j + 1));
                    }
                }
                preview.Items.Add(new ListViewItem(data));
            }
           
        }

        private void Models(DefaultContext defaultContext, ListView preview)
        {
            List<Model> l = defaultContext.Models.ToList();
            int pocet = 10;
            if (l.Count < 10)
                pocet = l.Count;
            for (int i = 0; i < pocet; i++)
            {
                String[] data = { l[i].Name.ToString() };
                if (i == 0)
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        preview.Columns.Add("Sloupec " + (j + 1));
                    }
                }
                preview.Items.Add(new ListViewItem(data));
            }
        }

        private void ContactTypes(DefaultContext defaultContext, ListView preview)
        {
            List<ContactType> l = defaultContext.ContactTypes.ToList();
            int pocet = 10;
            if (l.Count < 10)
                pocet = l.Count;
            for (int i = 0; i < pocet; i++)
            {
                String[] data = { l[i].Type.ToString() };
                if (i == 0)
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        preview.Columns.Add("Sloupec " + (j + 1));
                    }
                }
                preview.Items.Add(new ListViewItem(data));
            }
        }

        private void Drivers(DefaultContext defaultContext, ListView preview)
        {
            List<Driver> l = defaultContext.Drivers.ToList();
            int pocet = 10;
            if (l.Count < 10)
                pocet = l.Count;
            for (int i = 0; i < pocet; i++)
            {
                String[] data = { l[i].LicenseNumber.ToString(), l[i].Firstname.ToString(), l[i].Surname.ToString() };
                if (i == 0)
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        preview.Columns.Add("Sloupec " + (j + 1));
                    }
                }
                preview.Items.Add(new ListViewItem(data));
            }
        }

        private void Stations(DefaultContext defaultContext, ListView preview)
        {
            List<Station> l = defaultContext.Stations.ToList();
            int pocet = 10;
            if (l.Count < 10)
                pocet = l.Count;
            for (int i = 0; i < pocet; i++)
            {
                String[] data = { l[i].Location.ToString(), l[i].Name.ToString(), l[i].Route.ToString(), l[i].RouteNumber.ToString() };
                if (i == 0)
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        preview.Columns.Add("Sloupec " + (j + 1));
                    }
                }
                preview.Items.Add(new ListViewItem(data));
            }
        }

        private void Locations(DefaultContext defaultContext, ListView preview)
        {
            List<Location> l = defaultContext.Locations.ToList();
            int pocet = 10;
            if (l.Count < 10)
                pocet = l.Count;
            for (int i = 0; i < pocet; i++)
            {
                String[] data = { l[i].Name.ToString() };
                if (i == 0)
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        preview.Columns.Add("Sloupec " + (j + 1));
                    }
                }
                preview.Items.Add(new ListViewItem(data));
            }
        }

        private void Contacts(DefaultContext defaultContext, ListView preview)
        {
            List<Contact> l = defaultContext.Contacts.ToList();
            int pocet = 10;
            if (l.Count < 10)
                pocet = l.Count;
            for (int i = 0; i < pocet; i++)
            {
                String[] data = { l[i].DriverLicenseNumber.ToString(), l[i].ContactType.ToString(), l[i].Value.ToString() };
                if (i == 0)
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        preview.Columns.Add("Sloupec " + (j + 1));
                    }
                }
                preview.Items.Add(new ListViewItem(data));
            }
        }

        private void Clients(DefaultContext defaultContext, ListView preview)
        {
            List<Client> l = defaultContext.Clients.ToList();
            int pocet = 10;
            if (l.Count < 10)
                pocet = l.Count;
            for (int i = 0; i < pocet; i++)
            {
                String[] data = { l[i].Firstname.ToString(), l[i].Surname.ToString(), l[i].Email.ToString() };
                if (i == 0)
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        preview.Columns.Add("Sloupec " + (j + 1));
                    }
                }
                preview.Items.Add(new ListViewItem(data));
            }
        }

        private void Tickets(DefaultContext defaultContext, ListView preview)
        {
            List<Ticket> l = defaultContext.Tickets.ToList();
            int pocet = 10;
            if (l.Count < 10)
                pocet = l.Count;
            for (int i = 0; i < pocet; i++)
            {
                string mail = "NULL";
                if (l[i].ClientEmail != null)
                    mail = l[i].ClientEmail.ToString();
                String[] data = { l[i].RouteNumber.ToString(), mail,/*l[i].ClientEmail.Length > 0 ? l[i].ClientEmail.ToString() : "NULL",*/ l[i].DriveTime.ToString(), l[i].Number.ToString() };
                if (i == 0)
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        preview.Columns.Add("Sloupec " + (j + 1));
                    }
                }
                preview.Items.Add(new ListViewItem(data));
            }
        }

        private void Drives(DefaultContext defaultContext, ListView preview)
        {
            List<Drive> l = defaultContext.Drives.ToList();
            int pocet = 10;
            if (l.Count < 10)
                pocet = l.Count;
            for (int i = 0; i < pocet; i++)
            {
                String[] data = { l[i].RouteNumber.ToString(), l[i].DriveLicenseNumber.ToString(), l[i].BusPlate.ToString(), l[i].Time.ToString() };
                if (i == 0)
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        preview.Columns.Add("Sloupec " + (j + 1));
                    }
                }
                preview.Items.Add(new ListViewItem(data));
            }
        }

        private void Routes(DefaultContext defaultContext, ListView preview)
        {
            List<Route> l = defaultContext.Routes.ToList();
            int pocet = 10;
            if (l.Count < 10)
                pocet = l.Count;
            for (int i = 0; i < pocet; i++)
            {
                String[] data = { l[i].DepartureName.ToString(), l[i].ArrivalName.ToString(), l[i].Number.ToString() };
                if (i == 0)
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        preview.Columns.Add("Sloupec " + (j + 1));
                    }
                }
                preview.Items.Add(new ListViewItem(data));
            }
        }

    }
}
