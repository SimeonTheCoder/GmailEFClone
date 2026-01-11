using Gmail.Infrastructure.Data;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

public class Program
{
    static void Main(string[] args)
    {
        Env.Load();
        using var context = new GmailDbContext();
    }
}