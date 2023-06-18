using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBChangetrackerWebjobCore.Repositories.Interface;

namespace MongoDBChangetrackerWebjobCoreInfra.Repositories
{
    public class ChangestreamJob : IChangestreamJob
    {
        private readonly MongoClient _mongoClient;
      
        //private readonly IConfiguration _configuration;


        
        public ChangestreamJob(MongoClient mongoClient)
        {
            _mongoClient = mongoClient;               
          
           

        }


        public void WatchDatabsechange(Func<string,bool> operation) 
        {
            var database = _mongoClient.GetDatabase("sample_airbnb");
            //var collection = database.GetCollection<BsonDocument>("your_collection_name");
            var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<BsonDocument>>().Match(change => change.OperationType == ChangeStreamOperationType.Update);
            var options = new ChangeStreamOptions { FullDocument = ChangeStreamFullDocumentOption.UpdateLookup };
            var cursor = database.Watch(pipeline, options);
            using (var enumerator = cursor.ToEnumerable().GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var change = enumerator.Current;
                    var document = change.FullDocument; // Access the changed document
                   Console.WriteLine(document);
                    operation(change.ToJson());
                }
            }

            Console.WriteLine("stop");

        }


    }
}
