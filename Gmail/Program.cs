using Gmail.Infrastructure.Data;
using DotNetEnv;

public class Program
{
    static void Main(string[] args)
    {
        Env.Load();

        using var context = new GmailDbContext(Env.GetString("CONNECTION_STRING"));
    }
}