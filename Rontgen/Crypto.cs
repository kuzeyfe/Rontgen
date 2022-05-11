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
using System.Windows.Forms;

namespace Rontgen
{
    public class Crypto
    {
        private Bitmap Orijinal;
        private Bitmap Maske;
        private Bitmap EncrptBmp;
        private Bitmap DecrptBmp;

        private int m, N;
        public int[] Sayilar;
        public int[] fi;
        public double[] pi;

        Random xr = new Random();

        public string X0bilgiString;
        public string X0bilgiHex;
        public string X0bilgiBinary;
        public double X0bilgiDouble;

        public string MubilgiString;
        public string MubilgiHex;
        public string MubilgiBinary;
        public double MubilgiDouble;


        public string abilgiString;
        public string abilgiHex;
        public string abilgiBinary;
        public double abilgiDouble;

        public string Y1bilgiHex;
        public string Y1bilgiBinary;
        public ulong Y1bilgiUlong;

        public string Y2bilgiHex;
        public string Y2bilgiBinary;
        public ulong Y2bilgiUlong;



        public string KeyFinal;


        public Crypto(Bitmap bmp, int Diziboyutu, int kbits)
        {
            Orijinal = new Bitmap(bmp);
            Maske = new Bitmap(bmp);
            EncrptBmp = new Bitmap(bmp);
            DecrptBmp = new Bitmap(bmp);

            N = Diziboyutu; m = (int)Math.Pow(2, (double)(kbits));


            int i;
            Sayilar = new int[N];
            fi = new int[m];
            pi = new double[m];

            for (i = 0; i < N; i++)
            { Sayilar[i] = 0; }

            for (i = 0; i < m; i++)
            { fi[i] = 0; pi[i] = 0.0; }



            X0bilgiString = "";
            X0bilgiHex = "";
            X0bilgiBinary = "";
            X0bilgiDouble = 0.0;

            MubilgiString = "";
            MubilgiHex = "";
            MubilgiBinary = "";
            MubilgiDouble = 3.56;


            abilgiString = "";
            abilgiHex = "";
            abilgiBinary = "";
            abilgiDouble = 0.0;

            Y1bilgiHex = "";
            Y1bilgiBinary = "";
            Y1bilgiUlong = 0;

            Y2bilgiHex = "";
            Y2bilgiBinary = "";
            Y2bilgiUlong = 0;


            KeyFinal = "";


            X0stringUreteci2();
            MustringUreteci2();


            X0stringUreteci2();
            abilgiDouble = X0bilgiDouble;
            abilgiHex = X0bilgiHex;

            Y0stringUreteci1();
            Y0stringUreteci2();


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

        public Color ColorCaesarEnc(Color p9, int key)
        {
            int q1, q2, q3; Color c1;
            q1 = (p9.R + key) % 255;
            q2 = (p9.G + key) % 255;
            q3 = (p9.B + key) % 255;
            c1 = Color.FromArgb((int)q1, (int)q2, (int)q3);
            return c1;
        }


        public Color ColorCaesarDcp(Color p9, int key)
        {
            int q1, q2, q3, d; Color c1;

            d = p9.R - key;
            if (d > 0)
            { q1 = (p9.R - key) % 255; }
            else
            {
                q1 = ((-1) * (p9.R - key)) % 255;
                q1 = 255 - q1;
            }

            d = p9.G - key;
            if (d > 0)
            { q2 = (p9.G - key) % 255; }
            else
            {
                q2 = ((-1) * (p9.G - key)) % 255;
                q2 = 255 - q2;
            }

            d = p9.B - key;
            if (d > 0)
            { q3 = (p9.B - key) % 255; }
            else
            {
                q3 = ((-1) * (p9.B - key)) % 255;
                q3 = 255 - q3;
            }

            c1 = Color.FromArgb((int)q1, (int)q2, (int)q3);

            return c1;
        }

        public Color ColorEXOR(Color p9, int key)
        {
            int q1, q2, q3; Color c1;
            q1 = p9.R ^ key;
            q2 = p9.G ^ key;
            q3 = p9.B ^ key;
            c1 = Color.FromArgb((int)q1, (int)q2, (int)q3);
            return c1;
        }

        public Bitmap EncryptionCaesarMaskesiz(int key)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            int i, j; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorCaesarEnc(p9, key);
                    bmp2.SetPixel(i, j, c2);
                    EncrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap DecryptionCaesarMaskesiz(int key)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            int i, j; Color p9, c2;


            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorCaesarDcp(p9, key);
                    bmp2.SetPixel(i, j, c2);
                    DecrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }


        public Bitmap EncryptionCaesarMaskeli()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();
            Bitmap veri = (Bitmap)Maske.Clone();


            int i, j, key; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = veri.GetPixel(i, j);
                    key = p9.R;
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorCaesarEnc(p9, key);
                    bmp2.SetPixel(i, j, c2);
                    EncrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap DecryptionCaesarMaskeli()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            Bitmap veri = (Bitmap)Maske.Clone();

            int i, j, key; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = veri.GetPixel(i, j);
                    key = p9.R;
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorCaesarDcp(p9, key);
                    bmp2.SetPixel(i, j, c2);
                    DecrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap EncryptionExorMaskesiz(int key)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            int i, j; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorEXOR(p9, key);
                    bmp2.SetPixel(i, j, c2);
                    EncrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap DecryptionExorMaskesiz(int key)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            int i, j; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorEXOR(p9, key);
                    bmp2.SetPixel(i, j, c2);
                    DecrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }


        public Bitmap EncryptionExorMaskeli()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            Bitmap veri = (Bitmap)Maske.Clone();

            int i, j, key; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = veri.GetPixel(i, j);
                    key = p9.R;
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorEXOR(p9, key);
                    bmp2.SetPixel(i, j, c2);
                    EncrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap DecryptionExorMaskeli()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            Bitmap veri = (Bitmap)Maske.Clone();

            int i, j, key; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = veri.GetPixel(i, j);
                    key = p9.R;
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorEXOR(p9, key);
                    bmp2.SetPixel(i, j, c2);
                    DecrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public int GCDRecursive(int a, int b)
        {
            if (a == 0)
                return b;
            if (b == 0)
                return a;

            if (a > b)
                return GCDRecursive(a % b, b);
            else
                return GCDRecursive(a, b % a);
        }


        public int GCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            if (a == 0)
                return b;
            else
                return a;
        }

        public int EulerTotient(int n)
        {
            int i, say = 0;
            for (i = 0; i < n; i++)
            {
                if (GCD(i, n) == 1)
                { say = say + 1; }
            }
            return say;
        }
        public int ModuloTersi(int a, int m)
        {
            int i, ai = 0;
            int flag = 0;
            for (i = 0; i < m; i++)
            {
                flag = (a * i) % m;
                if (flag == 1)
                { ai = i; }
            }
            return ai;
        }

        public Color ColorAfineEnc(Color p9, int a, int b)
        {
            int q1, q2, q3, test; Color c1;
            test = GCD(a, 256);
            if (test == 1)
            {
                q1 = (a * p9.R + b) % 255;
                q2 = (a * p9.G + b) % 255;
                q3 = (a * p9.B + b) % 255;
                c1 = Color.FromArgb((int)q1, (int)q2, (int)q3);
            }
            else
            {
                c1 = Color.FromArgb(0, 0, 0);
                // c1 = p9;
            }
            return c1;
        }

        public Color ColorAfineDcp(Color p9, int a, int b)
        {
            int q1, q2, q3, d, at; Color c1;

            at = ModuloTersi(a, 255);

            if (at != 0)
            {
                d = p9.R - b;
                if (d > 0)
                { q1 = (at * (p9.R - b)) % 255; }
                else
                {
                    q1 = (at * (-1) * (p9.R - b)) % 255;
                    q1 = 255 - q1;
                }

                d = p9.G - b;
                if (d > 0)
                { q2 = (at * (p9.G - b)) % 255; }
                else
                {
                    q2 = (at * (-1) * (p9.G - b)) % 255;
                    q2 = 255 - q2;
                }

                d = p9.B - b;
                if (d > 0)
                { q3 = (at * (p9.B - b)) % 255; }
                else
                {
                    q3 = (at * (-1) * (p9.B - b)) % 255;
                    q3 = 255 - q3;
                }

                c1 = Color.FromArgb((int)q1, (int)q2, (int)q3);
            }
            else
            {
                c1 = Color.FromArgb(0, 0, 0);
                // c1 = p9;
            }
            return c1;
        }

        public Bitmap EncryptionAffineMaskesiz(int a, int b)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            int i, j; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorAfineEnc(p9, a, b);
                    bmp2.SetPixel(i, j, c2);
                    EncrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }


        public Bitmap DecryptionAffineMaskesiz(int a, int b)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            int i, j; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorAfineDcp(p9, a, b);
                    bmp2.SetPixel(i, j, c2);
                    DecrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap EncryptionAffineMaskeli(int a)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            Bitmap veri = (Bitmap)Maske.Clone();

            int i, j, b; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = veri.GetPixel(i, j);
                    b = p9.R;
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorAfineEnc(p9, a, b);
                    bmp2.SetPixel(i, j, c2);
                    EncrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }

        public Bitmap DecryptionAffineMaskeli(int a)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            Bitmap veri = (Bitmap)Maske.Clone();
            int i, j, b; Color p9, c2;

            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = veri.GetPixel(i, j);
                    b = p9.R;
                    p9 = bmp1.GetPixel(i, j);
                    c2 = ColorAfineDcp(p9, a, b);
                    bmp2.SetPixel(i, j, c2);
                    DecrptBmp.SetPixel(i, j, c2);
                }
            }
            return bmp2;
        }


        public void PRNGUreteciLcg(int a, int x0, int c)
        {
            int i, x, x1;
            x1 = x0;
            for (i = 0; i < Sayilar.Length; i++)
            {
                x = (a * x1 + c) % m;
                Sayilar[i] = x;
                x1 = x;
            }
        }



        public void DagilimHesapla()
        {
            int i, x;
            for (i = 0; i < Sayilar.Length; i++)
            {
                x = (int)Sayilar[i];
                fi[x] = fi[x] + 1;
                pi[x] = (double)fi[x] / (double)Sayilar.Length;
            }
        }

        public Bitmap MaskeUreteciLcg(int a, int x0, int c)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();
            int i, j; Color p9;
            int x, x1;
            x1 = x0;
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);

                    x = (a * x1 + c) % 255;
                    p9 = Color.FromArgb((int)x, (int)x, (int)x);
                    bmp2.SetPixel(i, j, p9);
                    Maske.SetPixel(i, j, p9);
                    x1 = x;
                }
            }
            return bmp2;
        }

        public void PRNGUreteciSin(double f0, double fs, int kbits)
        {
            double delta, y, y1, y2;
            double b0, a1, a2; int i;

            y1 = 0; y2 = 0; delta = 1.0;
            b0 = Math.Sin(2.0 * 3.14 * f0 / fs + 3.14 * 0 / 2); a1 = 2.0 * Math.Cos(2.0 * 3.14 * f0 / fs + 3.14 * 0 / 2); a2 = -1.0;

            for (i = 0; i < Sayilar.Length; i++)
            {
                if (i >= 1) delta = 0.0;
                y = a1 * y1 + a2 * y2 + b0 * delta;
                Sayilar[i] = (int)(100 * y);
                y2 = y1;
                y1 = y;
            }
        }

        public void PRNGUreteciSin1(double f0, double fs, double teta)
        {
            double delta, y, y1, y2;
            double b0, b1, a1, a2; int i;

            y1 = 0; y2 = 0; delta = 0.0;
            b0 = Math.Sin(3.14 * teta / 180.0); b1 = Math.Sin((2.0 * 3.14 * f0 / fs) - 3.14 * teta / 180.0);
            a1 = 2.0 * Math.Cos(2.0 * 3.14 * f0 / fs); a2 = -1.0;

            for (i = 0; i < Sayilar.Length; i++)
            {
                if (i == 0)
                {
                    delta = 1.0;
                    y = a1 * y1 + a2 * y2 + b0 * delta;
                }

                else if (i == 1)
                {
                    delta = 1.0;
                    y = a1 * y1 + a2 * y2 + b1 * delta;
                }
                else
                { y = a1 * y1 + a2 * y2; }

                Sayilar[i] = (int)(100 * y);
                y2 = y1;
                y1 = y;
            }
        }



        public Bitmap MaskeUreteciSin(double f0, double fs)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();

            int i, j, k; Color p9;

            double delta, y, y1, y2, z;
            double b0, a1, a2;

            b0 = Math.Sin(2.0 * 3.14 * f0 / fs); a1 = 2.0 * Math.Cos(2.0 * 3.14 * f0 / fs); a2 = -1.0;
            y1 = 0; y2 = 0; delta = 1.0; k = 0;
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);

                    if (k >= 1) delta = 0.0;
                    y = a1 * y1 + a2 * y2 + b0 * delta;
                    z = (255 + 255 * y) / 2;
                    p9 = Color.FromArgb((int)z, (int)z, (int)z);
                    bmp2.SetPixel(i, j, p9);
                    Maske.SetPixel(i, j, p9);
                    y2 = y1;
                    y1 = y;
                    k++;
                }
            }
            return bmp2;
        }

        public void PRNGUreteciLogistic(double x0, double mu)
        {
            double x, x1, z; int i;
            x1 = x0;
            for (i = 0; i < Sayilar.Length; i++)
            {
                x = mu * x1 * (1 - x1);
                z = m * x;
                Sayilar[i] = (int)z;
                x1 = x;
            }
        }



        public Bitmap MaskeUreteciLogistic8(double x0, double mu)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();
            int i, j; Color p9;
            double x, x1, z;
            x1 = x0;
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    x = mu * x1 * (1 - x1);
                    z = x * 255;
                    p9 = Color.FromArgb((int)z, (int)z, (int)z);
                    bmp2.SetPixel(i, j, p9);
                    Maske.SetPixel(i, j, p9);
                    x1 = x;
                }
            }
            return bmp2;
        }


        public Bitmap MaskeUreteciLogisticMap8()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();
            int i, j; Color p9;
            double x, x1, mu, z;

            x1 = X0bilgiDouble;
            mu = MubilgiDouble;


            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    x = mu * x1 * (1 - x1);
                    z = x * 255;
                    p9 = Color.FromArgb((int)z, (int)z, (int)z);
                    bmp2.SetPixel(i, j, p9);
                    Maske.SetPixel(i, j, p9);
                    x1 = x;
                }
            }
            return bmp2;
        }



        public Bitmap MaskeUreteciChebyshev8(double x0)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();
            int i, j; Color p9;
            double x, x1, z, t;
            x1 = x0; t = 0.0;
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    x = Math.Cos(t * Math.Acos(x1));
                    z = Math.Abs(x) * 255;
                    //  z = (x+1) * 255/2;
                    p9 = Color.FromArgb((int)z, (int)z, (int)z);
                    bmp2.SetPixel(i, j, p9);
                    Maske.SetPixel(i, j, p9);
                    x1 = x;
                    t = t + 1.0;
                }
            }
            return bmp2;
        }

        public Bitmap MaskeUreteciSinMap8(double x0)
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();
            int i, j; Color p9;
            double x, x1, z, t;
            x1 = x0; t = 0.0;
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    x = 2.3 * x1 * x1 * Math.Sin(3.14 * x1);
                    z = (x - 0.45) * 255 * 2;
                    p9 = Color.FromArgb((int)z, (int)z, (int)z);
                    bmp2.SetPixel(i, j, p9);
                    Maske.SetPixel(i, j, p9);
                    x1 = x;
                    t = t + 1;
                }
            }
            return bmp2;
        }



        public void PRNGUreteciExor1(int x0, int s)
        {
            int i, x, x1;
            x1 = x0;
            for (i = 0; i < Sayilar.Length; i++)
            {
                x = x1 ^ (x1 << s);
                x = x & 255;
                Sayilar[i] = x;
                x1 = x;
            }
        }







        public string Hex2binary(string hexvalue)
        {
            string binaryval = "";
            binaryval = Convert.ToString(Convert.ToInt64(hexvalue, 16), 2);
            return binaryval;
        }
        public string Binary2hex(string binvalue)
        {
            string binaryval = "";
            binaryval = Convert.ToString(Convert.ToInt64(binvalue, 2), 16);
            return binaryval;
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



        public string Decimaltobin64(ulong a)
        {
            string sonuc = string.Empty;
            string tampon = string.Empty;
            ulong n; n = a;
            int t = 0;
            for (ulong i = 0; n > 0; i++)
            {
                sonuc = n % 2 + sonuc;
                n = n / 2;
            }

            if (sonuc.Length < 64)
                t = 64 - sonuc.Length;

            for (int k = 0; k < t; k++)
            { tampon = tampon + '0'; }
            sonuc = tampon + sonuc;


            if (a == 0) sonuc = "0000000000000000000000000000000000000000000000000000000000000000";
            return sonuc;
        }


        public string Binarytohex64(string binaryvalue)
        {
            int i, k, n;
            string output;
            string sHex = "";

            char[] keys = new char[64];

            for (k = 0; k < keys.Length; k++)
            { keys[k] = '0'; }

            char[] bits = binaryvalue.ToCharArray();

            i = keys.Length - binaryvalue.Length;

            for (k = 0; k < bits.Length; k++)
            {
                if (bits[k] == '0')
                    keys[i + k] = '0';
                if (bits[k] == '1')
                    keys[i + k] = '1';
            }

            i = 0; n = 0; output = "";
            do
            {
                if (keys[i] == '0')
                    output = output + "0";
                if (keys[i] == '1')
                    output = output + "1";

                n = n + 1;
                if (n == 4)
                {
                    string hexvalue = "";
                    hexvalue = Convert.ToString(Convert.ToInt64(output, 2), 16);
                    sHex = sHex + hexvalue;
                    output = ""; n = 0;
                }
                i++;
            } while (i < keys.Length);

            return sHex;
        }


        public string Decimaltohex64(ulong a)
        {
            int i, k, n, t;
            string output;
            string sHex = "";
            string binaryvalue = "";

            char[] keys = new char[64];
            for (k = 0; k < keys.Length; k++)
            { keys[k] = '0'; }

            binaryvalue = Decimaltobin64(a);

            char[] bits = binaryvalue.ToCharArray();
            for (k = 0; k < bits.Length; k++)
            {
                if (bits[k] == '0')
                    keys[k] = '0';
                if (bits[k] == '1')
                    keys[k] = '1';
            }

            i = 0; n = 0; output = "";
            do
            {
                if (keys[i] == '0')
                    output = output + "0";
                if (keys[i] == '1')
                    output = output + "1";

                n = n + 1;
                if (n == 4)
                {
                    string hexvalue = "";
                    hexvalue = Convert.ToString(Convert.ToInt64(output, 2), 16);
                    sHex = sHex + hexvalue;
                    output = ""; n = 0;
                }
                i++;
            } while (i < keys.Length);

            return sHex;
        }



        public string Double2hex(double x)
        {
            long m = BitConverter.DoubleToInt64Bits(x);  //double to Int64

            string str = Convert.ToString(m, 2);         //Int64 to binarystring

            string strhex = Convert.ToString(Convert.ToInt64(str, 2), 16);   //binarystring to hexstring

            return strhex;
        }

        public double Hex2Double(string hexstrg)
        {
            string binarystrg = Convert.ToString(Convert.ToInt64(hexstrg, 16), 2);      //hexstring to binary string  

            long n = Convert.ToInt64(binarystrg, 2);                                    //binary string to long (Int64)

            double x = BitConverter.Int64BitsToDouble(n);                             //  Int64 to double           
            return x;
        }






        public double EntropiHesapla()
        {
            int i; double E; E = 0;
            for (i = 0; i < m; i++)
            { if (pi[i] > 0) E = E - (pi[i] * Math.Log(pi[i])); }
            return E;
        }


        public int OrtalamaHesapla()  //Mean
        {
            double w; int i, t;
            w = 0;
            for (i = 0; i < m; i++)
            { w = w + i * pi[i]; }
            t = (int)w;
            return t;
        }


        public double VaryansHesapla()  //variance
        {
            double w, t; int i;
            t = 0;
            for (i = 0; i < m; i++)
            { t = t + i * pi[i]; }
            w = 0;
            for (i = 0; i < m; i++)
            { w = w + (i - t) * (i - t) * pi[i]; }

            return w;
        }



        public void TextFileEncCaesar8(int key)
        {
            int n, q1, enc;
            StreamReader sr = new StreamReader("e:\\umut\\TEZ\\Mesaj.txt");
            FileStream fs = new FileStream("e:\\umut\\TEZ\\MesajC.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);

            n = sr.Read();
            while (n != -1)
            {
                q1 = (int)key;
                enc = (n + q1) % 255;
                char c = Convert.ToChar(enc);
                string ara = Char.ToString(c);
                dosya.Write(ara);
                n = sr.Read();
            }
            sr.Close();
            dosya.Close();
        }

        public void TextFileDcrCaesar8(int key)
        {
            int n, d, q1, enc;

            StreamReader sr = new StreamReader("e:\\umut\\TEZ\\MesajC.txt");
            FileStream fs = new FileStream("e:\\umut\\TEZ\\Mesajx.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);

            n = sr.Read();
            while (n != -1)
            {
                q1 = (int)key;
                d = n - key;
                if (d > 0)
                { enc = (n - key) % 255; }
                else
                {
                    enc = ((-1) * (n - q1)) % 255;
                    enc = 255 - enc;
                }

                char c = Convert.ToChar(enc);
                string ara = Char.ToString(c);
                dosya.Write(ara);
                n = sr.Read();
            }
            sr.Close();
            dosya.Close();
        }



        public void TextFileEncExor8(int key)
        {
            int n, q1, enc;

            StreamReader sr = new StreamReader("e:\\umut\\TEZ\\Mesaj.txt");
            FileStream fs = new FileStream("e:\\umut\\TEZ\\MesajC.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);

            n = sr.Read();
            while (n != -1)
            {
                q1 = (int)key;
                enc = n ^ q1;
                char c = Convert.ToChar(enc);
                string ara = Char.ToString(c);
                dosya.Write(ara);
                n = sr.Read();
            }
            sr.Close();
            dosya.Close();
        }

        public void TextFileDcrExor8(int key)
        {
            int n, q1, enc;

            StreamReader sr = new StreamReader("e:\\umut\\TEZ\\MesajC.txt");
            FileStream fs = new FileStream("e:\\umut\\TEZ\\Mesajx.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);

            n = sr.Read();
            while (n != -1)
            {
                q1 = (int)key;
                enc = n ^ q1;
                char c = Convert.ToChar(enc);
                string ara = Char.ToString(c);
                dosya.Write(ara);
                n = sr.Read();
            }
            sr.Close();
            dosya.Close();
        }

        public void TextFileEncAffine8(int a, int b)
        {
            int n, enc, test;
            StreamReader sr = new StreamReader("e:\\umut\\TEZ\\Mesaj.txt");
            FileStream fs = new FileStream("e:\\umut\\TEZ\\MesajC.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);

            n = sr.Read();
            while (n != -1)
            {
                test = GCD(a, 256);
                if (test == 1)
                {
                    enc = (a * n + b) % 255;
                }
                else
                { enc = 61; }

                char c = Convert.ToChar(enc);
                string ara = Char.ToString(c);
                dosya.Write(ara);
                n = sr.Read();
            }
            sr.Close();
            dosya.Close();
        }

        public void TextFileDcrAffine8(int a, int b)
        {
            int n, d, enc;
            int q1, q2, q3, at;

            StreamReader sr = new StreamReader("e:\\umut\\TEZ\\MesajC.txt");
            FileStream fs = new FileStream("e:\\umut\\TEZ\\Mesajx.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);

            n = sr.Read();
            while (n != -1)
            {
                at = ModuloTersi(a, 255);
                if (at != 0)
                {
                    d = n - b;
                    if (d > 0)
                    { q1 = (at * (n - b)) % 255; }
                    else
                    {
                        q1 = (at * (-1) * (n - b)) % 255;
                        q1 = 255 - q1;
                    }
                }
                else
                { q1 = 61; }

                enc = q1;

                char c = Convert.ToChar(enc);
                string ara = Char.ToString(c);
                dosya.Write(ara);
                n = sr.Read();
            }
            sr.Close();
            dosya.Close();
        }






        public void KeyTextFile1(double x0, double mu, int t, int tip)
        {
            int n;
            double x, x1; x1 = x0;
            ulong y; y = 0;
            FileStream fs = new FileStream("e:\\umut\\TEZ\\Keyl.txt", FileMode.Create, FileAccess.Write);
            StreamWriter dosya = new StreamWriter(fs);

            for (n = 0; n < t; n++)
            {
                x = mu * x1 * (1 - x1);
                y = (ulong)(x * ulong.MaxValue);
                string ara;
                if (tip == 0)
                    ara = Convert.ToString(x);
                else if (tip == 1)
                    ara = Convert.ToString(y);
                else if (tip == 2)
                    ara = Decimaltobin64(y);
                else if (tip == 3)
                    ara = Decimaltohex64(y);
                else
                    ara = "dogrutip sec";

                dosya.WriteLine(ara);
                //  dosya.Write(ara);
                x1 = x;
            }

            dosya.Close();
        }





        public string X0stringUreteci2()
        {
            int k, n;
            char[] Rkeys = new char[16];

            Rkeys[0] = '0'; Rkeys[1] = ',';

            for (k = 2; k < Rkeys.Length; k++)
            { Rkeys[k] = '0'; }

            for (k = 2; k < Rkeys.Length; k++)
            {
                n = xr.Next(48, 57);
                char c = (char)n;
                Rkeys[k] = c;
            }

            string bilR = new string(Rkeys);

            X0bilgiString = bilR;
            X0bilgiDouble = double.Parse(X0bilgiString);

            string bilgi = Double2hex(X0bilgiDouble);
            X0bilgiHex = bilgi;
            X0bilgiBinary = Hex2binary(bilgi);

            return bilR;
        }


        public ulong Y0stringUreteci()
        {
            ulong z;
            double q;
            int k, n;
            char[] Rkeys = new char[16];

            Rkeys[0] = '0'; Rkeys[1] = ',';

            for (k = 2; k < Rkeys.Length; k++)
            { Rkeys[k] = '0'; }

            for (k = 2; k < Rkeys.Length; k++)
            {
                n = xr.Next(48, 57);
                char c = (char)n;
                Rkeys[k] = c;
            }

            string bilR = new string(Rkeys);
            q = double.Parse(bilR);
            string bilgi = Double2hex(q);
            string bilgibinary = Hex2binary(bilgi);
            z = Convert.ToUInt64(bilgibinary, 2);
            return z;
        }

        public ulong Y0stringUreteci1()
        {
            ulong z;
            double q;
            int k, n;
            char[] Rkeys = new char[16];

            Rkeys[0] = '0'; Rkeys[1] = ',';

            for (k = 2; k < Rkeys.Length; k++)
            { Rkeys[k] = '0'; }

            for (k = 2; k < Rkeys.Length; k++)
            {
                n = xr.Next(48, 57);
                char c = (char)n;
                Rkeys[k] = c;
            }

            string bilR = new string(Rkeys);
            q = double.Parse(bilR);

            z = (ulong)(q * ulong.MaxValue);

            Y1bilgiUlong = z;

            string bilgi; bilgi = Decimaltohex64(z);
            Y1bilgiHex = bilgi;

            string bilgibinary = Hex2binary(bilgi);
            Y1bilgiBinary = bilgibinary;
            return z;
        }


        public ulong Y0stringUreteci2()
        {
            ulong z;
            double q;
            int k, n;
            char[] Rkeys = new char[16];

            Rkeys[0] = '0'; Rkeys[1] = ',';

            for (k = 2; k < Rkeys.Length; k++)
            { Rkeys[k] = '0'; }

            for (k = 2; k < Rkeys.Length; k++)
            {
                n = xr.Next(48, 57);
                char c = (char)n;
                Rkeys[k] = c;
            }

            string bilR = new string(Rkeys);
            q = double.Parse(bilR);

            z = (ulong)(q * ulong.MaxValue);

            Y2bilgiUlong = z;

            string bilgi; bilgi = Decimaltohex64(z);
            Y2bilgiHex = bilgi;

            string bilgibinary = Hex2binary(bilgi);
            Y2bilgiBinary = bilgibinary;
            return z;
        }



        public string MustringUreteci2()
        {
            int k, n; char c;
            char[] Rkeys = new char[16];

            Rkeys[0] = '3'; Rkeys[1] = ',';

            n = xr.Next(53, 57); c = (char)n;
            Rkeys[2] = c;

            n = xr.Next(54, 57); c = (char)n;
            Rkeys[3] = c;


            for (k = 4; k < Rkeys.Length; k++)
            { Rkeys[k] = '0'; }

            for (k = 4; k < Rkeys.Length; k++)
            {
                n = xr.Next(48, 57); c = (char)n;
                Rkeys[k] = c;
            }

            string bilR = new string(Rkeys);

            MubilgiString = bilR;

            MubilgiDouble = double.Parse(MubilgiString);

            string bilgi = Double2hex(MubilgiDouble);
            MubilgiHex = bilgi;
            MubilgiBinary = Hex2binary(bilgi);

            return bilR;
        }


        public void KeyGenerater128()
        {
            X0bilgiString = "";
            X0bilgiHex = "";
            X0bilgiBinary = "";
            X0bilgiDouble = 0.0;

            MubilgiString = "";
            MubilgiHex = "";
            MubilgiBinary = "";
            MubilgiDouble = 3.56;

            X0stringUreteci2();
            MustringUreteci2();

            KeyFinal = "";
            KeyFinal = X0bilgiHex + MubilgiHex;
            File.WriteAllText("e:\\umut\\TEZ\\KeyHex.txt", KeyFinal);


            KeyFinal = "";
            string bilgi;
            bilgi = Convert.ToString(X0bilgiDouble);
            KeyFinal = bilgi;
            bilgi = Convert.ToString(MubilgiDouble);
            KeyFinal = KeyFinal + ":" + bilgi;
            File.WriteAllText("e:\\umut\\TEZ\\KeyText.txt", KeyFinal);

        }

        public void AnahtarAlStringHex128()
        {
            int n, q;
            string KeyDizisi = File.ReadAllText("e:\\umut\\TEZ\\KeyHex.txt");
            n = KeyDizisi.Length % 16;
            if (n == 0)
            {
                q = (int)KeyDizisi.Length / 16;
                if (q == 2)
                {
                    X0bilgiHex = KeyDizisi.Substring(0, 16);
                    X0bilgiDouble = Hex2Double(X0bilgiHex);

                    MubilgiHex = KeyDizisi.Substring(16, 16);
                    MubilgiDouble = Hex2Double(MubilgiHex);
                }

            }
            else
            {
                X0bilgiDouble = 0.0;
                MubilgiDouble = 0.0;
            }

        }


        public string LamdastringUreteci1()
        {
            int k, n;
            char[] Rkeys = new char[16];

            Rkeys[0] = '0'; Rkeys[1] = ',';

            for (k = 2; k < Rkeys.Length; k++)
            { Rkeys[k] = '0'; }

            for (k = 2; k < Rkeys.Length; k++)
            {
                n = xr.Next(48, 57);
                char c = (char)n;
                Rkeys[k] = c;
            }

            string bilR = new string(Rkeys);

            MubilgiString = bilR;
            MubilgiDouble = double.Parse(MubilgiString);

            MubilgiDouble = 1.0 + MubilgiDouble;

            MubilgiString = Convert.ToString(MubilgiDouble);
            bilR = MubilgiString;

            string bilgi = Double2hex(MubilgiDouble);

            MubilgiHex = bilgi;
            MubilgiBinary = Hex2binary(bilgi);

            return bilR;
        }

        public string LamdastringUreteci2()
        {
            int k, n;
            char[] Rkeys = new char[16];

            Rkeys[0] = '0'; Rkeys[1] = ',';

            for (k = 2; k < Rkeys.Length; k++)
            { Rkeys[k] = '0'; }

            for (k = 2; k < Rkeys.Length; k++)
            {
                n = xr.Next(48, 57);
                char c = (char)n;
                Rkeys[k] = c;
            }

            string bilR = new string(Rkeys);

            MubilgiString = bilR;
            MubilgiDouble = double.Parse(MubilgiString);

            string bilgi = Double2hex(MubilgiDouble);

            MubilgiHex = bilgi;
            MubilgiBinary = Hex2binary(bilgi);

            return bilR;
        }


        public void KeyGeneraterTent128()   // x0:0-1, mu:0-2
        {
            X0bilgiString = "";
            X0bilgiHex = "";
            X0bilgiBinary = "";
            X0bilgiDouble = 0.0;

            X0stringUreteci2();

            MubilgiString = "";
            MubilgiHex = "";
            MubilgiBinary = "";
            MubilgiDouble = 1.50;

            LamdastringUreteci1();

            KeyFinal = "";
            KeyFinal = X0bilgiHex + MubilgiHex;
            File.WriteAllText("e:\\umut\\TEZ\\KeyHex.txt", KeyFinal);

            KeyFinal = "";
            string bilgi;
            bilgi = Convert.ToString(X0bilgiDouble);
            KeyFinal = bilgi;
            bilgi = Convert.ToString(MubilgiDouble);
            KeyFinal = KeyFinal + ":" + bilgi;
            File.WriteAllText("e:\\umut\\TEZ\\KeyText.txt", KeyFinal);
        }


        public void KeyGeneraterATent128()   // x0:0-1, mu:0-1
        {
            X0bilgiString = "";
            X0bilgiHex = "";
            X0bilgiBinary = "";
            X0bilgiDouble = 0.0;

            X0stringUreteci2();

            MubilgiString = "";
            MubilgiHex = "";
            MubilgiBinary = "";
            MubilgiDouble = 1.50;

            LamdastringUreteci2();

            KeyFinal = "";
            KeyFinal = abilgiHex + MubilgiHex;
            File.WriteAllText("e:\\umut\\TEZ\\KeyHex.txt", KeyFinal);

            KeyFinal = "";
            string bilgi;
            bilgi = Convert.ToString(X0bilgiDouble);
            KeyFinal = bilgi;
            bilgi = Convert.ToString(MubilgiDouble);
            KeyFinal = KeyFinal + ":" + bilgi;
            File.WriteAllText("e:\\umut\\TEZ\\KeyText.txt", KeyFinal);
        }



        public Bitmap MaskeUreteciTentMap128()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();
            int i, j; Color p9;
            double x, x1, mu, z;

            x1 = X0bilgiDouble;
            mu = MubilgiDouble;
            x = 0;
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);
                    if (x1 < 0.5)
                        x = mu * x1;
                    else if (x1 >= 0.5)
                        x = mu * (1 - x1);
                    else x = 0.0;
                    z = x * 255;
                    p9 = Color.FromArgb((int)z, (int)z, (int)z);
                    bmp2.SetPixel(i, j, p9);
                    Maske.SetPixel(i, j, p9);
                    x1 = x;

                }
            }
            return bmp2;
        }

        public Bitmap MaskeUreteciATentMap128()
        {
            Bitmap bmp1 = (Bitmap)Orijinal.Clone();
            Bitmap bmp2 = (Bitmap)Orijinal.Clone();
            int i, j; Color p9;
            double x, x1, mu, z;

            x1 = X0bilgiDouble;
            mu = MubilgiDouble;
            x = 0;
            for (j = 0; j < bmp1.Height; j++)
            {
                for (i = 0; i < bmp1.Width; i++)
                {
                    p9 = bmp1.GetPixel(i, j);

                    if (x1 < mu)
                    {
                        x = x1 / mu;
                    }
                    else if (x1 >= mu)
                    {
                        x = (1 - x1) / (1 - mu);
                    }

                    z = x * 255;
                    p9 = Color.FromArgb((int)z, (int)z, (int)z);
                    bmp2.SetPixel(i, j, p9);
                    Maske.SetPixel(i, j, p9);
                    x1 = x;

                }
            }
            return bmp2;
        }




    }
}
