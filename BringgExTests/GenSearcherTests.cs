using BringgEx;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BringgExTests
{
    [TestClass]
    public class GenSearcherTests
    {
        private GenSearcher _genSearcher;

        [TestInitialize]
        public void SetUp()
        {
            _genSearcher = new GenSearcher();
        }

        [TestMethod]
        public void TestIsGenValid()
        {
            string gen = "AAAAAAAAAAAGCGCGCTTAGG";
            bool isValid = _genSearcher.IsGenValid(gen);
            Assert.IsTrue(isValid);

            gen = "AGCGCGCTTAGG";
            isValid = _genSearcher.IsGenValid(gen);
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void TestIsGenExist()
        {
            string gen = "GCGCGCTTAGG";
            bool isExist = _genSearcher.IsGenExist(gen);
            Assert.IsTrue(isExist);

            gen = "AGCGCGCTTAGG";
            isExist = _genSearcher.IsGenExist(gen);
            Assert.IsFalse(isExist);
        }
    }
}
