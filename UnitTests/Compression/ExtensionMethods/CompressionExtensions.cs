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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoonUnit.Attributes;
using MoonUnit;
using Utilities.Compression.ExtensionMethods;
using Utilities.DataTypes.ExtensionMethods;
using Utilities.Compression.ExtensionMethods.Enums;

namespace UnitTests.Compression.ExtensionMethods
{
    public class CompressionExtensions
    {
        [Test]
        public void DeflateTest()
        {
            string Data="This is a bit of data that I want to compress";
            Assert.NotEqual("This is a bit of data that I want to compress", Data.ToByteArray().Compress().ToEncodedString());
            Assert.Equal("This is a bit of data that I want to compress", Data.ToByteArray().Compress().Decompress().ToEncodedString());
            Assert.Equal("This is a bit of data that I want to compress", Data.Compress().Decompress());
        }

        [Test]
        public void GZipTest()
        {
            string Data = "This is a bit of data that I want to compress";
            Assert.NotEqual("This is a bit of data that I want to compress", Data.ToByteArray().Compress(CompressionType.GZip).ToEncodedString());
            Assert.Equal("This is a bit of data that I want to compress", Data.ToByteArray().Compress(CompressionType.GZip).Decompress(CompressionType.GZip).ToEncodedString());
            Assert.Equal("This is a bit of data that I want to compress", Data.Compress(CompressionType: CompressionType.GZip).Decompress(CompressionType: CompressionType.GZip));
        }
    }
}
