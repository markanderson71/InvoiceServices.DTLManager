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

        public MongoDb(DatabaseSettings dbSettings)
        {

            //DatabaseSettings dbSettings = new DatabaseSettings
            //{
            //    ConnectionString = "mongodb://mongodb", DatabaseName = "InvoiceDetails"

            //};

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
            BsonClassMap.RegisterClassMap<LineItem>(cm => { cm.AutoMap(); cm.UnmapMember(m => m.LineItemTotal); });
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


        public IEnumerable<LineItem> GetAllCreated()
        {
            return InvoiceCollection.AsQueryable().Where(l => l.Status == "created");
        }

        public bool IsAvailable()
        {
            var command = new BsonDocumentCommand<BsonDocument>(new BsonDocument { { "dbstats", 1 }, { "scale", 1 } });

            var result = database.RunCommand<BsonDocument>(command);

            return true;
        }

        public void Delete(string itemId)
        {
            LineItem existingItem = GetItem(itemId);
            if (existingItem != null)
            {
                if (existingItem.Status == "canceled")
                {
                    throw new ArgumentOutOfRangeException($"ItemId was found but already canceled in the database for value:{itemId}");
                }
                else
                {
                    existingItem.Status = "canceled";
                    existingItem.ModifiedOn = DateTime.UtcNow;
                    InvoiceCollection.FindOneAndReplace<LineItem>(p => p.Id == itemId, existingItem);
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException($"ItemId was not found in the database for value:{itemId}");
            }
        }
    }
}
