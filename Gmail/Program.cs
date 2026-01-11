using Gmail.Infrastructure.Data;
using DotNetEnv;
using Gmail;
using Gmail.Core.Services;
using Gmail.Runner;
using Microsoft.EntityFrameworkCore;
using Gmail.Infrastructure.Common;

public class Program
{

    static void Main(string[] args)
    {
        Env.Load();
        Env.TraversePath().Load();

        string connectionString = Env.GetString("CONNECTION_STRING");

        var context = new GmailDbContext(
            new DbContextOptionsBuilder().UseLazyLoadingProxies().UseSqlServer( connectionString).Options
        );

        var repository = new Repository(context);

        var userService = new UserService(repository);
        var mailService = new MailService(repository);
        var inboxMailService = new InboxMailService(repository);

        var engine = new Engine(userService, mailService, inboxMailService);

        var manager = new ApplicationManager(engine);

        manager.Run();
    }
}