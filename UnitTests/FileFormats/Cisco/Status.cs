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
    public class Status
    {
        protected Utilities.Cisco.Status Entry = null;
        protected Utilities.Random.Random Random = null;

        public Status()
        {
            Entry = new Utilities.Cisco.Status();
            Random = new Utilities.Random.Random();
        }

        [Test]
        public void NullTest()
        {
            Entry.SoftKeys = null;
            Entry.Text = null;
            Entry.Timer = 0;
            Entry.URL = null;
            Entry.X = 0;
            Entry.Y = 0;
            Assert.NotEmpty(Entry.ToString());
        }

        [Test]
        public void NullItemTest()
        {
            Entry.SoftKeys.Add(null);
            Assert.NotEmpty(Entry.ToString());
        }

        [Test]
        public void RandomTest()
        {
            Entry.Text = Random.NextString(30);
            Entry.URL = Random.NextString(30);
            Entry.X = Random.Next(int.MinValue, int.MaxValue);
            Entry.Y = Random.Next(int.MinValue, int.MaxValue);
            Entry.Timer = Random.Next(int.MinValue, int.MaxValue);
            Assert.Contains(Entry.Text, Entry.ToString());
            Assert.Contains(Entry.URL, Entry.ToString());
            Assert.Contains(Entry.Timer.ToString(), Entry.ToString());
            Assert.Contains(Entry.X.ToString(), Entry.ToString());
            Assert.Contains(Entry.Y.ToString(), Entry.ToString());
        }
    }
}
