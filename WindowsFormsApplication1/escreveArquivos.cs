using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WindowsFormsApplication1;
using XML;

public class escreveArquivos
{
    public static void escreveAutores()
    {
        //onde vai salvar
        string nomeArq = "autores.bin";


        FileStream stream = new FileStream(nomeArq, FileMode.Create);
        BinaryWriter binario = new BinaryWriter(stream);




        //escreve todos os autores no arquivo
        for (int i = 0; i < 50 && Program.estru.autor[i].nome != null; i++)
        {
            binario.Write(Program.estru.autor[i].codigo);
            binario.Write(Program.estru.autor[i].refbibliografica);
            binario.Write(Program.estru.autor[i].nome);
            binario.Write(Program.estru.autor[i].local);
            binario.Write(Program.estru.autor[i].pais);
        }

        binario.Close();
        stream.Close();
        return;
    }
    public static void escrevePeriodicos()
    {
        //onde vai salvar
        string nomeArq = "periodicos.bin";


        FileStream stream = new FileStream(nomeArq, FileMode.Create);
        BinaryWriter binario = new BinaryWriter(stream);



        int i = 0;
        //escreve todos os periodicos no arquivo
        while (i < Program.estru.artigo.Count)
        {
            binario.Write(Program.estru.artigo[i].codigo);
            binario.Write(Program.estru.artigo[i].titulo);
            binario.Write(Program.estru.artigo[i].natureza);
            binario.Write(Program.estru.artigo[i].ano);
            binario.Write(Program.estru.artigo[i].quantcoautores);
            binario.Write(Program.estru.artigo[i].autor);
            binario.Write(Program.estru.artigo[i].qualis);
            i++;
        }

        binario.Close();
        stream.Close();
        return;
    }
    public static void escreveConferencias()
    {
        //onde vai salvar
        string nomeArq = "conferencias.bin";

        //não fiz ainda o teste se já existe pq não pensei como vamoslidar com esse caso, por enquanto deleta o arquivo quando for fazer de novo, senão buga
        FileStream stream = new FileStream(nomeArq, FileMode.Create);
        BinaryWriter binario = new BinaryWriter(stream);



        int i = 0;
        //escreve todos os autores no arquivo
        while (i < Program.estru.coferencia.Count)
        {
            binario.Write(Program.estru.coferencia[i].codigo); // id do trabalho
            binario.Write(Program.estru.coferencia[i].titulo); // nome do trabalho
            binario.Write(Program.estru.coferencia[i].natureza); // 0-completo 1-estendido 2-resumo
            binario.Write(Program.estru.coferencia[i].ano); // ano do trabalho            
            binario.Write(Program.estru.coferencia[i].quantcoautores); // conta quantos coautores tem
            binario.Write(Program.estru.coferencia[i].autor); // a id do autor
            binario.Write(Program.estru.coferencia[i].qualis); // nota dada por a qualidade da conferencia
            i++;
        }

        binario.Close();
        stream.Close();
        return;
    }
}
