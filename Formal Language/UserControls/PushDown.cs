using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formal_Language.UserControls
{
    public partial class PushDown : UserControl
    {
        string[,] actiuni;
        string[,] salt;
        string[,] productii;

        string[] terminale;
        string[] stari;
        string[] terminaleSalt;
        

        Stack<string> stivaIntrare;

        public PushDown()
        {
            InitializeComponent();
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader streamReader = new System.IO.StreamReader(openFileDialog1.FileName);
                string line;
                int lineCounter = 0;
                stivaIntrare = new Stack<string>();

                string fileContents = streamReader.ReadToEnd();
                string[] fileContentsArray = fileContents.Split(
                    new[] { Environment.NewLine },
                    StringSplitOptions.None
                );

                string[] cuvinte;

                // ----- First line
                cuvinte = fileContentsArray[0].Split();

                for (int index = cuvinte.Length - 1; index >= 0; index--)
                {
                    stivaIntrare.Push(cuvinte[index]);
                }


                // ----- Second line
                cuvinte = fileContentsArray[1].Split();

                stari = new string[cuvinte.Length];

                for (int index = 0; index < cuvinte.Length; index++)
                {
                    stari[index] = cuvinte[index];
                }


                // ----- Third line
                cuvinte = fileContentsArray[2].Split();

                terminale = new string[cuvinte.Length];

                for (int index = 0; index < cuvinte.Length; index++)
                {
                    terminale[index] = cuvinte[index];
                }


                actiuni = new string[stari.Length, terminale.Length];


                // ----- Tabel actiuni
                int fileLine = 3;
                for (; fileLine < 3 + stari.Length; fileLine++)
                {
                    cuvinte = fileContentsArray[fileLine].Split();

                    for (int secondIndex = 0; secondIndex < cuvinte.Length; secondIndex++)
                    {
                        actiuni[fileLine - 3, secondIndex] = cuvinte[secondIndex];
                    }
                }


                // ----- Linia cu terminalele salturilor
                cuvinte = fileContentsArray[fileLine].Split();
                terminaleSalt = new string[cuvinte.Length];

                for (int index = 0; index < cuvinte.Length; index++)
                {
                    terminaleSalt[index] = cuvinte[index];
                }

                salt = new string[stari.Length, terminaleSalt.Length];


                // ----- Citire terminale salturi :|
                fileLine++;
                int oldFileLine = fileLine;
                for (; fileLine < oldFileLine + stari.Length; fileLine++)
                {
                    cuvinte = fileContentsArray[fileLine].Split();

                    for (int secondIndex = 0; secondIndex < cuvinte.Length; secondIndex++)
                    {
                        salt[fileLine - oldFileLine, secondIndex] = cuvinte[secondIndex];
                    }
                }

                int size = fileContentsArray.Length - fileLine;
                productii = new string[size + 1, 2];
                int numarProductie = 1;
                for (; fileLine < fileContentsArray.Length; fileLine++, numarProductie++)
                {
                    cuvinte = fileContentsArray[fileLine].Split();

                    productii[numarProductie, 0] = cuvinte[1];
                    productii[numarProductie, 1] = cuvinte[0];
                }

            }

            else
            {
                textBoxOutput.Text = "Data couldn't be read... Make sure you selected a file.";
            }
        }
    }
}
