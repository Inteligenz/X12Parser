namespace OopFactory.X12.Tests.Unit.ExtensionsTests
{
    using System;

    using OopFactory.X12.Shared.Attributes;
    using OopFactory.X12.Shared.Extensions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EnumExtensionsTests
    {
        private enum TestEDIField
        {
            [EdiFieldValue("101")]
            Value1,

            [EdiFieldValue("102")]
            Value2,

            Value3
        }

        #region EDIFieldValue Tests

        [TestMethod]
        public void EDIFieldValue_WhenAttributeExists_ShouldReturnAttributeValue()
        {
            Assert.AreEqual("101", TestEDIField.Value1.EDIFieldValue());
            Assert.AreEqual("102", TestEDIField.Value2.EDIFieldValue());
        }

        [TestMethod]
        public void EDIFieldValue_WhenNoAttributeExists_ShouldThrowInvalidException()
        {
            // Arrange

            // Act
            InvalidOperationException exceptionThrown = null;
            try
            {
                TestEDIField.Value3.EDIFieldValue();
            }
            catch (InvalidOperationException exception)
            {
                exceptionThrown = exception;
            }

            // Assert
            Assert.IsNotNull(exceptionThrown);
        }

        #endregion

        #region ToEnumFromEDIFieldValue Tests

        [TestMethod]
        public void ToEnumFromEDIFieldValue_WhenValidEnumEDIFieldValues_ShouldReturnEnum()
        {
            Assert.AreEqual(TestEDIField.Value1 , "101".ToEnumFromEDIFieldValue<TestEDIField>());
            Assert.AreEqual(TestEDIField.Value2, "102".ToEnumFromEDIFieldValue<TestEDIField>());

        }

        #endregion

    }
}
