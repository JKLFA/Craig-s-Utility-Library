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
using Utilities.IO.Logging.BaseClasses;
using Utilities.IO.Logging.Enums;
#endregion

namespace Utilities.IO.Logging
{
    /// <summary>
    /// Outputs messages to the console
    /// </summary>
    public class ConsoleLog : LogBase<ConsoleLog>
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ConsoleLog()
            : base(x => Console.WriteLine("--------------------------------Logging started--------------------------------"))
        {
            End = x => Console.WriteLine("---------------------------------Logging ended---------------------------------");
            Log.Add(MessageType.Debug, x => Console.WriteLine(x));
            Log.Add(MessageType.Error, x => Console.WriteLine(x));
            Log.Add(MessageType.General, x => Console.WriteLine(x));
            Log.Add(MessageType.Info, x => Console.WriteLine(x));
            Log.Add(MessageType.Trace, x => Console.WriteLine(x));
            Log.Add(MessageType.Warn, x => Console.WriteLine(x));
            FormatMessage = (Message, Type, args) => Type.ToString() 
                + ": " + (args.Length > 0 ? string.Format(Message, args) : Message);
        }

        #endregion
    }
}