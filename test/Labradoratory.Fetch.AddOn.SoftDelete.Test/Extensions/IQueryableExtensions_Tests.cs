using System.Collections.Generic;
using System.Linq;
using Xunit;
using Labradoratory.Fetch.AddOn.SoftDelete.Extensions;

namespace Labradoratory.Fetch.AddOn.SoftDelete.Test.Extensions
{
    public class IQueryableExtensions_Tests
    {
        [Fact]
        public void GetDeleted_Success()
        {
            var expected = new TestEntity { IsDeleted = true };
            var subject = new List<TestEntity>
            {
                new TestEntity(),
                new TestEntity(),
                expected,
                new TestEntity()
            }.AsQueryable();

            var result = subject.GetDeleted();

            Assert.Single(result);
            Assert.Contains(expected, result);
        }

        [Fact]
        public void GetNotDeleted_Success()
        {
            var expected = new TestEntity { IsDeleted = true };
            var subject = new List<TestEntity>
            {
                new TestEntity(),
                new TestEntity(),
                expected,
                new TestEntity()
            }.AsQueryable();

            var result = subject.GetNotDeleted();

            Assert.True(result.Count() == 3);
            Assert.DoesNotContain(expected, result);
        }

        private class TestEntity : Entity, ISoftDeletable
        {
            public bool IsDeleted { get; set; }

            public override object[] DecodeKeys(string encodedKeys)
            {
                throw new System.NotImplementedException();
            }

            public override string EncodeKeys()
            {
                throw new System.NotImplementedException();
            }

            public override object[] GetKeys()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
