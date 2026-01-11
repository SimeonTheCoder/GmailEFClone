using Gmail.Core.Models.Mail;
using Gmail.Core.Models.User;
using Gmail.Infrastructure.Data.Models;
using System.Text;

namespace Gmail.UI
{
    public class ConsoleUI
    {
        public ConsoleGraphics g { get; set; }

        private int selector = 0;

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
                throw new Exception("Passwords do not match!");

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

            TablePrint(["From: ", senderMail], [0, 10], [ConsoleGraphics.AccentColor, ConsoleColor.Gray]);

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
            Console.ForegroundColor = color;
            g.Print($"{question}: ", color);
            Console.SetCursorPosition(g.GetXFromCol(col), g.CurrRow - 1);

            Console.ForegroundColor = ConsoleColor.White;

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

        public void RenderInbox(List<InboxMail> inbox)
        {
            this.selector = Math.Min(selector, inbox.Count - 1);

            g.Clear();
            g.Print("Inbox", ConsoleColor.Cyan);

            TablePrint(["SI", "RECIPIENTS", "| SUBJECT"], [0, 4, 30], [ConsoleColor.White, ConsoleColor.White, ConsoleColor.White]);
            Console.ForegroundColor = ConsoleGraphics.DefaultColor;

            int mailIndex = 0;

            foreach (InboxMail mail in inbox)
            {
                if (mailIndex++ == selector)
                    Console.ForegroundColor = ConsoleColor.Magenta;
                else
                    Console.ForegroundColor = ConsoleGraphics.DefaultColor;

                string bookmarkStr = string.Empty;

                bookmarkStr += mail.IsStarred ? "*" : ".";
                bookmarkStr += mail.IsImportant ? "I" : ".";

                string recipientsStr = string.Join(", ", mail.Mail.Recipients.Select(r => r.Address.Address));

                if (recipientsStr.Length > 22)
                    recipientsStr = recipientsStr.Substring(0, 22) + "...";

                TablePrint([bookmarkStr, recipientsStr, "| " + mail.Mail.Subject], [0, 4, 30], [Console.ForegroundColor, Console.ForegroundColor, Console.ForegroundColor]);
            }

            g.EmptyLine();
        }

        public void TablePrint(string[] strings, int[] columns, ConsoleColor[] colors)
        {
            for (int i = 0; i < strings.Length; i ++)
            {
                Console.ForegroundColor = colors[i];
                Console.SetCursorPosition(columns[i], g.CurrRow);
                Console.Write(strings[i]);
            }

            g.CurrRow++;
        }

        public void RenderMail(Mail mail)
        {
            g.Clear();

            TablePrint(["Subject", mail.Subject], [0, 10], [ConsoleColor.Magenta, ConsoleColor.Gray]);
            TablePrint(["From", mail.Sender.Address], [0, 10], [ConsoleColor.Magenta, ConsoleColor.Gray]);
            TablePrint(["To", string.Join(", ", mail.Recipients.Select(r => r.Address.Address))], [0, 10], [ConsoleColor.Magenta, ConsoleColor.Gray]);

            g.Print(new string('=', Console.BufferWidth));
            g.EmptyLine();

            g.Print(mail.Content);
            g.CurrRow += mail.Content.ToCharArray().Where(c => c == '\n').Count() + 1;

            g.Print(new string('=', Console.BufferWidth));

            g.EmptyLine();
        }

        public void SelectorUp()
        {
            this.selector = Math.Max(0, --selector);
        }

        public void SelectorDown()
        {
            this.selector++;
        }

        public int GetSelectorIndex()
        {
            return this.selector;
        }
    }
}
