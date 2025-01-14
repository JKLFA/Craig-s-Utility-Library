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
using System.Reflection.Emit;
using System.Text;
using Utilities.Reflection.Emit.BaseClasses;
using Utilities.Reflection.Emit.Interfaces;
using Utilities.Reflection.ExtensionMethods;
#endregion

namespace Utilities.Reflection.Emit.Commands
{
    /// <summary>
    /// Boxes an object
    /// </summary>
    public class Box : CommandBase
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Value">Value to box</param>
        public Box(object Value)
            : base()
        {
            if (Value is VariableBase)
                this.Value = (VariableBase)Value;
            else
                this.Value = new ConstantBuilder(Value);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Value to box
        /// </summary>
        public virtual VariableBase Value { get; set; }

        #endregion

        #region Functions

        public override void Setup()
        {
            if (!Value.DataType.IsValueType)
                throw new ArgumentException("Value is not a value type, box operations convert value types to reference types");
            Result = Utilities.Reflection.Emit.BaseClasses.MethodBase.CurrentMethod.CreateLocal("BoxResult" + Value.Name+Utilities.Reflection.Emit.BaseClasses.MethodBase.ObjectCounter.ToString(), typeof(object));
            ILGenerator Generator = Utilities.Reflection.Emit.BaseClasses.MethodBase.CurrentMethod.Generator;
            if (Value is FieldBuilder || Value is IPropertyBuilder)
                Generator.Emit(OpCodes.Ldarg_0);
            Value.Load(Generator);
            Generator.Emit(OpCodes.Box, Value.DataType);
            Result.Save(Generator);
        }

        public override string ToString()
        {
            StringBuilder Output = new StringBuilder();
            Output.Append(Result).Append("=(").Append(typeof(object).GetName())
                .Append(")").Append(Value).Append(";\n");
            return Output.ToString();
        }

        #endregion
    }
}