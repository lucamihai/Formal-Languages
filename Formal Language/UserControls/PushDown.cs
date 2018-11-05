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

                    for (int secondIndex = 0; secondIndex < cuvinte.Length - 1; secondIndex++)
                    {
                        actiuni[fileLine, secondIndex] = cuvinte[secondIndex];
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
                for (; fileLine < fileLine + stari.Length; fileLine++)
                {
                    cuvinte = fileContentsArray[fileLine].Split();

                    for (int secondIndex = 0; secondIndex < cuvinte.Length; secondIndex++)
                    {
                        salt[fileLine, secondIndex] = cuvinte[secondIndex];
                    }
                }

            }

            else
            {
                textBoxOutput.Text = "Data couldn't be read... Make sure you selected a file.";
            }
        }
    }
}
