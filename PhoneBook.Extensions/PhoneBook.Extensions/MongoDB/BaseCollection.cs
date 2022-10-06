using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace PhoneBook.Extensions.MongoDB
{
    public class BaseCollection
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        public string RowId => Id.ToString();
    }
}
