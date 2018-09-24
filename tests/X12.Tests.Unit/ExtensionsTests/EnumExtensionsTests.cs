namespace X12.Tests.Unit.ExtensionsTests
{
    using System;

    using X12.Shared.Attributes;
    using X12.Shared.Extensions;

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
            Assert.AreEqual("101", TestEDIField.Value1.EdiFieldValue());
            Assert.AreEqual("102", TestEDIField.Value2.EdiFieldValue());
        }

        [TestMethod]
        public void EDIFieldValue_WhenNoAttributeExists_ShouldThrowInvalidException()
        {
            // Arrange

            // Act
            InvalidOperationException exceptionThrown = null;
            try
            {
                TestEDIField.Value3.EdiFieldValue();
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
            Assert.AreEqual(TestEDIField.Value1 , "101".ToEnumFromEdiFieldValue<TestEDIField>());
            Assert.AreEqual(TestEDIField.Value2, "102".ToEnumFromEdiFieldValue<TestEDIField>());

        }

        #endregion

    }
}
