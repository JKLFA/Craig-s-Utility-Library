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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities.ORM.Mapping.Interfaces;
using System.Linq.Expressions;
using Utilities.ORM.Mapping.PropertyTypes;
using Utilities.ORM.QueryProviders.Interfaces;
using Utilities.SQL.MicroORM;
using System.Data;
#endregion

namespace Utilities.ORM.Mapping
{
    /// <summary>
    /// Holds information about a command
    /// </summary>
    public class Command
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Command">Command to call</param>
        /// <param name="CommandType">Command type</param>
        public Command(string Command, CommandType CommandType)
        {
            this.CommandToRun = Command;
            this.CommandType = CommandType;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Command to call
        /// </summary>
        public virtual string CommandToRun { get; private set; }

        /// <summary>
        /// Command type to call
        /// </summary>
        public virtual CommandType CommandType { get; private set; }

        #endregion
    }
}