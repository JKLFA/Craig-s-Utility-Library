﻿/*
Copyright (c) 2011 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoonUnit.Attributes;
using Utilities.IO.ExtensionMethods;
using System.IO;
using MoonUnit;
using System.Reflection;
using System.Xml;
#endregion

namespace UnitTests.IO.ExtensionMethods
{
    public class Serialization : IDisposable
    {
        public TestClass TestItem { get; set; }

        public Serialization() { new DirectoryInfo(@".\Testing").Create(); TestItem = new TestClass(); TestItem.ID = 123; TestItem.Content = "This is test content"; }

        [Test]
        public void ToBinary()
        {
            byte[] Content= TestItem.ToBinary(@".\Testing\Test.dat");
            Assert.NotNull(Content);
            Assert.NotEmpty(Content);
            TestClass Temp = Content.ToObject<TestClass>();
            Assert.Equal(123, Temp.ID);
            Assert.Equal("This is test content", Temp.Content);
        }

        [Test]
        public void ToXML()
        {
            Assert.NotNull(TestItem.ToXML(@".\Testing\Test.xml"));
            TestClass Temp = new FileInfo(@".\Testing\Test.xml").Read().XMLToObject<TestClass>();
            Assert.Equal(123, Temp.ID);
            Assert.Equal("This is test content", Temp.Content);
        }

        [Test]
        public void ToXML2()
        {
            Assert.NotNull(TestItem.ToXML(@".\Testing\Test.xml"));
            XmlDocument Document = new XmlDocument();
            Document.LoadXml(new FileInfo(@".\Testing\Test.xml").Read());
            TestClass Temp=Document.ToObject<TestClass>();
            Assert.Equal(123, Temp.ID);
            Assert.Equal("This is test content", Temp.Content);
        }

        [Test]
        public void ToXML3()
        {
            Assert.NotNull(TestItem.ToXML(@".\Testing\Test.xml"));
            TestClass Temp = new FileInfo(@".\Testing\Test.xml").XMLToObject<TestClass>();
            Assert.Equal(123, Temp.ID);
            Assert.Equal("This is test content", Temp.Content);
        }

        [Test]
        public void ToJSON()
        {
            Assert.Equal(@"{""<Content>k__BackingField"":""This is test content"",""<ID>k__BackingField"":123}", TestItem.ToJSON(@".\Testing\Test.dat"));
            TestClass Temp = new FileInfo(@".\Testing\Test.dat").Read().JSONToObject<TestClass>();
            Assert.Equal(123, Temp.ID);
            Assert.Equal("This is test content", Temp.Content);
        }

        [Test]
        public void ToJSON2()
        {
            TestItem.ToJSON(@".\Testing\Test.dat");
            TestClass Temp = new FileInfo(@".\Testing\Test.dat").JSONToObject<TestClass>();
            Assert.Equal(123, Temp.ID);
            Assert.Equal("This is test content", Temp.Content);
        }

        [Test]
        public void ToSOAP()
        {
            Assert.Equal(@"<SOAP-ENV:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/"" xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:clr=""http://schemas.microsoft.com/soap/encoding/clr/1.0"" SOAP-ENV:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
<SOAP-ENV:Body>
<a1:TestClass id=""ref-1"" xmlns:a1=""http://schemas.microsoft.com/clr/nsassem/UnitTests.IO.ExtensionMethods/UnitTests%2C%20Version%3D1.0.0.0%2C%20Culture%3Dneutral%2C%20PublicKeyToken%3Dc774ddc815643583"">
<_x003C_ID_x003E_k__BackingField>123</_x003C_ID_x003E_k__BackingField>
<_x003C_Content_x003E_k__BackingField id=""ref-3"">This is test content</_x003C_Content_x003E_k__BackingField>
</a1:TestClass>
</SOAP-ENV:Body>
</SOAP-ENV:Envelope>
", TestItem.ToSOAP(@".\Testing\Test.xml"));
            TestClass Temp = new FileInfo(@".\Testing\Test.xml").Read().SOAPToObject<TestClass>();
            Assert.Equal(123, Temp.ID);
            Assert.Equal("This is test content", Temp.Content);
        }

        [Test]
        public void ToSOAP2()
        {
            TestItem.ToSOAP(@".\Testing\Test.xml");
            TestClass Temp = new FileInfo(@".\Testing\Test.xml").SOAPToObject<TestClass>();
            Assert.Equal(123, Temp.ID);
            Assert.Equal("This is test content", Temp.Content);
        }

        public void Dispose()
        {
            new DirectoryInfo(@".\Testing").DeleteAll();
        }
    }

    [Serializable]
    public class TestClass
    {
        public int ID { get; set; }
        public string Content { get; set; }
    }
}