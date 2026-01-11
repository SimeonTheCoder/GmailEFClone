using Gmail.Core.Models.Mail;
using Gmail.Core.Models.User;
using Gmail.Infrastructure.Data.Models;
using System.Text;

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
            g.Print("LOGIN", ConsoleColor.Cyan);
            g.EmptyLine();

            string email = Prompt("Email", 1);
            string password = Prompt("Password", 1);

            return new UserCredentials()
                {
                    Email = email,
                    Password = password
                };
        }

        public UserCredentials RegisterScreen()
        {
            g.Clear();
            g.Print("REGISTER", ConsoleColor.Cyan);
            g.EmptyLine();

            string email = Prompt("Email", 2);
            string password = Prompt("Password", 2);
            string repeatPassword = Prompt("Repeat Password", 2);
            string name = Prompt("Name (optional)", 2);

            if (password != repeatPassword)
            {
                //g.Print("Passwords do not match!", ConsoleColor.Red);
                throw new Exception("Passwords do not match!");
            }

            UserCredentials credentials = new()
            {
                Email = email,
                Password = password,
            };

            if (name.Trim() != "") credentials.Name = name;

            return credentials;
        }

        public MailDTO ComposeMailScreen(string senderMail)
        {
            g.Clear();
            g.Print("COMPOSE", ConsoleColor.Cyan);
            g.EmptyLine();

            Console.ForegroundColor = ConsoleGraphics.DefaultColor;

            Console.SetCursorPosition(0, g.CurrRow - 1);
            Console.Write("From: ");
            Console.SetCursorPosition(10, g.CurrRow - 1);
            Console.Write(senderMail);

            string recipients = Prompt("To", 1);

            string subject = Prompt("Subject", 1);

            Console.ForegroundColor = ConsoleColor.Magenta;

            g.EmptyLine();
            g.Print("Content (write 'END' to signify the end of the mail): ");
            g.EmptyLine();

            Console.ForegroundColor = ConsoleColor.White;

            StringBuilder sb = new();

            string line = Console.ReadLine();
            g.EmptyLine();

            while (line != "END")
            {
                sb.AppendLine(line);
                line = Console.ReadLine();
                g.EmptyLine();
            }

            string content = sb.ToString().Trim();
            string[] mails = recipients.Split(", ");

            MailDTO mail = new MailDTO()
            {
                SenderMail = senderMail,
                Recipients = mails.ToList(),
                Content = content,
                Subject = subject,
            };

            return mail;
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
