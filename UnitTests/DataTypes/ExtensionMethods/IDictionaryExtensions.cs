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
using MoonUnit;
using MoonUnit.Attributes;
using Utilities.DataTypes.ExtensionMethods;
using System.Data;

namespace UnitTests.DataTypes.ExtensionMethods
{
    public class IDictionaryExtensions
    {
        [Test]
        public void SortTest()
        {
            IDictionary<string, int> Test = new Dictionary<string, int>();
            Test.Add("Q", 4);
            Test.Add("Z", 2);
            Test.Add("C", 3);
            Test.Add("A", 1);
            Test = Test.Sort();
            string Value = "";
            foreach (string Key in Test.Keys)
                Value += Key;
            Assert.Equal("ACQZ", Value);
        }

        [Test]
        public void SortByValueTest()
        {
            IDictionary<string, int> Test = new Dictionary<string, int>();
            Test.Add("Q", 4);
            Test.Add("Z", 2);
            Test.Add("C", 3);
            Test.Add("A", 1);
            Test = Test.SortByValue();
            string Value = "";
            foreach (string Key in Test.Keys)
                Value += Test[Key].ToString();
            Assert.Equal("1234", Value);
        }
    }
}