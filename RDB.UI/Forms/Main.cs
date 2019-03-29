using RDB.Data.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace RDB.UI.Forms
{
    public partial class Main : Form
    {
        #region Fields

        private readonly DefaultContext defaultContext;
        private Import imp;
        private Export exp;

        #endregion

        #region Constructors

        public Main()
        {
            defaultContext = new DefaultContext();
            //defaultContext.Database.Connection.Open();
            InitializeComponent();
            //GetScheme();
            insert_bt.Enabled = false;
            BindingSource bs = new BindingSource();
            imp = new Import(defaultContext, bs, tables_cb);

            exp = new Export(defaultContext, bs, tables_cb_e);
        }

        #endregion

        #region Import events

        private void checkBox1_CheckedChanged(object sender, EventArgs e)   //Vyběr jedné tabulky nebo v souboru jsou všechny
        {
            if (all_tables_ch.Checked)
            {
                tables_cb.Enabled = false;
                imp.All_tables = true;
            }
            else
            {
                tables_cb.Enabled = true;
                imp.All_tables = false;
            }
        }

        private void soubor_in_bt_Click(object sender, EventArgs e)     //otevření filedialogu pro výběr CSV
        {
            imp.OpenFile(od_car_rad, od_str_rad, od_tab_rad, cesta_in_tb, insert_bt, preview);
        }   

        private void insert_bt_Click(object sender, EventArgs e) //Vložení dat z CSV do DB
        {
            imp.Insert();
        }

        private void tables_cb_SelectedValueChanged(object sender, EventArgs e)
        {
            imp.Tabulka = tables_cb.Text;
        }

        #endregion

        #region Export events

        private void preview_bt_Click(object sender, EventArgs e)   //Náhled dat z DB
        {

        }

        #endregion
    }
}
