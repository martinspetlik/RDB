namespace RDB.UI.Forms
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tables_cb = new System.Windows.Forms.ComboBox();
            this.insert_bt = new System.Windows.Forms.Button();
            this.preview = new System.Windows.Forms.ListView();
            this.soubor_in_bt = new System.Windows.Forms.Button();
            this.cesta_in_tb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.all_tables_ch = new System.Windows.Forms.CheckBox();
            this.od_car_rad = new System.Windows.Forms.RadioButton();
            this.od_str_rad = new System.Windows.Forms.RadioButton();
            this.od_tab_rad = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabPage1);
            this.tabs.Controls.Add(this.tabPage2);
            this.tabs.Location = new System.Drawing.Point(12, 12);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(580, 335);
            this.tabs.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.all_tables_ch);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.tables_cb);
            this.tabPage1.Controls.Add(this.insert_bt);
            this.tabPage1.Controls.Add(this.preview);
            this.tabPage1.Controls.Add(this.soubor_in_bt);
            this.tabPage1.Controls.Add(this.cesta_in_tb);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(572, 309);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Import";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(572, 484);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Export";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tables_cb
            // 
            this.tables_cb.FormattingEnabled = true;
            this.tables_cb.Location = new System.Drawing.Point(14, 81);
            this.tables_cb.Name = "tables_cb";
            this.tables_cb.Size = new System.Drawing.Size(130, 21);
            this.tables_cb.TabIndex = 22;
            // 
            // insert_bt
            // 
            this.insert_bt.Location = new System.Drawing.Point(465, 256);
            this.insert_bt.Name = "insert_bt";
            this.insert_bt.Size = new System.Drawing.Size(83, 44);
            this.insert_bt.TabIndex = 21;
            this.insert_bt.Text = "Vložit";
            this.insert_bt.UseVisualStyleBackColor = true;
            this.insert_bt.Click += new System.EventHandler(this.insert_bt_Click);
            // 
            // preview
            // 
            this.preview.Location = new System.Drawing.Point(161, 42);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(387, 208);
            this.preview.TabIndex = 20;
            this.preview.UseCompatibleStateImageBehavior = false;
            // 
            // soubor_in_bt
            // 
            this.soubor_in_bt.Location = new System.Drawing.Point(465, 6);
            this.soubor_in_bt.Name = "soubor_in_bt";
            this.soubor_in_bt.Size = new System.Drawing.Size(83, 31);
            this.soubor_in_bt.TabIndex = 19;
            this.soubor_in_bt.Text = "Vybrat soubor";
            this.soubor_in_bt.UseVisualStyleBackColor = true;
            this.soubor_in_bt.Click += new System.EventHandler(this.soubor_in_bt_Click);
            // 
            // cesta_in_tb
            // 
            this.cesta_in_tb.Location = new System.Drawing.Point(161, 11);
            this.cesta_in_tb.Name = "cesta_in_tb";
            this.cesta_in_tb.Size = new System.Drawing.Size(298, 20);
            this.cesta_in_tb.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Cesta k souboru pro import";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Rozložení dat:";
            // 
            // all_tables_ch
            // 
            this.all_tables_ch.AutoSize = true;
            this.all_tables_ch.Location = new System.Drawing.Point(14, 58);
            this.all_tables_ch.Name = "all_tables_ch";
            this.all_tables_ch.Size = new System.Drawing.Size(104, 17);
            this.all_tables_ch.TabIndex = 24;
            this.all_tables_ch.Text = "Všechny tabulky";
            this.all_tables_ch.UseVisualStyleBackColor = true;
            this.all_tables_ch.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // od_car_rad
            // 
            this.od_car_rad.AutoSize = true;
            this.od_car_rad.Location = new System.Drawing.Point(3, 3);
            this.od_car_rad.Name = "od_car_rad";
            this.od_car_rad.Size = new System.Drawing.Size(53, 17);
            this.od_car_rad.TabIndex = 25;
            this.od_car_rad.Text = "Čárka";
            this.od_car_rad.UseVisualStyleBackColor = true;
            // 
            // od_str_rad
            // 
            this.od_str_rad.AutoSize = true;
            this.od_str_rad.Checked = true;
            this.od_str_rad.Location = new System.Drawing.Point(3, 26);
            this.od_str_rad.Name = "od_str_rad";
            this.od_str_rad.Size = new System.Drawing.Size(67, 17);
            this.od_str_rad.TabIndex = 26;
            this.od_str_rad.TabStop = true;
            this.od_str_rad.Text = "Středník";
            this.od_str_rad.UseVisualStyleBackColor = true;
            // 
            // od_tab_rad
            // 
            this.od_tab_rad.AutoSize = true;
            this.od_tab_rad.Location = new System.Drawing.Point(3, 49);
            this.od_tab_rad.Name = "od_tab_rad";
            this.od_tab_rad.Size = new System.Drawing.Size(70, 17);
            this.od_tab_rad.TabIndex = 27;
            this.od_tab_rad.Text = "Tabulátor";
            this.od_tab_rad.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.od_car_rad);
            this.panel1.Controls.Add(this.od_tab_rad);
            this.panel1.Controls.Add(this.od_str_rad);
            this.panel1.Location = new System.Drawing.Point(14, 134);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(130, 72);
            this.panel1.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Oddělovač sloupců:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 355);
            this.Controls.Add(this.tabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.Text = "RDB";
            this.tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox tables_cb;
        private System.Windows.Forms.Button insert_bt;
        private System.Windows.Forms.ListView preview;
        private System.Windows.Forms.Button soubor_in_bt;
        private System.Windows.Forms.TextBox cesta_in_tb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox all_tables_ch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton od_car_rad;
        private System.Windows.Forms.RadioButton od_tab_rad;
        private System.Windows.Forms.RadioButton od_str_rad;
    }
}

