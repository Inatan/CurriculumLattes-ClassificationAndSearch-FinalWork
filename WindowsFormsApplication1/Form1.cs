using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using XML;
using System.Diagnostics;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowser = new FolderBrowserDialog();
            folderbrowser.Description = "Selecione o diretório que contém os arquivos .xml";
            if (folderbrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //quando selecionar, guarda caminho de cada file em um vetor de strings
                int i = 0; // contador
                string[] files = Directory.GetFiles(folderbrowser.SelectedPath);
                foreach (string s in files)
                {
                    //para cada caminho, abrir o arquivo XML
                    
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(s);




                    //if (xmldoc.DocumentElement.NodeType == XmlNodeType.XmlDeclaration)
                    //{
                    // XmlDeclaration dec = (XmlDeclaration)xmldoc.FirstChild;
                    // Debug.WriteLine(dec.Encoding);    
                    // dec.Encoding = "UTF-8";
                    // }

                    XmlElement root = (XmlElement)xmldoc.DocumentElement.FirstChild;
                    XmlNodeList next = xmldoc.DocumentElement.ChildNodes;
                    string referencia;

                    if (root.HasAttribute("NOME-COMPLETO"))
                    {
                        String nome = root.GetAttribute("NOME-COMPLETO");
                        Debug.WriteLine(nome);
                    }

                    if (root.HasAttribute("NOME-EM-CITACOES-BIBLIOGRAFICAS"))
                    {
                        referencia = root.GetAttribute("NOME-EM-CITACOES-BIBLIOGRAFICAS");
                        Debug.WriteLine(referencia);
                    }
                    foreach (XmlElement artigos in next[1].ChildNodes[1])
                    {
                        XmlElement codigo = (XmlElement)artigos.ChildNodes[0];
                        XmlNode procura = artigos;
                        Debug.WriteLine(codigo.GetAttribute("TITULO-DO-ARTIGO"));
                        Debug.WriteLine(codigo.GetAttribute("NATUREZA"));
                        Debug.WriteLine(codigo.GetAttribute("ANO-DO-ARTIGO"));
                        foreach (XmlElement autoria in procura.SelectNodes("AUTORES"))
                        {
                            if (autoria.GetAttribute("NOME-COMPLETO-DO-AUTOR") == root.GetAttribute("NOME-COMPLETO"))
                                Debug.Write("AUTOR->");

                                Debug.WriteLine(autoria.GetAttribute("NOME-COMPLETO-DO-AUTOR"));
                        }

                    }
                    //Aqui eu faço um cast para transformar o .FirstChild que devolve um nodo para um elemento,
                    //assim podendo ler os atributos




                }
            }
        }
    }
}

