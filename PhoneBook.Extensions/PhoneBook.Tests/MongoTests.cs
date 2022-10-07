using MongoDB.Bson.Serialization.Attributes;
using PhoneBook.Extensions;
using System;
using System.Collections.Generic;
using Xunit;

namespace PhoneBook.Tests
{
    public class MongoTests
    {
        [Fact]
        public void CollectionListTests()
        {
            ITestRepository testRepository = new TestRepository();
            var result = testRepository.GetList();
        }

        [Fact]
        public void CollectionAddTests()
        {
            ITestRepository testRepository = new TestRepository();
            testRepository.InsertItem(new Test { Text = "deneme 4" });
            var result = testRepository.GetList();
        }
    }

    public class Test : Extensions.MongoDB.BaseCollection
    {
        [BsonElement("text")]
        public string Text { get; set; }
    }

    public interface ITestRepository : Extensions.MongoDB.IBaseRepository<Test>
    {

    }

    public class TestRepository : Extensions.MongoDB.BaseRepository<Test>, ITestRepository
    {
        private Extensions.MongoDB.MongoDBConnectionSetting _mongoDBConnectionSetting = new Extensions.MongoDB.MongoDBConnectionSetting
        {

        };

        public TestRepository() : base(new Extensions.MongoDB.MongoDBConnectionSetting
        {
            CollectionName = "test_collection",
            DBName = "phonebook_db",
            Password = "user123456",
            UserName = "phonebook_user"
        })
        {

        }
    }
}
