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
                tables.Add(tablename);
            }
            defaultContext.Database.Connection.Close();
            return tables;

        }

        #endregion
    }
}
