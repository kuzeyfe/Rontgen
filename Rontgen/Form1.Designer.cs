namespace Rontgen
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.resim1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.YeniHasta = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rontgenekayit = new System.Windows.Forms.Button();
            this.yorum1 = new System.Windows.Forms.TextBox();
            this.Kimlik = new System.Windows.Forms.TextBox();
            this.ad = new System.Windows.Forms.TextBox();
            this.cinsiyet = new System.Windows.Forms.TextBox();
            this.yas = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dogum = new System.Windows.Forms.TextBox();
            this.Texxt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.bilgiler = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.YeniHasta.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(6, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(294, 254);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(6, 57);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(294, 254);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 22;
            this.pictureBox2.TabStop = false;
            // 
            // resim1
            // 
            this.resim1.Location = new System.Drawing.Point(6, 6);
            this.resim1.Name = "resim1";
            this.resim1.Size = new System.Drawing.Size(291, 48);
            this.resim1.TabIndex = 24;
            this.resim1.Text = "Yeni Hasta Röntgen Yükle";
            this.resim1.UseVisualStyleBackColor = true;
            this.resim1.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(291, 47);
            this.button4.TabIndex = 25;
            this.button4.Text = "Varolan Röntgen Görüntüle";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // YeniHasta
            // 
            this.YeniHasta.Controls.Add(this.tabPage1);
            this.YeniHasta.Controls.Add(this.tabPage2);
            this.YeniHasta.Location = new System.Drawing.Point(2, 22);
            this.YeniHasta.Name = "YeniHasta";
            this.YeniHasta.SelectedIndex = 0;
            this.YeniHasta.Size = new System.Drawing.Size(846, 513);
            this.YeniHasta.TabIndex = 38;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.SteelBlue;
            this.tabPage1.Controls.Add(this.rontgenekayit);
            this.tabPage1.Controls.Add(this.yorum1);
            this.tabPage1.Controls.Add(this.resim1);
            this.tabPage1.Controls.Add(this.Kimlik);
            this.tabPage1.Controls.Add(this.ad);
            this.tabPage1.Controls.Add(this.cinsiyet);
            this.tabPage1.Controls.Add(this.yas);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.dogum);
            this.tabPage1.Controls.Add(this.Texxt);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(838, 484);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Yani Hasta";
            // 
            // rontgenekayit
            // 
            this.rontgenekayit.Location = new System.Drawing.Point(507, 263);
            this.rontgenekayit.Name = "rontgenekayit";
            this.rontgenekayit.Size = new System.Drawing.Size(117, 51);
            this.rontgenekayit.TabIndex = 37;
            this.rontgenekayit.Text = "Röntgene Kaydet";
            this.rontgenekayit.UseVisualStyleBackColor = true;
            this.rontgenekayit.Click += new System.EventHandler(this.rontgenekayit_Click);
            // 
            // yorum1
            // 
            this.yorum1.Location = new System.Drawing.Point(635, 201);
            this.yorum1.Multiline = true;
            this.yorum1.Name = "yorum1";
            this.yorum1.Size = new System.Drawing.Size(169, 113);
            this.yorum1.TabIndex = 36;
            // 
            // Kimlik
            // 
            this.Kimlik.Location = new System.Drawing.Point(635, 60);
            this.Kimlik.Name = "Kimlik";
            this.Kimlik.Size = new System.Drawing.Size(169, 22);
            this.Kimlik.TabIndex = 0;
            // 
            // ad
            // 
            this.ad.Location = new System.Drawing.Point(635, 88);
            this.ad.Name = "ad";
            this.ad.Size = new System.Drawing.Size(169, 22);
            this.ad.TabIndex = 1;
            // 
            // cinsiyet
            // 
            this.cinsiyet.Location = new System.Drawing.Point(635, 172);
            this.cinsiyet.Name = "cinsiyet";
            this.cinsiyet.Size = new System.Drawing.Size(169, 22);
            this.cinsiyet.TabIndex = 35;
            // 
            // yas
            // 
            this.yas.Location = new System.Drawing.Point(635, 116);
            this.yas.Name = "yas";
            this.yas.Size = new System.Drawing.Size(169, 22);
            this.yas.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(507, 60);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 17);
            this.label11.TabIndex = 34;
            this.label11.Text = "KimlikNo";
            // 
            // dogum
            // 
            this.dogum.Location = new System.Drawing.Point(635, 144);
            this.dogum.Name = "dogum";
            this.dogum.Size = new System.Drawing.Size(169, 22);
            this.dogum.TabIndex = 3;
            // 
            // Texxt
            // 
            this.Texxt.Location = new System.Drawing.Point(507, 227);
            this.Texxt.Name = "Texxt";
            this.Texxt.Size = new System.Drawing.Size(117, 29);
            this.Texxt.TabIndex = 5;
            this.Texxt.Text = "Texte Kaydet";
            this.Texxt.UseVisualStyleBackColor = true;
            this.Texxt.Click += new System.EventHandler(this.Texxt_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(507, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Hasta Adı-Soyadı";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(507, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Yaş";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(507, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Doğum Tarihi";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(507, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Doktor Yorumu";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(507, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Cinsiyet";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.SteelBlue;
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.bilgiler);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.pictureBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(838, 484);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Varolan Hasta";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(515, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 17);
            this.label7.TabIndex = 52;
            this.label7.Text = "Kimlik No Giriniz:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(638, 63);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(149, 22);
            this.textBox1.TabIndex = 51;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(638, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 47);
            this.button1.TabIndex = 50;
            this.button1.Text = "Röntgeni Görüntüle";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bilgiler
            // 
            this.bilgiler.AutoSize = true;
            this.bilgiler.Location = new System.Drawing.Point(640, 141);
            this.bilgiler.Name = "bilgiler";
            this.bilgiler.Size = new System.Drawing.Size(0, 17);
            this.bilgiler.TabIndex = 49;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(512, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 102);
            this.label6.TabIndex = 47;
            this.label6.Text = "Kimlik-No        :\r\nAdı-Soyadı      :\r\nYaşı                 :\r\nDoğumTarihi   :\r\nC" +
    "insiyet           :\r\nDoktor Yorum  :\r\n";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(515, 91);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 47);
            this.button2.TabIndex = 41;
            this.button2.Text = "Bilgilerini Görüntüle";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(849, 535);
            this.Controls.Add(this.YeniHasta);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.YeniHasta.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button resim1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TabControl YeniHasta;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox Kimlik;
        private System.Windows.Forms.TextBox ad;
        private System.Windows.Forms.TextBox cinsiyet;
        private System.Windows.Forms.TextBox yas;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox dogum;
        private System.Windows.Forms.Button Texxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox yorum1;
        private System.Windows.Forms.Label bilgiler;
        private System.Windows.Forms.Button rontgenekayit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
    }
}

