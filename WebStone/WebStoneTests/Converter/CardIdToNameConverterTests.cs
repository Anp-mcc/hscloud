using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStone.Converter;

namespace WebStoneTests.Converter
{
    [TestClass]
    public class CardIdToNameConverterTests
    {
        private CardIdToNameConverter _target;

        [TestInitialize]
        public void SetUp()
        {
            _target = new CardIdToNameConverter();
        }

        [TestMethod]
        public void Convert_AnyString_ReturnId()
        {
            var result = _target.Convert("someId");

            Assert.AreEqual("someId", result);
        }
    }
}