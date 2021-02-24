using NUnit.Framework;
using MovieDatabase;
using System;
using System.Text.Json;

namespace MovieDatabase.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MetadataItemConstructor_SetsPropertiesCorrectly()
        {
            // Act
            var metadataItem = new MetadataItem(id:          1, 
                                                movieId:     2, 
                                                title:       "Test", 
                                                language:    "AnotherTest", 
                                                duration:    TimeSpan.Parse("10:10:10"),  
                                                releaseYear: 2019);

            // Assert
            Assert.AreEqual(metadataItem.Id, 1);
            Assert.AreEqual(metadataItem.MovieId, 2);
            Assert.AreEqual(metadataItem.Title, "Test");
            Assert.AreEqual(metadataItem.Duration, TimeSpan.Parse("10:10:10"));
            Assert.AreEqual(metadataItem.ReleaseYear, 2019);
        }

        [Test]
        public void MetadateItem_Can_Deserialise_From_Json_Using_DotNet_System_Text_Json()
        {
            // Arrange
            const string json = "{\"movieId\": 3, \"title\": \"Elysium\",\"language\": \"EN\",\"duration\": \"1:49:00\",\"releaseYear\": 2013}";
           
            // Act
            var metadataItem = JsonSerializer.Deserialize<MetadataItem>(json, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            // Assert
            Assert.AreEqual(metadataItem.MovieId, 3);
            Assert.AreEqual(metadataItem.Title, "Elysium");
            Assert.AreEqual(metadataItem.Duration, TimeSpan.Parse("1:49:00"));
            Assert.AreEqual(metadataItem.ReleaseYear, 2013);
        }
    }
}