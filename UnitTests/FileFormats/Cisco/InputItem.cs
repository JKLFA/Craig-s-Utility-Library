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

namespace UnitTests.FileFormats.Cisco
{
    public class InputItem
    {
        protected Utilities.Cisco.InputItem Entry = null;
        protected Utilities.Random.Random Random = null;

        public InputItem()
        {
            Entry = new Utilities.Cisco.InputItem();
            Random = new Utilities.Random.Random();
        }

        [Test]
        public void NullTest()
        {
            Entry.DefaultValue = null;
            Entry.DisplayName = null;
            Entry.InputFlags = default(Utilities.Cisco.InputFlag);
            Entry.QueryStringParam = null;
            Assert.NotEmpty(Entry.ToString());
        }

        [Test]
        public void RandomTest()
        {
            Entry.DefaultValue = Random.NextString(30);
            Entry.DisplayName = Random.NextString(30);
            Entry.QueryStringParam = Random.NextString(30);
            Assert.Contains(Entry.DefaultValue, Entry.ToString());
            Assert.Contains(Entry.DisplayName, Entry.ToString());
            Assert.Contains(Entry.QueryStringParam, Entry.ToString());
        }
    }
}
