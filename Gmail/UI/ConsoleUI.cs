using Gmail.Core.Models.User;

namespace Gmail.UI
{
    public class ConsoleUI
    {
        public ConsoleGraphics g { get; set; }

        public ConsoleUI()
        {
            this.g = new ConsoleGraphics();
        }

        public UserCredentials LoginScreen()
        {
            g.Clear();
            g.Print("LOGIN");
            g.EmptyLine();

            string email = Prompt("Email");
            string password = Prompt("Password");

            return new UserCredentials()
                {
                    Email = email,
                    Password = password
                };
        }

        public UserCredentials RegisterScreen()
        {
            g.Clear();
            g.Print("REGISTER");
            g.EmptyLine();

            string email = Prompt("Email", 2);
            string password = Prompt("Password", 2);
            string repeatPassword = Prompt("Repeat Password", 2);
            string name = Prompt("Name (none if left blank)", 2);

            if (password != repeatPassword)
            {
                g.Print("Passwords do not match!", ConsoleColor.Red);
                return null;
            }

            UserCredentials credentials = new()
            {
                Email = email,
                Password = password,
            };

            if (name.Trim() != "") credentials.Name = name;

            return credentials;
        }

        public string Prompt(string question, int col = 1, ConsoleColor color = ConsoleGraphics.AccentColor)
        {
            g.Print($"{question}: ");
            Console.SetCursorPosition(g.GetXFromCol(col), g.CurrRow - 1);

            Console.ForegroundColor = color;

            return Console.ReadLine();
        }

        public bool PromptYesOrNo(string question, int col = 1, ConsoleColor color = ConsoleGraphics.AccentColor)
        {
            g.Print($"{question} (Y/N): ");
            Console.SetCursorPosition(g.GetXFromCol(col), g.CurrRow - 1);

            Console.ForegroundColor = color;

            string response = Console.ReadLine();

            return response.Trim().ToLower().StartsWith("y");
        }

        public void Error(string error)
        {
            g.Print(error, ConsoleColor.Red);
        }
    }
}
