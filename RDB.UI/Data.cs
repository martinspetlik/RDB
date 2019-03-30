using RDB.Data.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace RDB.UI
{
    class Data
    {
        #region Fields

        public DefaultContext defaultContext;
        List<string> tables = new List<string>();

        #endregion

        #region Constructors

        public Data(DefaultContext defaultContext, BindingSource bs, ComboBox tables_cb)
        {
            this.defaultContext = defaultContext;
            bs.DataSource = GetScheme();
            tables_cb.DataSource = bs;
        }
        #endregion

        #region Public methods

        public List<string> GetScheme() //Získání názvu tabulek pro výběr
        {
            defaultContext.Database.Connection.Open();
            DataTable dt = defaultContext.Database.Connection.GetSchema("Tables");
            tables = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                string tablename = (string)row[2];
                if(tablename != "__migrationhistory")
                    tables.Add(tablename);
            }
            defaultContext.Database.Connection.Close();
            return tables;

        }

        public List<string> GetColumns(string tabulka)    //získání názvů sloupců z tabulky pro vkládání dat
        {
            List<string> sloupce_list = new List<string>();
            String[] columnRestrictions = new String[4];
            columnRestrictions[2] = tabulka;
            DataTable sloupce = defaultContext.Database.Connection.GetSchema("Columns", columnRestrictions);
            foreach (DataRow row in sloupce.Rows)
            {
                string column = (string)row[3];
                sloupce_list.Add(column);
            }
            return sloupce_list;
        }

        #endregion
    }
}
