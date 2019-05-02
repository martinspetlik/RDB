using RDB.Data.DAL;
using RDB.Data.Models.Scheme;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RDB.Data.Extensions
{
    public static class SchemeExtension
    {
        #region Public methods

        public static List<String> GetScheme(this DefaultContext defaultContext)
        {
            List<String> tables = new List<String>();

            foreach (DataRow row in defaultContext.Database.Connection.GetSchema("Tables").Rows)
            {
                String tablename = row[2].ToString();
                if (tablename.ToLower() != "__migrationhistory")
                    tables.Add(tablename);
            }

            return tables;
        }

        public static List<Column> GetTableColumns(this DefaultContext defaultContext, String tableName)
        {
            List<Column> columns = new List<Column>();
            DataRowCollection rows = defaultContext.Database.Connection.GetSchema("Columns", new String[] { "", "", tableName, "" }).Rows;
            foreach (DataRow row in rows)
            {
                    columns.Add(new Column(row));
            }

            return columns.OrderBy(c => c.Order).ToList();
        }

        #endregion
    }
}
