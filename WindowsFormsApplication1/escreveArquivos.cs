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
    public static void escreveAutores(autores [] autores)
    {
        //onde vai salvar
        string nomeArq = @"C:\Users\ML\Documents\GitHub\CPD-Trab\periodicos.bin";

        //não fiz ainda o teste se já existe pq não pensei como vamoslidar com esse caso, por enquanto deleta o arquivo quando for fazer de novo, senão buga
        FileStream stream = new FileStream(nomeArq, FileMode.CreateNew);
        BinaryWriter binario = new BinaryWriter(stream);

        
    

        //escreve todos os autores no arquivo
        for (int i = 0; i < 50 && Program.estru.autor[i].nome != null; i++)
        {
            binario.Write(Program.estru.autor[i].codigo);  
            binario.Write(Program.estru.autor[i].refbibliografica);
            binario.Write(Program.estru.autor[i].nome);
        }

        binario.Close();
        stream.Close();
        return;
    }
}
