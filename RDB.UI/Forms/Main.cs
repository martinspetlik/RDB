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
            export = new Export(defaultContext, tables_cb_e, tableNames);
            import = new Import(defaultContext, tables_cb, tableNames);
        }

        #endregion

        #region Private methods

        #region Import events

        /// <summary>
        /// Vyběr jedné tabulky nebo v souboru jsou všechny
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void all_tables_ch_CheckedChanged(object sender, EventArgs e)
        {
            if (all_tables_ch.Checked)
            {
                tables_cb.Enabled = false;
                import.AllTables = true;
            }
            else
            {
                tables_cb.Enabled = true;
                import.AllTables = false;
            }
        }

        /// <summary>
        /// Otevření filedialogu pro výběr CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void soubor_in_bt_Click(object sender, EventArgs e)
        {
            import.OpenFile(od_car_rad, od_str_rad, od_tab_rad, cesta_in_tb, insert_bt, preview);
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
            if (import != null)
                import.TableName = tables_cb.Text;
        }

        #endregion

        #region Export events

        private void preview_bt_Click(object sender, EventArgs e)   //Náhled dat z DB
        {
            export.ShowPreview(preview_e);
        }

        private void all_tables_ch_e_CheckedChanged(object sender, EventArgs e)
        {
            if (all_tables_ch_e.Checked)
            {
                tables_cb_e.Enabled = false;
                export.AllTables = true;
                export_bt.Enabled = true;
            }
            else
            {
                tables_cb_e.Enabled = true;
                export.AllTables = false;
                if (tables_cb_e.Text.Length > 0)
                    export_bt.Enabled = true;
                else
                    export_bt.Enabled = false;
            }
        }

        private void tables_cb_e_SelectedValueChanged(object sender, EventArgs e)
        {
            if(export != null)
                export.TableName = tables_cb_e.Text;
        }

        private void export_bt_Click(object sender, EventArgs e)
        {
            export.SaveFile(od_car_rad_e, od_str_rad_e, od_tab_rad_e);
        }

        #endregion

        #endregion
    }
}
