﻿using RDB.Data.DAL;
using System;
using System.Collections.Generic;
using System.Data;

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

        public static List<String> GetTableColumns(this DefaultContext defaultContext, String tableName)
        {
            List<String> columns = new List<String>();

            foreach (DataRow row in defaultContext.Database.Connection.GetSchema("Columns", new String[] { "", "", tableName }).Rows)
            {
                columns.Add(row[3].ToString());
            }

            return columns;
        }

        #endregion
    }
}
