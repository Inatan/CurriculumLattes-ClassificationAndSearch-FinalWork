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
                            Program.estru.conf = new conferencias();
                        // adiciona dados da conferencia
                           // if (codigo.GetAttribute("TITULO-DO-TRABALHO") != "")
                        // {
                            
                            Program.estru.conf.adiciona(Program.estru.coferencia.Count, codigo.GetAttribute("TITULO-DO-TRABALHO"), natureza, data, contador, autor, coautores);
                        /*    Debug.WriteLine(Program.estru.conf.codigo);
                            Debug.WriteLine(Program.estru.conf.ano);
                            Debug.WriteLine(Program.estru.conf.quantcoautores);*/
                           // Debug.WriteLine(Program.estru.conf.titulo);
                           // Debug.WriteLine(Program.estru.conf.autor);
                            Program.estru.coferencia.Add(Program.estru.conf); // adiciona na lista de conferencias
                         //}
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
                            Program.estru.artig = new artigo();
                            //if (codigo.GetAttribute("TITULO-DO-TRABALHO") != "")
                            //{
                                
                                Program.estru.artig.adiciona(Program.estru.artigo.Count, codigo.GetAttribute("TITULO-DO-ARTIGO"), natureza, data, contador, autor, coautores);
                                /* Debug.WriteLine(Program.estru.artig.codigo);
                                 Debug.WriteLine(Program.estru.artig.ano);
                                 Debug.WriteLine(Program.estru.artig.natureza);
                                 Debug.WriteLine(Program.estru.artig.quantcoautores);*/
                                // Debug.WriteLine(Program.estru.artig.titulo);
                                // Debug.WriteLine(Program.estru.artig.autor);
                                Program.estru.artigo.Add(Program.estru.artig);
                           // }
                    }
                    i++; // contador utilizado para cada xml, para atribuir cada autor
                }
            }
            Array.Sort(Program.estru.autor, delegate(autores x, autores y) { return x.nome.CompareTo(y.nome); }); // seleciona a chave para ordenar a classe a travez dessa chave
            for (int i = 0; i < 50 && Program.estru.autor[i].nome != null; i++) // deixa em ordem alfabetica
            {
                listBox1.Items.Add(Program.estru.autor[i].nome); // coloca na textlistbox
            }

            for (int j = 0; j < Program.estru.artigo.Count; j++) // imprime os periodicos e as conferencias na outra lista
            {
                listBox2.Items.Add(Program.estru.artigo[j].titulo);
            }
            for (int j = 0; j < Program.estru.coferencia.Count; j++)
            {
                listBox2.Items.Add(Program.estru.coferencia[j].titulo);
            }

            //teste da manipulação de arquivos
          //  escreveArquivos.escreveAutores(Program.estru.autor);
           // leArquivos.leAutores();
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public object OrderDetailsTable { get; set; }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear(); // paga tudo
            for (int i = 0; i < Program.estru.artigo.Count; i++) //ordena tipo artigo
                listBox2.Items.Add(Program.estru.artigo[i].titulo);
            for (int i = 0; i < Program.estru.coferencia.Count; i++)
                listBox2.Items.Add(Program.estru.coferencia[i].titulo);
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.qualis.CompareTo(y.qualis); });
            Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.qualis.CompareTo(y.qualis); });
            int i = Program.estru.coferencia.Count - 1; // começa do maior para o menor
            int j = Program.estru.artigo.Count - 1;
            while (j > 0 && i > 0) // compara até o laço for zero
               
            {
                if (string.Compare(Program.estru.artigo[j].qualis, Program.estru.coferencia[i].qualis) < 0) // testa a ordem alfabetica e coloca o maior na lista
                {
                    listBox2.Items.Add(Program.estru.artigo[j].qualis);
                    j--;
                }
                else
                {
                    listBox2.Items.Add(Program.estru.coferencia[i].qualis);
                    i--;
                }
            }


        }

        private void button3_Click(object sender, EventArgs e) // processo igual porem usando quantidade de coautores
        {

            listBox2.Items.Clear(); 
            Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.quantcoautores.CompareTo(y.quantcoautores); });
            Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.quantcoautores.CompareTo(y.quantcoautores); });
            int i = Program.estru.coferencia.Count-1;
            int j = Program.estru.artigo.Count-1;
            while(j>0 && i>0)
            {
                if (Program.estru.artigo[j].quantcoautores > Program.estru.coferencia[i].quantcoautores)
                {
                    listBox2.Items.Add(Program.estru.artigo[j].titulo);
                        j--;
                }
                else
                {
                    listBox2.Items.Add(Program.estru.coferencia[i].titulo);
                    i--;
                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear(); // coloca na lista primeiro as conferencias
            for(int i=0; i<Program.estru.coferencia.Count;i++)
                listBox2.Items.Add(Program.estru.coferencia[i].titulo);
            for (int i = 0; i < Program.estru.artigo.Count; i++)
                listBox2.Items.Add(Program.estru.artigo[i].titulo);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();// faz o contrario com a natureza imprime primeiro a menor
            Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.natureza.CompareTo(y.natureza); });
            Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.natureza.CompareTo(y.natureza); });
            int i = 0;
            int j = 0;
            while (j < Program.estru.artigo.Count && i < Program.estru.coferencia.Count)
            {
                if (Program.estru.artigo[j].natureza > Program.estru.coferencia[i].natureza)
                {
                    listBox2.Items.Add(Program.estru.artigo[j].titulo);
                    j++;
                }
                else
                {
                    listBox2.Items.Add(Program.estru.coferencia[i].titulo);
                    i++;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear(); // imprime só os resumos caso maior para o menor
            Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.natureza.CompareTo(y.natureza); });
            Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.natureza.CompareTo(y.natureza); });
            int i = Program.estru.coferencia.Count - 1;
            int j = Program.estru.artigo.Count - 1;
            while (j > 0 && i > 0)
            {
                if (Program.estru.artigo[j].natureza > Program.estru.coferencia[i].natureza)
                {
                    listBox2.Items.Add(Program.estru.artigo[j].titulo);
                    j--;
                }
                else
                {
                    listBox2.Items.Add(Program.estru.coferencia[i].titulo);
                    i--;
                }
            }
            
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.natureza.CompareTo(y.natureza); });
            Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.natureza.CompareTo(y.natureza); });
            int i = 0;
            int j = 0;
            while (j < Program.estru.artigo.Count && i < Program.estru.coferencia.Count) // primeiro busca os estendidos e imprime
            {
                if (Program.estru.artigo[j].natureza > Program.estru.coferencia[i].natureza)
                {
                    if(Program.estru.artigo[j].natureza == 1)
                        listBox2.Items.Add(Program.estru.artigo[j].titulo);
                    j++;
                }
                else
                {
                    if (Program.estru.artigo[j].natureza == 1)
                        listBox2.Items.Add(Program.estru.coferencia[i].titulo);
                    i++;
                }
            }
            i = 0;
            j = 0;
            while (j < Program.estru.artigo.Count && i < Program.estru.coferencia.Count)// faz o laço novamente e imprime o resto
            {
                if (Program.estru.artigo[j].natureza > Program.estru.coferencia[i].natureza)
                {
                    if (Program.estru.artigo[j].natureza != 1)
                        listBox2.Items.Add(Program.estru.artigo[j].titulo);
                    j++;
                }
                else
                {
                    if (Program.estru.artigo[j].natureza != 1)
                        listBox2.Items.Add(Program.estru.coferencia[i].titulo);
                    i++;
                }
            }
        }
    }
}

