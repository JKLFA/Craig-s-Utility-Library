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
using Utilities.DataMapper;

namespace UnitTests.DataMapper
{
    public class Mapping
    {
        [Test]
        public void CreationTest()
        {
            Utilities.DataMapper.Mapping<MappingA, MappingB> TempObject = null;
            Assert.DoesNotThrow<Exception>(() => TempObject = new Mapping<MappingA, MappingB>(x => x.Item1, x => x.Item1));
            Assert.NotNull(TempObject);
        }

        [Test]
        public void LeftToRight()
        {
            Utilities.DataMapper.Mapping<MappingA, MappingB> TempObject = new Mapping<MappingA, MappingB>(x => x.Item1, x => x.Item1);
            MappingA A=new MappingA();
            A.Item1=12;
            A.Item2 = "ASDF";
            MappingB B=new MappingB();
            B.Item1=13;
            B.Item2 = "ZXCV";
            TempObject.CopyLeftToRight(A, B);
            Assert.Equal(B.Item1, 12);
            Assert.NotEqual(B.Item2, "ASDF");
        }

        [Test]
        public void RightToLeft()
        {
            Utilities.DataMapper.Mapping<MappingA, MappingB> TempObject = new Mapping<MappingA, MappingB>(x => x.Item1, x => x.Item1);
            MappingA A = new MappingA();
            A.Item1 = 12;
            A.Item2 = "ASDF";
            MappingB B = new MappingB();
            B.Item1 = 13;
            B.Item2 = "ZXCV";
            TempObject.CopyRightToLeft(B, A);
            Assert.Equal(A.Item1, 13);
            Assert.NotEqual(A.Item2, "ZXCV");
        }
    }

    public class MappingA
    {
        public int Item1 { get; set; }
        public string Item2 { get; set; }
    }

    public class MappingB
    {
        public int Item1 { get; set; }
        public string Item2 { get; set; }
    }
}
