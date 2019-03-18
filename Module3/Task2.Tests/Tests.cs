using System;
using NUnit.Framework;

namespace Task2.Tests
{
    public class Tests
    {
        private IntConverter _intConverter;

        [SetUp]
        public void Setup()
        {
            _intConverter = new IntConverter();
        }
        [TestCase("123", 123)]
        [TestCase("-123", -123)]
        [TestCase("2147483647", int.MaxValue)]
        [TestCase("-2147483648", int.MinValue)]
        [TestCase("0001", 1)]
        [TestCase("-0001", -1)]
        [TestCase("  21  ", 21)]
        [Test]
        public void CorrectString(string str, int expectedResult)
        {
            Assert.AreEqual(expectedResult, _intConverter.ConvertStringToInt(str));
        }

        [TestCase("qq12")]
        [TestCase("--12")]
        [TestCase("")]
        [TestCase("     ")]
        [Test]
        public void IncorrectString(string str)
        {
            Assert.Throws<ArgumentException>(() => _intConverter.ConvertStringToInt(str));
        }

        [TestCase("21474836481111")]
        [TestCase("-214748364911111")]
        [Test]
        public void OverflowNubmer(string str)
        {
            Assert.Throws<OverflowException>(() => _intConverter.ConvertStringToInt(str));
        }
    }
}