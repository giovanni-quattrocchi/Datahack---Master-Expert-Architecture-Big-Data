using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace PracticaEval01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PracticaController : ControllerBase
    {
        [HttpGet]
        public Result Get()
        {
            var dbClient = new MongoClient("mongodb://"+ (Environment.GetEnvironmentVariable("MONGOHOST") ?? "127.0.0.1") +":27017");
            IMongoDatabase db = dbClient.GetDatabase("testdb");
            var cars = db.GetCollection<BsonDocument>("cars");

            var result = new Result
            {
                Version = Environment.GetEnvironmentVariable("Version") ?? "1",
                Entorno = Environment.GetEnvironmentVariable("Entorno") ?? "sin definir",
                List = cars.Find(new BsonDocument()).ToList()
            };
            Console.WriteLine(JsonConvert.SerializeObject(result));
            return result;
        }
    }

    public class Result
    {
        public string? Version;
        public string? Entorno;
        public object? List;
    }
}