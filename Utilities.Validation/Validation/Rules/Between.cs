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
using Utilities.Validation.BaseClasses;
using Utilities.Validation.Exceptions;
using Utilities.DataTypes.Comparison;
#endregion

namespace Utilities.Validation.Rules
{
    /// <summary>
    /// This item is between two values
    /// </summary>
    /// <typeparam name="ObjectType">Object type that the rule applies to</typeparam>
    /// <typeparam name="DataType">Data type of the object validating</typeparam>
    public class Between<ObjectType, DataType> : Rule<ObjectType, DataType> where DataType : IComparable
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ItemToValidate">Item to validate</param>
        /// <param name="MinValue">Min value</param>
        /// <param name="MaxValue">Max value</param>
        /// <param name="ErrorMessage">Error message</param>
        public Between(Func<ObjectType, DataType> ItemToValidate, DataType MinValue, DataType MaxValue, string ErrorMessage)
            : base(ItemToValidate, ErrorMessage)
        {
            this.MaxValue = MaxValue;
            this.MinValue = MinValue;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Min value
        /// </summary>
        protected virtual DataType MinValue { get; set; }

        /// <summary>
        /// Max value
        /// </summary>
        protected virtual DataType MaxValue { get; set; }

        #endregion

        #region Functions

        public override void Validate(ObjectType Object)
        {
            GenericComparer<DataType> Comparer = new GenericComparer<DataType>();
            if (Comparer.Compare(MaxValue, ItemToValidate(Object)) < 0
                || Comparer.Compare(ItemToValidate(Object), MinValue) < 0)
                throw new NotValid(ErrorMessage);
        }

        #endregion
    }

    /// <summary>
    /// Between attribute
    /// </summary>
    public class Between : BaseAttribute
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ErrorMessage">Error message</param>
        /// <param name="MinValue">Min value to compare to</param>
        /// <param name="MaxValue">Max value to compare to</param>
        public Between(object MinValue,object MaxValue, string ErrorMessage = "")
            : base(ErrorMessage)
        {
            this.MinValue = (IComparable)MinValue;
            this.MaxValue = (IComparable)MaxValue;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Min value to compare to
        /// </summary>
        public IComparable MinValue { get; set; }

        /// <summary>
        /// Max value to compare to
        /// </summary>
        public IComparable MaxValue { get; set; }

        #endregion
    }
}