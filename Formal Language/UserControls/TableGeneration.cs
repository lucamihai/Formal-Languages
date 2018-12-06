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
    public partial class TableGeneration : UserControl
    {
        public TableGeneration()
        {
            InitializeComponent();
        }

        List<string> contents;

        List<string> terminali;
        List<string> neterminali;
        List<string> productii;

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                contents = new List<string>();
                System.IO.StreamReader streamReader = new System.IO.StreamReader(openFileDialog.FileName);
                string line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    contents.Add(line);
                }

                int ceva = 1;

                terminali = ObtineTerminai(contents);
                neterminali = ObtineNeterminali(contents);
                productii = ObtineProductii(contents);

                buttonGenerate.Enabled = true;
            }
        }

        List<string> ObtineTerminai(List<string> contents)
        {
            string linieTerminali = contents[0];
            string[] vectorTerminali = linieTerminali.Split();

            List<string> terminali = new List<string>();
            foreach (string terminal in vectorTerminali)
                terminali.Add(terminal);

            return terminali;
        }

        List<string> ObtineNeterminali(List<string> contents)
        {
            string linieNeterminali = contents[1];
            string[] vectorNeterminali = linieNeterminali.Split();

            List<string> neterminali = new List<string>();
            foreach (string neterminal in vectorNeterminali)
                neterminali.Add(neterminal);

            return neterminali;
        }

        List<string> ObtineProductii(List<string> contents)
        {
            List<string> productii = new List<string>();
            for (int index = 2; index < contents.Count; index++)
            {
                productii.Add(contents[index]);
            }
            return productii;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            string primaProductie = productii[0];

            string[] elementeProductie = primaProductie.Split();
            string neterminal = elementeProductie[0];

            string rezultat = neterminal + " ." + neterminal;

            List<string> listaStart = new List<string>();
            listaStart.Add(rezultat);

            List<string> I0 = GenereazaProductii(listaStart);

            //string rez = NeterminalDupaPunct(listaStart, neterminali);
        }

        List<string> GenereazaProductii(List<string> lista)
        {
            List<string> rezultat = new List<string>();

            List<string> neterminaliGasiti = new List<string>();


            return rezultat;
        }

        string NeterminalDupaPunct(List<string> productii, List<string> neterminali, List<string> neterminaliGasiti)
        {
            foreach (string productie in productii)
            {
                string[] elementeProductie = productie.Split();
                string valoareProductie = elementeProductie[1];
                int indexPunct = valoareProductie.IndexOf('.');

                foreach (string neterminal in neterminali)
                {
                    string dupaPunct = valoareProductie.Substring(indexPunct + 1);

                    if (dupaPunct.StartsWith(neterminal))
                    {
                        if (!neterminaliGasiti.Contains(neterminal))
                            return neterminal;
                    }
                }
            }

            return null;
        }
    }
}
