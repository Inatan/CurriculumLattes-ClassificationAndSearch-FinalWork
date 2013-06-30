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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        public List<string> titulos = new List<string>();
        public List<string> titulosco = new List<string>();

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            for (int i = 0; i < titulos.Count; i++)
            {
                for (int j = 0; j < Program.estru.coferencia.Count; j++)
                {
                    if (titulos[i] == Program.estru.coferencia[j].titulo)
                    {
                        bool teste = false;
                        for (int m = 0; m < listBox1.Items.Count; m++)
                            if (titulos[i] == listBox1.Items[m].ToString())
                                teste = true;
                        if (!teste)
                        listBox2.Items.Add(Program.estru.coferencia[j].titulo);
                    }
                }
            }
            for (int i = 0; i < titulosco.Count; i++)
            {
                for (int j = 0; j < Program.estru.coferencia.Count; j++)
                {
                    if (titulosco[i] == Program.estru.coferencia[j].titulo)
                    {
                        bool teste = false;
                        for (int m = 0; m < listBox1.Items.Count; m++)
                            if (titulosco[i] == listBox1.Items[m].ToString())
                                teste = true;
                        if (!teste)
                        listBox1.Items.Add(Program.estru.coferencia[j].titulo);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            for (int i = 0; i < titulos.Count; i++)
            {
                for (int j = 0; j < Program.estru.artigo.Count; j++)
                {
                    if (titulos[i] == Program.estru.artigo[j].titulo)
                    {
                        bool teste = false;
                        for (int m = 0; m < listBox1.Items.Count; m++)
                            if (titulos[i] == listBox1.Items[m].ToString())
                                teste = true;
                        if (!teste)
                        listBox2.Items.Add(Program.estru.artigo[j].titulo);
                    }
                }
            }
            for (int i = 0; i < titulosco.Count; i++)
            {
                for (int j = 0; j < Program.estru.artigo.Count; j++)
                {
                    if (titulosco[i] == Program.estru.artigo[j].titulo)
                    {
                        bool teste = false;
                        for (int m = 0; m < listBox1.Items.Count; m++)
                            if (titulosco[i] == listBox1.Items[m].ToString())
                                teste = true;
                        if (!teste)
                        listBox1.Items.Add(Program.estru.artigo[j].titulo);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox1.Items.Clear();
            Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.titulo.CompareTo(y.titulo); });
            Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.titulo.CompareTo(y.titulo); });
            Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.qualis.CompareTo(y.qualis); });
            Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.qualis.CompareTo(y.qualis); });
            int i = Program.estru.coferencia.Count - 1; // começa do maior para o menor
            int j = Program.estru.artigo.Count - 1;
            while (j >= 0 && i >= 0) // compara até o laço for zero
            {
                if (string.Compare(Program.estru.artigo[j].qualis, Program.estru.coferencia[i].qualis) < 0 || i == 0) // testa a ordem alfabetica e coloca o maior na lista
                {
                    int k = 0;
                    while (k < titulos.Count)
                    {
                       if (titulos[k] == Program.estru.artigo[j].titulo)
                        {
                            bool teste = false;
                            for (int m = 0; m < listBox1.Items.Count; m++)
                                if (titulos[k] == listBox1.Items[m].ToString())
                                    teste = true;
                            if (!teste) 
                           listBox2.Items.Add(titulos[k]);
                        }
                        k++;
                    }
                    j--;
                }
                else
                {
                    int k = 0;
                    while (k < titulos.Count)
                    {
                        if (titulos[k] == Program.estru.coferencia[i].titulo)
                        {
                            bool teste = false;
                            for (int m = 0; m < listBox1.Items.Count; m++)
                                if (titulos[k] == listBox1.Items[m].ToString())
                                    teste = true;
                            if (!teste)
                            listBox2.Items.Add(titulos[k]);
                        }
                        k++;
                    }
                    i--;
                }
            }
            i = Program.estru.coferencia.Count - 1; // começa do maior para o menor
            j = Program.estru.artigo.Count - 1;
            while (j >= 0 && i >= 0) // compara até o laço for zero
            {
                if (string.Compare(Program.estru.artigo[j].qualis, Program.estru.coferencia[i].qualis) < 0 || i == 0) // testa a ordem alfabetica e coloca o maior na lista
                {
                    int k = 0;
                    while (k < titulosco.Count)
                    {
                        if (titulosco[k] == Program.estru.artigo[j].titulo)
                        {
                            bool teste = false;
                            for (int m = 0; m < listBox1.Items.Count; m++)
                                if (titulosco[k] == listBox1.Items[m].ToString())
                                    teste = true;
                            if(!teste)
                            listBox1.Items.Add(titulosco[k]);
                        }
                        k++;
                    }
                    j--;
                }
                else
                {
                    int k = 0;
                    while (k < titulosco.Count)
                    {
                        if (titulosco[k] == Program.estru.coferencia[i].titulo)
                        {
                            bool teste = false;
                            for (int m = 0; m < listBox1.Items.Count; m++)
                                if (titulosco[k] == listBox1.Items[m].ToString())
                                    teste = true;
                            if (!teste)
                            listBox1.Items.Add(titulosco[k]);
                        }
                        k++;
                    }
                    i--;
                }
            }
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox2.SelectedIndex;
            if (listBox2.Items[index] != null)
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
                            if (cod == Program.estru.artigo[i].codigo)
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

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (listBox1.Items[index] != null)
            {
                string item = listBox1.Items[index].ToString();
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
                            if (cod == Program.estru.artigo[i].codigo)
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

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox1.Items.Clear();
            Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.titulo.CompareTo(y.titulo); });
            Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.titulo.CompareTo(y.titulo); });
            Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.quantcoautores.CompareTo(y.quantcoautores); });
            Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.quantcoautores.CompareTo(y.quantcoautores); });
            int i = Program.estru.coferencia.Count - 1;
            int j = Program.estru.artigo.Count - 1;
            while (j >= 0 && i >= 0)
            {
                if (Program.estru.artigo[j].quantcoautores > Program.estru.coferencia[i].quantcoautores || i==0)
                {
                    int k = 0;
                    while (k < titulos.Count)
                    {
                        if (titulos[k] == Program.estru.artigo[j].titulo)
                        {
                            bool teste = false;
                            for (int m = 0; m < listBox1.Items.Count; m++)
                                if (titulos[k] == listBox1.Items[m].ToString())
                                    teste = true;
                            if (!teste)
                            listBox2.Items.Add(Program.estru.artigo[j].titulo);
                        }
                        k++;
                    }
                    j--;
                }
                else
                {
                    int k = 0;
                    while (k < titulos.Count)
                    {
                        if (titulos[k] == Program.estru.coferencia[i].titulo)
                        {
                            bool teste = false;
                            for (int m = 0; m < listBox1.Items.Count; m++)
                                if (titulos[k] == listBox1.Items[m].ToString())
                                    teste = true;
                            if (!teste)
                            listBox2.Items.Add(Program.estru.coferencia[i].titulo);
                        }
                        k++;
                    }
                    i--;
                }
            }
            i = Program.estru.coferencia.Count - 1;
            j = Program.estru.artigo.Count - 1;
            while (j >= 0 && i >= 0)
            {
                if (Program.estru.artigo[j].quantcoautores > Program.estru.coferencia[i].quantcoautores ||i==0)
                {
                    int k = 0;
                    while (k < titulosco.Count)
                    {
                        if (titulosco[k] == Program.estru.artigo[j].titulo)
                        {
                            bool teste = false;
                            for (int m = 0; m < listBox1.Items.Count; m++)
                                if (titulosco[k] == listBox1.Items[m].ToString())
                                    teste = true;
                            if (!teste)
                            listBox1.Items.Add(Program.estru.artigo[j].titulo);
                        }
                        k++;
                    }
                    j--;
                }
                else
                {
                    int k = 0;
                    while (k < titulosco.Count)
                    {
                        if (titulosco[k] == Program.estru.coferencia[i].titulo)
                        {
                            bool teste = false;
                            for (int m = 0; m < listBox1.Items.Count; m++)
                                if (titulosco[k] == listBox1.Items[m].ToString())
                                    teste = true;
                            if (!teste)
                            listBox1.Items.Add(Program.estru.coferencia[i].titulo);
                        }
                        k++;
                    }
                    i--;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();// faz o contrario com a natureza imprime primeiro a menor
            listBox1.Items.Clear();
            Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.titulo.CompareTo(y.titulo); });
            Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.titulo.CompareTo(y.titulo); });
            int i = 0;
            int j = 0;
            while (j < Program.estru.artigo.Count && i < Program.estru.coferencia.Count)
            {
                if (string.Compare(Program.estru.artigo[j].titulo, Program.estru.coferencia[i].titulo) < 0)
                {
                        int k = 0;
                        while (k < titulos.Count)
                        {
                            if (titulos[k] == Program.estru.artigo[j].titulo)
                            {
                                if (Program.estru.coferencia[i].natureza == 0)
                                {
                                    bool teste = false;
                                    for (int m = 0; m < listBox1.Items.Count; m++)
                                        if (titulos[k] == listBox1.Items[m].ToString())
                                            teste = true;
                                    if (!teste)
                                    listBox2.Items.Add(Program.estru.artigo[j].titulo);
                                }
                            }
                            k++;
                        }
                    j++;
                }
                else
                {

                        int k = 0;
                        while (k < titulos.Count)
                        {
                            if (titulos[k] == Program.estru.coferencia[i].titulo)
                            {
                                if (Program.estru.coferencia[i].natureza == 0)
                                {
                                    bool teste = false;
                                    for (int m = 0; m < listBox1.Items.Count; m++)
                                        if (titulos[k] == listBox1.Items[m].ToString())
                                            teste = true;
                                    if (!teste)
                                    listBox2.Items.Add(Program.estru.coferencia[i].titulo);
                                }
                            }
                            k++;
                        }
                        i++;
                }
            }
            i = 0;
            j = 0;
            while (j < Program.estru.artigo.Count && i < Program.estru.coferencia.Count)
            {
                if (string.Compare(Program.estru.artigo[j].titulo, Program.estru.coferencia[i].titulo) < 0)
                {
                        int k = 0;
                        while (k < titulosco.Count)
                        {
                            if (titulosco[k] == Program.estru.artigo[j].titulo)
                            {
                                if (Program.estru.coferencia[i].natureza == 0)
                                {
                                    bool teste = false;
                                    for (int m = 0; m < listBox1.Items.Count; m++)
                                        if (titulosco[k] == listBox1.Items[m].ToString())
                                            teste = true;
                                    if (!teste)
                                    listBox1.Items.Add(Program.estru.artigo[j].titulo);
                                }
                            }
                            k++;
                        }
                    j++;
                }
                else
                {
                    
                        int k = 0;
                        while (k < titulosco.Count)
                        {
                            if (titulosco[k] == Program.estru.coferencia[i].titulo)
                            {
                                if (Program.estru.coferencia[i].natureza == 0)
                                {
                                    bool teste = false;
                                    for (int m = 0; m < listBox1.Items.Count; m++)
                                        if (titulosco[k] == listBox1.Items[m].ToString())
                                            teste = true;
                                    if (!teste)
                                    listBox1.Items.Add(Program.estru.coferencia[i].titulo);
                                }
                            }
                            k++;
                        }
                        i++;
                   
                }
            }
   
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();// faz o contrario com a natureza imprime primeiro a menor
            listBox1.Items.Clear();
            Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.titulo.CompareTo(y.titulo); });
            Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.titulo.CompareTo(y.titulo); });
            int i = 0;
            int j = 0;
            while (j < Program.estru.artigo.Count && i < Program.estru.coferencia.Count)
            {
                if (string.Compare(Program.estru.artigo[j].titulo, Program.estru.coferencia[i].titulo) < 0)
                {

                        int k = 0;
                        while (k < titulos.Count)
                        {
                            if (titulos[k] == Program.estru.artigo[j].titulo)
                            {
                                if (Program.estru.coferencia[i].natureza == 1)
                                {
                                    bool teste = false;
                                    for (int m = 0; m < listBox1.Items.Count; m++)
                                        if (titulos[k] == listBox1.Items[m].ToString())
                                            teste = true;
                                    if (!teste)
                                    listBox2.Items.Add(Program.estru.artigo[j].titulo);
                                }
                            }
                            k++;
                        }
                    j++;
                }
                else
                {

                        int k = 0;
                        while (k < titulos.Count)
                        {
                            if (titulos[k] == Program.estru.coferencia[i].titulo)
                            {
                                if (Program.estru.coferencia[i].natureza == 1)
                                {
                                    bool teste = false;
                                    for (int m = 0; m < listBox1.Items.Count; m++)
                                        if (titulos[k] == listBox1.Items[m].ToString())
                                            teste = true;
                                    if (!teste)
                                    listBox2.Items.Add(Program.estru.coferencia[i].titulo);
                                }
                            }
                            k++;
                        }
                        i++;
                }
            }
            i = 0;
            j = 0;
            while (j < Program.estru.artigo.Count && i < Program.estru.coferencia.Count)
            {
                if (string.Compare(Program.estru.artigo[j].titulo, Program.estru.coferencia[i].titulo) < 0)
                {
                        int k = 0;
                        while (k < titulosco.Count)
                        {
                            if (titulosco[k] == Program.estru.artigo[j].titulo)
                            {
                                if (Program.estru.coferencia[i].natureza == 1)
                                {
                                    bool teste = false;
                                    for (int m = 0; m < listBox1.Items.Count; m++)
                                        if (titulosco[k] == listBox1.Items[m].ToString())
                                            teste = true;
                                    if (!teste)
                                    listBox1.Items.Add(Program.estru.artigo[j].titulo);
                                }
                            }
                            k++;
                    }
                    j++;
                }
                else
                {
                    
                        int k = 0;
                        while (k < titulosco.Count)
                        {
                            if (titulosco[k] == Program.estru.coferencia[i].titulo)
                            {
                                if (Program.estru.coferencia[i].natureza == 1)
                                {
                                    bool teste = false;
                                    for (int m = 0; m < listBox1.Items.Count; m++)
                                        if (titulosco[k] == listBox1.Items[m].ToString())
                                            teste = true;
                                    if (!teste)
                                    listBox1.Items.Add(Program.estru.coferencia[i].titulo);
                                }
                            }
                            k++;
                        }
                        i++;
                   
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
                      listBox2.Items.Clear();// faz o contrario com a natureza imprime primeiro a menor
            listBox1.Items.Clear();
            Program.estru.coferencia.Sort(delegate(conferencias x, conferencias y) { return x.titulo.CompareTo(y.titulo); });
            Program.estru.artigo.Sort(delegate(artigo x, artigo y) { return x.titulo.CompareTo(y.titulo); });
            int i = 0;
            int j = 0;
            while (j < Program.estru.artigo.Count && i < Program.estru.coferencia.Count)
            {
                if (string.Compare(Program.estru.artigo[j].titulo, Program.estru.coferencia[i].titulo) < 0)
                {

                        int k = 0;
                        while (k < titulos.Count)
                        {
                            if (titulos[k] == Program.estru.artigo[j].titulo)
                            {
                                if (Program.estru.coferencia[i].natureza == 2)
                                {
                                    bool teste = false;
                                    for (int m = 0; m < listBox1.Items.Count; m++)
                                        if (titulos[k] == listBox1.Items[m].ToString())
                                            teste = true;
                                    if (!teste)
                                    listBox2.Items.Add(Program.estru.artigo[j].titulo);
                                }
                            }
                            k++;
                        }
                    j++;
                }
                else
                {

                        int k = 0;
                        while (k < titulos.Count)
                        {
                            if (titulos[k] == Program.estru.coferencia[i].titulo)
                            {
                                if (Program.estru.coferencia[i].natureza == 2)
                                {
                                    bool teste = false;
                                    for (int m = 0; m < listBox1.Items.Count; m++)
                                        if (titulos[k] == listBox1.Items[m].ToString())
                                            teste = true;
                                    if (!teste)
                                    listBox2.Items.Add(Program.estru.coferencia[i].titulo);
                                }
                            }
                            k++;
                        }
                        i++;
                }
            }
            i = 0;
            j = 0;
            while (j < Program.estru.artigo.Count && i < Program.estru.coferencia.Count)
            {
                if (string.Compare(Program.estru.artigo[j].titulo, Program.estru.coferencia[i].titulo) < 0)
                {
                        int k = 0;
                        while (k < titulosco.Count)
                        {
                            if (titulosco[k] == Program.estru.artigo[j].titulo)
                            {
                                if (Program.estru.coferencia[i].natureza == 2)
                                {
                                    bool teste = false;
                                    for (int m = 0; m < listBox1.Items.Count; m++)
                                        if (titulosco[k] == listBox1.Items[m].ToString())
                                            teste = true;
                                    if (!teste) listBox1.Items.Add(Program.estru.artigo[j].titulo);
                                }
                            }
                            k++;
                    }
                    j++;
                }
                else
                {
                    
                        int k = 0;
                        while (k < titulosco.Count)
                        {
                            if (titulosco[k] == Program.estru.coferencia[i].titulo)
                            {
                                if (Program.estru.coferencia[i].natureza == 2)
                                {
                                    bool teste = false;
                                    for (int m = 0; m < listBox1.Items.Count; m++)
                                        if (titulosco[k] == listBox1.Items[m].ToString())
                                            teste = true;
                                    if (!teste)
                                    listBox1.Items.Add(Program.estru.coferencia[i].titulo);
                                }
                            }
                            k++;
                        }
                        i++;
                   
                }
            }

        }

    }

}

