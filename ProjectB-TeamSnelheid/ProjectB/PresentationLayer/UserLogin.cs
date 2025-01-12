using Spectre.Console;
static class UserLogin
{
    public static void Login()
    {
        var email = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your email address:"));

        AccountsLogic accountsLogic = new AccountsLogic();
        var user = accountsLogic.CheckEmail(email);

        if (user == null)
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"[red]Invalid email address[/]");
            StartingMenu.Menu();
            return;
        }

        int attempts = 3;

        while (attempts > 0)
        {
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter your password:").Secret());

            if (accountsLogic.CheckPassword(user, password))
            {
                Console.Clear();
                AnsiConsole.MarkupLine($"Welcome back, [green3_1]{user.Name}[/]!");
                AccountsLogic.CurrentAccount = user;

                if (user.IsAdmin)
                {
                    AdminMenu.AdminMenuStart();
                }
                else if (user.IsEmployee)
                {
                    EmployeeMenu.EmployeeMenuStart();
                }
                else
                {
                    UserMenu.UserMenuStart();
                }
                return;
            }

            attempts--;
            if (attempts > 0)
            {
                AnsiConsole.MarkupLine($"[red]Invalid password. You have {attempts} attempt(s) left.[/]");
            }
        }

        Console.Clear();
        Console.WriteLine("Too many failed attempts. Returning to the main menu.");
        StartingMenu.Menu();
    }

    public static void NewUserLogin(string email, string password)
    {
        AccountsLogic accountsLogic = new AccountsLogic();
        var user = accountsLogic.CheckLogin(email, password);

        if (user != null)
        {
            AnsiConsole.MarkupLine($"Welcome [green3_1]{user.Name}[/]!");
            AccountsLogic.CurrentAccount = user;
            UserMenu.UserMenuStart();
        }
        else
        {
            Console.WriteLine("Invalid field(s), please try again.");
            Login();
        }
    }
}