using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Rontgen
{
    public class Stegano
    {
        public Bitmap Orijinal;
        public Bitmap StegoBmp;
        public Bitmap Maske;

        public string MesajAscii;
        public string MesajBits;

        public int KapasiteBits;
        public int KapasiteBytes;


        public Stegano(Bitmap bmp)
        {
            Orijinal = new Bitmap(bmp);
            StegoBmp = new Bitmap(bmp);
            Maske = new Bitmap(bmp);

        }

        public void MaskeYukle(Bitmap bmp1)
        {
            int i, j; Color p9;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    Maske.SetPixel(i, j, p9);
                }
            }
        }

        public int KapasiteHesapla(int rbits, int gbits, int bbits)
        {
            KapasiteBits = Orijinal.Width * Orijinal.Height * (rbits + gbits + bbits);
            KapasiteBytes = KapasiteBits / 8;
            return KapasiteBits;
        }

        public void MesajAl(string veri)
        {
            MesajAscii = veri;
            MesajAsciiKaydet();

            MesajtoBits();
            MesajBitsKaydet();
        }



        public void MesajtoBits()
        {
            string Rstr; Rstr = "";
            MesajBits = "";
            int n, d;
            char[] mesaj = MesajAscii.ToCharArray();

            for (n = 0; n < mesaj.Length; n++)
            {
                d = (int)mesaj[n];
                Rstr = Decimaltobin(d);
                MesajBits = MesajBits + Rstr;
            }
        }



        public void MesajBitstoAscii()
        {
            int i, n, t;
            string output;
            MesajAscii = "";

            char[] bits = MesajBits.ToCharArray();

            i = 0; n = 0;
            output = "";
            do
            {
                if (bits[i] == '0')
                    output = output + "0";
                if (bits[i] == '1')
                    output = output + "1";

                n = n + 1;
                if (n == 8)
                {
                    int number = int.Parse(output);
                    t = Bintodecimal(number);
                    output = "";                            //output = String.Empty;   
                    n = 0;
                    char c = Convert.ToChar(t);            // char c = (char)t;
                    string ara = Char.ToString(c);
                    // string ara= c.ToString();
                    // ara=Convert.ToString(t);
                    MesajAscii = MesajAscii + ara;
                }
                i++;
            } while (i < bits.Length);

        }

        public void MesajAsciiKaydet()
        {
            FileStream fs2 = new FileStream("e:\\umut\\TEZ\\Mesaj2.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya2 = new StreamWriter(fs2);
            dosya2.Write(MesajAscii);
            dosya2.Close();
        }

        public void MesajBitsKaydet()
        {
            FileStream fs = new FileStream("e:\\umut\\TEZ\\Mesaj1.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);
            dosya.Write(MesajBits);
            dosya.Close();
        }



        public string Decimaltobin(int a)
        {
            string sonuc = string.Empty;
            string tampon = string.Empty;
            int n, t = 0; n = a;
            for (int i = 0; n > 0; i++)
            {
                sonuc = n % 2 + sonuc;
                n = n / 2;
            }

            if (sonuc.Length < 8)
                t = 8 - sonuc.Length;

            for (int i = 0; i < t; i++)
            { tampon = tampon + '0'; }
            sonuc = tampon + sonuc;


            if (a == 0) sonuc = "00000000";
            return sonuc;
        }

        public int Bintodecimal(int num)
        {
            int b, d = 0, taban = 1, rem;

            b = num;
            while (num > 0)
            {
                rem = num % 10;
                d = d + rem * taban;
                num = num / 10;
                taban = taban * 2;
            }

            return d;
        }

        public int Bintodecimal2(int n)
        {
            int dec = 0, i = 0, rem;
            while (n != 0)
            {
                rem = n % 10;
                n /= 10;
                dec = dec + rem * (int)(Math.Pow(2, i));
                ++i;
            }
            return dec;
        }
        public Bitmap LSBmap(int kanal)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            string Rstr = string.Empty;
            string Gstr = string.Empty;
            string Bstr = string.Empty;
            string output = string.Empty;
            Color p9, c2;
            int x, y;

            c2 = Color.FromArgb(0, 0, 0);

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y);
                    Rstr = Decimaltobin(p9.R);
                    Gstr = Decimaltobin(p9.G);
                    Bstr = Decimaltobin(p9.B);
                    char[] Ra = Rstr.ToCharArray();
                    char[] Ga = Gstr.ToCharArray();
                    char[] Ba = Bstr.ToCharArray();

                    if (kanal == 1)
                    {
                        if (Ra[7] == '0')
                            c2 = Color.FromArgb(0, 0, 0);
                        if (Ra[7] == '1')
                            c2 = Color.FromArgb(255, 255, 255);
                    }
                    else if (kanal == 2)
                    {
                        if (Ga[7] == '0')
                            c2 = Color.FromArgb(0, 0, 0);
                        if (Ga[7] == '1')
                            c2 = Color.FromArgb(255, 255, 255);
                    }
                    else if (kanal == 3)
                    {
                        if (Ba[7] == '0')
                            c2 = Color.FromArgb(0, 0, 0);
                        if (Ba[7] == '1')
                            c2 = Color.FromArgb(255, 255, 255);
                    }
                    else
                        c2 = Color.FromArgb(0, 0, 0);


                    bmp2.SetPixel(x, y, c2);
                }
            }
            return bmp2;
        }


        public Bitmap TextSakla100(int kanal)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone(); Color p9, c2;
            string Rstr, Gstr, Bstr; Rstr = ""; Gstr = ""; Bstr = "";
            int x, y, n, q1, q2, q3;
            c2 = Color.FromArgb(0, 0, 0);

            char[] bits = MesajBits.ToCharArray();
            char ch; n = 0;

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    ch = bits[n];

                    p9 = bmp1.GetPixel(x, y);

                    Rstr = Decimaltobin(p9.R);
                    Gstr = Decimaltobin(p9.G);
                    Bstr = Decimaltobin(p9.B);

                    char[] Ra = Rstr.ToCharArray();
                    char[] Ga = Gstr.ToCharArray();
                    char[] Ba = Bstr.ToCharArray();

                    if (kanal == 1)
                    {
                        if (ch == '0')
                            Ra[7] = '0';
                        if (ch == '1')
                            Ra[7] = '1';
                    }
                    else if (kanal == 2)
                    {
                        if (ch == '0')
                            Ga[7] = '0';
                        if (ch == '1')
                            Ga[7] = '1';
                    }
                    else if (kanal == 3)
                    {
                        if (ch == '0')
                            Ba[7] = '0';
                        if (ch == '1')
                            Ba[7] = '1';
                    }
                    else
                        c2 = Color.FromArgb(0, 0, 0);


                    string bilR = new string(Ra);
                    string bilG = new string(Ga);
                    string bilB = new string(Ba);


                    q1 = int.Parse(bilR);
                    q2 = int.Parse(bilG);
                    q3 = int.Parse(bilB);

                    q1 = Bintodecimal(q1);
                    q2 = Bintodecimal(q2);
                    q3 = Bintodecimal(q3);

                    c2 = Color.FromArgb(q1, q2, q3);
                    bmp2.SetPixel(x, y, c2);
                    StegoBmp.SetPixel(x, y, c2);

                    n = n + 1;
                    if (n >= (bits.Length - 1))
                    { n = bits.Length - 1; }

                }
            }
            return bmp2;
        }






        public void TextCikar100(int kanal)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone(); Color p9;

            string Rstr, Gstr, Bstr; Rstr = ""; Gstr = ""; Bstr = "";
            int x, y;

            MesajBits = "";

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {

                    p9 = bmp1.GetPixel(x, y);

                    Rstr = Decimaltobin(p9.R);
                    Gstr = Decimaltobin(p9.G);
                    Bstr = Decimaltobin(p9.B);

                    char[] Ra = Rstr.ToCharArray();
                    char[] Ga = Gstr.ToCharArray();
                    char[] Ba = Bstr.ToCharArray();

                    if (kanal == 1)
                    {
                        if (Ra[7] == '0')
                            MesajBits = MesajBits + "0";
                        if (Ra[7] == '1')
                            MesajBits = MesajBits + "1";
                    }
                    else if (kanal == 2)
                    {
                        if (Ga[7] == '0')
                            MesajBits = MesajBits + "0";
                        if (Ga[7] == '1')
                            MesajBits = MesajBits + "1";
                    }
                    else if (kanal == 3)
                    {
                        if (Ba[7] == '0')
                            MesajBits = MesajBits + "0";
                        if (Ba[7] == '1')
                            MesajBits = MesajBits + "1";
                    }
                    else
                        MesajBits = MesajBits + "9";

                }
            }

        }




        public Bitmap TextSakla111()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone(); Color p9, c2;
            string Rstr, Gstr, Bstr; Rstr = ""; Gstr = ""; Bstr = "";
            int x, y, n, q1, q2, q3;
            c2 = Color.FromArgb(0, 0, 0);

            char[] bits = MesajBits.ToCharArray();
            char ch; n = 0;

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y);
                    Rstr = Decimaltobin(p9.R);
                    Gstr = Decimaltobin(p9.G);
                    Bstr = Decimaltobin(p9.B);
                    char[] Ra = Rstr.ToCharArray();
                    char[] Ga = Gstr.ToCharArray();
                    char[] Ba = Bstr.ToCharArray();


                    ch = bits[n];
                    if (ch == '0')
                        Ra[7] = '0';
                    if (ch == '1')
                        Ra[7] = '1';


                    n = n + 1;
                    if (n >= (bits.Length - 1))
                    { n = bits.Length - 1; }


                    ch = bits[n];
                    if (ch == '0')
                        Ga[7] = '0';
                    else if (ch == '1')
                        Ga[7] = '1';


                    n = n + 1;
                    if (n >= (bits.Length - 1))
                    { n = bits.Length - 1; }

                    ch = bits[n];
                    if (ch == '0')
                        Ba[7] = '0';
                    else if (ch == '1')
                        Ba[7] = '1';


                    n = n + 1;
                    if (n >= (bits.Length - 1))
                    { n = bits.Length - 1; }


                    string bilR = new string(Ra);
                    string bilG = new string(Ga);
                    string bilB = new string(Ba);


                    q1 = int.Parse(bilR);
                    q2 = int.Parse(bilG);
                    q3 = int.Parse(bilB);

                    q1 = Bintodecimal(q1);
                    q2 = Bintodecimal(q2);
                    q3 = Bintodecimal(q3);

                    c2 = Color.FromArgb(q1, q2, q3);
                    bmp2.SetPixel(x, y, c2);
                    StegoBmp.SetPixel(x, y, c2);
                }
            }
            return bmp2;
        }




        public void TextCikar111()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone(); Color p9;

            string Rstr, Gstr, Bstr; Rstr = ""; Gstr = ""; Bstr = "";
            int x, y;

            MesajBits = "";

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y);

                    Rstr = Decimaltobin(p9.R);
                    Gstr = Decimaltobin(p9.G);
                    Bstr = Decimaltobin(p9.B);
                    char[] Ra = Rstr.ToCharArray();
                    char[] Ga = Gstr.ToCharArray();
                    char[] Ba = Bstr.ToCharArray();


                    if (Ra[7] == '0')
                        MesajBits = MesajBits + "0";
                    else if (Ra[7] == '1')
                        MesajBits = MesajBits + "1";

                    if (Ga[7] == '0')
                        MesajBits = MesajBits + "0";
                    else if (Ga[7] == '1')
                        MesajBits = MesajBits + "1";

                    if (Ba[7] == '0')
                        MesajBits = MesajBits + "0";
                    else if (Ba[7] == '1')
                        MesajBits = MesajBits + "1";
                }
            }

        }





        public Bitmap TextSakla332()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone(); Color p9, c2;
            string Rstr, Gstr, Bstr; Rstr = ""; Gstr = ""; Bstr = "";
            int x, y, n, q1, q2, q3;
            c2 = Color.FromArgb(0, 0, 0);

            char[] bits = MesajBits.ToCharArray();
            char ch;

            n = 0;

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y);

                    Rstr = Decimaltobin(p9.R);
                    Gstr = Decimaltobin(p9.G);
                    Bstr = Decimaltobin(p9.B);

                    char[] Ra = Rstr.ToCharArray();
                    char[] Ga = Gstr.ToCharArray();
                    char[] Ba = Bstr.ToCharArray();



                    ch = bits[n];
                    if (ch == '0')
                        Ra[7] = '0';
                    if (ch == '1')
                        Ra[7] = '1';

                    n = n + 1;
                    if (n >= (bits.Length - 1))
                    { n = bits.Length - 1; }

                    ch = bits[n];
                    if (ch == '0')
                        Ra[6] = '0';
                    else if (ch == '1')
                        Ra[6] = '1';

                    n = n + 1;
                    if (n >= (bits.Length - 1))
                    { n = bits.Length - 1; }

                    ch = bits[n];
                    if (ch == '0')
                        Ra[5] = '0';
                    else if (ch == '1')
                        Ra[5] = '1';




                    n = n + 1;
                    if (n >= (bits.Length - 1))
                    { n = bits.Length - 1; }


                    ch = bits[n];
                    if (ch == '0')
                        Ga[7] = '0';
                    else if (ch == '1')
                        Ga[7] = '1';

                    n = n + 1;
                    if (n >= (bits.Length - 1))
                    { n = bits.Length - 1; }

                    ch = bits[n];
                    if (ch == '0')
                        Ga[6] = '0';
                    else if (ch == '1')
                        Ga[6] = '1';


                    n = n + 1;
                    if (n >= (bits.Length - 1))
                    { n = bits.Length - 1; }

                    ch = bits[n];
                    if (ch == '0')
                        Ga[5] = '0';
                    else if (ch == '1')
                        Ga[5] = '1';


                    n = n + 1;
                    if (n >= (bits.Length - 1))
                    { n = bits.Length - 1; }

                    ch = bits[n];
                    if (ch == '0')
                        Ba[7] = '0';
                    else if (ch == '1')
                        Ba[7] = '1';


                    n = n + 1;
                    if (n >= (bits.Length - 1))
                    { n = bits.Length - 1; }

                    ch = bits[n];
                    if (ch == '0')
                        Ba[6] = '0';
                    else if (ch == '1')
                        Ba[6] = '1';


                    n = n + 1;
                    if (n >= (bits.Length - 1))
                    { n = bits.Length - 1; }


                    string bilR = new string(Ra);
                    string bilG = new string(Ga);
                    string bilB = new string(Ba);


                    q1 = int.Parse(bilR);
                    q2 = int.Parse(bilG);
                    q3 = int.Parse(bilB);

                    q1 = Bintodecimal(q1);
                    q2 = Bintodecimal(q2);
                    q3 = Bintodecimal(q3);

                    c2 = Color.FromArgb(q1, q2, q3);
                    bmp2.SetPixel(x, y, c2);
                    StegoBmp.SetPixel(x, y, c2);

                }
            }

            return bmp2;
        }





        public void TextCikar332()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone(); Color p9;

            string Rstr, Gstr, Bstr; Rstr = ""; Gstr = ""; Bstr = "";
            int x, y;

            MesajBits = "";

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y);
                    Rstr = Decimaltobin(p9.R);
                    Gstr = Decimaltobin(p9.G);
                    Bstr = Decimaltobin(p9.B);

                    char[] Ra = Rstr.ToCharArray();
                    char[] Ga = Gstr.ToCharArray();
                    char[] Ba = Bstr.ToCharArray();



                    if (Ra[7] == '0')
                        MesajBits = MesajBits + "0";
                    else if (Ra[7] == '1')
                        MesajBits = MesajBits + "1";

                    if (Ra[6] == '0')
                        MesajBits = MesajBits + "0";
                    else if (Ra[6] == '1')
                        MesajBits = MesajBits + "1";

                    if (Ra[5] == '0')
                        MesajBits = MesajBits + "0";
                    else if (Ra[5] == '1')
                        MesajBits = MesajBits + "1";



                    if (Ga[7] == '0')
                        MesajBits = MesajBits + "0";
                    else if (Ga[7] == '1')
                        MesajBits = MesajBits + "1";

                    if (Ga[6] == '0')
                        MesajBits = MesajBits + "0";
                    else if (Ga[6] == '1')
                        MesajBits = MesajBits + "1";

                    if (Ga[5] == '0')
                        MesajBits = MesajBits + "0";
                    else if (Ga[5] == '1')
                        MesajBits = MesajBits + "1";


                    if (Ba[7] == '0')
                        MesajBits = MesajBits + "0";
                    else if (Ba[7] == '1')
                        MesajBits = MesajBits + "1";

                    if (Ba[6] == '0')
                        MesajBits = MesajBits + "0";
                    else if (Ba[6] == '1')
                        MesajBits = MesajBits + "1";


                }
            }


        }




        public void FileTextToBinary()
        {
            int n;
            string Rstr; Rstr = "";

            StreamReader sr = new StreamReader("e:\\umut\\TEZ\\Mesaj2.txt");
            FileStream fs = new FileStream("e:\\umut\\TEZ\\Mesaj1.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);

            n = sr.Read();
            while (n != -1)
            {
                Rstr = Decimaltobin(n);
                dosya.Write(Rstr);
                n = sr.Read();
            }
            sr.Close();
            dosya.Close();
        }


        public void FileBinaryToAscii()
        {
            int n, x, t; char ch;
            string output = ""; output = "";

            StreamReader sr = new StreamReader("e:\\umut\\TEZ\\Mesaj1.txt");
            FileStream fs = new FileStream("e:\\umut\\TEZ\\Mesaj2.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);

            n = sr.Read(); ch = (char)n; x = 1;

            if (ch == '0')
                output = output + '0';
            if (ch == '1')
                output = output + '1';

            while (n != -1)
            {
                if (x == 8)
                {
                    int number = int.Parse(output);
                    t = Bintodecimal(number);
                    output = String.Empty; x = 0;
                    char c = Convert.ToChar(t);
                    string ara = Char.ToString(c);
                    dosya.Write(ara);
                }


                n = sr.Read(); ch = (char)n;

                if (ch == '0')
                    output = output + '0';
                if (ch == '1')
                    output = output + '1';
                x = x + 1;
            }
            sr.Close();
            dosya.Close();
        }


        public Bitmap FileTextSakla100(int kanal)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone(); Color p9, c2;

            string Rstr, Gstr, Bstr; Rstr = ""; Gstr = ""; Bstr = "";

            int x, y, n, d, q1, q2, q3;
            c2 = Color.FromArgb(0, 0, 0);


            FileTextToBinary();

            StreamReader sr = new StreamReader("e:\\umut\\TEZ\\Mesaj1.txt");
            char ch;
            /*
            n = sr.Read(); 
            while (n != -1)
            {
                ch = (char)n;
                //do stuff here
                n = sr.Read();               
            }
            */

            n = sr.Read();
            ch = (char)n;

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {

                    p9 = bmp1.GetPixel(x, y);

                    Rstr = Decimaltobin(p9.R);
                    Gstr = Decimaltobin(p9.G);
                    Bstr = Decimaltobin(p9.B);

                    char[] Ra = Rstr.ToCharArray();
                    char[] Ga = Gstr.ToCharArray();
                    char[] Ba = Bstr.ToCharArray();

                    if (kanal == 1)
                    {

                        if (ch == '0')
                            Ra[7] = '0';
                        if (ch == '1')
                            Ra[7] = '1';
                    }
                    else if (kanal == 2)
                    {
                        if (ch == '0')
                            Ga[7] = '0';
                        if (ch == '1')
                            Ga[7] = '1';
                    }
                    else if (kanal == 3)
                    {
                        if (ch == '0')
                            Ba[7] = '0';
                        if (ch == '1')
                            Ba[7] = '1';
                    }
                    else
                        c2 = Color.FromArgb(0, 0, 0);


                    string bilR = new string(Ra);
                    string bilG = new string(Ga);
                    string bilB = new string(Ba);


                    q1 = int.Parse(bilR);               // q1=Convert.ToInt32(Rstr);
                    q2 = int.Parse(bilG);
                    q3 = int.Parse(bilB);

                    q1 = Bintodecimal(q1);
                    q2 = Bintodecimal(q2);
                    q3 = Bintodecimal(q3);

                    c2 = Color.FromArgb(q1, q2, q3);
                    bmp2.SetPixel(x, y, c2);
                    StegoBmp.SetPixel(x, y, c2);

                    n = sr.Read();
                    if (n == -1)
                    { //sr.Close();
                        n = 0;
                    }
                    ch = (char)n;
                }
            }
            sr.Close();
            return bmp2;
        }


        public void FileTextCikar100(int kanal)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone(); Color p9;
            string Rstr, Gstr, Bstr, output;
            Rstr = ""; Gstr = ""; Bstr = "";
            int x, y;

            FileStream fs = new FileStream("e:\\umut\\TEZ\\Mesaj1.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);

            output = "";

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {

                    p9 = bmp1.GetPixel(x, y);

                    Rstr = Decimaltobin(p9.R);
                    Gstr = Decimaltobin(p9.G);
                    Bstr = Decimaltobin(p9.B);

                    char[] Ra = Rstr.ToCharArray();
                    char[] Ga = Gstr.ToCharArray();
                    char[] Ba = Bstr.ToCharArray();

                    if (kanal == 1)
                    {
                        if (Ra[7] == '0')
                            output = output + "0";
                        if (Ra[7] == '1')
                            output = output + "1";
                    }
                    else if (kanal == 2)
                    {
                        if (Ga[7] == '0')
                            output = output + "0";
                        if (Ga[7] == '1')
                            output = output + "1";
                    }
                    else if (kanal == 3)
                    {
                        if (Ba[7] == '0')
                            output = output + "0";
                        if (Ba[7] == '1')
                            output = output + "1";
                    }
                    else
                        output = "9999";

                    dosya.Write(output);
                    output = "";              // output = String.Empty; 
                }
            }

            dosya.Close();

            FileBinaryToAscii();
        }




        public Bitmap FileTextSakla111()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone(); Color p9, c2;
            string Rstr, Gstr, Bstr;
            Rstr = ""; Gstr = ""; Bstr = "";
            int x, y, n, d, q1, q2, q3;
            c2 = Color.FromArgb(0, 0, 0);

            FileTextToBinary();

            StreamReader sr = new StreamReader("e:\\umut\\TEZ\\Mesaj1.txt");
            char ch;

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y);
                    Rstr = Decimaltobin(p9.R);
                    Gstr = Decimaltobin(p9.G);
                    Bstr = Decimaltobin(p9.B);
                    char[] Ra = Rstr.ToCharArray();
                    char[] Ga = Gstr.ToCharArray();
                    char[] Ba = Bstr.ToCharArray();

                    n = sr.Read();
                    ch = (char)n;
                    if (n == -1)
                    { n = 0; }
                    ch = (char)n;

                    if (ch == '0')
                        Ra[7] = '0';
                    if (ch == '1')
                        Ra[7] = '1';

                    n = sr.Read();
                    ch = (char)n;
                    if (n == -1)
                    { n = 0; }
                    ch = (char)n;

                    if (ch == '0')
                        Ga[7] = '0';
                    else if (ch == '1')
                        Ga[7] = '1';

                    n = sr.Read();
                    ch = (char)n;
                    if (n == -1)
                    { n = 0; }
                    ch = (char)n;

                    if (ch == '0')
                        Ba[7] = '0';
                    else if (ch == '1')
                        Ba[7] = '1';

                    string bilR = new string(Ra);
                    string bilG = new string(Ga);
                    string bilB = new string(Ba);


                    q1 = int.Parse(bilR);
                    q2 = int.Parse(bilG);
                    q3 = int.Parse(bilB);

                    q1 = Bintodecimal(q1);
                    q2 = Bintodecimal(q2);
                    q3 = Bintodecimal(q3);

                    c2 = Color.FromArgb(q1, q2, q3);
                    bmp2.SetPixel(x, y, c2);
                    StegoBmp.SetPixel(x, y, c2);

                }
            }

            sr.Close();
            return bmp2;
        }

        public void FileTextCikar111()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone(); Color p9;
            string Rstr, Gstr, Bstr, output;
            Rstr = ""; Gstr = ""; Bstr = "";
            int x, y;

            FileStream fs = new FileStream("e:\\umut\\TEZ\\Mesaj1.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);

            output = "";

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y);
                    Rstr = Decimaltobin(p9.R);
                    Gstr = Decimaltobin(p9.G);
                    Bstr = Decimaltobin(p9.B);
                    char[] Ra = Rstr.ToCharArray();
                    char[] Ga = Gstr.ToCharArray();
                    char[] Ba = Bstr.ToCharArray();


                    if (Ra[7] == '0')
                        output = output + "0";
                    else if (Ra[7] == '1')
                        output = output + "1";


                    if (Ga[7] == '0')
                        output = output + "0";
                    else if (Ga[7] == '1')
                        output = output + "1";


                    if (Ba[7] == '0')
                        output = output + "0";
                    else if (Ba[7] == '1')
                        output = output + "1";


                    dosya.Write(output);
                    output = "";              // output = String.Empty; 

                }
            }

            dosya.Close();
            FileBinaryToAscii();
        }





        public Bitmap FileTextSakla332()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone(); Color p9, c2;
            string Rstr, Gstr, Bstr;
            Rstr = ""; Gstr = ""; Bstr = "";
            int x, y, n, d, q1, q2, q3;
            c2 = Color.FromArgb(0, 0, 0);

            FileTextToBinary();

            StreamReader sr = new StreamReader("e:\\umut\\TEZ\\Mesaj1.txt");
            char ch;

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y);
                    Rstr = Decimaltobin(p9.R);
                    Gstr = Decimaltobin(p9.G);
                    Bstr = Decimaltobin(p9.B);
                    char[] Ra = Rstr.ToCharArray();
                    char[] Ga = Gstr.ToCharArray();
                    char[] Ba = Bstr.ToCharArray();

                    n = sr.Read();
                    ch = (char)n;
                    if (n == -1)
                    { n = 0; }
                    ch = (char)n;

                    if (ch == '0')
                        Ra[7] = '0';
                    if (ch == '1')
                        Ra[7] = '1';

                    n = sr.Read();
                    ch = (char)n;
                    if (n == -1)
                    { n = 0; }
                    ch = (char)n;

                    if (ch == '0')
                        Ra[6] = '0';
                    if (ch == '1')
                        Ra[6] = '1';


                    n = sr.Read();
                    ch = (char)n;
                    if (n == -1)
                    { n = 0; }
                    ch = (char)n;

                    if (ch == '0')
                        Ra[5] = '0';
                    if (ch == '1')
                        Ra[5] = '1';


                    n = sr.Read();
                    ch = (char)n;
                    if (n == -1)
                    { n = 0; }
                    ch = (char)n;

                    if (ch == '0')
                        Ga[7] = '0';
                    else if (ch == '1')
                        Ga[7] = '1';


                    n = sr.Read();
                    ch = (char)n;
                    if (n == -1)
                    { n = 0; }
                    ch = (char)n;

                    if (ch == '0')
                        Ga[6] = '0';
                    else if (ch == '1')
                        Ga[6] = '1';

                    n = sr.Read();
                    ch = (char)n;
                    if (n == -1)
                    { n = 0; }
                    ch = (char)n;

                    if (ch == '0')
                        Ga[5] = '0';
                    else if (ch == '1')
                        Ga[5] = '1';


                    n = sr.Read();
                    ch = (char)n;
                    if (n == -1)
                    { n = 0; }
                    ch = (char)n;

                    if (ch == '0')
                        Ba[7] = '0';
                    else if (ch == '1')
                        Ba[7] = '1';

                    n = sr.Read();
                    ch = (char)n;
                    if (n == -1)
                    { n = 0; }
                    ch = (char)n;

                    if (ch == '0')
                        Ba[6] = '0';
                    else if (ch == '1')
                        Ba[6] = '1';



                    string bilR = new string(Ra);
                    string bilG = new string(Ga);
                    string bilB = new string(Ba);


                    q1 = int.Parse(bilR);
                    q2 = int.Parse(bilG);
                    q3 = int.Parse(bilB);

                    q1 = Bintodecimal(q1);
                    q2 = Bintodecimal(q2);
                    q3 = Bintodecimal(q3);

                    c2 = Color.FromArgb(q1, q2, q3);
                    bmp2.SetPixel(x, y, c2);
                    StegoBmp.SetPixel(x, y, c2);

                }
            }

            sr.Close();
            return bmp2;
        }




        public void FileTextCikar332()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone(); Color p9;
            string Rstr, Gstr, Bstr, output;
            Rstr = ""; Gstr = ""; Bstr = "";
            int x, y;

            FileStream fs = new FileStream("e:\\umut\\TEZ\\Mesaj1.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);

            output = "";

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    p9 = bmp1.GetPixel(x, y);
                    Rstr = Decimaltobin(p9.R);
                    Gstr = Decimaltobin(p9.G);
                    Bstr = Decimaltobin(p9.B);
                    char[] Ra = Rstr.ToCharArray();
                    char[] Ga = Gstr.ToCharArray();
                    char[] Ba = Bstr.ToCharArray();


                    if (Ra[7] == '0')
                        output = output + "0";
                    else if (Ra[7] == '1')
                        output = output + "1";

                    if (Ra[6] == '0')
                        output = output + "0";
                    else if (Ra[6] == '1')
                        output = output + "1";

                    if (Ra[5] == '0')
                        output = output + "0";
                    else if (Ra[5] == '1')
                        output = output + "1";



                    if (Ga[7] == '0')
                        output = output + "0";
                    else if (Ga[7] == '1')
                        output = output + "1";

                    if (Ga[6] == '0')
                        output = output + "0";
                    else if (Ga[6] == '1')
                        output = output + "1";

                    if (Ga[5] == '0')
                        output = output + "0";
                    else if (Ga[5] == '1')
                        output = output + "1";




                    if (Ba[7] == '0')
                        output = output + "0";
                    else if (Ba[7] == '1')
                        output = output + "1";

                    if (Ba[6] == '0')
                        output = output + "0";
                    else if (Ba[6] == '1')
                        output = output + "1";


                    dosya.Write(output);
                    output = "";              // output = String.Empty; 

                }
            }
            dosya.Close();

            FileBinaryToAscii();
        }










        public Bitmap GrayCikar332()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap mesaj = (Bitmap)Maske.Clone(); Color p9, c2;

            string Rstr, Gstr, Bstr, output;
            Rstr = ""; Gstr = ""; Bstr = ""; output = "";
            int x, y, q1;
            c2 = Color.FromArgb(0, 0, 0);

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {


                    p9 = bmp1.GetPixel(x, y);
                    Rstr = Decimaltobin(p9.R);
                    Gstr = Decimaltobin(p9.G);
                    Bstr = Decimaltobin(p9.B);


                    char[] Ra = Rstr.ToCharArray();
                    char[] Ga = Gstr.ToCharArray();
                    char[] Ba = Bstr.ToCharArray();

                    output = String.Empty;

                    if (Ra[7] == '0')
                        output = output + "0";
                    else if (Ra[7] == '1')
                        output = output + "1";

                    if (Ra[6] == '0')
                        output = output + "0";
                    else if (Ra[6] == '1')
                        output = output + "1";

                    if (Ra[5] == '0')
                        output = output + "0";
                    else if (Ra[5] == '1')
                        output = output + "1";


                    if (Ga[7] == '0')
                        output = output + "0";
                    else if (Ga[7] == '1')
                        output = output + "1";

                    if (Ga[6] == '0')
                        output = output + "0";
                    else if (Ga[6] == '1')
                        output = output + "1";

                    if (Ga[5] == '0')
                        output = output + "0";
                    else if (Ga[5] == '1')
                        output = output + "1";


                    if (Ba[7] == '0')
                        output = output + "0";
                    else if (Ba[7] == '1')
                        output = output + "1";

                    if (Ba[6] == '0')
                        output = output + "0";
                    else if (Ba[6] == '1')
                        output = output + "1";

                    int number = int.Parse(output);
                    q1 = Bintodecimal(number);

                    c2 = Color.FromArgb(q1, q1, q1);
                    mesaj.SetPixel(x, y, c2);
                    Maske.SetPixel(x, y, c2);
                }
            }

            return mesaj;
        }

        public Bitmap GraySakla332()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();
            Bitmap mesaj = (Bitmap)Maske.Clone();

            Color p9, c1, c2;
            string Rstr, Gstr, Bstr, veri;
            Rstr = ""; Gstr = ""; Bstr = ""; veri = "";
            int x, y, q1, q2, q3;
            c2 = Color.FromArgb(0, 0, 0);

            for (x = 0; x < bmp1.Width; x++)
            {
                for (y = 0; y < bmp1.Height; y++)
                {
                    c1 = mesaj.GetPixel(x, y);
                    veri = Decimaltobin(c1.R);
                    char[] Ve = veri.ToCharArray();

                    p9 = bmp1.GetPixel(x, y);
                    Rstr = Decimaltobin(p9.R);
                    Gstr = Decimaltobin(p9.G);
                    Bstr = Decimaltobin(p9.B);


                    char[] Ra = Rstr.ToCharArray();
                    char[] Ga = Gstr.ToCharArray();
                    char[] Ba = Bstr.ToCharArray();



                    if (Ve[0] == '0')
                        Ra[7] = '0';
                    if (Ve[0] == '1')
                        Ra[7] = '1';

                    if (Ve[1] == '0')
                        Ra[6] = '0';
                    if (Ve[1] == '1')
                        Ra[6] = '1';

                    if (Ve[2] == '0')
                        Ra[5] = '0';
                    if (Ve[2] == '1')
                        Ra[5] = '1';

                    if (Ve[3] == '0')
                        Ga[7] = '0';
                    if (Ve[3] == '1')
                        Ga[7] = '1';

                    if (Ve[4] == '0')
                        Ga[6] = '0';
                    if (Ve[4] == '1')
                        Ga[6] = '1';

                    if (Ve[5] == '0')
                        Ga[5] = '0';
                    if (Ve[5] == '1')
                        Ga[5] = '1';

                    if (Ve[6] == '0')
                        Ba[7] = '0';
                    if (Ve[6] == '1')
                        Ba[7] = '1';

                    if (Ve[7] == '0')
                        Ba[6] = '0';
                    if (Ve[7] == '1')
                        Ba[6] = '1';


                    string bilR = new string(Ra);
                    string bilG = new string(Ga);
                    string bilB = new string(Ba);


                    q1 = int.Parse(bilR);
                    q2 = int.Parse(bilG);
                    q3 = int.Parse(bilB);

                    q1 = Bintodecimal(q1);
                    q2 = Bintodecimal(q2);
                    q3 = Bintodecimal(q3);

                    c2 = Color.FromArgb(q1, q2, q3);
                    bmp2.SetPixel(x, y, c2);
                    StegoBmp.SetPixel(x, y, c2);
                }
            }

            return bmp2;
        }




    }
}
