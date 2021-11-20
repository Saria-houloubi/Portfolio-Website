using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portfolio.Core.Abstractions;
using Portfolio.Web.Services;
using System;

namespace Portfolio.Tests
{
    [TestClass]
    public class UtcDateTimeProviderTests
    {
        #region Properties

        private IDateTimeProvider _dateTimeProvider;
        #endregion

        [TestInitialize]
        public void Setup()
        {
            _dateTimeProvider = new UtcDateTimeProvider();
        }
        /// <summary>
        /// Checks if the provider returns UTC timezone
        /// </summary>
        [TestMethod]
        public void Get_Date_Now_UTC()
        {
            var now = _dateTimeProvider.Now;

            Assert.IsTrue(now.ToString("yyyy-mm-dd hh:MM") == DateTime.UtcNow.ToString("yyyy-mm-dd hh:MM"));
        }
    }
}
