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
            FolderBrowserDialog folderbrowser = new FolderBrowserDialog(); // cria uma janela para abrir pasta
            folderbrowser.Description = "Selecione o diretório que contém os arquivos .xml"; //texto da janela
            if (folderbrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)// se a pasta tiver xml
            {
                //quando selecionar, guarda caminho de cada file em um vetor de strings
                int i = 0; // contador
                string[] files = Directory.GetFiles(folderbrowser.SelectedPath); //pega todos os xml
                foreach (string s in files) //pega os xml e transfora em nomes, em cada nome de xml é utilizado para abrir todos os xml
                {
                    XmlDocument xmldoc = new XmlDocument(); //atribui o movimento
                    xmldoc.Load(s); // abre o xml com um determinado arquivo xml para a leitura
                    XmlElement root = (XmlElement)xmldoc.DocumentElement.FirstChild; // elementos do nodo CURRICULO-VITAE/DADO-GERAIS
                    XmlNodeList next = xmldoc.DocumentElement.ChildNodes;// todos os nodos filhos do CURRICULO-VITAE
                    string referencia; // referencia bibliografica
                    String nome = root.GetAttribute("NOME-COMPLETO"); // pega o nome completo e coloca e nome para adicionar na estrutura do autor
                    referencia = root.GetAttribute("NOME-EM-CITACOES-BIBLIOGRAFICAS"); // pega a referencia como é dada para esse autor
                    Program.estru.declara_autor(i); // constroi o autor no array
                    Program.estru.autor[i].adiciona(i, root.GetAttribute("NOME-EM-CITACOES-BIBLIOGRAFICAS"), root.GetAttribute("NOME-COMPLETO"));// atribuit todos os dados do autor
                    if (next[1].ChildNodes[0] != null)// verifica se o nodo não é vazio para evitar erros de referencia
                    
                    foreach (XmlElement artigos in next[1].ChildNodes[0]) //transforma o nodo PRODUCAO-BIBLIOGRAFICA/TRABALHOS-EM-EVENTOS em um elemento para esse nodo
                    {
                        XmlElement codigo = (XmlElement)artigos.ChildNodes[0]; // pega o elemento de DADOS-BASICOS-DO-TRABALHO 
                        XmlNode procura = artigos; // pega a estrutura nodo TRABALHOS-EM-EVENTOS
                        string autor="";
                        List<string> coautores = new List<string>();// para pegar o nome dos autores
                       // dar valor da natureza da conferencia
                        int natureza = 0;// para COMPLETO
                        if (codigo.GetAttribute("NATUREZA") == "RESUMO")
                            natureza = 2;//para RESUMO
                        if (codigo.GetAttribute("NATUREZA") == "ESTENDIDO")
                            natureza = 1;//para ESTENDIDO
                        //contador dos coautores
                        int contador = 0;
                        
                            foreach (XmlElement autoria in procura.SelectNodes("AUTORES"))// pesquisa todos os nodos AUTORES
                            {
                                if (autoria.GetAttribute("ORDEM-DE-AUTORIA") == "1")
                                {
                                    autor = autoria.GetAttribute("NOME-COMPLETO-DO-AUTOR");
                                }
                                else
                                {
                                    coautores.Add(autoria.GetAttribute("NOME-COMPLETO-DO-AUTOR")); // da o nome completo desse co autor
                                    contador++; // adiciona coautor
                                }
                            }
                            int data=0; // se a data for 0 é considerado que não foi dada ou está com erro
                            if (codigo.GetAttribute("ANO-DO-ARTIGO") == "") // caso não seja dada a data
                                data = 0;
                            else
                                data = Convert.ToInt32(codigo.GetAttribute("ANO-DO-ARTIGO"));
                            if (data > 2013 || data < 1900)// para evitar anos absurdos
                                data = 0;
                        // adiciona dados da conferencia
                            Program.estru.conf.adiciona(Program.estru.coferencia.Count, codigo.GetAttribute("TITULO-DO-TRABALHO"), natureza, data, contador, autor, coautores);
                        /*    Debug.WriteLine(Program.estru.conf.codigo);
                            Debug.WriteLine(Program.estru.conf.ano);
                            Debug.WriteLine(Program.estru.conf.quantcoautores);
                            Debug.WriteLine(Program.estru.conf.titulo);
                            Debug.WriteLine(Program.estru.conf.autor);*/
                            Program.estru.coferencia.Add(Program.estru.conf); // adiciona na lista de conferencias
                        }


                    //  Debug.WriteLine(estru.autor[i].nome);
                    //  Debug.WriteLine(estru.autor[i].refbibliografica);
                     // Debug.WriteLine(estru.autor[i].codigo);
                   if(next[1].ChildNodes[1]!=null) // repete o processo das conferencia mas agora com os arquivos periódigos
                       foreach (XmlElement artigos in next[1].ChildNodes[1]) // nodo PRODUCAO-BIBLIOGRAFICA/ARTIGOS-PUBLICADOS
                    {
                        XmlElement codigo = (XmlElement)artigos.ChildNodes[0];
                        XmlNode procura = artigos;
                        string autor="";
                        List<string> coautores = new List<string>();
                        int contador = 0;
                          
                        //Debug.WriteLine(codigo.GetAttribute("TITULO-DO-ARTIGO"));
                        //Debug.WriteLine(codigo.GetAttribute("NATUREZA"));
                        //Debug.WriteLine(codigo.GetAttribute("ANO-DO-ARTIGO"));
                            int natureza=0;
                            if (codigo.GetAttribute("NATUREZA") == "RESUMO")
                                natureza = 2;
                            if (codigo.GetAttribute("NATUREZA") == "ESTENDIDO")
                                natureza = 1;

                            foreach (XmlElement autoria in procura.SelectNodes("AUTORES"))
                            {
                                if (autoria.GetAttribute("ORDEM-DE-AUTORIA") == "1")
                                {
                                    autor = autoria.GetAttribute("NOME-COMPLETO-DO-AUTOR");
                                }
                                else
                                {
                                    coautores.Add(autoria.GetAttribute("NOME-COMPLETO-DO-AUTOR")); // da o nome completo desse co autor
                                    contador++; // adiciona coautor
                                }
                            }
                            int data;
                            if (codigo.GetAttribute("ANO-DO-ARTIGO") == "")
                                data = 0;
                            else
                                data=  Convert.ToInt32(codigo.GetAttribute("ANO-DO-ARTIGO"));
                            if (data > 2013 && data < 1900)
                                data = 0;
                            Program.estru.artig.adiciona(Program.estru.artigo.Count, codigo.GetAttribute("TITULO-DO-ARTIGO"), natureza,data, contador, autor, coautores);
                           /* Debug.WriteLine(Program.estru.artig.codigo);
                            Debug.WriteLine(Program.estru.artig.ano);
                            Debug.WriteLine(Program.estru.artig.natureza);
                            Debug.WriteLine(Program.estru.artig.quantcoautores);
                            Debug.WriteLine(Program.estru.artig.titulo);
                            Debug.WriteLine(Program.estru.artig.autor);*/
                            Program.estru.artigo.Add(Program.estru.artig);
                        
                    }
                    i++; // contador utilizado para cada xml, para atribuir cada autor
                }
            }
            Array.Sort(Program.estru.autor, delegate(autores x, autores y) { return x.nome.CompareTo(y.nome); }); // black magic man!!!
            for (int i = 0; i < 50 && Program.estru.autor[i].nome != null; i++)
            {
                listBox1.Items.Add(Program.estru.autor[i].nome);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public object OrderDetailsTable { get; set; }
    }
}

