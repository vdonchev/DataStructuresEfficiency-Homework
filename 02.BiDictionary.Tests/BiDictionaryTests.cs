namespace _02.BiDictionary.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BiDictionaryTests
    {
        private BiDictionary<string, string, int> distances;

        [TestInitialize]
        public void TestInitialize()
        {
            this.distances = new BiDictionary<string, string, int>();

            this.distances.Add("Sofia", "Varna", 443);
            this.distances.Add("Sofia", "Varna", 468);
            this.distances.Add("Sofia", "Varna", 490);
            this.distances.Add("Sofia", "Plovdiv", 145);
            this.distances.Add("Sofia", "Bourgas", 383);
            this.distances.Add("Plovdiv", "Bourgas", 253);
            this.distances.Add("Plovdiv", "Bourgas", 292);
        }

        [TestMethod]
        public void MultiplySearches_ShouldFindCorrectly()
        {
            var distancesFromSofia = this.distances.FindByKey1("Sofia"); // [443, 468, 490, 145, 383]
            var expectedFromSofia = new[]
            {
                443, 468, 490, 145, 383
            };

            CollectionAssert.AreEqual(expectedFromSofia, distancesFromSofia.ToArray());

            var distancesToBourgas = this.distances.FindByKey2("Bourgas"); // [383, 253, 292]
            var expectedToBourgas = new[]
            {
                383, 253, 292
            };

            CollectionAssert.AreEqual(expectedToBourgas, distancesToBourgas.ToArray());

            var distancesPlovdivBourgas = this.distances.Find("Plovdiv", "Bourgas"); // [253, 292]
            var expectedPlovdivBourgas = new[]
            {
                253, 292
            };

            CollectionAssert.AreEqual(expectedPlovdivBourgas, distancesPlovdivBourgas.ToArray());

            var distancesRousseVarna = this.distances.Find("Rousse", "Varna"); // []
            var expectedRousseVarna = new int[0];

            CollectionAssert.AreEqual(expectedRousseVarna, distancesRousseVarna.ToArray());

            var distancesSofiaVarna = this.distances.Find("Sofia", "Varna"); // [443, 468, 490]
            var expectedSofiaVarna = new[]
            {
                443, 468, 490
            };

            CollectionAssert.AreEqual(expectedSofiaVarna, distancesSofiaVarna.ToArray());
        }

        [TestMethod]
        public void Remove_ShouldRemoveCorrectly()
        {
            var distancesSofiaVarna = this.distances.Find("Sofia", "Varna"); // [443, 468, 490]
            var expectedSofiaVarna = new[]
            {
                443, 468, 490
            };

            CollectionAssert.AreEqual(expectedSofiaVarna, distancesSofiaVarna.ToArray());

            var removed = this.distances.Remove("Sofia", "Varna"); // true
            Assert.IsTrue(removed);

            var distancesSofiaVarnaAgain = this.distances.Find("Sofia", "Varna"); // []
            CollectionAssert.AreEqual(new int[0], distancesSofiaVarnaAgain.ToArray());
        }

        [TestMethod]
        public void Remove_Search_ShouldFindCorrectly()
        {
            var removed = this.distances.Remove("Sofia", "Varna"); // true
            Assert.IsTrue(removed);

            var distancesFromSofiaAgain = this.distances.FindByKey1("Sofia"); // [145, 383]
            var expectedSofiaVarna = new[]
            {
                145, 383
            };

            CollectionAssert.AreEqual(expectedSofiaVarna, distancesFromSofiaAgain.ToArray());

            var distancesToVarna = this.distances.FindByKey2("Varna"); // []
            CollectionAssert.AreEqual(new int[0], distancesToVarna.ToArray());

            var distancesSofiaVarnaAgain = this.distances.Find("Sofia", "Varna"); // []
            CollectionAssert.AreEqual(new int[0], distancesSofiaVarnaAgain.ToArray());
        }
    }
}
