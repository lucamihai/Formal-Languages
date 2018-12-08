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
        List<string> elemente;

        #region Citire din fisier

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
                elemente = ObtineElemente();

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

        List<string> ObtineElemente()
        {
            List<string> elemente = new List<string>();

            foreach (string terminal in terminali)
                elemente.Add(terminal);

            foreach (string neterminal in neterminali)
                elemente.Add(neterminal);

            return elemente;
        }

        #endregion

        #region Generare

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            string primaProductie = productii[0];

            string[] elementeProductie = primaProductie.Split();
            string neterminal = elementeProductie[0];

            string rezultat = neterminal + " ." + neterminal;

            List<string> listaStart = new List<string>();
            listaStart.Add(rezultat);

            List< List<string> > listeGenerate = new List< List<string> >();
            List< List<string> > listeMarcate = new List< List<string> >();

            List<string> I0 = new List<string>();
            foreach (string start in listaStart)
            {
                I0.Add(start);
            }

            List<string> chestii = GenereazaIn(listaStart, productii);
            foreach (string chestie in chestii)
            {
                I0.Add(chestie);
            }

            listeGenerate.Add(I0);


            List<string> elemente = ElementeDupaPunct(I0);
            foreach (string element in elemente)
            {
                List<string> salt = Salt(I0, element);
                listeGenerate.Add(salt);
            }

            while (listeGenerate.Count - 1 != listeMarcate.Count)
            {
                FaCeva(listeGenerate, listeMarcate);
            }

            int i = 10;
        }

        List<string> GenereazaProductii(List<string> lista)
        {
            List<string> rezultat = new List<string>();

            List<string> neterminaliGasiti = new List<string>();


            return rezultat;
        }

        List<string> GenereazaIn(List<string> start, List<string> productii)
        {
            List<string> rezultat = new List<string>();

            List<string> neterminaliGasiti = new List<string>();
            string neterminal = NeterminalDupaPunct(start, neterminali, neterminaliGasiti);
            while (neterminal != null)
            {
                foreach(string productie in productii)
                {
                    string[] elementeProductie = productie.Split();
                    string neterminalProductie = elementeProductie[0];
                    string valoareProductie = elementeProductie[1];

                    if (neterminalProductie == neterminal)
                    {
                        string productieGenerata = neterminalProductie + " ." + valoareProductie;
                        rezultat.Add(productieGenerata);
                    }
                }

                neterminal = NeterminalDupaPunct(rezultat, neterminali, neterminaliGasiti);
            }

            return rezultat;
        }

        string NeterminalDupaPunct(List<string> productii, List<string> neterminali, List<string> neterminaliGasiti)
        {
            foreach (string productie in productii)
            {
                string[] elementeProductie = productie.Split();
                string valoareProductie = elementeProductie[1];
                int indexPunct = valoareProductie.IndexOf('.');

                // N-a fost gasit punctul
                if (indexPunct == -1)
                {
                    continue;
                }

                string dupaPunct = valoareProductie.Substring(indexPunct + 1);
                foreach (string neterminal in neterminali)
                {
                    
                    if (dupaPunct.StartsWith(neterminal))
                    {
                        if (!neterminaliGasiti.Contains(neterminal))
                        {
                            neterminaliGasiti.Add(neterminal);
                            return neterminal;
                        }
                            
                    }
                }
            }

            return null;
        }

        List<string> ElementeDupaPunct(List<string> In)
        {
            List<string> elementeDupaPunct = new List<string>();

            foreach (string productie in In)
            {
                string[] elementeProductie = productie.Split();
                string valoareProductie = elementeProductie[1];
                int indexPunct = valoareProductie.IndexOf('.');

                // N-a fost gasit punctul
                if (indexPunct == -1)
                {
                    continue;
                }

                string dupaPunct = valoareProductie.Substring(indexPunct + 1);
                foreach (string element in elemente)
                {
                    if (dupaPunct.StartsWith(element))
                    {
                        if (!elementeDupaPunct.Contains(element))
                        {
                            elementeDupaPunct.Add(element);
                        }
                    }
                }
            }

            return elementeDupaPunct;
        }

        List<string> ProductiiCuElementDupaPunctCuTerminal(List<string> In, string element)
        {
            List<string> productii = new List<string>();

            foreach (string productie in In)
            {
                string[] elementeProductie = productie.Split();
                string valoareProductie = elementeProductie[1];
                int indexPunct = valoareProductie.IndexOf('.');

                // N-a fost gasit punctul
                if (indexPunct == -1)
                {
                    continue;
                }

                string dupaPunct = valoareProductie.Substring(indexPunct + 1);
                
                if (dupaPunct.StartsWith(element))
                {
                    if (!productii.Contains(productie))
                    {
                        productii.Add(productie);
                    }
                }
                
            }

            return productii;
        }

        List<string> Salt(List<string> In, string elementSalt)
        {
            List<string> rezultat = new List<string>();

            if ( EsteNeterminal(elementSalt) )
            {
                foreach (string productie in In)
                {
                    string[] elementeProductie = productie.Split();
                    string neterminalProductie = elementeProductie[0];
                    string valoareProductie = elementeProductie[1];


                    int indexPunct = valoareProductie.IndexOf('.');

                    // N-a fost gasit punctul
                    if (indexPunct == -1)
                    {
                        return null;
                    }

                    // Punctul e la capatul sirului
                    if (indexPunct == valoareProductie.Length - 1)
                    {
                        return null;
                    }

                    string dupaPunct = valoareProductie.Substring(indexPunct + 1);
                    foreach (string element in elemente)
                    {
                        if (dupaPunct.StartsWith(element) && element == elementSalt)
                        {
                            string inchidere = InchidereMultime(productie);
                            rezultat.Add(inchidere);
                        }
                    }


                }
            }

            if ( EsteTerminal(elementSalt) )
            {
                List<string> productiiCuElementDupaPunct = ProductiiCuElementDupaPunctCuTerminal(In, elementSalt);
                List<string> toateProductiile = new List<string>();

                for (int index = 0; index < productiiCuElementDupaPunct.Count; index++)
                {
                    productiiCuElementDupaPunct[index] = InchidereMultime(productiiCuElementDupaPunct[index]);
                    rezultat.Add(productiiCuElementDupaPunct[index]);
                }


                List<string> generare = GenereazaIn(productiiCuElementDupaPunct, productii);
                foreach (string productie in generare)
                {
                    rezultat.Add(productie);
                }
            }

            return rezultat;
        }

        string InchidereMultime(string productie)
        {
            string rezultat = null;

            string[] elementeProductie = productie.Split();

            string neterminalProductie = elementeProductie[0];
            string valoareProductie = elementeProductie[1];
            int indexPunct = valoareProductie.IndexOf('.');

            // N-a fost gasit punctul
            if (indexPunct == -1)
            {
                return null;
            }

            // Punctul e la capatul sirului
            if (indexPunct == valoareProductie.Length - 1)
            {
                return null;
            }

            string dupaPunct = valoareProductie.Substring(indexPunct + 1);
            foreach (string element in elemente)
            {
                if (dupaPunct.StartsWith(element))
                {
                    int numarCaractereElement = element.Length;

                    // Inserare punct dupa element
                    rezultat = valoareProductie.Insert(indexPunct + numarCaractereElement + 1, ".");

                    // Stergere punct din fara elementului
                    rezultat = rezultat.Remove(indexPunct, 1);
                }
            }

            rezultat = neterminalProductie + " " + rezultat;
            return rezultat;
        }

        bool EsteTerminal(string element)
        {
            foreach (string terminal in terminali)
            {
                if (element == terminal)
                {
                    return true;
                }
            }

            return false;
        }

        bool EsteNeterminal(string element)
        {
            foreach (string neterminal in neterminali)
            {
                if (element == neterminal)
                {
                    return true;
                }
            }

            return false;
        }

        void FaCeva( List< List<string> > listeGenerate, List< List<string> > listeMarcate)
        {
            List<List<string>> listeDeAdaugat = new List<List<string>>();
            foreach (List<string> In in listeGenerate)
            {
                List<string> elemente = ElementeDupaPunct(In);
                foreach (string element in elemente)
                {
                    List<string> salt = Salt(In, element);

                    if (!ContineLista(listeGenerate, salt))
                    {
                        listeDeAdaugat.Add(salt);
                    }
                    else
                    {
                        if (!ContineLista(listeMarcate, salt))
                        {
                            listeMarcate.Add(salt);
                        }
                        
                    }
                }
            }

            foreach (List<string> lista in listeDeAdaugat)
            {
                listeGenerate.Add(lista);
            }
        }

        public bool ContineLista(List< List<string> > liste, List<string> listaCautata)
        {
            bool rezultat = false;

            foreach(List<string> lista in liste)
            {
                int counterElementeGasite = 0;
                foreach (string elementLista in lista)
                {
                    if (listaCautata.Contains(elementLista))
                        counterElementeGasite++;
                }

                if (counterElementeGasite == listaCautata.Count)
                {
                    rezultat = true;
                    break;
                }
            }

            return rezultat;
        }

        #endregion
    }
}
