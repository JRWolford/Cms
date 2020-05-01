using Cms.Core.Wpf.Utils;
using NUnit.Framework;
using System;
using System.Linq;

namespace Cms.Core.Wpf.Tests.Utils
{
    /// <summary>
    ///     This test fixture contains all tests for the <see cref="RegionNames"/> class.
    /// </summary>
    [TestFixture]
    public sealed class RegionNamesTests
    {
        /// <summary>
        ///     Asserts that no two fields in the <see cref="RegionNames"/> have the same value.
        /// </summary>
        [Test]
        public void NoRegionNamesShouldMatch()
        {
            string field1Value = string.Empty;
            foreach (var field1 in typeof(RegionNames).GetFields())
            {
                if (field1.FieldType != typeof(string))
                    throw new Exception($@"The {nameof(RegionNames)} class is not expected to have any properties that are not strings.");

                field1Value = (string)field1.GetValue(null);
                foreach(var field2 in typeof(RegionNames).GetFields().Where(f => f.Name != field1.Name))
                {
                    if ((string)field2.GetValue(null) == field1Value)
                        Assert.Fail($@"The fields, {field1.Name} and {field2.Name}, have the same region name value.");
                }
            }

            Assert.Pass();
        }
    }
}
