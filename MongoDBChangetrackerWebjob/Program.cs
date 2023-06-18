using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDBChangetrackerWebjobCore.Repositories.Interface;
using MongoDBChangetrackerWebjobCoreInfra.Repositories;
using Microsoft.Extensions.Logging;




IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

IServiceProvider serviceProvider = InitializeDependency(configuration);

 IChangestreamJob changestream = GetImplementation<IChangestreamJob>(serviceProvider);




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
 


    return services.BuildServiceProvider();
}