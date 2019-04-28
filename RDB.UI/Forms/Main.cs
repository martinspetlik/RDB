using RDB.Data.DAL;
using RDB.Data.Extensions;
using RDB.UI.ImpExps;
using RDB.UI.Watermarking;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RDB.UI.Forms
{
    public partial class Main : Form
    {
        #region Fields

        private readonly DefaultContext defaultContext;

        private readonly Import import;

        private readonly Export export;

        #endregion

        #region Constructors

        public Main()
        {
            InitializeComponent();

            defaultContext = new DefaultContext();

            Watermark marker = new Watermark(defaultContext);
            marker.Watermarking();

            List<String> tableNames = defaultContext.GetScheme();
            import = new Import(defaultContext, tables_cb, tableNames);
            export = new Export(defaultContext, tables_cb_e, tableNames);

            export_bt.Enabled = true;
        }

        #endregion

        #region Private methods

        #region Import events

        /// <summary>
        /// Otevření filedialogu pro výběr CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void soubor_in_bt_Click(object sender, EventArgs e)
        {
            import.OpenFile(od_car_rad, od_str_rad, od_tab_rad, cesta_in_tb, insert_bt, preview, zip_ch);
        }

        /// <summary>
        /// Vložení dat z CSV do DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void insert_bt_Click(object sender, EventArgs e)
        {
            import.Insert();
        }

        private void tables_cb_SelectedValueChanged(object sender, EventArgs e)
        {
            if(import != null)
                import.TableName = tables_cb.Text;
        }

        #endregion

        #region Export events

        private void preview_bt_Click(object sender, EventArgs e)   //Náhled dat z DB
        {
            export.ShowPreview(preview_e);
        }

        private void tables_cb_e_SelectedValueChanged(object sender, EventArgs e)
        {
            if (export != null)
                export.TableName = tables_cb_e.Text;
        }

        private void export_bt_Click(object sender, EventArgs e)
        {
            export.SaveFile(od_car_rad_e, od_str_rad_e, od_tab_rad_e);
        }

        private void zip_ch_CheckedChanged(object sender, EventArgs e)
        {
            if (zip_ch.Checked)
                tables_cb.Enabled = false;
            else
                tables_cb.Enabled = true;
        }
        #endregion
        #endregion
    }
}
