using NUnit.Framework;
using MovieDatabase;
using System;
using System.Text.Json;
using System.Linq;

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
            var metadataItem = new MetadataItem(id: 1,
                                                movieId: 2,
                                                title: "Test",
                                                language: "AnotherTest",
                                                duration: TimeSpan.Parse("10:10:10"),
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


        /// <summary>
        /// ● Only the latest piece of metadata (highest Id) should be returned where there are multiple metadata records for a given language.
        /// </summary>
        [Test]
        public void MetadataRepositoryGetByMovieId_RetrievesHighestIdForLanguage()
        {
            // Arrange
            var itemUnderTest = new MetadataRepository();
            itemUnderTest.Movies.AddRange(new[]
            {
                new MetadataItem(id:          1,
                                 movieId:     2,
                                 title:       "Test",
                                 language:    "BB",
                                 duration:    TimeSpan.Parse("10:10:10"),
                                 releaseYear: 2019),

                new MetadataItem(id:          2,
                                 movieId:     2,
                                 title:       "Test",
                                 language:    "BB",
                                 duration:    TimeSpan.Parse("10:10:10"),
                                 releaseYear: 2019),

                new MetadataItem(id:          3,
                                 movieId:     2,
                                 title:       "Test",
                                 language:    "AA",
                                 duration:    TimeSpan.Parse("10:10:10"),
                                 releaseYear: 2019),
            });

            // Act
            var data = itemUnderTest.GetByMovieId(2).ToArray();

            // Assert
            Assert.That(data.Length == 2);              // One record (id: 1) should be filtered
            Assert.That(data[0].Language == "AA");      // We should be sorted by language
            Assert.That(data[1].Language == "BB");
            Assert.That(data[1].Id == 2);              // We want the higher Id with language BB


         }
    }
}