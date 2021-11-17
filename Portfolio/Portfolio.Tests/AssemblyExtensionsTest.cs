using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portfolio.Shared.Extensions;
using System;

namespace Portfolio.Tests
{
    /// <summary>
    /// A test class for any assembly extions inside solution
    /// </summary>
    [TestClass]
    public class AssemblyExtensionsTest
    {
        /// <summary>
        /// Tries to load assembly from an empty of null folder path
        /// </summary>
        [TestMethod]
        public void LoadAsseblyFromFolderPath_Fail_NullOrEmtpyPath_ThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => string.Empty.GetOrLoadAssembly());
        }
        /// <summary>
        /// Tries to load assembly from folder pathj
        /// </summary>
        [TestMethod]
        public void LoadAsseblyFromFolderPath_Success()
        {
            //Get the assembly from the same folder path as the project already refrences it
            var assembly = "Portfolio.Shared.Extensions".GetOrLoadAssembly();

            Assert.IsNotNull(assembly);
            Assert.IsTrue(assembly.FullName?.Contains("Portfolio.Shared.Extensions"));
        }
    }
}