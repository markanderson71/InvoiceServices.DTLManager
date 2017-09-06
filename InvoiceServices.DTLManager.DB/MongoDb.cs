using System;
using InvoiceServices.DTLManager.Core;
using InvoiceServices.DTLManager.Core.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace InvoiceServices.DTLManager.DB
{
    public class MongoDb : IRepository
    {
        private MongoClient client;
        private IMongoDatabase database;

        public IMongoCollection<LineItem> InvoiceCollection { get; }

        public MongoDb()
        {

            DatabaseSettings dbSettings = new DatabaseSettings
            {
                ConnectionString = "mongodb://localhost:27017", DatabaseName = "InvoiceDetails"

            };

            this.client = new MongoClient(dbSettings.ConnectionString);
            this.database = client.GetDatabase(dbSettings.DatabaseName);
            this.InvoiceCollection = database.GetCollection<LineItem>("LineItems");

            if (!BsonClassMap.IsClassMapRegistered(typeof(LineItem)))
            {
                SetSerializationForObjectId();
            }
        }

        private static void SetSerializationForObjectId()
        {
            BsonClassMap.RegisterClassMap<LineItem>(cm => { cm.AutoMap(); cm.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId)); });
            // BsonClassMap.RegisterClassMap<Customer>(cm => { cm.AutoMap(); cm.IdMemberMap(new StringSerializer(BsonType.ObjectId)); });
        }


        public string Add(LineItem lineItem)
        {
            lineItem.Id = ObjectId.GenerateNewId().ToString();

            InvoiceCollection.InsertOne(lineItem);
            return lineItem.Id;
        }

        public LineItem GetItem(string itemId)
        {
            return InvoiceCollection.AsQueryable().Where(item => item.Id == itemId).SingleOrDefault();
        }


        public IEnumerable<LineItem> GetAll()
        {
            return InvoiceCollection.AsQueryable().Where(_ => true);
        }
    }
}
