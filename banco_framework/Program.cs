using Domain.Model;

namespace bancoFramework;

internal class Program
{

    private static readonly Dictionary<short, string> menuOptions = new()
        {
            {1,  "Depósito"},
            {2,  "Saque"},
            {3,  "Sair"}
        };

    private static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Seja bem vindo ao banco Framework");
        Console.WriteLine("Por favor, identifique-se");
        Console.WriteLine("");
        var pessoa = Identificacao();
        BeginOperation(pessoa);
    }

    static Pessoa Identificacao()
    {
        var pessoa = new Pessoa();

        Console.WriteLine("Seu número de identificação:");
        pessoa.Id = int.Parse(Console.ReadLine());

        Console.WriteLine("Seu nome:");
        pessoa.Nome = Console.ReadLine();

        Console.WriteLine("Seu CPF:");
        pessoa.Cpf = Console.ReadLine();
        Console.Clear();

        // Console.WriteLine($"Como posso ajudar {pessoa.Nome}?");
        // Console.ReadKey();

        return pessoa;
    }

    private static void BeginOperation(Pessoa pessoa)
    {
        Console.WriteLine($"Como posso ajudar {pessoa.Nome}?");
        ShowMenuOptions();
        short option = ReadOption();
        OnSelecOption(pessoa, option);
    }
    private static void ShowMenuOptions()
    {
        foreach (KeyValuePair<short, string> option in menuOptions)
        {
            Console.WriteLine($"{option.Key} - {option.Value}");
        }
        Console.WriteLine("-------------");
        Console.WriteLine("Selecione uma opção:");
    }

    private static short ReadOption()
    {
        try
        {
            return Convert.ToInt16(Console.ReadLine());
        }
        catch (System.Exception)
        {
            return 0;
        }
    }

    private static void OnSelecOption(Pessoa pessoa, short option)
    {
        switch (option)
        {
            case 1:
                Console.Clear();
                Console.WriteLine($"{menuOptions[1]}");
                Console.ReadKey();
                break;

            case 2:
                Console.Clear();
                Console.WriteLine($"{menuOptions[2]}");
                Console.ReadKey();
                break;

            case 3:
                break;

            default:
                Console.Clear();
                BeginOperation(pessoa);
                break;
        }
    }
}