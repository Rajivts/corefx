﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text;
using Xunit;

namespace System.Xml.Tests
{
    public class XmlTextReaderReadTests
    {
        [Fact]
        public void XmlTextReaderReadContentAsBas64Test()
        {
            byte[] byteData = Encoding.ASCII.GetBytes("Sample String");
            XmlTextReader textReader =
                XmlTextReaderTestHelper.CreateReader($"<List xmlns:ns='urn:NameSpace'><element1 ns:attr='{Convert.ToBase64String(byteData)}' /></List>", 
                new NameTable());
            Assert.True(textReader.Read());
            Assert.True(textReader.Read());
            Assert.True(textReader.MoveToAttribute("attr", "urn:NameSpace"));
            var output = new byte[byteData.Length];
            Assert.Equal(byteData.Length, textReader.ReadContentAsBase64(output, 0, byteData.Length));
            Assert.Equal("Sample String", Encoding.ASCII.GetString(output));
            Assert.Equal(byteData, output);
        }

        [Fact]
        public void XmlTextReaderReadContentAsBinHexTest()
        {
            byte[] byteData = Encoding.ASCII.GetBytes("Sample String");
            XmlTextReader textReader =
                XmlTextReaderTestHelper.CreateReader(
                    $"<List xmlns:ns='urn:NameSpace'><element1 ns:attr='{BitConverter.ToString(byteData).Replace("-", "")}' /></List>",
                    new NameTable());
            Assert.True(textReader.Read());
            Assert.True(textReader.Read());
            Assert.True(textReader.MoveToAttribute("attr", "urn:NameSpace"));
            byte[] output = new byte[byteData.Length];
            Assert.Equal(byteData.Length, textReader.ReadContentAsBinHex(output, 0, byteData.Length));
            Assert.Equal("Sample String", Encoding.ASCII.GetString(output));
            Assert.Equal(byteData, output);
        }

        [Fact]
        public void XmlTextReaderReadCharsTest()
        {
            string expectedOutput = "Sample String";
            XmlTextReader textReader = XmlTextReaderTestHelper.CreateReader($"<element1 attr='val'>{expectedOutput}</element1>", new NameTable());
            Assert.True(textReader.Read());
            var output = new char[expectedOutput.Length];
            Assert.Equal(output.Length, textReader.ReadChars(output, 0, output.Length));
            Assert.Equal(expectedOutput, new string(output));
        }

        [Fact]
        public void XmlTextReaderReadBase64Test()
        {
            byte[] byteData = Encoding.ASCII.GetBytes("Sample String");
            XmlTextReader textReader = 
                XmlTextReaderTestHelper.CreateReader($"<element1 attr='val'>{Convert.ToBase64String(byteData)}</element1>", new NameTable());
            Assert.True(textReader.Read());
            var output = new byte[byteData.Length];
            Assert.Equal(output.Length, textReader.ReadBase64(output, 0, output.Length));
            Assert.Equal("Sample String", Encoding.ASCII.GetString(output));
            Assert.Equal(byteData, output);
        }

        [Fact]
        public void XmlTextReaderReadBinHexTest()
        {
            byte[] byteData = Encoding.ASCII.GetBytes("Sample String");
            XmlTextReader textReader =
                XmlTextReaderTestHelper.CreateReader(
                    $"<element1 attr='val'>{BitConverter.ToString(byteData).Replace("-", "")}</element1>", new NameTable());
            Assert.True(textReader.Read());
            var output = new byte[byteData.Length];
            Assert.Equal(output.Length, textReader.ReadBinHex(output, 0, output.Length));
            Assert.Equal("Sample String", Encoding.ASCII.GetString(output));
            Assert.Equal(byteData, output);
        }
    }
}
