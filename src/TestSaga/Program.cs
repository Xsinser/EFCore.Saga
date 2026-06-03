using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Saga;
using TestSaga.Db.Context;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

string alphaConnectionString = configuration["ConnectionStrings:AlphaConnectionString"] ?? throw new NullReferenceException("Empty required parameter ConnectionStrings:AlphaConnectionString");
string betaConnectionString = configuration["ConnectionStrings:BetaConnectionString"] ?? throw new NullReferenceException("Empty required parameter ConnectionStrings:BetaConnectionString");

var alphaOptionsBuilder = new DbContextOptionsBuilder<AlphaContext>();
alphaOptionsBuilder.UseNpgsql(alphaConnectionString);
var betaOptionsBuilder = new DbContextOptionsBuilder<BetaContext>();
betaOptionsBuilder.UseNpgsql(betaConnectionString);

var alphaContext = new AlphaContext(alphaOptionsBuilder.Options);
var betaContext = new BetaContext(betaOptionsBuilder.Options);

using (var context = new SagaContext())
{
    try
    {
        alphaContext.BeginSagaTransaction(context);
        betaContext.BeginSagaTransaction(context);

        alphaContext.AlphaEntities.Add(new() { Id = 2 });
        alphaContext.SaveChanges();

        betaContext.BetaEntities.Add(new() { Id = 3 });
        betaContext.BetaEntities.Add(new() { Id = 4 });
        betaContext.SaveChanges();

        context.Commit();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        context.Rollback();
    }
}