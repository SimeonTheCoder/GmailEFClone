using Gmail.Core.Exceptions;
using Gmail.Core.Models.Mail;
using Gmail.Core.Models.User;
using Gmail.Core.Services;
using Gmail.Infrastructure.Data.Models;
using Gmail.Runner;
using Gmail.UI;
using System;

namespace Gmail
{
    public class ApplicationManager
    {
        private Engine Engine;
        private ConsoleUI ui;

        private User session;

        public ApplicationManager(Engine engine)
        {
            this.Engine = engine;
            this.ui = new();
        }

        public void Run()
        {
            bool readingMode = false;

            while (true)
            {
                try
                {
                    if (session == null)
                    {
                        if (ui.PromptYesOrNo("Already have an account?", 4)) LoginState();
                        else RegisterState();
                    }
                    else
                    {
                        List<InboxMail> inbox = Engine.GetInboxForUser(session);

                        if (!readingMode)
                        {
                            ui.RenderInbox(inbox);
                        }
                        else
                        {
                            Mail currMail = inbox[Math.Min(inbox.Count - 1, ui.GetSelectorIndex())].Mail;
                            ui.RenderMail(currMail);
                        }

                        Console.SetCursorPosition(0, Console.BufferHeight - 4);
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, Console.BufferHeight - 4);

                        ui.g.CurrRow = Console.BufferHeight - 4;
                        ui.TablePrint(
                            ["c/w/n: write", "r: read", "i: inbox", "u: up", "d: down", "q/e: quit", "l: logout"],
                            [0, 10, 20, 30, 40, 50, 60],
                            [ConsoleColor.Gray, ConsoleColor.Gray, ConsoleColor.Gray, ConsoleColor.Gray, ConsoleColor.Gray, ConsoleColor.Gray, ConsoleColor.Gray]
                        );

                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        
                        Console.SetCursorPosition(0, Console.BufferHeight - 3);
                        Console.WriteLine(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, Console.BufferHeight - 3);
                        Console.ForegroundColor = ConsoleColor.White;

                        string command = Console.ReadLine().ToLower().Trim();

                        Console.BackgroundColor = ConsoleColor.Black;

                        switch (command)
                        {
                            case "compose":
                            case "write":
                            case "new":
                            case "c":
                            case "w":
                            case "n":
                                ComposeMailState();
                                ui.g.Print("Successfuly sent email!");

                                break;

                            case "u":
                                ui.SelectorUp();
                                break;

                            case "d":
                                ui.SelectorDown();
                                break;

                            case "read":
                            case "r":
                                readingMode = true;
                                break;

                            case "inbox":
                            case "i":
                                readingMode = false;
                                break;

                            case "quit":
                            case "exit":
                            case "q":
                            case "e":
                                return;

                            case "l":
                            case "logout":
                                session = null;
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    ui.Error(e.Message);

                    if (!ui.PromptYesOrNo("Continue?"))
                        return;

                    ui.g.Clear();
                }
            }
        }

        private void LoginState()
        {
            UserCredentials loginCredentials = ui.LoginScreen();

            while (loginCredentials == null)
            {
                ui.Error("Invalid credentials!");

                if (!ui.PromptYesOrNo("Continue?"))
                    return;

                ui.g.Clear();
                loginCredentials = ui.LoginScreen();
            }

            LoginUser(loginCredentials);
        }

        private void RegisterState()
        {
            UserCredentials registerCredentials = ui.RegisterScreen();

            while (registerCredentials == null)
            {
                ui.Error("Invalid credentials!");

                if (!ui.PromptYesOrNo("Continue?"))
                    return;

                ui.g.Clear();
                registerCredentials = ui.RegisterScreen();
            }

            Engine.RegisterUser(registerCredentials);

            ui.g.EmptyLine();
            ui.g.Print("Registration Successful!", ConsoleColor.Green);

            LoginUser(registerCredentials);
        }

        private void LoginUser(UserCredentials loginCredentials)
        {
            try
            {
                this.session = Engine.GetUser(loginCredentials);
                ui.g.Print("Login successful!");
            }
            catch (NotFoundException e)
            {
                ui.Error(e.Message);
                return;
            }
        }

        private void ComposeMailState()
        {
            ui.g.Clear();

            MailDTO mailDTO = ui.ComposeMailScreen(session.Email.Address);
            Engine.SendMail(mailDTO);
        }
    }
}
