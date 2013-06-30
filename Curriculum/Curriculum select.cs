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
            //criar arquivo pras produções dos autores
            string nomeArq = "prodAut.bin";
            if (System.IO.File.Exists("autores.bin") || System.IO.File.Exists("periodicos.bin") || System.IO.File.Exists("conferencias.bin") || System.IO.File.Exists(nomeArq))
            {
                MessageBox.Show("Arquivo já existe, clique em carregar");
            }
            else
            {
                FileStream stream = new FileStream(nomeArq, FileMode.CreateNew);
                BinaryWriter binario = new BinaryWriter(stream);

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
                        Program.estru.autor[i].adiciona(i, root.GetAttribute("NOME-EM-CITACOES-BIBLIOGRAFICAS"), root.GetAttribute("NOME-COMPLETO"), root.GetAttribute("CIDADE-NASCIMENTO"), root.GetAttribute("PAIS-DE-NASCIMENTO"));// atribuit todos os dados do autor
                        if (next[1].ChildNodes[0] != null)// verifica se o nodo não é vazio para evitar erros de referencia

                            foreach (XmlElement artigos in next[1].ChildNodes[0]) //transforma o nodo PRODUCAO-BIBLIOGRAFICA/TRABALHOS-EM-EVENTOS em um elemento para esse nodo
                            {
                                XmlElement codigo = (XmlElement)artigos.ChildNodes[0];
                                XmlElement detalhe = (XmlElement)artigos.ChildNodes[1];// pega o elemento de DADOS-BASICOS-DO-TRABALHO 
                                XmlNode procura = artigos; // pega a estrutura nodo TRABALHOS-EM-EVENTOS
                                string autor = "";
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
                                        binario.Write(Program.estru.coferencia.Count + Program.estru.artigo.Count);
                                        binario.Write(autoria.GetAttribute("NOME-COMPLETO-DO-AUTOR"));
                                        binario.Flush();
                                    }
                                }
                                int data = 0; // se a data for 0 é considerado que não foi dada ou está com erro
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
                                
                                Program.estru.conf.adiciona(Program.estru.coferencia.Count + Program.estru.artigo.Count, codigo.GetAttribute("TITULO-DO-TRABALHO"), natureza, data, contador, autor);
                                Program.estru.conf.da_qualis(leArquivos.achaQUALISC(detalhe.GetAttribute("NOME-DO-EVENTO")));
                                //*******************************************************************************************************************************//
                                //.Código
                                //binario.Write(Program.estru.autor[i].codigo); 
                                //     binario.Write(Program.estru.conf.codigo);
                                //LEMMMMMMMMMMMMMMMBRAR     
                                /*    Debug.WriteLine(Program.estru.conf.codigo);
                                    Debug.WriteLine(Program.estru.conf.ano);
                                    Debug.WriteLine(Program.estru.conf.quantcoautores);*/
                                // Debug.WriteLine(Program.estru.conf.titulo);
                                // Debug.WriteLine(Program.estru.conf.autor);
                                Program.estru.conf.titulo = Program.estru.conf.titulo.TrimStart();
                                if (string.Compare(Program.estru.conf.titulo, "") != 0)
                                    Program.estru.coferencia.Add(Program.estru.conf); // adiciona na lista de conferencias
                                //}
                            }


                        //  Debug.WriteLine(estru.autor[i].nome);
                        //  Debug.WriteLine(estru.autor[i].refbibliografica);
                        // Debug.WriteLine(estru.autor[i].codigo);
                        if (next[1].ChildNodes[1] != null) // repete o processo das conferencia mas agora com os arquivos periódigos
                            foreach (XmlElement artigos in next[1].ChildNodes[1]) // nodo PRODUCAO-BIBLIOGRAFICA/ARTIGOS-PUBLICADOS
                            {
                                XmlElement codigo = (XmlElement)artigos.ChildNodes[0];
                                XmlElement detalhe = (XmlElement)artigos.ChildNodes[1];
                                XmlNode procura = artigos;
                                string autor = "";
                                List<string> coautores = new List<string>();
                                int contador = 0;

                                //Debug.WriteLine(codigo.GetAttribute("TITULO-DO-ARTIGO"));
                                //Debug.WriteLine(codigo.GetAttribute("NATUREZA"));
                                //Debug.WriteLine(codigo.GetAttribute("ANO-DO-ARTIGO"));
                                int natureza = 0;
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
                                        binario.Write(Program.estru.coferencia.Count + Program.estru.artigo.Count);
                                        binario.Write(autoria.GetAttribute("NOME-COMPLETO-DO-AUTOR"));
                                    }
                                }
                                int data;
                                if (codigo.GetAttribute("ANO-DO-ARTIGO") == "")
                                    data = 0;
                                else
                                    data = Convert.ToInt32(codigo.GetAttribute("ANO-DO-ARTIGO"));
                                if (data > 2013 && data < 1900)
                                    data = 0;
                                Program.estru.artig = new artigo();
                                //if (codigo.GetAttribute("TITULO-DO-TRABALHO") != "")
                                //{

                                Program.estru.artig.adiciona(Program.estru.artigo.Count + Program.estru.coferencia.Count, codigo.GetAttribute("TITULO-DO-ARTIGO"), natureza, data, contador, autor);
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                Program.estru.artig.da_qualis( leArquivos.achaQUALIS(detalhe.GetAttribute("TITULO-DO-PERIODICO-OU-REVISTA")));
                               
 















                                /* Debug.WriteLine(Program.estru.artig.codigo);
                                 Debug.WriteLine(Program.estru.artig.ano);
                                 Debug.WriteLine(Program.estru.artig.natureza);
                                 Debug.WriteLine(Program.estru.artig.quantcoautores);*/
                                // Debug.WriteLine(Program.estru.artig.titulo);
                                // Debug.WriteLine(Program.estru.artig.autor);
                                Program.estru.artig.titulo = Program.estru.artig.titulo.TrimStart();
                                if (string.Compare(Program.estru.artig.titulo, "") != 0)
                                    Program.estru.artigo.Add(Program.estru.artig);
                                // }
                            }
                        i++; // contador utilizado para cada xml, para atribuir cada autor
                    }
                }
                binario.Close();
                Array.Sort(Program.estru.autor, delegate(autores x, autores y) { return x.nome.CompareTo(y.nome); }); // seleciona a chave para ordenar a classe a travez dessa chave
                for (int i = 0; i < 50 && Program.estru.autor[i].nome != null; i++) // deixa em ordem alfabetica
                {
                    listBox1.Items.Add(Program.estru.autor[i].nome); // coloca na textlistbox
                }
                Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.titulo.CompareTo(y.titulo); });
                Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.titulo.CompareTo(y.titulo); });
                int k = 0;
                int j = 0;
                while (j < Program.estru.artigo.Count && k < Program.estru.coferencia.Count)
                {
                    if (string.Compare(Program.estru.artigo[j].titulo, Program.estru.coferencia[k].titulo) < 0)
                    {
                        listBox2.Items.Add(Program.estru.artigo[j].titulo);
                        j++;
                    }
                    else
                    {
                        listBox2.Items.Add(Program.estru.coferencia[k].titulo);
                        k++;
                    }
                }

                //teste da manipulação de arquivos
                escreveArquivos.escreveConferencias();
                escreveArquivos.escrevePeriodicos();
                escreveArquivos.escreveAutores();
            }
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
            Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.titulo.CompareTo(y.titulo); });
            for (int i = 0; i < Program.estru.artigo.Count; i++) //ordena tipo artigo
                listBox2.Items.Add(Program.estru.artigo[i].titulo);
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.titulo.CompareTo(y.titulo); });
            Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.titulo.CompareTo(y.titulo); });
            Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.qualis.CompareTo(y.qualis); });
            Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.qualis.CompareTo(y.qualis); });
            int i = 0; // começa do maior para o menor
            int j = 0;
            while (j < Program.estru.artigo.Count && i < Program.estru.coferencia.Count) // compara até o laço for zero
               
            {
                if (string.Compare(Program.estru.artigo[j].qualis, Program.estru.coferencia[i].qualis) < 0) // testa a ordem alfabetica e coloca o maior na lista
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

        private void button3_Click(object sender, EventArgs e) // processo igual porem usando quantidade de coautores
        {

            listBox2.Items.Clear();
            Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.titulo.CompareTo(y.titulo); });
            Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.titulo.CompareTo(y.titulo); });
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
            Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.titulo.CompareTo(y.titulo); });
            for(int i=0; i<Program.estru.coferencia.Count;i++)
                listBox2.Items.Add(Program.estru.coferencia[i].titulo);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();// faz o contrario com a natureza imprime primeiro a menor
            Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.titulo.CompareTo(y.titulo); });
            Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.titulo.CompareTo(y.titulo); });
            int i = 0;
            int j = 0;
            while (j < Program.estru.artigo.Count && i < Program.estru.coferencia.Count)
            {
                if (string.Compare(Program.estru.artigo[j].titulo, Program.estru.coferencia[i].titulo) < 0)
                {
                    if (Program.estru.artigo[j].natureza == 0)
                    listBox2.Items.Add(Program.estru.artigo[j].titulo);
                    j++;
                }
                else
                {
                    if (Program.estru.artigo[j].natureza == 0)
                    listBox2.Items.Add(Program.estru.coferencia[i].titulo);
                    i++;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear(); // imprime só os resumos caso maior para o menor
            Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.titulo.CompareTo(y.titulo); });
            Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.titulo.CompareTo(y.titulo); });
            int i = 0;
            int j = 0;
            while (j < Program.estru.artigo.Count && i < Program.estru.coferencia.Count)
            {
                if (string.Compare(Program.estru.artigo[j].titulo, Program.estru.coferencia[i].titulo)<0)
                {
                    if(Program.estru.artigo[j].natureza== 2)
                    listBox2.Items.Add(Program.estru.artigo[j].titulo);
                    j++;
                }
                else
                {
                    if (Program.estru.artigo[j].natureza == 2)
                    listBox2.Items.Add(Program.estru.coferencia[i].titulo);
                    i++;
                }
            }
            
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.titulo.CompareTo(y.titulo); });
            Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.titulo.CompareTo(y.titulo); });
            int i = 0;
            int j = 0;
            while (j < Program.estru.artigo.Count && i < Program.estru.coferencia.Count) // primeiro busca os estendidos e imprime
            {
                if (string.Compare(Program.estru.artigo[j].titulo, Program.estru.coferencia[i].titulo) < 0)
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
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {

            if (Program.estru.autor[0] != null)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string text = remove.RemoverAcentuacao(textBox1.Text.ToLower());
                    string text2;
                    listBox1.Items.Clear();
                    for (int i = 0; i < 50; i++)
                    {
                        text2 = remove.RemoverAcentuacao(Program.estru.autor[i].nome.ToLower());
                        if (text2.Contains(text))
                            listBox1.Items.Add(Program.estru.autor[i].nome);
                    }
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            string text = remove.RemoverAcentuacao(textBox2.Text.ToLower());
            string text2;
            if (e.KeyCode == Keys.Enter)
            {
                listBox2.Items.Clear();
                for (int i = 0; i < Program.estru.artigo.Count; i++)
                {

                    text2 = remove.RemoverAcentuacao(Program.estru.artigo[i].titulo.ToLower());
                    if (text2.Contains(text))
                        listBox2.Items.Add(Program.estru.artigo[i].titulo);
                }
                for (int i = 0; i < Program.estru.coferencia.Count; i++)
                {
                    text2 = remove.RemoverAcentuacao(Program.estru.coferencia[i].titulo.ToLower());
                    if(text2.Contains(text))
                        listBox2.Items.Add(Program.estru.coferencia[i].titulo);
                }
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index != -1)
            {
                string item = listBox1.Items[index].ToString();
                for (int i = 0; i < 50; i++)
                    if (item == Program.estru.autor[i].nome)
                    {
                        Form2 formautores = new Form2();
                        formautores.autor_nome.Text = Program.estru.autor[i].nome;
                        formautores.País.Text = Program.estru.autor[i].pais;
                        formautores.Cidade.Text = Program.estru.autor[i].local;
                        int j = 0, k = 0;
                        while (j < Program.estru.artigo.Count && k < Program.estru.coferencia.Count)
                        {
                            if (string.Compare(Program.estru.artigo[j].titulo, Program.estru.coferencia[k].titulo) < 0)
                            {
                                if (Program.estru.autor[i].nome == Program.estru.coferencia[k].autor)
                                    formautores.titulos.Add(Program.estru.coferencia[k].titulo);
                                k++;
                            }
                            else
                            {
                                if (Program.estru.autor[i].nome == Program.estru.artigo[j].autor)
                                    formautores.titulos.Add(Program.estru.artigo[j].titulo);
                                j++;
                            }
                        }
                        formautores.titulos.Sort();
                        for (j = 0; j < formautores.titulos.Count; j++)
                            formautores.listBox2.Items.Add(formautores.titulos[j]);
                        string nomeArq = "prodAut.bin";
                        FileStream stream = new FileStream(nomeArq, FileMode.Open);
                        BinaryReader binario = new BinaryReader(stream);
                        //int i = 0;
                        while (binario.BaseStream.Position != binario.BaseStream.Length)
                        {
                            int cod = binario.ReadInt32();
                            string coautor = binario.ReadString();
                            if (coautor == Program.estru.autor[i].nome)
                            {
                                j = 0;
                                k = 0;
                                while (j < Program.estru.artigo.Count && k < Program.estru.coferencia.Count)
                                {
                                    if (string.Compare(Program.estru.artigo[j].titulo, Program.estru.coferencia[k].titulo) < 0)
                                    {
                                        if (cod == Program.estru.coferencia[k].codigo)
                                            formautores.titulosco.Add(Program.estru.coferencia[k].titulo);
                                        k++;
                                    }
                                    else
                                    {
                                        if (cod == Program.estru.artigo[j].codigo)
                                            formautores.titulosco.Add(Program.estru.artigo[j].titulo);
                                        j++;
                                    }
                                }
                            }
                        }
                        formautores.titulosco.Sort();
                        for (j = 0; j < formautores.titulosco.Count; j++)
                            formautores.listBox1.Items.Add(formautores.titulosco[j]);
                        binario.Close();
                        formautores.ShowDialog();

                    }
            }
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox2.SelectedIndex;
            if (index!=-1)
            {
                string item = listBox2.Items[index].ToString();
                for (int i = 0; i < Program.estru.coferencia.Count; i++)
                    if (item == Program.estru.coferencia[i].titulo)
                    {
                        List<string> coautores = new List<string>();
                        Form3 formartigo = new Form3();
                        formartigo.titulo.Text = Program.estru.coferencia[i].titulo;
                        formartigo.tipo.Text = "Conferência";
                        formartigo.Qualis.Text = Program.estru.coferencia[i].qualis;
                        formartigo.Autor.Text = Program.estru.coferencia[i].autor;
                        formartigo.ano.Text = Program.estru.coferencia[i].ano.ToString();
                        if (Program.estru.coferencia[i].natureza == 0)
                            formartigo.Natureza.Text = "Completo";
                        if (Program.estru.coferencia[i].natureza == 1)
                            formartigo.Natureza.Text = "Estendido";
                        if (Program.estru.coferencia[i].natureza == 2)
                            formartigo.Natureza.Text = "Resumo";
                        string nomeArq = "prodAut.bin";
                        FileStream stream = new FileStream(nomeArq, FileMode.Open);
                        BinaryReader binario = new BinaryReader(stream);
                        //int i = 0;
                        while (binario.BaseStream.Position != binario.BaseStream.Length)
                        {
                            int cod = binario.ReadInt32();
                            string coautor = binario.ReadString();
                            if (cod == Program.estru.coferencia[i].codigo)
                                coautores.Add(coautor);

                        }
                        binario.Close();
                        coautores.Sort();
                        for (int j = 0; j < coautores.Count; j++)
                            formartigo.listBox1.Items.Add(coautores[j]);
                        formartigo.ShowDialog();
                    }
                for (int i = 0; i < Program.estru.artigo.Count; i++)
                    if (item == Program.estru.artigo[i].titulo)
                    {
                        List<string> coautores = new List<string>();
                        Form3 formartigo = new Form3();
                        formartigo.titulo.Text = Program.estru.artigo[i].titulo;
                        formartigo.tipo.Text = "Periódico";
                        formartigo.Qualis.Text = Program.estru.artigo[i].qualis;
                        formartigo.Autor.Text = Program.estru.artigo[i].autor;
                        formartigo.ano.Text = Program.estru.artigo[i].ano.ToString();
                        if (Program.estru.artigo[i].natureza == 0)
                            formartigo.Natureza.Text = "Completo";
                        if (Program.estru.artigo[i].natureza == 1)
                            formartigo.Natureza.Text = "Estendido";
                        if (Program.estru.artigo[i].natureza == 2)
                            formartigo.Natureza.Text = "Resumo";
                        string nomeArq = "prodAut.bin";
                        FileStream stream = new FileStream(nomeArq, FileMode.Open);
                        BinaryReader binario = new BinaryReader(stream);
                        //int i = 0;
                        while (binario.BaseStream.Position != binario.BaseStream.Length)
                        {
                            int cod = binario.ReadInt32();
                            string coautor = binario.ReadString();
                            if ( cod == Program.estru.artigo[i].codigo)
                                coautores.Add(coautor);
                        }
                        coautores.Sort();
                        for (int j = 0; j < coautores.Count; j++)
                            formartigo.listBox1.Items.Add(coautores[j]);
                        binario.Close();
                        formartigo.ShowDialog();
                    }

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
                listBox2.Items.Clear();
            if (System.IO.File.Exists("autores.bin") && System.IO.File.Exists("periodicos.bin") && System.IO.File.Exists("prodAut.bin") && System.IO.File.Exists("conferencias.bin"))
            {
                leArquivos.lePeriodicos();
                leArquivos.leConferencias();
                leArquivos.leAutores();
                for (int i = 0; i < 50 && Program.estru.autor[i].nome != null; i++) // deixa em ordem alfabetica
                {
                    listBox1.Items.Add(Program.estru.autor[i].nome); // coloca na textlistbox
                }
                int k = 0, j = 0;
                while (j < Program.estru.artigo.Count && k < Program.estru.coferencia.Count)
                {
                    if (string.Compare(Program.estru.artigo[j].titulo, Program.estru.coferencia[k].titulo) < 0)
                    {
                        listBox2.Items.Add(Program.estru.artigo[j].titulo);
                        j++;
                    }
                    else
                    {
                        listBox2.Items.Add(Program.estru.coferencia[k].titulo);
                        k++;
                    }
                }
            }
            else
            {
                MessageBox.Show("Arquivos não encontrados, use a opção de abrir os xml");
                if(System.IO.File.Exists("autores.bin"))
                {
                    File.Delete("autores.bin");
                }
                if(System.IO.File.Exists("periodicos.bin"))
                {
                    File.Delete("periodicos.bin");
                }
                if(System.IO.File.Exists("conferencias.bin"))
                {
                    File.Delete("conferencias.bin");
                }
                if(System.IO.File.Exists("prodAut.bin"))
                {
                    File.Delete("prodAut.bin");
                }
            }
        }
        

    }

}

