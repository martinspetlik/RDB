using System;
using System.Data;

namespace RDB.Data.Models.Scheme
{
    public class Column
    {
        #region Properties

        public String Name { get; set; }

        public String Type { get; set; }

        public Int16 Order { get; set; }

        public Boolean IsString
        {
            get
            {
                if (String.IsNullOrEmpty(Type))
                    return false;

                switch (Type.ToLower())
                {
                    case "varchar":
                        return true;
                    default:
                        return false;
                }
            }
        }

        #endregion

        #region Constructors

        public Column(DataRow dataRow)
        {
            Name = dataRow[3].ToString();
            Order = Int16.Parse(dataRow[4].ToString());
            Type = dataRow[7].ToString();
        }

        #endregion
    }
}
