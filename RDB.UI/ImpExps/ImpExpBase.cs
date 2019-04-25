using RDB.Data.DAL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RDB.UI.ImpExps
{
    public class ImpExpBase
    {
        #region Fields

        protected readonly DefaultContext defaultContext;

        #endregion

        #region Properties

        public String TableName { get; set; }

        public String FilePath { get; set; }

        public Char Separator { get; set; }

        #endregion

        #region Constructors

        public ImpExpBase(DefaultContext defaultContext, ComboBox comboBox, List<String> tableNames)
        {
            this.defaultContext = defaultContext;

            comboBox.DataSource = tableNames;
        }

        #endregion

        #region Protected methods

        protected void SetSeparator(RadioButton od_car_rad, RadioButton od_str_rad, RadioButton od_tab_rad)
        {
            if (od_car_rad.Checked)
                Separator = ',';
            else if (od_str_rad.Checked)
                Separator = ';';
            else if (od_tab_rad.Checked)
                Separator = '\t';
        }

        #endregion
    }
}
