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
using Utilities.DataTypes;
using System.Data;
using Utilities.Events.EventArgs;
using Utilities.Random;

namespace UnitTests.DataTypes
{
    public class PriorityQueue
    {
        [Test]
        public void RandomTest()
        {
            PriorityQueue<int> TestObject = new PriorityQueue<int>();
            Utilities.Random.Random Rand = new Utilities.Random.Random();
            int Value=0;
            for (int x = 0; x < 10; ++x)
            {
                Value=Rand.Next();
                TestObject.Add(x, Value);
                Assert.Equal(Value, TestObject.Peek());
            }
            int HighestValue = TestObject.Peek();
            for (int x = 9; x >= 0; --x)
            {
                Value = Rand.Next();
                TestObject.Add(x, Value);
                Assert.Equal(HighestValue, TestObject.Peek());
            }
            int Count=0;
            foreach(int Priority in TestObject.Keys)
            {
                foreach(int Item in TestObject[Priority])
                {
                    ++Count;
                }
            }
            Assert.Equal(20, Count);
        }
    }
}
