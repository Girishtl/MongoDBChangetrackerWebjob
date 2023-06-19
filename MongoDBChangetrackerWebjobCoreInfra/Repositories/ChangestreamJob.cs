using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using MongoDBChangetrackerWebjobCore.Repositories.Interface;

namespace MongoDBChangetrackerWebjobCoreInfra.Repositories
{
    public class ChangestreamJob : IChangestreamJob
    {
        private readonly MongoClient _mongoClient;
      
 
        
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
                   

                    operation(ConvertChangestreamtoJson(enumerator.Current));
                }
            }

            Console.WriteLine("stop");

        }


        private string ConvertChangestreamtoJson(ChangeStreamDocument<BsonDocument> changeStreamDoc)
        {

            var jsonObject = new
            {
                DocumentKey = changeStreamDoc.DocumentKey.ToJson(),
                OperationType = changeStreamDoc.OperationType,
                FullDocument = changeStreamDoc.FullDocument.ToJson(),
                DatabaseNamespace = changeStreamDoc.DatabaseNamespace.DatabaseName,
                collectionNamespace = changeStreamDoc.CollectionNamespace.CollectionName,
                UpdateDescription = changeStreamDoc.UpdateDescription.UpdatedFields.ToJson(),
                
            };



            return Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject);

        }
        

    }
}
