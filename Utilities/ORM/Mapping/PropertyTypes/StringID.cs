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
using Utilities.ORM.Mapping.BaseClasses;
using Utilities.ORM.QueryProviders.Interfaces;
using Utilities.SQL.MicroORM;
using Utilities.SQL.MicroORM.Enums;
#endregion

namespace Utilities.ORM.Mapping.PropertyTypes
{
    /// <summary>
    /// ID class
    /// </summary>
    /// <typeparam name="ClassType">Class type</typeparam>
    public class StringID<ClassType> : PropertyBase<ClassType, string, IID<ClassType, string>>,
        IID<ClassType, string>
        where ClassType : class,new()
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Expression">Expression pointing to the ID</param>
        /// <param name="Mapping">Mapping the StringID is added to</param>
        public StringID(Expression<Func<ClassType, string>> Expression, IMapping Mapping)
            : base(Expression, Mapping)
        {
            SetDefaultValue(() => "");
            SetFieldName(Name + "_");
            SetMaxLength(100);
        }

        #endregion

        #region Functions

        public override void SetupLoadCommands()
        {

        }

        public override void JoinsDelete(ClassType Object, MicroORM MicroORM)
        {
        }

        public override void JoinsSave(ClassType Object, MicroORM MicroORM)
        {
        }

        public override void CascadeJoinsDelete(ClassType Object, MicroORM MicroORM)
        {
        }

        public override void CascadeJoinsSave(ClassType Object, MicroORM MicroORM)
        {
        }

        public override void CascadeDelete(ClassType Object, MicroORM MicroORM)
        {

        }

        public override void CascadeSave(ClassType Object, MicroORM MicroORM)
        {
        }

        public override IParameter GetAsParameter(ClassType Object)
        {
            return null;
        }

        public override IID<ClassType, string> LoadUsingCommand(string Command, System.Data.CommandType CommandType)
        {
            this.CommandToLoad = new Command(Command, CommandType);
            return (IID<ClassType, string>)this;
        }

        public override void AddToQueryProvider(IDatabase Database, Mapping<ClassType> Mapping)
        {
            Mode Mode = Mode.Neither;
            if (Database.Readable)
                Mode |= Mode.Read;
            if (Database.Writable)
                Mode |= Mode.Write;
            Mapping.Map(this.Expression, this.FieldName, this.MaxLength, DefaultValue(), Mode);
        }

        public override IID<ClassType, string> SetDefaultValue(Func<string> DefaultValue)
        {
            this.DefaultValue = DefaultValue;
            return (IID<ClassType, string>)this;
        }

        public override IID<ClassType, string> DoNotAllowNullValues()
        {
            this.NotNull = true;
            Validation.ValidationManager.GetValidator<ClassType>().Required(Expression);
            return (IID<ClassType, string>)this;
        }

        public override IID<ClassType, string> ThisShouldBeUnique()
        {
            this.Unique = true;
            return (IID<ClassType, string>)this;
        }

        public override IID<ClassType, string> TurnOnIndexing()
        {
            this.Index = true;
            return (IID<ClassType, string>)this;
        }

        public override IID<ClassType, string> TurnOnAutoIncrement()
        {
            this.AutoIncrement = true;
            return (IID<ClassType, string>)this;
        }

        public override IID<ClassType, string> SetFieldName(string FieldName)
        {
            this.FieldName = FieldName;
            return (IID<ClassType, string>)this;
        }


        public override IID<ClassType, string> SetTableName(string TableName)
        {
            this.TableName = TableName;
            return (IID<ClassType, string>)this;
        }

        public override IID<ClassType, string> TurnOnCascade()
        {
            this.Cascade = true;
            return (IID<ClassType, string>)this;
        }

        public override IID<ClassType, string> SetMaxLength(int MaxLength)
        {
            this.MaxLength = MaxLength;
            if (MaxLength != -1)
                Validation.ValidationManager.GetValidator<ClassType>().MaxLength(Expression, MaxLength);
            return (IID<ClassType, string>)this;
        }

        #endregion
    }
}