using System;
class Program
{
    struct Eletro
    {
        public string nome;
        public double potencia;
        public double tempoMedioUso;
    }//fim do struct
    static int menu()
    {
        int op;
        Console.Write("\t*** Sistema de controle de Energia C# ***\n\n");
        Console.WriteLine("1-Cadastrar");
        Console.WriteLine("2-Listar");
        Console.WriteLine("0-Sair");
        Console.Write("\nEscolha uma opção: ");
        op = Convert.ToInt32(Console.ReadLine());
        return op;
    }//fim da função menu

    static void cadastEletro(List<Eletro> lista)
    {
        Eletro novoEletro = new Eletro();// declarando uma variavel Eletro
        Console.Write("Nome do eletrodomestico:");
        novoEletro.nome = Console.ReadLine();
        Console.Write("Potencia do eletrodomestico:");
        novoEletro.potencia = Convert.ToDouble(Console.ReadLine());
        Console.Write("Tempo médio ativo por dia:");
        novoEletro.tempoMedioUso = Convert.ToDouble(Console.ReadLine());
        lista.Add(novoEletro);
    }// fim funcao 


    static void calcularCustoEletro(List<Eletro> vetorEletros, string nomeEletro)
    {
        double consumoDia, valorGastoDia, valorKw;
        Console.Write("Valor do Kw em R$: ");
        valorKw = Convert.ToDouble(Console.ReadLine());
        foreach(Eletro eletro in vetorEletros)
        {
            if (eletro.nome.ToUpper().Equals(nomeEletro.ToUpper()))
            {
                consumoDia = eletro.potencia * eletro.tempoMedioUso;
                valorGastoDia = consumoDia * valorKw;
                Console.WriteLine($"Consumo em Kw por dia: {consumoDia}, por mês: {consumoDia*30}");
            }
        }
    }
    static void listaEletro(List<Eletro> lista)
    {
        Console.WriteLine("Lista de Eletrodomesticos:");
        foreach (Eletro eletro in lista)
        {
            Console.WriteLine($"\nNome: {eletro.nome}");
            Console.WriteLine($"Potencia:  {eletro.potencia} kw");
            Console.WriteLine($"Tempo medio de uso: {eletro.tempoMedioUso}");
            Console.WriteLine();
        }// fim for
    }// fim lista

    static void salvarDados(List<Eletro> eletroD, string nomeArquivo)
    {

        using (StreamWriter writer = new StreamWriter(nomeArquivo))
        {
            foreach (Eletro eletroDom in eletroD)
            {
                writer.WriteLine($"{eletroDom.nome};{eletroDom.potencia};{eletroDom.tempoMedioUso}");
            }
        }
        Console.WriteLine("Dados salvos com sucesso!");
    }//fim do salvar

    static void carregarDados(List<Eletro> eletroD, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhas = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhas)
            {
                string[] campos = linha.Split(';');
                Eletro eletro = new Eletro
                {
                    nome = campos[0],
                    potencia = double.Parse(campos[1]),
                    tempoMedioUso = double.Parse(campos[2]),
                };
                eletroD.Add(eletro);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }//fim do carregar dados
    static void Main()
    {
        List<Eletro> vetorEletros = new List<Eletro>();//vetor que armazena um elemento do tipo eletro em cada posição
        int op = 0;
        carregarDados(vetorEletros, "dados.txt");
        do
        {
            op = menu();
            switch (op){
                case 1: Console.WriteLine("\n\tCadastrar\n");
                        cadastEletro(vetorEletros);
                    break;
                case 2: Console.WriteLine("\t\nListar\n");
                    Console.WriteLine("\t*** Dados do Eletrodomestico ***");
                    listaEletro(vetorEletros);
                    break;
                case 0: Console.WriteLine("Saindo");
                    salvarDados(vetorEletros, "dados.txt");
                    break;
            }//fim switch
            Console.ReadKey();//espera uma tecla ser clicada para que feche ou avance no codigo, é uma pausa
            Console.Clear();

        }while (op != 0);
        
    }

}
