namespace RDB_db_autopujcovna
{
    partial class Form1
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.jmeno_db_tb = new System.Windows.Forms.TextBox();
            this.heslo_db_tb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.zprava_lb = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.id_tb = new System.Windows.Forms.TextBox();
            this.server_tb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cesta_in_tb = new System.Windows.Forms.TextBox();
            this.soubor_in_tb = new System.Windows.Forms.Button();
            this.preview = new System.Windows.Forms.ListView();
            this.insert = new System.Windows.Forms.Button();
            this.tables_cb = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(79, 114);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Připojit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Jméno db:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Jméno (ID):";
            // 
            // jmeno_db_tb
            // 
            this.jmeno_db_tb.Location = new System.Drawing.Point(79, 39);
            this.jmeno_db_tb.Name = "jmeno_db_tb";
            this.jmeno_db_tb.Size = new System.Drawing.Size(100, 20);
            this.jmeno_db_tb.TabIndex = 3;
            this.jmeno_db_tb.Text = "autopujcovna";
            // 
            // heslo_db_tb
            // 
            this.heslo_db_tb.Location = new System.Drawing.Point(79, 91);
            this.heslo_db_tb.Name = "heslo_db_tb";
            this.heslo_db_tb.PasswordChar = '*';
            this.heslo_db_tb.Size = new System.Drawing.Size(100, 20);
            this.heslo_db_tb.TabIndex = 4;
            this.heslo_db_tb.Text = "123";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Hlášení: ";
            // 
            // zprava_lb
            // 
            this.zprava_lb.AutoSize = true;
            this.zprava_lb.Location = new System.Drawing.Point(71, 148);
            this.zprava_lb.Name = "zprava_lb";
            this.zprava_lb.Size = new System.Drawing.Size(129, 13);
            this.zprava_lb.TabIndex = 6;
            this.zprava_lb.Text = "Zdejte údaje pro připojení";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Heslo:";
            // 
            // id_tb
            // 
            this.id_tb.Location = new System.Drawing.Point(79, 65);
            this.id_tb.Name = "id_tb";
            this.id_tb.Size = new System.Drawing.Size(100, 20);
            this.id_tb.TabIndex = 8;
            this.id_tb.Text = "vojta";
            // 
            // server_tb
            // 
            this.server_tb.Location = new System.Drawing.Point(79, 12);
            this.server_tb.Name = "server_tb";
            this.server_tb.Size = new System.Drawing.Size(100, 20);
            this.server_tb.TabIndex = 9;
            this.server_tb.Text = "localhost";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Server:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(204, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Cesta k souboru pro vložení:";
            // 
            // cesta_in_tb
            // 
            this.cesta_in_tb.Location = new System.Drawing.Point(354, 11);
            this.cesta_in_tb.Name = "cesta_in_tb";
            this.cesta_in_tb.Size = new System.Drawing.Size(287, 20);
            this.cesta_in_tb.TabIndex = 12;
            // 
            // soubor_in_tb
            // 
            this.soubor_in_tb.Location = new System.Drawing.Point(658, 6);
            this.soubor_in_tb.Name = "soubor_in_tb";
            this.soubor_in_tb.Size = new System.Drawing.Size(130, 31);
            this.soubor_in_tb.TabIndex = 13;
            this.soubor_in_tb.Text = "Vybrat soubor";
            this.soubor_in_tb.UseVisualStyleBackColor = true;
            this.soubor_in_tb.Click += new System.EventHandler(this.soubor_in_tb_Click);
            // 
            // preview
            // 
            this.preview.Location = new System.Drawing.Point(354, 42);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(434, 346);
            this.preview.TabIndex = 14;
            this.preview.UseCompatibleStateImageBehavior = false;
            // 
            // insert
            // 
            this.insert.Enabled = false;
            this.insert.Location = new System.Drawing.Point(658, 394);
            this.insert.Name = "insert";
            this.insert.Size = new System.Drawing.Size(130, 44);
            this.insert.TabIndex = 15;
            this.insert.Text = "Vložit";
            this.insert.UseVisualStyleBackColor = true;
            this.insert.Click += new System.EventHandler(this.insert_Click);
            // 
            // tables_cb
            // 
            this.tables_cb.FormattingEnabled = true;
            this.tables_cb.Location = new System.Drawing.Point(227, 42);
            this.tables_cb.Name = "tables_cb";
            this.tables_cb.Size = new System.Drawing.Size(121, 21);
            this.tables_cb.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tables_cb);
            this.Controls.Add(this.insert);
            this.Controls.Add(this.preview);
            this.Controls.Add(this.soubor_in_tb);
            this.Controls.Add(this.cesta_in_tb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.server_tb);
            this.Controls.Add(this.id_tb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.zprava_lb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.heslo_db_tb);
            this.Controls.Add(this.jmeno_db_tb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox jmeno_db_tb;
        private System.Windows.Forms.TextBox heslo_db_tb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label zprava_lb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox id_tb;
        private System.Windows.Forms.TextBox server_tb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox cesta_in_tb;
        private System.Windows.Forms.Button soubor_in_tb;
        private System.Windows.Forms.ListView preview;
        private System.Windows.Forms.Button insert;
        private System.Windows.Forms.ComboBox tables_cb;
    }
}

