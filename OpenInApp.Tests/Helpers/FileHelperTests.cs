﻿using NUnit.Framework;
using OpenInApp.Helpers;
using System.Collections.Generic;

namespace OpenInApp.Tests.Helpers.Tests
{
    [TestFixture()]
    public class FileHelperTests
    {
        private IEnumerable<string> typicalFileExtensions = new List<string>
        {
                "config",
                "csproj",
                "docx",
                "properties",
                "runsettings",
                "settings",
                "vsixmanifest",
                "wsdl",
                "xml",
                "xsd",
                "xslt",
        };

        [Test()]
        [TestCase("", false)]
        [TestCase(".", false)]
        [TestCase(".properties", true)]
        [TestCase(".propertis", false)]
        [TestCase(".xml", true)]
        [TestCase(".xsd", true)]
        [TestCase(".xslt", true)]
        [TestCase("a.properties", true)]
        [TestCase("BartSimpson.fest,wsd", false)]
        [TestCase("FredBloggs.x.ml", false)]
        [TestCase("HillsTrump.vs.DonnieClinton", false)]
        [TestCase("JaneDoe.xslt", true)]
        [TestCase("JoePublic.cs", false)]
        [TestCase("JohnDoe.Xml", true)]
        [TestCase("MadsKristensen.", false)]
        [TestCase(null, false)]
        [Category("I")]
        public void IsTypicalFileTest(string fileName, bool expected)
        {
            var fullFileNames = new List<string> { fileName };

            //Act
            var actual = FileHelper.AreTypicalFileExtensions(fullFileNames, typicalFileExtensions);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        [Category("U")]
        public void GetDefaultTypicalFileExtensionsAsCsvTest()
        {
            //Arrange
            var testList = new List<string> { "a", "b", "c" };

            //Act
            var actual = FileHelper.GetDefaultTypicalFileExtensionsAsCsv(testList);

            //Assert
            Assert.AreEqual("a,b,c", actual);
        }

        [TestCase("a,b,c")]
        [Category("U")]
        public void GetTypicalFileExtensionAsListTest(string csvString)
        {
            //Arrange
            var expected = new List<string> { "a", "b", "c" };

            //Act
            var actual = FileHelper.GetTypicalFileExtensionAsList(csvString);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}