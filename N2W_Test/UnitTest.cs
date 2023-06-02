using Microsoft.VisualBasic;
using N2W_Core;

namespace N2W_Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMandatoryCases()
        {
            string output = String.Empty;

            output = AmountConverter.Convert("0");
            Assert.AreEqual("zero dollars", output);

            output = AmountConverter.Convert("1");
            Assert.AreEqual("one dollar", output);

            output = AmountConverter.Convert("25,1");
            Assert.AreEqual("twenty-five dollars and ten cents", output);

            output = AmountConverter.Convert("0,01");
            Assert.AreEqual("zero dollars and one cent", output);

            output = AmountConverter.Convert("45 100");
            Assert.AreEqual("forty-five thousand one hundred dollars", output);

            output = AmountConverter.Convert("999 999 999,99");
            Assert.AreEqual("nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents", output);

        }

        [TestMethod]
        public void TestExtraCases()
        {
            string output = String.Empty;

            output = AmountConverter.Convert("301.599.000,9");
            Assert.AreEqual("three hundred one million five hundred ninety-nine thousand dollars and ninety cents", output);

            output = AmountConverter.Convert("004,00");
            Assert.AreEqual("four dollars", output);

            output = AmountConverter.Convert("004,04");
            Assert.AreEqual("four dollars and four cents", output);

            output = AmountConverter.Convert(",19");
            Assert.AreEqual("zero dollars and nineteen cents", output);

            output = AmountConverter.Convert("20 478,49");
            Assert.AreEqual("twenty thousand four hundred seventy-eight dollars and forty-nine cents", output);

            output = AmountConverter.Convert("3 000 000 000 000");
            Assert.AreEqual("three trillion dollars", output);

            output = AmountConverter.Convert("700 000 000 444 599 000,9");
            Assert.AreEqual("seven hundred quadrillion four hundred forty-four million five hundred ninety-nine thousand dollars and ninety cents", output);

            output = AmountConverter.Convert("");
            Assert.IsTrue(output.Contains("Error Occured"));

            output = AmountConverter.Convert("-1992,2");
            Assert.IsTrue(output.Contains("Error Occured"));

            output = AmountConverter.Convert(",-2");
            Assert.IsTrue(output.Contains("Error Occured"));

            output = AmountConverter.Convert(",999999");
            Assert.IsTrue(output.Contains("Error Occured"));

            output = AmountConverter.Convert("abcd");
            Assert.IsTrue(output.Contains("Error Occured"));
        }
    }
}