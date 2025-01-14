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
using Utilities.DataTypes.Formatters;

namespace UnitTests.DataTypes.ExtensionMethods
{
    public class TimeSpanExtensions
    {
        [Test]
        public void DaysRemainder()
        {
            Assert.Equal(0, (new DateTime(2011, 12, 1) - new DateTime(1977, 1, 1)).DaysRemainder());
        }

        [Test]
        public void Years()
        {
            Assert.Equal(34, (new DateTime(2011, 12, 1) - new DateTime(1977, 1, 1)).Years());
        }

        [Test]
        public void Months()
        {
            Assert.Equal(11, (new DateTime(2011, 12, 1) - new DateTime(1977, 1, 1)).Months());
        }
    }
}