using Application;
using Domain.Model;
using CpfCnpjLibrary;

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
    private static List<string> IdentificationErrors = new List<string>();

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
        try
        {
            cliente.Id = int.Parse(Console.ReadLine());
        }
        catch (Exception)
        {
            cliente.Id = -1;
        }

        Console.WriteLine("Seu nome:");
        cliente.Nome = Console.ReadLine();

        Console.WriteLine("Seu CPF:");
        cliente.Cpf = Console.ReadLine();

        try
        {
            Console.WriteLine("Seu Saldo:");
            cliente.Saldo = float.Parse(Console.ReadLine());
        }
        catch (Exception)
        {
            cliente.Saldo = -1;
        }

        bool clientWithValidInfo = ValidateInfo(cliente);
        if (!clientWithValidInfo)
        {
            ShowValidationErrors();
            cliente = Identificacao();
        }

        return cliente;
    }

    private static void BeginOperation(Cliente pessoa)
    {
        Console.Clear();
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
            float value = float.Parse(Console.ReadLine());
            cliente.Saldo = CalcManager.Soma(value, cliente.Saldo);
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
            float value = float.Parse(Console.ReadLine());
            cliente.Saldo = CalcManager.Subtrair(n2: value, n1: cliente.Saldo);
            Console.Clear();
            BeginOperation(cliente);
        }
        catch (Exception)
        {
            WithdrawAction(cliente);
        }
    }

    private static bool ValidateInfo(Cliente client)
    {
        IdentificationErrors.Clear();
        if (!Cpf.Validar(client.Cpf))
        {
            IdentificationErrors.Add("CPF digitado não é válido");
        }

        if (client.Id == -1)
        {
            IdentificationErrors.Add("Identificador não é válido");
        }

        if (client.Saldo == -1)
        {
            IdentificationErrors.Add("Saldo não é válido");
        }

        if (client.Nome == "")
        {
            IdentificationErrors.Add("Nome informado não é válido");
        }

        return !(IdentificationErrors.Count > 0);
    }

    private static void ShowValidationErrors()
    {
        Console.WriteLine("\n### Atenção: ###");
        for (var i = 0; i < IdentificationErrors.Count; i++)
        {
            Console.WriteLine(IdentificationErrors[i]);
        }
        Console.ReadKey();
        Console.Clear();
    }
}
