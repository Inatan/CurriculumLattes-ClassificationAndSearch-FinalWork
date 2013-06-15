using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            FolderBrowserDialog folderbrowser = new FolderBrowserDialog();
            folderbrowser.Description = "Selecione o diretório que contem os arquivos .xml";
            if (folderbrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //quando selecionar, guarda caminho de cada file em um vetor de strings
                string[] files = Directory.GetFiles(folderbrowser.SelectedPath);
                foreach (string s in files)
                {
                    //para cada caminho, abrir o arquivo XML
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(s);

                    XmlElement root = (XmlElement)xmldoc.DocumentElement.FirstChild;
                    XmlNodeList next = xmldoc.DocumentElement.ChildNodes;

                    foreach (XmlNode filho in next)
                    {
                        XmlElement elemento = (XmlElement)filho.FirstChild.FirstChild;
                        if (elemento.HasAttribute("BAIRRO"))
                        {
                            string curso = elemento.GetAttribute("BAIRRO");
                            MessageBox.Show(curso);
                        }
                    }

                    //Aqui eu faço um cast para transformar o .FirstChild que devolve um nodo para um elemento,
                    //assim podendo ler os atributos

                    if (root.HasAttribute("NOME-COMPLETO"))
                    {
                        String nome = root.GetAttribute("NOME-COMPLETO"); //WORKS BITCH
                        MessageBox.Show(nome);
                    }





                }

            }
        }

    }
}