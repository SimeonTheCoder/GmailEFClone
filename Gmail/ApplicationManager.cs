using Gmail.Core.Exceptions;
using Gmail.Core.Models.User;
using Gmail.Core.Services;
using Gmail.Infrastructure.Data.Models;
using Gmail.Runner;
using Gmail.UI;

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
                        ui.g.Print("Welcome!");
                        return;
                    }

                    ui.g.Clear();
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
    }
}
