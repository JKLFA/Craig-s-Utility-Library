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

namespace UnitTests.Caching
{
    public class Cache
    {
        [Test]
        public void CacheTest()
        {
            Utilities.Caching.Cache<string> TestObject = new Utilities.Caching.Cache<string>();
            Assert.DoesNotThrow<Exception>(() => TestObject.Add("A", "Testing"));
            Assert.DoesNotThrow<Exception>(() => TestObject.Add("B", "Testing2"));
            Assert.DoesNotThrow<Exception>(() => TestObject.Add("C", "Testing3"));
            Assert.Equal(3, TestObject.Count);
            Assert.Equal("Testing", TestObject.Get<string>("A"));
            Assert.DoesNotThrow<Exception>(() => TestObject.Remove("A"));
            Assert.Equal(null, TestObject.Get<string>("A"));
            Assert.Equal(2, TestObject.Count);
            Assert.DoesNotThrow<Exception>(() => TestObject.Clear());
            Assert.Equal(0, TestObject.Count);
        }
    }
}