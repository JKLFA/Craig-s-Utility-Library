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
using Utilities.Validation.Rules;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Linq.Expressions;
using Utilities.Validation.Exceptions;

namespace UnitTests.Validation
{
    public class ValidationManager
    {
        [Test]
        public void Test()
        {
            Utilities.Validation.ValidationManager.GetValidator<MyClass>()
                .GreaterThan<int>(x => x.Item1, 0)
                .MaxLength<char>(x => x.Item2, 5);
            MyClass Temp = new MyClass();
            Temp.Item1 = 1;
            Temp.Item2 = "ASDF";
            Assert.DoesNotThrow<Exception>(() => Utilities.Validation.ValidationManager.Validate<MyClass>(Temp));
            Temp.Item2 = "ASDFGH";
            Assert.Throws<NotValid>(() => Utilities.Validation.ValidationManager.Validate<MyClass>(Temp));
        }
    }

    public class MyClass
    {
        public int Item1 { get; set; }
        public string Item2 { get; set; }
    }
}
