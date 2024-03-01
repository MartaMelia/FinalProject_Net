using Bank.Features;
using Bank.Models;


namespace Bank;

// პროგრამის კლასი
public class Program 
{
    // მომხმარებელი || არ არის ავტორიზებული ან არა
    private static Guid? _userId;

    // ბანკომატი მუშაობს
    private static bool IsOn = true;

    public async static Task Main(string[] args) 
    {
        // მთავარი გამშვები კოდი
        while (IsOn) 
        {
            await Start();
        }
    }

    // გამშვები მეთოდი
    public static async Task Start() 
    {
        // მისალმება და ფუნქციები
        Console.WriteLine("Hello To our Bank system!");
        Console.WriteLine("Here Are the Choices");
        Console.WriteLine("(1) Create an account");
        Console.WriteLine("(2) Login to your account");
        Console.WriteLine("(3) Create Wallet");
        Console.WriteLine("(4) My Wallets");
        Console.WriteLine("Select (1 | 2 | 3 | 4):");

        // ვთხოვთ არჩევას
        string input = Console.ReadLine()?? string.Empty;

        // ტექსტის შემოწმება
        if (string.IsNullOrEmpty(input)) 
        {
            Console.WriteLine("Invalid Selection");
            await Start();
        }

        // სერვისის მიხედვით გადანაწილება
        if (input == "1")
        {
            // მომხმარებლის ველების შვსება
            var account = AccountCredentials();

            // რეგისტრაციის სერვისის ინიციალიზაცია
            var registrationService = new AccountRegistration(account);

            // სერვისის გაშვება
            await registrationService.Execute();

            Console.WriteLine("You have registered successfully");

            return;
        }
        else if (input == "2")
        {
            // ავტორიზაცია
            await Authorize();
        }
        else if (input == "3")
        {
            // ანგარიშის დამატება
            await AddWallet();
        }
        else if (input == "4") 
        {
            //ანგარიშები
            await MyWallets();

            // თანხის/შეტანა გამოტანა
            await Wallet();
        }
        else
        {
            //არასწორი სერვისი
            Console.WriteLine("Invalid Selection");
            await Start();
        }
    }

    // მომხმარებლის ანგარიშები
    private async static Task MyWallets() 
    {
        // თუ არ არის ავტორიზებული არ აქვს წვდომა
        if (_userId is null)
        {
            Console.WriteLine("Authorization is required.");
            return;
        }

        // ანგარიშებს წამოღების სერვისი
        GetWallets service = new(_userId.Value);

        // სერვისის გამოძახება
        var response = await service.Execute();

        // პასუხის დაბრუნება
        Console.WriteLine(response);
        Console.WriteLine("\n");
    }

    // ანგარიშის დამატება
    private async static Task AddWallet() 
    {
        // თუ არ არის ავტორიზებული არ აქვს წვდომა
        if (_userId is null) 
        {
            Console.WriteLine("Authorization is required.");
            return;
        }

        // ვთხოვთ სახელს
        Console.WriteLine("To Create a new wallet fill following field:");
        Console.WriteLine("Enter the name:");
        string name = Console.ReadLine() ?? string.Empty;

        // სახელის შემოწმება
        if (string.IsNullOrEmpty(name)) 
        {
            Console.WriteLine("Name is required.");
            await AddWallet();
        }

        // ანგარიშის შექმნა
        Wallet wallet = new(Guid.NewGuid(), _userId.Value, name, 0);

        // შექმნის სერვისის გამოძახება
        CreateWallet service = new(wallet);

        // სერვისის გაშვება
        await service.Execute();

        Console.WriteLine("Wallet created successfully");
        Console.WriteLine("\n");
    }

    // მომხმარებლის რეგისტრაცია
    private static Account AccountCredentials() 
    {
        // ვთხოვთ მონაცემებს
        Console.WriteLine("To Create an account fill the following form:");
        Console.WriteLine("Write your firstname:");

        // სახელი
        string firstName = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Write your lastname:");

        // გვარი
        string lastname = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Write your password:");

        // პაროლი
        string password = Console.ReadLine() ?? string.Empty;

        // თუ მონაცემები არასწორია
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(firstName))
        {
            Console.WriteLine("Invalid credentials try again.");
            AccountCredentials();
        }

        // თუ სწორია
        Account account = new(Guid.NewGuid(),firstName, lastname, password);

        return account;
    }

    // ავტორიზაცია
    private async static Task Authorize() 
    {
        // ვთხოვთ მონაცემებს
        Console.WriteLine("To authorize enter your credentials:");
        Console.WriteLine("Enter firstname:");

        string firstName = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Enter password:");

        string password = Console.ReadLine() ?? string.Empty;

        // თუ მონაცემები არაა მისაღები
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Invalid credentials try again.");
            await Authorize();
        }
        else 
        {
            // თუ მისაღებია ვიძახებთ სერვისს
            var authorize = new AccountAuthorization(firstName, password);

            // სერვისის გამშვები
            var userId = await authorize.Execute();

            // თუ ვერ გაიარა ავტორიზაცია
            if (userId is null) 
            {
                await Authorize();
            }

            // ავტორიზაცია წარმატებით დასრულდა
            _userId = userId;
            Console.WriteLine("\n");
        }
    }

    // ანგარიში
    private async static Task Wallet() 
    {
        // ანგარიშის გამოყენება ვთხოვთ ანგარიშის სახელს
        Console.WriteLine("Enter name of the wallet you want to use:");
        string name = Console.ReadLine() ?? string.Empty;

        // თუ არა ვალიდურია
        if (string.IsNullOrEmpty(name)) 
        {
            Console.WriteLine("Invalid name try again");
            await Wallet();
        }

        // ირჩევს ან დამატებას ან თანხის გამოტანას
        Console.WriteLine("Now choose Add/Take amount");
        Console.WriteLine("To choose write (1/2)");
        string input = Console.ReadLine() ?? string.Empty;

        // შეყავს თანხის რაოდენობა
        Console.WriteLine("Enter the amount");
        string amount = Console.ReadLine()?? string.Empty;

        // თუ ყველაფერი ვალიდურია
        if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(amount)) 
        {
            Console.WriteLine("Invalid input try again");
            await Wallet();
        }

        // თანხის დამატება
        if (input == "1")
        {
            // ვიძახებთ სერვისს
            var service = new AddWalletAmount(_userId!.Value, decimal.Parse(amount), name);

            // ვუშვებთ სერვისს
            await service.Execute();

            return;
        }
        else if (input == "2")
        {
            // თანხის გამოტანა
            // ვიძახებთ სერვისს
            var service = new TakeWalletAmount(_userId!.Value, decimal.Parse(amount), name);

            // ვუშვებთ სერვისს
            await service.Execute();
        }
        else 
        {
            // თუ შეცდომით აირჩია 
            await Wallet();
        }
    }
}