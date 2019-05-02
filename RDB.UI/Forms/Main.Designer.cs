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
            this.panel1 = new System.Windows.Forms.Panel();
            this.zip_ch = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.od_car_rad = new System.Windows.Forms.RadioButton();
            this.od_tab_rad = new System.Windows.Forms.RadioButton();
            this.od_str_rad = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tables_cb = new System.Windows.Forms.ComboBox();
            this.insert_bt = new System.Windows.Forms.Button();
            this.preview = new System.Windows.Forms.ListView();
            this.soubor_in_bt = new System.Windows.Forms.Button();
            this.cesta_in_tb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.zip_ch_e = new System.Windows.Forms.CheckBox();
            this.preview_bt = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.od_car_rad_e = new System.Windows.Forms.RadioButton();
            this.od_tab_rad_e = new System.Windows.Forms.RadioButton();
            this.od_str_rad_e = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.tables_cb_e = new System.Windows.Forms.ComboBox();
            this.export_bt = new System.Windows.Forms.Button();
            this.preview_e = new System.Windows.Forms.ListView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.vymaz_bt = new System.Windows.Forms.Button();
            this.mark_bt = new System.Windows.Forms.Button();
            this.check_bt = new System.Windows.Forms.Button();
            this.result_lb = new System.Windows.Forms.Label();
            this.tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabPage1);
            this.tabs.Controls.Add(this.tabPage2);
            this.tabs.Controls.Add(this.tabPage3);
            this.tabs.Location = new System.Drawing.Point(12, 12);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(580, 266);
            this.tabs.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(572, 240);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Import";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.zip_ch);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tables_cb);
            this.panel1.Controls.Add(this.insert_bt);
            this.panel1.Controls.Add(this.preview);
            this.panel1.Controls.Add(this.soubor_in_bt);
            this.panel1.Controls.Add(this.cesta_in_tb);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(572, 240);
            this.panel1.TabIndex = 0;
            // 
            // zip_ch
            // 
            this.zip_ch.AutoSize = true;
            this.zip_ch.Location = new System.Drawing.Point(9, 31);
            this.zip_ch.Name = "zip_ch";
            this.zip_ch.Size = new System.Drawing.Size(76, 17);
            this.zip_ch.TabIndex = 40;
            this.zip_ch.Text = "Soubor zip";
            this.zip_ch.UseVisualStyleBackColor = true;
            this.zip_ch.CheckedChanged += new System.EventHandler(this.zip_ch_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "Oddělovač sloupců:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.od_car_rad);
            this.panel2.Controls.Add(this.od_tab_rad);
            this.panel2.Controls.Add(this.od_str_rad);
            this.panel2.Location = new System.Drawing.Point(9, 113);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(130, 72);
            this.panel2.TabIndex = 38;
            // 
            // od_car_rad
            // 
            this.od_car_rad.AutoSize = true;
            this.od_car_rad.Checked = true;
            this.od_car_rad.Location = new System.Drawing.Point(3, 3);
            this.od_car_rad.Name = "od_car_rad";
            this.od_car_rad.Size = new System.Drawing.Size(53, 17);
            this.od_car_rad.TabIndex = 25;
            this.od_car_rad.TabStop = true;
            this.od_car_rad.Text = "Čárka";
            this.od_car_rad.UseVisualStyleBackColor = true;
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
            // od_str_rad
            // 
            this.od_str_rad.AutoSize = true;
            this.od_str_rad.Location = new System.Drawing.Point(3, 26);
            this.od_str_rad.Name = "od_str_rad";
            this.od_str_rad.Size = new System.Drawing.Size(67, 17);
            this.od_str_rad.TabIndex = 26;
            this.od_str_rad.Text = "Středník";
            this.od_str_rad.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Rozložení dat:";
            // 
            // tables_cb
            // 
            this.tables_cb.FormattingEnabled = true;
            this.tables_cb.Location = new System.Drawing.Point(6, 73);
            this.tables_cb.Name = "tables_cb";
            this.tables_cb.Size = new System.Drawing.Size(130, 21);
            this.tables_cb.TabIndex = 35;
            this.tables_cb.SelectedValueChanged += new System.EventHandler(this.tables_cb_SelectedValueChanged);
            // 
            // insert_bt
            // 
            this.insert_bt.Enabled = false;
            this.insert_bt.Location = new System.Drawing.Point(12, 191);
            this.insert_bt.Name = "insert_bt";
            this.insert_bt.Size = new System.Drawing.Size(127, 42);
            this.insert_bt.TabIndex = 34;
            this.insert_bt.Text = "Import";
            this.insert_bt.UseVisualStyleBackColor = true;
            this.insert_bt.Click += new System.EventHandler(this.insert_bt_Click);
            // 
            // preview
            // 
            this.preview.Location = new System.Drawing.Point(168, 46);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(398, 187);
            this.preview.TabIndex = 33;
            this.preview.UseCompatibleStateImageBehavior = false;
            // 
            // soubor_in_bt
            // 
            this.soubor_in_bt.Location = new System.Drawing.Point(483, 11);
            this.soubor_in_bt.Name = "soubor_in_bt";
            this.soubor_in_bt.Size = new System.Drawing.Size(83, 22);
            this.soubor_in_bt.TabIndex = 32;
            this.soubor_in_bt.Text = "Vybrat soubor";
            this.soubor_in_bt.UseVisualStyleBackColor = true;
            this.soubor_in_bt.Click += new System.EventHandler(this.soubor_in_bt_Click);
            // 
            // cesta_in_tb
            // 
            this.cesta_in_tb.Location = new System.Drawing.Point(168, 12);
            this.cesta_in_tb.Name = "cesta_in_tb";
            this.cesta_in_tb.Size = new System.Drawing.Size(309, 20);
            this.cesta_in_tb.TabIndex = 31;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Cesta k souboru pro import";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(572, 240);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Export";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.zip_ch_e);
            this.panel3.Controls.Add(this.preview_bt);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.tables_cb_e);
            this.panel3.Controls.Add(this.export_bt);
            this.panel3.Controls.Add(this.preview_e);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(572, 240);
            this.panel3.TabIndex = 1;
            // 
            // zip_ch_e
            // 
            this.zip_ch_e.AutoSize = true;
            this.zip_ch_e.Location = new System.Drawing.Point(6, 5);
            this.zip_ch_e.Name = "zip_ch_e";
            this.zip_ch_e.Size = new System.Drawing.Size(76, 17);
            this.zip_ch_e.TabIndex = 41;
            this.zip_ch_e.Text = "Soubor zip";
            this.zip_ch_e.UseVisualStyleBackColor = true;
            // 
            // preview_bt
            // 
            this.preview_bt.Location = new System.Drawing.Point(6, 159);
            this.preview_bt.Name = "preview_bt";
            this.preview_bt.Size = new System.Drawing.Size(130, 27);
            this.preview_bt.TabIndex = 40;
            this.preview_bt.Text = "Náhled";
            this.preview_bt.UseVisualStyleBackColor = true;
            this.preview_bt.Click += new System.EventHandler(this.preview_bt_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Oddělovač sloupců:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.od_car_rad_e);
            this.panel4.Controls.Add(this.od_tab_rad_e);
            this.panel4.Controls.Add(this.od_str_rad_e);
            this.panel4.Location = new System.Drawing.Point(6, 81);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(130, 72);
            this.panel4.TabIndex = 38;
            // 
            // od_car_rad_e
            // 
            this.od_car_rad_e.AutoSize = true;
            this.od_car_rad_e.Checked = true;
            this.od_car_rad_e.Location = new System.Drawing.Point(3, 3);
            this.od_car_rad_e.Name = "od_car_rad_e";
            this.od_car_rad_e.Size = new System.Drawing.Size(53, 17);
            this.od_car_rad_e.TabIndex = 25;
            this.od_car_rad_e.TabStop = true;
            this.od_car_rad_e.Text = "Čárka";
            this.od_car_rad_e.UseVisualStyleBackColor = true;
            // 
            // od_tab_rad_e
            // 
            this.od_tab_rad_e.AutoSize = true;
            this.od_tab_rad_e.Location = new System.Drawing.Point(3, 49);
            this.od_tab_rad_e.Name = "od_tab_rad_e";
            this.od_tab_rad_e.Size = new System.Drawing.Size(70, 17);
            this.od_tab_rad_e.TabIndex = 27;
            this.od_tab_rad_e.Text = "Tabulátor";
            this.od_tab_rad_e.UseVisualStyleBackColor = true;
            // 
            // od_str_rad_e
            // 
            this.od_str_rad_e.AutoSize = true;
            this.od_str_rad_e.Location = new System.Drawing.Point(3, 26);
            this.od_str_rad_e.Name = "od_str_rad_e";
            this.od_str_rad_e.Size = new System.Drawing.Size(67, 17);
            this.od_str_rad_e.TabIndex = 26;
            this.od_str_rad_e.Text = "Středník";
            this.od_str_rad_e.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Rozložení dat:";
            // 
            // tables_cb_e
            // 
            this.tables_cb_e.FormattingEnabled = true;
            this.tables_cb_e.Location = new System.Drawing.Point(6, 41);
            this.tables_cb_e.Name = "tables_cb_e";
            this.tables_cb_e.Size = new System.Drawing.Size(130, 21);
            this.tables_cb_e.TabIndex = 35;
            this.tables_cb_e.SelectedValueChanged += new System.EventHandler(this.tables_cb_e_SelectedValueChanged);
            // 
            // export_bt
            // 
            this.export_bt.Enabled = false;
            this.export_bt.Location = new System.Drawing.Point(6, 192);
            this.export_bt.Name = "export_bt";
            this.export_bt.Size = new System.Drawing.Size(130, 42);
            this.export_bt.TabIndex = 34;
            this.export_bt.Text = "Export";
            this.export_bt.UseVisualStyleBackColor = true;
            this.export_bt.Click += new System.EventHandler(this.export_bt_Click);
            // 
            // preview_e
            // 
            this.preview_e.Location = new System.Drawing.Point(168, 6);
            this.preview_e.Name = "preview_e";
            this.preview_e.Size = new System.Drawing.Size(398, 228);
            this.preview_e.TabIndex = 33;
            this.preview_e.UseCompatibleStateImageBehavior = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.result_lb);
            this.tabPage3.Controls.Add(this.check_bt);
            this.tabPage3.Controls.Add(this.mark_bt);
            this.tabPage3.Controls.Add(this.vymaz_bt);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(572, 240);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Označování";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // vymaz_bt
            // 
            this.vymaz_bt.Location = new System.Drawing.Point(65, 88);
            this.vymaz_bt.Name = "vymaz_bt";
            this.vymaz_bt.Size = new System.Drawing.Size(127, 64);
            this.vymaz_bt.TabIndex = 0;
            this.vymaz_bt.Text = "Vymazat databázi";
            this.vymaz_bt.UseVisualStyleBackColor = true;
            this.vymaz_bt.Click += new System.EventHandler(this.vymaz_bt_Click);
            // 
            // mark_bt
            // 
            this.mark_bt.Location = new System.Drawing.Point(212, 88);
            this.mark_bt.Name = "mark_bt";
            this.mark_bt.Size = new System.Drawing.Size(127, 64);
            this.mark_bt.TabIndex = 1;
            this.mark_bt.Text = "Označit data";
            this.mark_bt.UseVisualStyleBackColor = true;
            this.mark_bt.Click += new System.EventHandler(this.mark_bt_Click);
            // 
            // check_bt
            // 
            this.check_bt.Location = new System.Drawing.Point(356, 88);
            this.check_bt.Name = "check_bt";
            this.check_bt.Size = new System.Drawing.Size(127, 64);
            this.check_bt.TabIndex = 2;
            this.check_bt.Text = "Kontrola dat";
            this.check_bt.UseVisualStyleBackColor = true;
            this.check_bt.Click += new System.EventHandler(this.check_bt_Click);
            // 
            // result_lb
            // 
            this.result_lb.AutoSize = true;
            this.result_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.result_lb.Location = new System.Drawing.Point(192, 197);
            this.result_lb.Name = "result_lb";
            this.result_lb.Size = new System.Drawing.Size(0, 26);
            this.result_lb.TabIndex = 3;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 286);
            this.Controls.Add(this.tabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.Text = "RDB";
            this.tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton od_car_rad;
        private System.Windows.Forms.RadioButton od_tab_rad;
        private System.Windows.Forms.RadioButton od_str_rad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox tables_cb;
        private System.Windows.Forms.Button insert_bt;
        private System.Windows.Forms.ListView preview;
        private System.Windows.Forms.Button soubor_in_bt;
        private System.Windows.Forms.TextBox cesta_in_tb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton od_car_rad_e;
        private System.Windows.Forms.RadioButton od_tab_rad_e;
        private System.Windows.Forms.RadioButton od_str_rad_e;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox tables_cb_e;
        private System.Windows.Forms.Button export_bt;
        private System.Windows.Forms.ListView preview_e;
        private System.Windows.Forms.Button preview_bt;
        private System.Windows.Forms.CheckBox zip_ch;
        private System.Windows.Forms.CheckBox zip_ch_e;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label result_lb;
        private System.Windows.Forms.Button check_bt;
        private System.Windows.Forms.Button mark_bt;
        private System.Windows.Forms.Button vymaz_bt;
    }
}

