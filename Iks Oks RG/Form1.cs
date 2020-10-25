using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Iks_Oks_RG
{
    public partial class mainForm : Form
    {
        public string[] nizZnakova = new string[81];
        public string[] nizZnakovaStanje = new string[81];
        public static Rectangle[] velikiKvadrati = new Rectangle[9];
        public Rectangle[,] rectangles = new Rectangle[9, 9];
        public Rectangle[] rects = new Rectangle[81];
        public static float sirinaPanela = 0;
        public static float visinaPanela = 0;
        public static float sirinaKvadrata = 0;
        public static float visinaKvadrata = 0;
        public static int[] malaPripadaVelikomKvadratu = new int[81];

        public static Graphics g = null;

        public static int brojacMalihKvadratica = 0;
        public static int prebaciTest = 0;

        public static bool[] pobjednikNiz = new bool[9];

        public static string[] nizPobjednikVelikiZnakovi = new String[9];

        public static int brojKolone;
        public static string naPotezu;
        public static bool potez = true;
        public static SolidBrush cetka = null;
        public static Font font = null;
        public static Font fontResize = null;

        public static Font fontPobjednik = null;
        public static Font fontResizeVeliki = null;

        public bool detektovanaPobjeda = false;
        public static int isEnd = 0;

        public static int sirinaPen = 0;

        int brojRedaKvadratica;
        int brojKoloneKvadratica;

        public mainForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.game;
        }

        public mainForm(string Player1Name, string Player2Name)
        {
            // U ovom dijelu koda inicijalizujemo formu na kojoj se prikazuje igra. Podesavamo sadrzaj labela da prikazuje imena igraca koja su nam proslijedjena iz Welcome Forme.
            InitializeComponent();
            this.Icon = Properties.Resources.game;
            lblPlayerX.Text = "X - " + Player1Name;
            lblPlayerO.Text = Player2Name + " - O";
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            // Kreiranje olovke kojom se vrsi crtanje horizontalnih i vertikalnih linija glavne seme.
            Pen pen = new Pen(Color.Black);
            pen.Width = Convert.ToSingle(mainPanel.Width / 80);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            sirinaPen = Convert.ToInt32(pen.Width);

            // Kreiranje olovke kojom se vrsi iscrtavanje horizontalnih i vertikalnih linija sporednih sema.
            Pen pen2 = new Pen(Color.Black);
            pen2.Width = Convert.ToSingle(mainPanel.Width / 120);
            pen2.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen2.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            int sirinaPen2 = Convert.ToInt32(pen2.Width);

            // Oredjivanje visine i sirine jednog kvadrata tj jednog polja glavne seme.

            sirinaPanela = (mainPanel.Width / 3 - sirinaPen + sirinaPen / 3);
            visinaPanela = (mainPanel.Height / 3 - sirinaPen + sirinaPen / 3);

            int sirinaPanelaInt = Convert.ToInt32(sirinaPanela);
            int visinaPanelaInt = Convert.ToInt32(visinaPanela);

            g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Crtanje vertikalnih linija glavne seme.
            g.DrawLine(pen, new Point(sirinaPanelaInt + sirinaPen / 2, mainPanel.Height - visinaPanelaInt * 3 - sirinaPen - sirinaPen / 2), new Point(sirinaPanelaInt + sirinaPen / 2, visinaPanelaInt * 3 + sirinaPen + sirinaPen / 2));
            g.DrawLine(pen, new Point(sirinaPanelaInt * 2 + sirinaPen / 2 + sirinaPen, mainPanel.Height - visinaPanelaInt * 3 - sirinaPen - sirinaPen / 2), new Point(sirinaPanelaInt * 2 + sirinaPen / 2 + sirinaPen, visinaPanelaInt * 3 + sirinaPen + sirinaPen / 2));

            // Crtanje horizontalnih linija glavne seme.
            g.DrawLine(pen, new Point(mainPanel.Width - sirinaPanelaInt * 3 - sirinaPen - sirinaPen / 2, visinaPanelaInt + sirinaPen / 2), new Point(sirinaPanelaInt * 3 + sirinaPen / 2 + sirinaPen, visinaPanelaInt + sirinaPen / 2));
            g.DrawLine(pen, new Point(mainPanel.Width - sirinaPanelaInt * 3 - sirinaPen - sirinaPen / 2, visinaPanelaInt * 2 + sirinaPen / 2 + sirinaPen), new Point(sirinaPanelaInt * 3 + sirinaPen + sirinaPen / 2, visinaPanelaInt * 2 + sirinaPen / 2 + sirinaPen));

            // Odredjivanje dimenzija jednog kvadrata tj jednog polja sporedne seme.

            sirinaKvadrata = (sirinaPanela / 3 - sirinaPen2 + sirinaPen2 / 3);
            visinaKvadrata = (visinaPanela / 3 - sirinaPen2 + sirinaPen2 / 3);

            int sirinaKvadrataInt = Convert.ToInt32(sirinaKvadrata);
            int visinaKvadrataInt = Convert.ToInt32(visinaKvadrata);

            // Crtanje vertikalnih linija sporednih sema.
            for (int j = 0; j < 3; j++)
            {

                for (int i = 0; i < 3; i++)
                {
                    g.DrawLine(pen2, new Point((sirinaKvadrataInt + sirinaPen2 / 2) + (sirinaPanelaInt + sirinaPen) * i, (visinaPanelaInt - visinaKvadrataInt * 3 - sirinaPen2 - sirinaPen2 / 2) + (j * (visinaPanelaInt + sirinaPen))), new Point(sirinaKvadrataInt + sirinaPen2 / 2 + (sirinaPanelaInt + sirinaPen) * i, (visinaKvadrataInt * 3 + sirinaPen2 + sirinaPen2 / 2) + ((visinaPanelaInt + sirinaPen) * j)));
                    g.DrawLine(pen2, new Point(sirinaKvadrataInt * 2 + sirinaPen2 / 2 + sirinaPen2 + (sirinaPanelaInt + sirinaPen) * i, (visinaPanelaInt - visinaKvadrataInt * 3 - sirinaPen2 - sirinaPen2 / 2) + (j * (visinaPanelaInt + sirinaPen))), new Point(sirinaKvadrataInt * 2 + sirinaPen2 / 2 + sirinaPen2 + (sirinaPanelaInt + sirinaPen) * i, (visinaKvadrataInt * 3 + sirinaPen2 + sirinaPen2 / 2) + (j * (visinaPanelaInt + sirinaPen))));
                }

            }
            // Crtanje horizontalnih linija sporednih sema.
            
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    g.DrawLine(pen2, new Point((sirinaPanelaInt - sirinaKvadrataInt * 3 - sirinaPen2 - sirinaPen2 / 2) + (i * (sirinaPanelaInt + sirinaPen)), (visinaKvadrataInt + sirinaPen2 / 2) + ((visinaPanelaInt + sirinaPen) * j)), new Point((sirinaKvadrataInt * 3 + sirinaPen2 + sirinaPen2 / 2) + (i * (sirinaPanelaInt + sirinaPen)), (visinaKvadrataInt + sirinaPen2 / 2) + ((visinaPanelaInt + sirinaPen) * j)));
                    g.DrawLine(pen2, new Point((sirinaPanelaInt - sirinaKvadrataInt * 3 - sirinaPen2 - sirinaPen2 / 2) + (i * (sirinaPanelaInt + sirinaPen)), (visinaKvadrataInt * 2 + sirinaPen2 + sirinaPen2 / 2) + ((visinaPanelaInt + sirinaPen) * j)), new Point((sirinaKvadrataInt * 3 + sirinaPen2 + sirinaPen2 / 2) + (i * (sirinaPanelaInt + sirinaPen)), (visinaKvadrataInt * 2 + sirinaPen2 + sirinaPen2 / 2) + ((visinaPanelaInt + sirinaPen) * j)));
                }
            }

            // Prolazak po citavoj semi kako bi se dobile tacne duzine i lokacije velikih kvadrata tj. polja glavne seme.
            for (int j = 0; j < 3; j++)
            {

                for (int i = 0; i < 3; i++)
                {
                    velikiKvadrati[i + (j * 3)] = new Rectangle(i * (sirinaPanelaInt + sirinaPen), j * (visinaPanelaInt + sirinaPen), sirinaPanelaInt, visinaPanelaInt);
                }

            }

            int brRed = 0;
            int brojac = 0;

            // Ovdje pravimo niz koji ce da pamti kom velikom kvadratu pripada jedan mali kvadrat. Taj niz kasnije mozemo iskoristiti za iscrtavanje velikog znaka X ili O u slucaju kada
            // dobijemo pobjednika u jednom od 9 polja.

            // Ovaj dio koda takodje koristimo i da bi popunili niz malih kvadrata kako bi imali njihove tacne lokacije i dimenzije.
            brojacMalihKvadratica = 0;
            prebaciTest = 0;


            int brKol = 0;
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (brKol == 3)
                    {
                        brKol = 0;
                        brojac++;
                        if (brojac == 3)
                        {
                            brojac = 0;
                            brRed++;
                        }
                    }

                    if (brojacMalihKvadratica == 3)
                    {
                        prebaciTest++;
                        brojacMalihKvadratica = 0;
                    }

                    rects[i + (j * 3)] = new Rectangle((sirinaKvadrataInt + sirinaPen2) * i + ((sirinaPanelaInt + sirinaPen) * brKol), (visinaKvadrataInt + sirinaPen2) * 0 + (visinaPanelaInt + sirinaPen) * 0, sirinaKvadrataInt, visinaKvadrataInt);

                    malaPripadaVelikomKvadratu[brojacMalihKvadratica + (3 * prebaciTest)] = prebaciTest;
                    brojacMalihKvadratica++;
                }
                brKol++;
            }

            brojacMalihKvadratica = 0;
            prebaciTest = 0;

            brKol = 0;
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (brKol == 3)
                    {
                        brKol = 0;
                        brojac++;
                        if (brojac == 3)
                        {
                            brojac = 0;
                            brRed++;
                        }
                    }

                    if (brojacMalihKvadratica == 3)
                    {
                        prebaciTest++;
                        brojacMalihKvadratica = 0;
                    }

                    rects[(i + (j * 3)) + (9 * 1)] = new Rectangle((sirinaKvadrataInt + sirinaPen2) * i + ((sirinaPanelaInt + sirinaPen) * brKol), (visinaKvadrataInt + sirinaPen2) * 1 + (visinaPanelaInt + sirinaPen) * 0, sirinaKvadrataInt, visinaKvadrataInt);

                    malaPripadaVelikomKvadratu[brojacMalihKvadratica + (3 * prebaciTest) + 9] = prebaciTest;
                    brojacMalihKvadratica++;
                }
                brKol++;
            }

            brojacMalihKvadratica = 0;
            prebaciTest = 0;

            brKol = 0;
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (brKol == 3)
                    {
                        brKol = 0;
                        brojac++;
                        if (brojac == 3)
                        {
                            brojac = 0;
                            brRed++;
                        }
                    }
                    if (brojacMalihKvadratica == 3)
                    {
                        prebaciTest++;
                        brojacMalihKvadratica = 0;
                    }

                    rects[(i + (j * 3)) + (9 * 2)] = new Rectangle((sirinaKvadrataInt + sirinaPen2) * i + ((sirinaPanelaInt + sirinaPen) * brKol), (visinaKvadrataInt + sirinaPen2) * 2 + (visinaPanelaInt + sirinaPen) * 0, sirinaKvadrataInt, visinaKvadrataInt);

                    malaPripadaVelikomKvadratu[brojacMalihKvadratica + (3 * prebaciTest) + 9 * 2] = prebaciTest;
                    brojacMalihKvadratica++;
                }
                brKol++;
            }

            brojacMalihKvadratica = 0;
            prebaciTest = 0;


            brKol = 0;
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (brKol == 3)
                    {
                        brKol = 0;
                        brojac++;
                        if (brojac == 3)
                        {
                            brojac = 0;
                            brRed++;
                        }
                    }

                    if (brojacMalihKvadratica == 3)
                    {
                        prebaciTest++;
                        brojacMalihKvadratica = 0;
                    }

                    rects[(i + (j * 3)) + (9 * 0) + (27 * 1)] = new Rectangle((sirinaKvadrataInt + sirinaPen2) * i + ((sirinaPanelaInt + sirinaPen) * brKol), (visinaKvadrataInt + sirinaPen2) * 0 + (visinaPanelaInt + sirinaPen) * 1, sirinaKvadrataInt, visinaKvadrataInt);

                    malaPripadaVelikomKvadratu[brojacMalihKvadratica + (3 * (prebaciTest + 9))] = prebaciTest + 3;
                    brojacMalihKvadratica++;

                }
                brKol++;
            }
            brojacMalihKvadratica = 0;
            prebaciTest = 0;

            brKol = 0;
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (brKol == 3)
                    {
                        brKol = 0;
                        brojac++;
                        if (brojac == 3)
                        {
                            brojac = 0;
                            brRed++;
                        }
                    }
                    if (brojacMalihKvadratica == 3)
                    {
                        prebaciTest++;
                        brojacMalihKvadratica = 0;
                    }

                    rects[(i + (j * 3)) + (9 * 1) + (27 * 1)] = new Rectangle((sirinaKvadrataInt + sirinaPen2) * i + ((sirinaPanelaInt + sirinaPen) * brKol), (visinaKvadrataInt + sirinaPen2) * 1 + (visinaPanelaInt + sirinaPen) * 1, sirinaKvadrataInt, visinaKvadrataInt);

                    malaPripadaVelikomKvadratu[brojacMalihKvadratica + (3 * (prebaciTest + 9) + 9)] = prebaciTest + 3;
                    brojacMalihKvadratica++;
                }
                brKol++;
            }

            brojacMalihKvadratica = 0;
            prebaciTest = 0;

            brKol = 0;
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (brKol == 3)
                    {
                        brKol = 0;
                        brojac++;
                        if (brojac == 3)
                        {
                            brojac = 0;
                            brRed++;
                        }
                    }

                    if (brojacMalihKvadratica == 3)
                    {
                        prebaciTest++;
                        brojacMalihKvadratica = 0;
                    }

                    rects[(i + (j * 3)) + (9 * 2) + (27 * 1)] = new Rectangle((sirinaKvadrataInt + sirinaPen2) * i + ((sirinaPanelaInt + sirinaPen) * brKol), (visinaKvadrataInt + sirinaPen2) * 2 + (visinaPanelaInt + sirinaPen) * 1, sirinaKvadrataInt, visinaKvadrataInt);

                    malaPripadaVelikomKvadratu[brojacMalihKvadratica + (3 * (prebaciTest + 9) + 9 * 2)] = prebaciTest + 3;
                    brojacMalihKvadratica++;
                }
                brKol++;
            }

            brojacMalihKvadratica = 0;
            prebaciTest = 0;

            brKol = 0;
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (brKol == 3)
                    {
                        brKol = 0;
                        brojac++;
                        if (brojac == 3)
                        {
                            brojac = 0;
                            brRed++;
                        }
                    }
                    if (brojacMalihKvadratica == 3)
                    {
                        prebaciTest++;
                        brojacMalihKvadratica = 0;
                    }

                    rects[(i + (j * 3)) + (9 * 0) + (27 * 2)] = new Rectangle((sirinaKvadrataInt + sirinaPen2) * i + ((sirinaPanelaInt + sirinaPen) * brKol), (visinaKvadrataInt + sirinaPen2) * 0 + (visinaPanelaInt + sirinaPen) * 2, sirinaKvadrataInt, visinaKvadrataInt);

                    malaPripadaVelikomKvadratu[brojacMalihKvadratica + (3 * (prebaciTest + 18))] = prebaciTest + 6;
                    brojacMalihKvadratica++;
                }
                brKol++;
            }

            brojacMalihKvadratica = 0;
            prebaciTest = 0;

            brKol = 0;
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (brKol == 3)
                    {
                        brKol = 0;
                        brojac++;
                        if (brojac == 3)
                        {
                            brojac = 0;
                            brRed++;
                        }
                    }

                    if (brojacMalihKvadratica == 3)
                    {
                        prebaciTest++;
                        brojacMalihKvadratica = 0;
                    }

                    rects[(i + (j * 3)) + (9 * 1) + (27 * 2)] = new Rectangle((sirinaKvadrataInt + sirinaPen2) * i + ((sirinaPanelaInt + sirinaPen) * brKol), (visinaKvadrataInt + sirinaPen2) * 1 + (visinaPanelaInt + sirinaPen) * 2, sirinaKvadrataInt, visinaKvadrataInt);

                    malaPripadaVelikomKvadratu[brojacMalihKvadratica + (3 * (prebaciTest + 18) + 9)] = prebaciTest + 6;
                    brojacMalihKvadratica++;
                }
                brKol++;
            }

            brojacMalihKvadratica = 0;
            prebaciTest = 0;

            brKol = 0;
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (brKol == 3)
                    {
                        brKol = 0;
                        brojac++;
                        if (brojac == 3)
                        {
                            brojac = 0;
                            brRed++;
                        }
                    }

                    if (brojacMalihKvadratica == 3)
                    {
                        prebaciTest++;
                        brojacMalihKvadratica = 0;
                    }

                    rects[(i + (j * 3)) + (9 * 2) + (27 * 2)] = new Rectangle((sirinaKvadrataInt + sirinaPen2) * i + ((sirinaPanelaInt + sirinaPen) * brKol), (visinaKvadrataInt + sirinaPen2) * 2 + (visinaPanelaInt + sirinaPen) * 2, sirinaKvadrataInt, visinaKvadrataInt);

                    malaPripadaVelikomKvadratu[brojacMalihKvadratica + (3 * (prebaciTest + 18) + 9 * 2)] = prebaciTest + 6;
                    brojacMalihKvadratica++;
                }
                brKol++;
            }

            // Popunjavanje malih polja odgovarajucim znakovima u slucaju da se desi bilo kakav resize forme i panela.

            brojKolone = 1;

            for (int i = 0; i < rects.Length; i++)
            {
                if (nizZnakovaStanje[i + (brojKolone - 1) * 3] != null)
                {
                    if (nizZnakova[i + (brojKolone - 1) * 3] == "X")
                    {
                        cetka.Color = Color.Blue;
                        fontResize = new Font("Arial", (sirinaKvadrata + visinaKvadrata) / 4, FontStyle.Regular, GraphicsUnit.Pixel);

                        g.DrawString(nizZnakova[i + (brojKolone - 1) * 3], fontResize, cetka, new PointF((rects[i + (brojKolone - 1) * 3].Location.X + rects[i + (brojKolone - 1) * 3].Width / 2 - ((sirinaKvadrata + visinaKvadrata) / 4) / 2), (rects[i + (brojKolone - 1) * 3].Location.Y + rects[i + (brojKolone - 1) * 3].Height / 2 - ((sirinaKvadrata + visinaKvadrata) / 4) / 2)));
                    }
                    else
                    {
                        cetka.Color = Color.Red;
                        fontResize = new Font("Arial", (sirinaKvadrata + visinaKvadrata) / 4, FontStyle.Regular, GraphicsUnit.Pixel);
                        g.DrawString(nizZnakova[i + (brojKolone - 1) * 3], fontResize, cetka, new PointF((rects[i + (brojKolone - 1) * 3].Location.X + rects[i + (brojKolone - 1) * 3].Width / 2 - ((sirinaKvadrata + visinaKvadrata) / 4) / 2), (rects[i + (brojKolone - 1) * 3].Location.Y + rects[i + (brojKolone - 1) * 3].Height / 2 - ((sirinaKvadrata + visinaKvadrata) / 4) / 2)));
                    }
                }
            }

            for (int i = 0; i < velikiKvadrati.Length; i++)
            {
                if (nizZnakovaStanje[i + (brojKolone - 1) * 3] != null)
                {
                    if (nizZnakova[i + (brojKolone - 1) * 3] == "X")
                    {
                        cetka.Color = Color.Blue;
                        fontResize = new Font("Arial", (sirinaKvadrata + visinaKvadrata) / 4, FontStyle.Regular, GraphicsUnit.Pixel);
                        g.DrawString(nizZnakova[i + (brojKolone - 1) * 3], fontResize, cetka, new PointF((rects[i + (brojKolone - 1) * 3].Location.X + rects[i + (brojKolone - 1) * 3].Width / 2 - ((sirinaKvadrata + visinaKvadrata) / 4) / 2), (rects[i + (brojKolone - 1) * 3].Location.Y + rects[i + (brojKolone - 1) * 3].Height / 2 - ((sirinaKvadrata + visinaKvadrata) / 4) / 2)));
                    }
                    else
                    {
                        cetka.Color = Color.Red;
                        fontResize = new Font("Arial", (sirinaKvadrata + visinaKvadrata) / 4, FontStyle.Regular, GraphicsUnit.Pixel);
                        g.DrawString(nizZnakova[i + (brojKolone - 1) * 3], fontResize, cetka, new PointF((rects[i + (brojKolone - 1) * 3].Location.X + rects[i + (brojKolone - 1) * 3].Width / 2 - ((sirinaKvadrata + visinaKvadrata) / 4) / 2), (rects[i + (brojKolone - 1) * 3].Location.Y + rects[i + (brojKolone - 1) * 3].Height / 2 - ((sirinaKvadrata + visinaKvadrata) / 4) / 2)));
                    }
                }
            }

            // Popunjavanje velikih polja u slucaju da se desi bilo kakav resize forme i panela.

            fontResizeVeliki = new Font("Arial", (velikiKvadrati[0].Width + velikiKvadrati[0].Height) / 3, FontStyle.Bold, GraphicsUnit.Pixel);

            for (int i = 0; i < velikiKvadrati.Length; i++)
            {
                if (pobjednikNiz[i] == true)
                {
                    if (nizPobjednikVelikiZnakovi[i] == "X")
                    {
                        cetka.Color = Color.Blue;
                        fontResize = new Font("Arial", (sirinaKvadrata + visinaKvadrata) / 4, FontStyle.Regular, GraphicsUnit.Pixel);
                        g.DrawString(nizPobjednikVelikiZnakovi[i], fontResizeVeliki, cetka, new PointF((velikiKvadrati[i].Location.X + (velikiKvadrati[i].Width - fontResizeVeliki.Size) / 2), (velikiKvadrati[i].Location.Y + (velikiKvadrati[i].Height - fontResizeVeliki.Size) / 2)));
                    }
                    else
                    {
                        cetka.Color = Color.Red;
                        fontResize = new Font("Arial", (sirinaKvadrata + visinaKvadrata) / 4, FontStyle.Regular, GraphicsUnit.Pixel);
                        g.DrawString(nizPobjednikVelikiZnakovi[i], fontResizeVeliki, cetka, new PointF((velikiKvadrati[i].Location.X + (velikiKvadrati[i].Width - fontResizeVeliki.Size) / 2), (velikiKvadrati[i].Location.Y + (velikiKvadrati[i].Height - fontResizeVeliki.Size) / 2)));
                    }
                }
            }
        }

        private void mainPanel_Resize(object sender, EventArgs e)
        {
            // Metode koje pozivamo da bi se resize ispravno vrsio.

            mainPanel.Refresh();
            mainPanel.Invalidate();
        }

        private void mainPanel_MouseClick(object sender, MouseEventArgs e)
        {
            // U ovom dijelu koda vrsimo detekciju klika na panel, te na osnovu toga popunjavamo polja odgovarajucim znakovima.

            Graphics g = mainPanel.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            font = new Font("Arial", (sirinaKvadrata + visinaKvadrata) / 4, FontStyle.Regular, GraphicsUnit.Pixel);
            cetka = new SolidBrush(Color.Blue);

            int Xaxis = e.X;
            int Yaxis = e.Y;

            naPotezu = "";

            // Dobijanje broja kolona.
            brojKolone = 1;

            while (Xaxis - mainPanel.Width / 3 >= 0)
            {
                brojKolone++;
                Xaxis -= mainPanel.Width / 3;

            };

            // Provjera da li klik pripada nekom od polja i crtanje znakova na odgovarajuce mjesto u malim poljima.

            int broj = 0;
            for (int i = 0; i < rects.Length; i++)
            {
                if (Xaxis >= rects[i].Location.X && Xaxis <= rects[i].Location.X + rects[i].Width && Yaxis >= rects[i].Location.Y && Yaxis <= rects[i].Location.Y + rects[i].Height && pobjednikNiz[malaPripadaVelikomKvadratu[i + (brojKolone - 1) * 3]] != true)
                {
                    if (nizZnakovaStanje[i + (brojKolone - 1) * 3] == null)
                    {
                        if (potez == true)
                        {
                            naPotezu = "X";
                            potez = false;
                            cetka.Color = Color.Blue;
                        }
                        else
                        {
                            naPotezu = "O";
                            potez = true;
                            cetka.Color = Color.Red;
                        }

                        broj = (i + 1) + ((brojKolone - 1) * 3);
                        g.DrawString(naPotezu, font, cetka, new PointF((rects[i + (brojKolone - 1) * 3].Location.X + rects[i + (brojKolone - 1) * 3].Width / 2 - ((sirinaKvadrata + visinaKvadrata) / 4) / 2), (rects[i + (brojKolone - 1) * 3].Location.Y + rects[i + (brojKolone - 1) * 3].Height / 2 - ((sirinaKvadrata + visinaKvadrata) / 4) / 2)));
                        nizZnakova[i + (brojKolone - 1) * 3] = naPotezu;
                        nizZnakovaStanje[i + (brojKolone - 1) * 3] = "zauzeto";
                        ProvjeriPobjednika(rects[i + (brojKolone - 1) * 3], (i + (brojKolone - 1) * 3), naPotezu);
                    }
                }
            }
        }

        // Funkcija koju pozivamo da bi provjerili postoji li pobjednik unutar male seme.

        void ProvjeriPobjednika(Rectangle trenutniRect, int indeks, string trenutniZnak)
        {
            brojRedaKvadratica = 0;
            int tempIndeks = indeks;

            while (tempIndeks - 9 >= 0)
            {
                tempIndeks -= 9;
                brojRedaKvadratica++;
            }

            brojRedaKvadratica++;

            int tempInd = indeks;

            brojKoloneKvadratica = ((tempInd % 9) + 1);

            mainPanel.CreateGraphics().SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Algoritmi za provjeru pobjednika.

            fontPobjednik = new Font("Arial", (velikiKvadrati[0].Width + velikiKvadrati[0].Height) / 3, FontStyle.Bold, GraphicsUnit.Pixel);

            if (nizZnakovaStanje[indeks] != null)
            {

                if ((indeks + 1) % 3 == 0)
                {
                    if (nizZnakova[indeks - 2] == trenutniZnak && nizZnakova[indeks - 1] == trenutniZnak && nizZnakova[indeks] == trenutniZnak)
                    {
                        mainPanel.CreateGraphics().DrawString(trenutniZnak, fontPobjednik, cetka, new PointF((velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.X + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Width - (fontPobjednik.Size)) / 2), velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.Y + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Height - (fontPobjednik.Size)) / 2));
                        pobjednikNiz[malaPripadaVelikomKvadratu[indeks]] = true;
                        nizPobjednikVelikiZnakovi[malaPripadaVelikomKvadratu[indeks]] = trenutniZnak;
                        pobjednikVelike(malaPripadaVelikomKvadratu[indeks], trenutniZnak);
                    }

                }
                else if ((indeks + 1) % 3 == 1)
                {
                    if (nizZnakova[indeks] == trenutniZnak && nizZnakova[indeks + 1] == trenutniZnak && nizZnakova[indeks + 2] == trenutniZnak)
                    {
                        mainPanel.CreateGraphics().DrawString(trenutniZnak, fontPobjednik, cetka, new PointF((velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.X + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Width - (fontPobjednik.Size)) / 2), velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.Y + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Height - (fontPobjednik.Size)) / 2));
                        pobjednikNiz[malaPripadaVelikomKvadratu[indeks]] = true;
                        nizPobjednikVelikiZnakovi[malaPripadaVelikomKvadratu[indeks]] = trenutniZnak;
                        pobjednikVelike(malaPripadaVelikomKvadratu[indeks], trenutniZnak);
                    }
                }
                else if ((indeks + 1) % 3 == 2)
                {
                    if (nizZnakova[indeks] == trenutniZnak && nizZnakova[indeks + 1] == trenutniZnak && nizZnakova[indeks - 1] == trenutniZnak)
                    {
                        mainPanel.CreateGraphics().DrawString(trenutniZnak, fontPobjednik, cetka, new PointF((velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.X + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Width - (fontPobjednik.Size)) / 2), velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.Y + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Height - (fontPobjednik.Size)) / 2));
                        pobjednikNiz[malaPripadaVelikomKvadratu[indeks]] = true;
                        nizPobjednikVelikiZnakovi[malaPripadaVelikomKvadratu[indeks]] = trenutniZnak;
                        pobjednikVelike(malaPripadaVelikomKvadratu[indeks], trenutniZnak);
                    }
                }
            }

            if (nizZnakovaStanje[indeks] != null)
            {
                if (brojKoloneKvadratica % 3 == 2) // detektuje drugu kolonu
                {


                    if (brojRedaKvadratica % 3 == 0) // zadnji clan u drugoj koloni
                    {

                        if (nizZnakova[indeks] == trenutniZnak && nizZnakova[indeks - 9] == trenutniZnak && nizZnakova[indeks - 18] == trenutniZnak)
                        {
                            mainPanel.CreateGraphics().DrawString(trenutniZnak, fontPobjednik, cetka, new PointF((velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.X + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Width - (fontPobjednik.Size)) / 2), velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.Y + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Height - (fontPobjednik.Size)) / 2));
                            pobjednikNiz[malaPripadaVelikomKvadratu[indeks]] = true;
                            nizPobjednikVelikiZnakovi[malaPripadaVelikomKvadratu[indeks]] = trenutniZnak;
                            pobjednikVelike(malaPripadaVelikomKvadratu[indeks], trenutniZnak);
                        }
                    }

                }
            }

            if (nizZnakovaStanje[indeks] != null)
            {
                if (brojKoloneKvadratica % 3 == 2) // detektuje drugu kolonu
                {


                    if (brojRedaKvadratica % 3 == 1) // zadnji clan u drugoj koloni
                    {
                        // detektuje prvi clan u drugoj koloni
                        if (nizZnakova[indeks] == trenutniZnak && nizZnakova[indeks + 9] == trenutniZnak && nizZnakova[indeks + 18] == trenutniZnak)
                        {
                            mainPanel.CreateGraphics().DrawString(trenutniZnak, fontPobjednik, cetka, new PointF((velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.X + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Width - (fontPobjednik.Size)) / 2), velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.Y + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Height - (fontPobjednik.Size)) / 2));
                            pobjednikNiz[malaPripadaVelikomKvadratu[indeks]] = true;
                            nizPobjednikVelikiZnakovi[malaPripadaVelikomKvadratu[indeks]] = trenutniZnak;
                            pobjednikVelike(malaPripadaVelikomKvadratu[indeks], trenutniZnak);
                        }
                    }

                }
            }

            if (nizZnakovaStanje[indeks] != null)
            {
                if (brojKoloneKvadratica % 3 == 2) // detektuje drugu kolonu
                {
                    if (brojRedaKvadratica % 3 == 2) // drugi clan u drugoj koloni
                    {
                        if (nizZnakova[indeks] == trenutniZnak && nizZnakova[indeks + 9] == trenutniZnak && nizZnakova[indeks - 9] == trenutniZnak)
                        {
                            mainPanel.CreateGraphics().DrawString(trenutniZnak, fontPobjednik, cetka, new PointF((velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.X + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Width - (fontPobjednik.Size)) / 2), velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.Y + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Height - (fontPobjednik.Size)) / 2));
                            pobjednikNiz[malaPripadaVelikomKvadratu[indeks]] = true;
                            nizPobjednikVelikiZnakovi[malaPripadaVelikomKvadratu[indeks]] = trenutniZnak;
                            pobjednikVelike(malaPripadaVelikomKvadratu[indeks], trenutniZnak);
                        }
                    }

                }
            }

            // prva kolona

            if (nizZnakovaStanje[indeks] != null)
            {
                if (brojKoloneKvadratica % 3 == 1) // detektuje prvu kolonu
                {
                    if (brojRedaKvadratica % 3 == 1) // prvi clan u prvoj koloni
                    {
                        if (nizZnakova[indeks] == trenutniZnak && nizZnakova[indeks + 9] == trenutniZnak && nizZnakova[indeks + 18] == trenutniZnak)
                        {
                            mainPanel.CreateGraphics().DrawString(trenutniZnak, fontPobjednik, cetka, new PointF((velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.X + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Width - (fontPobjednik.Size)) / 2), velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.Y + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Height - (fontPobjednik.Size)) / 2));
                            pobjednikNiz[malaPripadaVelikomKvadratu[indeks]] = true;
                            nizPobjednikVelikiZnakovi[malaPripadaVelikomKvadratu[indeks]] = trenutniZnak;
                            pobjednikVelike(malaPripadaVelikomKvadratu[indeks], trenutniZnak);
                        }
                    }

                }
            }

            if (nizZnakovaStanje[indeks] != null)
            {
                if (brojKoloneKvadratica % 3 == 1) // detektuje prvu kolonu
                {
                    if (brojRedaKvadratica % 3 == 2) // drugi clan u prvoj koloni
                    {

                        if (nizZnakova[indeks] == trenutniZnak && nizZnakova[indeks + 9] == trenutniZnak && nizZnakova[indeks - 9] == trenutniZnak)
                        {
                            mainPanel.CreateGraphics().DrawString(trenutniZnak, fontPobjednik, cetka, new PointF((velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.X + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Width - (fontPobjednik.Size)) / 2), velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.Y + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Height - (fontPobjednik.Size)) / 2));
                            pobjednikNiz[malaPripadaVelikomKvadratu[indeks]] = true;
                            nizPobjednikVelikiZnakovi[malaPripadaVelikomKvadratu[indeks]] = trenutniZnak;
                            pobjednikVelike(malaPripadaVelikomKvadratu[indeks], trenutniZnak);
                        }
                    }

                }
            }

            if (nizZnakovaStanje[indeks] != null)
            {
                if (brojKoloneKvadratica % 3 == 1) // detektuje prvu kolonu
                {

                    if (brojRedaKvadratica % 3 == 0) // treci clan u prvoj koloni
                    {
                        if (nizZnakova[indeks] == trenutniZnak && nizZnakova[indeks - 18] == trenutniZnak && nizZnakova[indeks - 9] == trenutniZnak)
                        {
                            mainPanel.CreateGraphics().DrawString(trenutniZnak, fontPobjednik, cetka, new PointF((velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.X + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Width - (fontPobjednik.Size)) / 2), velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.Y + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Height - (fontPobjednik.Size)) / 2));
                            pobjednikNiz[malaPripadaVelikomKvadratu[indeks]] = true;
                            nizPobjednikVelikiZnakovi[malaPripadaVelikomKvadratu[indeks]] = trenutniZnak;
                            pobjednikVelike(malaPripadaVelikomKvadratu[indeks], trenutniZnak);
                        }
                    }

                }
            }

            // treca kolona

            if (nizZnakovaStanje[indeks] != null)
            {
                if (brojKoloneKvadratica % 3 == 0) // detektuje trecu kolonu
                {


                    if (brojRedaKvadratica % 3 == 1) // prvi clan u trecoj koloni
                    {
                        if (nizZnakova[indeks] == trenutniZnak && nizZnakova[indeks + 9] == trenutniZnak && nizZnakova[indeks + 18] == trenutniZnak)
                        {
                            mainPanel.CreateGraphics().DrawString(trenutniZnak, fontPobjednik, cetka, new PointF((velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.X + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Width - (fontPobjednik.Size)) / 2), velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.Y + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Height - (fontPobjednik.Size)) / 2));
                            pobjednikNiz[malaPripadaVelikomKvadratu[indeks]] = true;
                            nizPobjednikVelikiZnakovi[malaPripadaVelikomKvadratu[indeks]] = trenutniZnak;
                            pobjednikVelike(malaPripadaVelikomKvadratu[indeks], trenutniZnak);
                        }
                    }

                }
            }

            if (nizZnakovaStanje[indeks] != null)
            {
                if (brojKoloneKvadratica % 3 == 0) // detektuje trecu kolonu
                {
                    if (brojRedaKvadratica % 3 == 2) // drugi clan u trecoj koloni
                    {

                        if (nizZnakova[indeks] == trenutniZnak && nizZnakova[indeks + 9] == trenutniZnak && nizZnakova[indeks - 9] == trenutniZnak)
                        {
                            mainPanel.CreateGraphics().DrawString(trenutniZnak, fontPobjednik, cetka, new PointF((velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.X + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Width - (fontPobjednik.Size)) / 2), velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.Y + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Height - (fontPobjednik.Size)) / 2));
                            pobjednikNiz[malaPripadaVelikomKvadratu[indeks]] = true;
                            nizPobjednikVelikiZnakovi[malaPripadaVelikomKvadratu[indeks]] = trenutniZnak;
                            pobjednikVelike(malaPripadaVelikomKvadratu[indeks], trenutniZnak);
                        }
                    }

                }
            }

            if (nizZnakovaStanje[indeks] != null)
            {
                if (brojKoloneKvadratica % 3 == 0) // detektuje trecu kolonu
                {

                    if (brojRedaKvadratica % 3 == 0) // treci clan u trecoj koloni
                    {
                        if (nizZnakova[indeks] == trenutniZnak && nizZnakova[indeks - 18] == trenutniZnak && nizZnakova[indeks - 9] == trenutniZnak)
                        {
                            mainPanel.CreateGraphics().DrawString(trenutniZnak, fontPobjednik, cetka, new PointF((velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.X + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Width - (fontPobjednik.Size)) / 2), velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.Y + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Height - (fontPobjednik.Size)) / 2));
                            pobjednikNiz[malaPripadaVelikomKvadratu[indeks]] = true;
                            nizPobjednikVelikiZnakovi[malaPripadaVelikomKvadratu[indeks]] = trenutniZnak;
                            pobjednikVelike(malaPripadaVelikomKvadratu[indeks], trenutniZnak);
                        }
                    }

                }
            }

            // dijagonalna provjera od down-left do top-right

            if (nizZnakovaStanje[indeks] != null)
            {
                if (brojKoloneKvadratica % 3 == 0) // detektuje trecu kolonu
                {

                    if (brojRedaKvadratica % 3 == 1) // prvi clan u trecoj koloni
                    {
                        if (nizZnakova[indeks] == trenutniZnak && nizZnakova[indeks + 9 - 1] == trenutniZnak && nizZnakova[indeks + 2 * 9 - 2] == trenutniZnak)
                        {
                            mainPanel.CreateGraphics().DrawString(trenutniZnak, fontPobjednik, cetka, new PointF((velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.X + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Width - (fontPobjednik.Size)) / 2), velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.Y + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Height - (fontPobjednik.Size)) / 2));
                            pobjednikNiz[malaPripadaVelikomKvadratu[indeks]] = true;
                            nizPobjednikVelikiZnakovi[malaPripadaVelikomKvadratu[indeks]] = trenutniZnak;
                            pobjednikVelike(malaPripadaVelikomKvadratu[indeks], trenutniZnak);
                        }
                    }

                }
            }

            // dijagonalna provjera od top-left do down-right

            if (nizZnakovaStanje[indeks] != null)
            {
                if (brojKoloneKvadratica % 3 == 0) // detektuje trecu kolonu
                {

                    if (brojRedaKvadratica % 3 == 0) // treci clan u trecoj koloni
                    {
                        if (nizZnakova[indeks] == trenutniZnak && nizZnakova[indeks - 9 - 1] == trenutniZnak && nizZnakova[indeks - 2 * 9 - 2] == trenutniZnak)
                        {
                            mainPanel.CreateGraphics().DrawString(trenutniZnak, fontPobjednik, cetka, new PointF((velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.X + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Width - (fontPobjednik.Size)) / 2), velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.Y + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Height - (fontPobjednik.Size)) / 2));
                            pobjednikNiz[malaPripadaVelikomKvadratu[indeks]] = true;
                            nizPobjednikVelikiZnakovi[malaPripadaVelikomKvadratu[indeks]] = trenutniZnak;
                            pobjednikVelike(malaPripadaVelikomKvadratu[indeks], trenutniZnak);
                        }
                    }

                }
            }

            // dijagonalna provjera od top-left do down-right

            if (nizZnakovaStanje[indeks] != null)
            {
                if (brojKoloneKvadratica % 3 == 1) // detektuje prvu kolonu
                {

                    if (brojRedaKvadratica % 3 == 1) // prvi clan u trecoj koloni
                    {
                        if (nizZnakova[indeks] == trenutniZnak && nizZnakova[indeks + 9 + 1] == trenutniZnak && nizZnakova[indeks + 2 * 9 + 2] == trenutniZnak)
                        {
                            mainPanel.CreateGraphics().DrawString(trenutniZnak, fontPobjednik, cetka, new PointF((velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.X + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Width - (fontPobjednik.Size)) / 2), velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.Y + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Height - (fontPobjednik.Size)) / 2));
                            pobjednikNiz[malaPripadaVelikomKvadratu[indeks]] = true;
                            nizPobjednikVelikiZnakovi[malaPripadaVelikomKvadratu[indeks]] = trenutniZnak;
                            pobjednikVelike(malaPripadaVelikomKvadratu[indeks], trenutniZnak);
                        }
                    }

                }
            }

            // dijagonalna provjera od top-left do down-right

            if (nizZnakovaStanje[indeks] != null)
            {
                if (brojKoloneKvadratica % 3 == 1) // detektuje prvu kolonu
                {

                    if (brojRedaKvadratica % 3 == 0) // treci clan u prvoj koloni
                    {
                        if (nizZnakova[indeks] == trenutniZnak && nizZnakova[indeks - 9 + 1] == trenutniZnak && nizZnakova[indeks - 2 * 9 + 2] == trenutniZnak)
                        {
                            mainPanel.CreateGraphics().DrawString(trenutniZnak, fontPobjednik, cetka, new PointF((velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.X + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Width - (fontPobjednik.Size)) / 2), velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.Y + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Height - (fontPobjednik.Size)) / 2));
                            pobjednikNiz[malaPripadaVelikomKvadratu[indeks]] = true;
                            nizPobjednikVelikiZnakovi[malaPripadaVelikomKvadratu[indeks]] = trenutniZnak;
                            pobjednikVelike(malaPripadaVelikomKvadratu[indeks], trenutniZnak);
                        }
                    }

                }
            }

            // dijagonalna provjera od centra prema right top

            if (nizZnakovaStanje[indeks] != null)
            {
                if (brojKoloneKvadratica % 3 == 2) // detektuje drugu kolonu
                {

                    if (brojRedaKvadratica % 3 == 2) // drugi clan u drugoj koloni
                    {
                        if (nizZnakova[indeks] == trenutniZnak && nizZnakova[indeks - 9 + 1] == trenutniZnak && nizZnakova[indeks + 9 - 1] == trenutniZnak)
                        {
                            mainPanel.CreateGraphics().DrawString(trenutniZnak, fontPobjednik, cetka, new PointF((velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.X + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Width - (fontPobjednik.Size)) / 2), velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.Y + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Height - (fontPobjednik.Size)) / 2));
                            pobjednikNiz[malaPripadaVelikomKvadratu[indeks]] = true;
                            nizPobjednikVelikiZnakovi[malaPripadaVelikomKvadratu[indeks]] = trenutniZnak;
                            pobjednikVelike(malaPripadaVelikomKvadratu[indeks], trenutniZnak);
                        }
                    }

                }
            }

            // dijagonalna provjera od centra prema right down

            if (nizZnakovaStanje[indeks] != null)
            {
                if (brojKoloneKvadratica % 3 == 2) // detektuje drugu kolonu
                {

                    if (brojRedaKvadratica % 3 == 2) // drugi clan u drugoj koloni
                    {
                        if (nizZnakova[indeks] == trenutniZnak && nizZnakova[indeks + 9 + 1] == trenutniZnak && nizZnakova[indeks - 9 - 1] == trenutniZnak)
                        {
                            mainPanel.CreateGraphics().DrawString(trenutniZnak, fontPobjednik, cetka, new PointF((velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.X + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Width - (fontPobjednik.Size)) / 2), velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Location.Y + (velikiKvadrati[malaPripadaVelikomKvadratu[indeks]].Height - (fontPobjednik.Size)) / 2));
                            pobjednikNiz[malaPripadaVelikomKvadratu[indeks]] = true;
                            nizPobjednikVelikiZnakovi[malaPripadaVelikomKvadratu[indeks]] = trenutniZnak;
                            pobjednikVelike(malaPripadaVelikomKvadratu[indeks], trenutniZnak);
                        }
                    }

                }
            }
        }

        void pobjednikVelike(int indeksVelikogKvadrata, string pobjednickiZnak)
        {
            // Ova metoda provjerava pobjednika na glavnoj semi, tj pobjednika velikih znakova.

            // Provjera u slucaju da se radi o pobjedniku u velikom kvadratu sa indeksom 0.

            if (indeksVelikogKvadrata == 0)
            {
                if (nizPobjednikVelikiZnakovi[0] == pobjednickiZnak && nizPobjednikVelikiZnakovi[1] == pobjednickiZnak && nizPobjednikVelikiZnakovi[2] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
                else if (nizPobjednikVelikiZnakovi[0] == pobjednickiZnak && nizPobjednikVelikiZnakovi[3] == pobjednickiZnak && nizPobjednikVelikiZnakovi[6] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
                else if (nizPobjednikVelikiZnakovi[0] == pobjednickiZnak && nizPobjednikVelikiZnakovi[8] == pobjednickiZnak && nizPobjednikVelikiZnakovi[4] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
            }

            // Provjera u slucaju da se radi o pobjedniku u velikom kvadratu sa indeksom 1.

            if (indeksVelikogKvadrata == 1)
            {
                if (nizPobjednikVelikiZnakovi[0] == pobjednickiZnak && nizPobjednikVelikiZnakovi[1] == pobjednickiZnak && nizPobjednikVelikiZnakovi[2] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
                else if (nizPobjednikVelikiZnakovi[1] == pobjednickiZnak && nizPobjednikVelikiZnakovi[4] == pobjednickiZnak && nizPobjednikVelikiZnakovi[7] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
            }

            // Provjera u slucaju da se radi o pobjedniku u velikom kvadratu sa indeksom 2.

            if (indeksVelikogKvadrata == 2)
            {
                if (nizPobjednikVelikiZnakovi[0] == pobjednickiZnak && nizPobjednikVelikiZnakovi[1] == pobjednickiZnak && nizPobjednikVelikiZnakovi[2] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
                else if (nizPobjednikVelikiZnakovi[2] == pobjednickiZnak && nizPobjednikVelikiZnakovi[5] == pobjednickiZnak && nizPobjednikVelikiZnakovi[8] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
                else if (nizPobjednikVelikiZnakovi[6] == pobjednickiZnak && nizPobjednikVelikiZnakovi[2] == pobjednickiZnak && nizPobjednikVelikiZnakovi[4] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
            }

            // Provjera u slucaju da se radi o pobjedniku u velikom kvadratu sa indeksom 3.

            if (indeksVelikogKvadrata == 3)
            {
                if (nizPobjednikVelikiZnakovi[3] == pobjednickiZnak && nizPobjednikVelikiZnakovi[0] == pobjednickiZnak && nizPobjednikVelikiZnakovi[6] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
                else if (nizPobjednikVelikiZnakovi[3] == pobjednickiZnak && nizPobjednikVelikiZnakovi[4] == pobjednickiZnak && nizPobjednikVelikiZnakovi[5] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
            }

            // Provjera u slucaju da se radi o pobjedniku u velikom kvadratu sa indeksom 4.

            if (indeksVelikogKvadrata == 4)
            {
                if (nizPobjednikVelikiZnakovi[4] == pobjednickiZnak && nizPobjednikVelikiZnakovi[0] == pobjednickiZnak && nizPobjednikVelikiZnakovi[8] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
                else if (nizPobjednikVelikiZnakovi[4] == pobjednickiZnak && nizPobjednikVelikiZnakovi[1] == pobjednickiZnak && nizPobjednikVelikiZnakovi[7] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
                else if (nizPobjednikVelikiZnakovi[4] == pobjednickiZnak && nizPobjednikVelikiZnakovi[2] == pobjednickiZnak && nizPobjednikVelikiZnakovi[6] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
            }

            // Provjera u slucaju da se radi o pobjedniku u velikom kvadratu sa indeksom 5.

            if (indeksVelikogKvadrata == 5)
            {
                if (nizPobjednikVelikiZnakovi[5] == pobjednickiZnak && nizPobjednikVelikiZnakovi[2] == pobjednickiZnak && nizPobjednikVelikiZnakovi[8] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
                else if (nizPobjednikVelikiZnakovi[5] == pobjednickiZnak && nizPobjednikVelikiZnakovi[3] == pobjednickiZnak && nizPobjednikVelikiZnakovi[4] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
            }

            // Provjera u slucaju da se radi o pobjedniku u velikom kvadratu sa indeksom 6.

            if (indeksVelikogKvadrata == 6)
            {
                if (nizPobjednikVelikiZnakovi[6] == pobjednickiZnak && nizPobjednikVelikiZnakovi[3] == pobjednickiZnak && nizPobjednikVelikiZnakovi[0] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
                else if (nizPobjednikVelikiZnakovi[6] == pobjednickiZnak && nizPobjednikVelikiZnakovi[7] == pobjednickiZnak && nizPobjednikVelikiZnakovi[8] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
                else if (nizPobjednikVelikiZnakovi[4] == pobjednickiZnak && nizPobjednikVelikiZnakovi[6] == pobjednickiZnak && nizPobjednikVelikiZnakovi[2] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
            }

            // Provjera u slucaju da se radi o pobjedniku u velikom kvadratu sa indeksom 7.

            if (indeksVelikogKvadrata == 7)
            {
                if (nizPobjednikVelikiZnakovi[7] == pobjednickiZnak && nizPobjednikVelikiZnakovi[6] == pobjednickiZnak && nizPobjednikVelikiZnakovi[8] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
                else if (nizPobjednikVelikiZnakovi[7] == pobjednickiZnak && nizPobjednikVelikiZnakovi[4] == pobjednickiZnak && nizPobjednikVelikiZnakovi[1] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
            }

            // Provjera u slucaju da se radi o pobjedniku u velikom kvadratu sa indeksom 8.

            if (indeksVelikogKvadrata == 8)
            {
                if (nizPobjednikVelikiZnakovi[8] == pobjednickiZnak && nizPobjednikVelikiZnakovi[6] == pobjednickiZnak && nizPobjednikVelikiZnakovi[7] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
                else if (nizPobjednikVelikiZnakovi[8] == pobjednickiZnak && nizPobjednikVelikiZnakovi[5] == pobjednickiZnak && nizPobjednikVelikiZnakovi[2] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
                else if (nizPobjednikVelikiZnakovi[8] == pobjednickiZnak && nizPobjednikVelikiZnakovi[4] == pobjednickiZnak && nizPobjednikVelikiZnakovi[0] == pobjednickiZnak)
                {
                    ZabranaSvihPoljaZaKlik();
                    detektovanaPobjeda = true;
                    if (pobjednickiZnak == "X")
                    {
                        MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerX.Text);
                    }
                    else MessageBox.Show("Kraj igre! Pobjednik je : " + lblPlayerO.Text);
                }
            }
            
            // Provjera da li je rezultat na kraju igre nerijesen

            isEnd = 0;

            for (int i = 0; i < pobjednikNiz.Length; i++)
            {
                if (pobjednikNiz[i] != true)
                {
                    isEnd++;
                }
            }

            if (detektovanaPobjeda == false && isEnd == 0)
            {
                MessageBox.Show("Rezultat je nerijesen! Kraj igre!");
            }
        }

        void ZabranaSvihPoljaZaKlik()
        {
            for (int i = 0; i < pobjednikNiz.Length; i++)
            {
                // Ova metoda sluzi za prolazak kroz citav niz koji u sebi cuva podatke o tome da li se u nekoj sporednoj semi desila pobjeda. Pozivamo je u slucaju kada se desi
                // pobjeda na glavnoj semi, kako bi onemogucila dalji klik u bilo kom polju, jer smo dosli do kraja igre.

                pobjednikNiz[i] = true;
            }
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Ovim kodom omogucavamo zatvaranje cijele aplikacije i Welcome forme kada se zatvori forma na kojoj se nalazi igra.
            Application.Exit();
        }

        private void btnSnapshot_Click(object sender, EventArgs e)
        {
            // Ovim kodom omogucavamo snapshot ekrana u trenutku kada se klikne dugme Snapshot.
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            // Ova linija koda nam omogucava da dohvatimo putanju Desktop-a korisnika kako bi slika bila snimljena na Desktop.
            string putanja = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            bmp.Save(putanja + @"\Snap.png");
            string kompletnaPutanja = putanja + @"\Snap.png";
            MessageBox.Show("Snapshot je snimljen na lokaciji: " + kompletnaPutanja);
        }
    }
}
