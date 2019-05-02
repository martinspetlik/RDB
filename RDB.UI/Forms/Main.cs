using MySql.Data.MySqlClient;
using RDB.Data.DAL;
using RDB.Data.Extensions;
using RDB.Data.Models;
using RDB.UI.ImpExps;
using RDB.UI.Watermarking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RDB.UI.Forms
{
    public partial class Main : Form
    {
        #region Fields

        private readonly DefaultContext defaultContext;

        private readonly Import import;

        private readonly Export export;

        private readonly Watermark marker;

        #endregion

        #region Constructors

        public Main()
        {
            InitializeComponent();

            defaultContext = new DefaultContext();

            List<String> tableNames = defaultContext.GetScheme();
            import = new Import(defaultContext, tables_cb, tableNames);
            export = new Export(defaultContext, tables_cb_e, tableNames);
            marker = new Watermark(defaultContext);

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
            export.SaveFile(od_car_rad_e, od_str_rad_e, od_tab_rad_e, zip_ch_e);
        }

        private void zip_ch_CheckedChanged(object sender, EventArgs e)
        {
            if (zip_ch.Checked)
                tables_cb.Enabled = false;
            else
                tables_cb.Enabled = true;
        }
        #endregion

        

        private void vymaz_bt_Click(object sender, EventArgs e)
        {
            try
            {
                defaultContext.Database.ExecuteSqlCommand("SET FOREIGN_KEY_CHECKS = 0;TRUNCATE TABLE jizdenka;SET FOREIGN_KEY_CHECKS = 1;");
                defaultContext.Database.ExecuteSqlCommand("SET FOREIGN_KEY_CHECKS = 0;TRUNCATE TABLE jizda;SET FOREIGN_KEY_CHECKS = 1;");
                defaultContext.Database.ExecuteSqlCommand("SET FOREIGN_KEY_CHECKS = 0;TRUNCATE TABLE mezizastavka;SET FOREIGN_KEY_CHECKS = 1;");
                defaultContext.Database.ExecuteSqlCommand("SET FOREIGN_KEY_CHECKS = 0;TRUNCATE TABLE autobus;SET FOREIGN_KEY_CHECKS = 1;");
                defaultContext.Database.ExecuteSqlCommand("SET FOREIGN_KEY_CHECKS = 0;TRUNCATE TABLE klient;SET FOREIGN_KEY_CHECKS = 1;");
                defaultContext.Database.ExecuteSqlCommand("SET FOREIGN_KEY_CHECKS = 0;TRUNCATE TABLE kontakt;SET FOREIGN_KEY_CHECKS = 1;");
                defaultContext.Database.ExecuteSqlCommand("SET FOREIGN_KEY_CHECKS = 0;TRUNCATE TABLE ridic;SET FOREIGN_KEY_CHECKS = 1;");
                defaultContext.Database.ExecuteSqlCommand("SET FOREIGN_KEY_CHECKS = 0;TRUNCATE TABLE trasy;SET FOREIGN_KEY_CHECKS = 1;");
                defaultContext.Database.ExecuteSqlCommand("SET FOREIGN_KEY_CHECKS = 0;TRUNCATE TABLE lokalita;SET FOREIGN_KEY_CHECKS = 1;");
                defaultContext.Database.ExecuteSqlCommand("SET FOREIGN_KEY_CHECKS = 0;TRUNCATE TABLE znacka;SET FOREIGN_KEY_CHECKS = 1;");
                defaultContext.Database.ExecuteSqlCommand("SET FOREIGN_KEY_CHECKS = 0;TRUNCATE TABLE typkontaktu;SET FOREIGN_KEY_CHECKS = 1;");
   
                MessageBox.Show("Data byla smazána");
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("Vyskytla se chyba: " + ex);
            }
        }

        #region Watermark events

        private void mark_bt_Click(object sender, EventArgs e)
        {
            marker.Watermarking();
        }

        private void check_bt_Click(object sender, EventArgs e)
        {
            if (marker.IsDataOurs())
                result_lb.Text = "Toto jsou naše data";
            else
                result_lb.Text = "Toto nejsou naše data";
        }

        #endregion

        #endregion

    }
}
