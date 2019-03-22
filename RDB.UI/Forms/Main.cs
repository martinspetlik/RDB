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

        #endregion

        #region Constructors

        public Main()
        {
            defaultContext = new DefaultContext();

            InitializeComponent();
            GetScheme();
            insert_bt.Enabled = false;
        }

        #endregion
        private void GetScheme() //Získání názvu tabulek pro výběr
        {
            defaultContext.Database.Connection.Open();
            DataTable dt = defaultContext.Database.Connection.GetSchema("Tables");
            List<string> tables = new List<string>();
            tables_cb.Items.Clear();
            if (!all_tables)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string tablename = (string)row[2];
                    tables.Add(tablename);
                    tables_cb.Items.Add(tablename);
                }
            }
            defaultContext.Database.Connection.Close();
        }

        private bool all_tables = false;
        private char oddelovac = ';';
        string filePath = "";

        private void checkBox1_CheckedChanged(object sender, EventArgs e)   //Vyběr jedné tabulky nebo v souboru jsou všechny
        {
            if (all_tables_ch.Checked)
            {
                tables_cb.Enabled = false;
                all_tables = true;
            }
            else
            {
                tables_cb.Enabled = true;
                all_tables = false;
            }
        }

        private void soubor_in_bt_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            //filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Textové soubory (*.txt)|*.txt|csv soubory (*.csv)|*.csv|xsl soubory (*.xsl)|*.xsl|Všechny soubory (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                if (od_car_rad.Checked)
                    oddelovac = ',';
                else if (od_str_rad.Checked)
                    oddelovac = ';';
                else if (od_tab_rad.Checked)
                    oddelovac = '\t';
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    cesta_in_tb.Text = filePath;
                    //Read the contents of the file into a stream
                    //var fileStream;
                    try
                    {
                        var fileStream = openFileDialog.OpenFile();
                        using (StreamReader reader = new StreamReader(fileStream, Encoding.Default))
                        {
                            bool prvni = true;
                            //fileContent = reader.ReadToEnd();
                            preview.View = View.Details;

                            for (int i = 0; i < 10; i++)
                            {
                                try
                                {
                                    var line = reader.ReadLine();
                                    if (line != null)
                                    {
                                        string[] values = line.Split(oddelovac);
                                        string[] arr = new string[values.Length];
                                        //var items = preview.Items;
                                        //ListViewItem lvi1 = new ListViewItem();
                                        ListViewItem item1 = new ListViewItem();
                                        if (prvni)
                                        {
                                            for (int j = 0; j < values.Length; j++)
                                            {
                                                preview.Columns.Add("Sloupec " + j);
                                                prvni = false;
                                            }
                                        }
                                        preview.Items.Add(new ListViewItem(values));
                                        //preview.Items.Add(values);
                                    }
                                }
                                catch { }

                            }
                            insert_bt.Enabled = true;
                            preview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                            preview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("SOubor nemohl být otevřen, může být otevřen v jiné aplikaci.");
                    }
                }
            }
        }

        private void insert_bt_Click(object sender, EventArgs e) 
        {
            if (filePath.Length > 0 && ((tables_cb.Text.Length > 0 && !all_tables) || all_tables))
            {
                string tabulka = tables_cb.Text;
                List<string> sloupce_list = new List<string>();
                try                                                 //získání názvů sloupců pro vkládání dat
                {
                    defaultContext.Database.Connection.Open();
                    String[] columnRestrictions = new String[4];
                    columnRestrictions[2] = tabulka;
                    DataTable sloupce = defaultContext.Database.Connection.GetSchema("Columns", columnRestrictions);
                    foreach (DataRow row in sloupce.Rows)
                    {
                        string column = (string)row[3];
                        sloupce_list.Add(column);
                    }

                    InsertColumns(sloupce_list);    //volání vkládání
                    defaultContext.Database.Connection.Close();
                }
                catch (SqlException exp)
                {
                    MessageBox.Show("Chyba:" + exp);
                }
            }
        }
        private void InsertColumns(List<string> sloupce_list) //vložení hodnot ze souboru
        {
            System.IO.StreamReader file =
            new System.IO.StreamReader(@filePath, Encoding.Default);
            
            if (!all_tables)
            {
                string tabulka = tables_cb.Text;
                InsertIntoTable(file, tabulka, sloupce_list);
            }
            else
            {
                /*
                 * 
                 *      Zde bude vložení všech tabulek naráz  
                 */
            }
            MessageBox.Show("Hodnoty vloženy.");
            file.Close();
        }

        private void InsertIntoTable(StreamReader file, string tabulka, List<string> sloupce_list)
        {
            int counter = 0;
            string line;
            defaultContext.Database.Connection.Open();
            while ((line = file.ReadLine()) != null)
            {
                try
                {
                    
                    string command = "INSERT INTO " + tabulka + " (";
                    string[] values = line.Split(oddelovac);
                    String[] columnRestrictions = new String[4];
                    columnRestrictions[2] = tabulka;

                    for (int i = 0; i < sloupce_list.Count; i++)
                    {
                        command += sloupce_list[i];
                        if (i < sloupce_list.Count - 1)
                            command += ", ";
                    }
                    command += ") VALUES (";
                    for (int i = 0; i < sloupce_list.Count; i++)
                    {
                        if (i < values.Length)
                            command += values[i];
                        else
                            command += DBNull.Value;
                        if (i < values.Length - 1)
                            command += ", ";
                    }
                    command += ")";
                    defaultContext.Database.ExecuteSqlCommand(command);

                }
                catch (SqlException e)
                {
                    MessageBox.Show("Chyba: " + e);
                }
                counter++;
            }
            defaultContext.Database.Connection.Close();
        }
    }

    public class Import
    {
        public Import()
        {

        }
    }
}
