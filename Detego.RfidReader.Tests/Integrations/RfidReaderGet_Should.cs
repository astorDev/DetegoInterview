using System;
using System.Linq;
using System.Threading.Tasks;
using Detego.RfidReader.Domain;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rfid;

namespace Detego.RfidReader.Tests.Integrations
{
    [TestClass]
    public class RfidReaderGetSeenTags_Should
    {
        private readonly WebApplicationFactory factory = new WebApplicationFactory();
        
        [TestMethod]
        public async Task ReturnTagAWithSetTimeOfLastSeenAndFirstSeen_WhenItIsTheOnlySeenTag()
        {
            var client = this.factory.Create();
            Clock.Time = new DateTime(2020, 2, 2);
            this.factory.FakeRfidReader.TagSeen += Raise.With(new TagSeenEventArgs
            {
                Identifier = "A"
            });
            
            var seenTags = await client.GetSeenTagsAsync();
            Assert.AreEqual(1, seenTags.Count);
            
            var seenTag = seenTags.Tags.Single();
            Assert.AreEqual("A", seenTag.Id);
            Assert.AreEqual(new DateTime(2020, 2, 2), seenTag.FirstSeen);
            Assert.AreEqual(new DateTime(2020, 2, 2), seenTag.LastSeen);
        }

        [TestMethod]
        public async Task ReturnValidTagsSet_WhenFewEventsOccured()
        {
            var client = this.factory.Create();
            
            Clock.Time = new DateTime(2020, 2,2);
            this.factory.FakeRfidReader.TagSeen += Raise.With(new TagSeenEventArgs
            {
                Identifier = "X1"
            });
            
            Clock.Time = new DateTime(2020, 2, 3);
            this.factory.FakeRfidReader.TagSeen += Raise.With(new TagSeenEventArgs
            {
                Identifier = "X2"
            });
            
            Clock.Time = new DateTime(2020, 2, 4);
            this.factory.FakeRfidReader.TagSeen += Raise.With(new TagSeenEventArgs
            {
                Identifier = "X1"
            });

            var seenTags = await client.GetSeenTagsAsync();
            Assert.AreEqual(2, seenTags.Count);

            var tagX1 = seenTags.Tags.Single(t => t.Id == "X1");
            var tagX2 = seenTags.Tags.Single(t => t.Id == "X2");
            
            Assert.AreEqual(2, tagX1.SeenTimes);
            Assert.AreEqual(new DateTime(2020, 2, 2),  tagX1.FirstSeen);
            Assert.AreEqual(new DateTime(2020, 2, 4), tagX1.LastSeen);
            
            Assert.AreEqual(1, tagX2.SeenTimes);
            Assert.AreEqual(new DateTime(2020, 2, 3), tagX2.FirstSeen);
            Assert.AreEqual(new DateTime(2020, 2, 3), tagX2.LastSeen);
        }

        [TestMethod]
        public async Task DisplayValidCount_WhenReadInParallel()
        {
            var client = this.factory.Create();

            var tasks = Enumerable.Range(1, 10000).Select((i) => Task.Run(() =>
            {
                this.factory.FakeRfidReader.TagSeen += Raise.With(new TagSeenEventArgs
                {
                    Identifier = "XXX"
                });
            }));

            await Task.WhenAll(tasks);

            var tags = await client.GetSeenTagsAsync();
            Assert.AreEqual(10000, tags.Tags.Single().SeenTimes);
        }
    }
}