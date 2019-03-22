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

namespace RDB_db_autopujcovna
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string server;
        string db_jmeno;
        string db_id;
        string db_heslo;

        private void button1_Click(object sender, EventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            server = server_tb.Text;
            db_jmeno = jmeno_db_tb.Text;
            db_id = id_tb.Text;
            db_heslo = heslo_db_tb.Text;
            connetionString = @"Data Source=" + server + ";Initial Catalog=" + db_jmeno + ";User ID=" + db_id + ";Password=" + db_heslo;
            zprava_lb.Text = "Čekejte...";
            try
            {
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                zprava_lb.Text = "Spojení navázáno";
                DataTable dt = cnn.GetSchema("Tables");
                List<string> tables = new List<string>();
                tables_cb.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    string tablename = (string)row[2];
                    tables.Add(tablename);
                    tables_cb.Items.Add(tablename);
                }
                insert.Enabled = true;
                cnn.Close();
            }
            catch (SqlException exp)
            {
                zprava_lb.Text = "Spojení nenalezeno";
                MessageBox.Show("Chyba:" + exp);
                MessageBox.Show(connetionString);
            }
        }
        string filePath = "";
        private void soubor_in_tb_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            //filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Textové soubory (*.txt)|*.txt|csv soubory (*.csv)|*.csv|xsl soubory (*.xsl)|*.xsl|Všechny soubory (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

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
                                        string[] values = line.Split(';');
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

        private void insert_Click(object sender, EventArgs e)
        {
            if(filePath.Length > 0 && tables_cb.Text.Length > 0)
            {
                string tabulka = tables_cb.Text;
                string connetionString;
                SqlConnection cnn;

                connetionString = @"Data Source=" + server + ";Initial Catalog=" + db_jmeno + ";User ID=" + db_id + ";Password=" + db_heslo;
                List<string> sloupce_list = new List<string>();
                try
                {
                    cnn = new SqlConnection(connetionString);
                    cnn.Open();
                    zprava_lb.Text = "Spojení navázáno";
                    String[] columnRestrictions = new String[4];
                    columnRestrictions[2] = tabulka;
                    DataTable sloupce = cnn.GetSchema("Columns", columnRestrictions);                   
                    foreach (DataRow row in sloupce.Rows)
                    {
                        string column = (string)row[3];
                        sloupce_list.Add(column);
                        //MessageBox.Show(column);
                    }

                    cnn.Close();
                }
                catch (SqlException exp)
                {
                    MessageBox.Show("Chyba:" + exp);
                }

                try
                {
                    int counter = 0;
                    string line;

                    System.IO.StreamReader file =
                    new System.IO.StreamReader(@filePath, Encoding.Default);
                    using (SqlConnection openCon = new SqlConnection(connetionString))
                    {
                        
                        using (SqlCommand command = new SqlCommand())
                        {        
                            command.Connection = openCon;            // <== lacking
                            command.CommandType = CommandType.Text;
                            openCon.Open();
                            
                            while ((line = file.ReadLine()) != null)
                            {
                                try
                                {
                                    string[] values = line.Split(';');
                                    command.CommandText = "INSERT into " + tabulka + " (";
                                    //command.Parameters.AddWithValue("@staffName", name);
                                    //command.Parameters.AddWithValue("@userID", userId);
                                    //command.Parameters.AddWithValue("@idDepart", idDepart);
                                    for(int i = 0; i < sloupce_list.Count; i++)
                                    {
                                        command.CommandText += sloupce_list[i];
                                        if (i < sloupce_list.Count - 1)
                                            command.CommandText += ", ";
                                    }
                                    command.CommandText += ") VALUES (";
                                    for(int i = 0; i < sloupce_list.Count; i++)
                                    {
                                        if (i < values.Length)
                                            command.CommandText += values[i];
                                        else
                                            command.CommandText += DBNull.Value;
                                        if (i < values.Length - 1)
                                            command.CommandText += ", ";
                                    }
                                    command.CommandText += ")";
                                    MessageBox.Show(command.CommandText);
                                    try
                                    {
                                        //MessageBox.Show(command.CommandText);
                                        int recordsAffected = command.ExecuteNonQuery();
                                    }
                                    catch (SqlException exp)
                                    {
                                        // error here
                                        MessageBox.Show(exp+"");
                                        //MessageBox.Show("Počet atributů v souboru a v tabulce se neshoduje");
                                    }
                                }
                                catch(SqlException ee)
                                {
                                    MessageBox.Show("Chyba:" + ee);
                                }
                                counter++;
                            }
                            openCon.Close();
                            MessageBox.Show("Hodnoty vloženy.");
                        }
                    }
                    file.Close();
                }
                catch (SqlException exp)
                {
                    zprava_lb.Text = "Spojení nenalezeno";
                    MessageBox.Show("Chyba:" + exp);
                    MessageBox.Show(connetionString);
                }

            }
        }
    }
}
