using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rontgen
{
    public partial class Form1 : Form
    {
        Resim Goruntu = new Resim();

        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg;*.tif; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.tif;*.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Goruntu.DosyaAdi = open.FileName;
                Goruntu.resim1 = new Bitmap(open.FileName);
                Goruntu.resim2 = new Bitmap(open.FileName);
                Goruntu.data = new int[Goruntu.resim1.Width, Goruntu.resim1.Height];
                int x, y;
                for (x = 0; x < Goruntu.resim1.Width; x++)
                {
                    for (y = 0; y < Goruntu.resim1.Height; y++)
                    { Goruntu.data[x, y] = -1; }
                }
                pictureBox1.Image = Goruntu.resim1;
                pictureBox2.Image = Goruntu.resim2;
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {

            StreamReader sr = new StreamReader("e:\\umut\\TEZ\\" + textBox1.Text+".txt");
            string Str;

            bilgiler.Text = Kimlik.Text;
            Str = sr.ReadLine();
            while (Str != null)
            {
                bilgiler.Text += Str +"\n";
                Str = sr.ReadLine();
            }
            sr.Close();
        }

            

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = textBox1.Text + "(*.jpg; *.jpeg;*.tif; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.tif;*.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Goruntu.DosyaAdi = open.FileName;
                Goruntu.resim1 = new Bitmap(open.FileName);
                Goruntu.data = new int[Goruntu.resim1.Width, Goruntu.resim1.Height];
                int x, y;
                for (x = 0; x < Goruntu.resim1.Width; x++)
                {
                    for (y = 0; y < Goruntu.resim1.Height; y++)
                    { Goruntu.data[x, y] = -1; }
                }
                pictureBox2.Image = Goruntu.resim1;
            }
        }

        private void Texxt_Click(object sender, EventArgs e)
        {
            //StreamReader sr = new StreamReader("e:\\umut\\TEZ\\Mesaj.txt");
            //FileStream fs = new FileStream("e:\\umut\\TEZ\\Mesaj.txt", FileMode.Create, FileAccess.Write);
            //fs.Flush();
            FileStream fs = new FileStream("e:\\umut\\TEZ\\" + Kimlik.Text+".txt", FileMode.Create, FileAccess.Write);
            fs.Flush();
            StreamWriter dosya = new StreamWriter(fs);

            dosya.WriteLine(Kimlik.Text);
            dosya.WriteLine(ad.Text);
            dosya.WriteLine(yas.Text);
            dosya.WriteLine(dogum.Text);
            dosya.WriteLine(cinsiyet.Text);
            dosya.WriteLine(yorum1.Text);
            dosya.Close();

            //Kimlik.Text = "";
            //ad.Text = "";
            //yas.Text = "";
            //dogum.Text = "";
            //cinsiyet.Text = "";
            //yorum1.Text = "";

            fs.Close();
        }

        private void rontgenekayit_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp1);
            Stegano Gizle = new Stegano(bmp1);
            int i;

            Gizle.KapasiteHesapla(3, 3, 2);

            string mesaj = Kimlik.Text+"\n";
            string mesaj1; mesaj1 = "";
            for (i = 0; i < Gizle.KapasiteBits; i = i + 800)
            { mesaj1 = mesaj1 + mesaj; }


            Gizle.MesajAl(mesaj1);
            //  Gizle.TextSakla332();    pictureBox2.Image =Gizle.StegoBmp;
            Gizle.FileTextSakla332(); pictureBox1.Image = Gizle.StegoBmp; 

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = Kimlik.Text+"(*.png,*.jpg) | *.png;*.jpg";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                Kimlik.Text = saveFile.FileName.ToString();
                pictureBox1.ImageLocation = Kimlik.Text;
                Gizle.MesajAl(mesaj1);
                //Gizle.TextSakla332();    pictureBox2.Image =Gizle.StegoBmp;
                Gizle.FileTextSakla332(); pictureBox1.Image = Gizle.StegoBmp;

                bmp1.Save(Kimlik.Text);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = textBox1.Text + "Image files (*.jpg, *.jpeg, *.jpe, *.png) | *.jpg; *.jpeg; *.jpe; *.png";

            Bitmap bmp1 = new Bitmap(pictureBox2.Image);
            Stegano Gizle = new Stegano(bmp1);
            Gizle.KapasiteHesapla(1, 0, 0);

            
            Gizle.TextCikar100(1);      Gizle.MesajBitsKaydet();                
            Gizle.MesajBitstoAscii();   Gizle.MesajAsciiKaydet();
            //textBox1.Text = Gizle.MesajBits;
            bilgiler.Text = Gizle.MesajAscii;

            Gizle.FileTextCikar332();

        }
    }
}
