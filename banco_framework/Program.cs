using Application;
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
    private static readonly Calculo CalcManager = new Calculo();

    private static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Seja bem vindo ao banco Framework");
        Console.WriteLine("Por favor, identifique-se");
        Console.WriteLine("");
        var pessoa = Identificacao();
        BeginOperation(pessoa);
    }

    static Cliente Identificacao()
    {
        var cliente = new Cliente();

        Console.WriteLine("Seu número de identificação:");
        cliente.Id = int.Parse(Console.ReadLine());

        Console.WriteLine("Seu nome:");
        cliente.Nome = Console.ReadLine();

        Console.WriteLine("Seu CPF:");
        cliente.Cpf = Console.ReadLine();

        Console.WriteLine("Seu Saldo:");
        cliente.Saldo = float.Parse(Console.ReadLine());
        Console.Clear();

        return cliente;
    }

    private static void BeginOperation(Cliente pessoa)
    {
        Console.WriteLine($"Seu saldo atual é: {pessoa.Saldo}");
        Console.WriteLine($"Como posso ajudar {pessoa.Nome}?");
        ShowMenuOptions();
        short option = ReadOption();
        OnSelectOption(pessoa, option);
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
        catch (Exception)
        {
            return 0;
        }
    }

    private static void OnSelectOption(Cliente cliente, short option)
    {
        switch (option)
        {
            case 1:
                Console.Clear();
                Console.WriteLine($"{menuOptions[1]}");
                DepositAction(cliente);
                break;

            case 2:
                Console.Clear();
                Console.WriteLine($"{menuOptions[2]}");
                WithdrawAction(cliente);
                break;

            case 3:
                break;

            default:
                Console.Clear();
                BeginOperation(cliente);
                break;
        }
    }

    private static void DepositAction(Cliente cliente)
    {
        try
        {
            Console.WriteLine("Digite o valor:");
            double value = Convert.ToDouble(Console.ReadLine());
            cliente.Saldo = CalcManager.Soma((float)value, cliente.Saldo);
            Console.Clear();
            BeginOperation(cliente);
        }
        catch (Exception)
        {
            DepositAction(cliente);
        }
    }

    private static void WithdrawAction(Cliente cliente)
    {
        try
        {
            Console.WriteLine("Digite o valor:");
            double value = Convert.ToDouble(Console.ReadLine());
            cliente.Saldo = CalcManager.Subtrair(n2: (float)value, n1: cliente.Saldo);
            Console.Clear();
            BeginOperation(cliente);
        }
        catch (Exception)
        {
            WithdrawAction(cliente);
        }
    }
}