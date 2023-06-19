using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDBChangetrackerWebjobCore.Repositories.Interface;
using MongoDBChangetrackerWebjobCoreInfra.Repositories;
using Microsoft.Extensions.Logging;
using MongoDBChangetrackerWebjobCore.Handler.Implemetation;
using MongoDBChangetrackerWebjobCore.Handler.Interface;
using System;
using MongoDBChangetrackerWebjobCore.Services.Interface;
using MongoDBChangetrackerWebjobCoreInfra.Services;

IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

IServiceProvider serviceProvider = InitializeDependency(configuration);

IBackfillerHandler  backfillerHandler= GetImplementation<IBackfillerHandler>(serviceProvider);

try
{
    backfillerHandler.StartBackFiller();
}
catch(Exception ex)
{
    Console.WriteLine(ex.ToString());
}

TService GetImplementation<TService>(IServiceProvider serviceProvider) where TService : class
{
    try
    {
        return serviceProvider.GetRequiredService<TService>();
    }
    catch (Exception ex)
    {
        throw;
    }
}


IServiceProvider InitializeDependency(IConfiguration configuration)
{
    ServiceCollection services = new ServiceCollection();
    services.AddLogging();
    services.AddSingleton(new MongoClient(configuration["ConnectionStrings"]));
    services.AddTransient<IChangestreamJob, ChangestreamJob>();
    services.AddTransient<IBackfillerHandler, BackFillerHandler>();
    services.AddTransient<IProcessDatabaseChange, ProcessDatabaseChange>();


    return services.BuildServiceProvider();
}