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
using Utilities.SQL;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Linq.Expressions;
using System.Data;

namespace UnitTests.SQL
{
    public class SQLHelper : IDisposable
    {
        public SQLHelper()
        {
            using (Utilities.SQL.SQLHelper Helper = new Utilities.SQL.SQLHelper("Create Database TestDatabase", "Data Source=localhost;Integrated Security=SSPI;Pooling=false", CommandType.Text))
            {
                Helper.ExecuteNonQuery();

            }
            using (Utilities.SQL.SQLHelper Helper = new Utilities.SQL.SQLHelper("Create Table TestTable(ID INT PRIMARY KEY IDENTITY,StringValue1 NVARCHAR(100),StringValue2 NVARCHAR(MAX),BigIntValue BIGINT,BitValue BIT,DecimalValue DECIMAL(12,6),FloatValue FLOAT,DateTimeValue DATETIME,GUIDValue UNIQUEIDENTIFIER)", "Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=SSPI;Pooling=false", CommandType.Text))
            {
                Helper.ExecuteNonQuery();
            }
        }

        [Test]
        public void Connect()
        {
            Assert.DoesNotThrow<Exception>(() =>
            {
                using (Utilities.SQL.SQLHelper Helper = new Utilities.SQL.SQLHelper("", "Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=SSPI;Pooling=false", CommandType.Text))
                {

                }
            });
        }

        [Test]
        public void Insert()
        {
            Guid TempGuid = Guid.NewGuid();
            using (Utilities.SQL.SQLHelper Helper = new Utilities.SQL.SQLHelper("insert into TestTable(StringValue1,StringValue2,BigIntValue,BitValue,DecimalValue,FloatValue,DateTimeValue,GUIDValue) VALUES (@StringValue1,@StringValue2,@BigIntValue,@BitValue,@DecimalValue,@FloatValue,@DateTimeValue,@GUIDValue)", "Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=SSPI;Pooling=false", CommandType.Text))
            {
                Helper.AddParameter<string>("@StringValue1", "Test String");
                Helper.AddParameter<string>("@StringValue2", "Test String");
                Helper.AddParameter<long>("@BigIntValue", 12345);
                Helper.AddParameter<bool>("@BitValue", true);
                Helper.AddParameter<decimal>("@DecimalValue", 1234.5678m);
                Helper.AddParameter<float>("@FloatValue", 12345.6534f);
                Helper.AddParameter<Guid>("@GUIDValue", TempGuid);
                Helper.AddParameter<DateTime>("@DateTimeValue", new DateTime(1999, 12, 31));
                Helper.ExecuteNonQuery();
            }
            using (Utilities.SQL.SQLHelper Helper = new Utilities.SQL.SQLHelper("SELECT * FROM TestTable", "Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=SSPI;Pooling=false", CommandType.Text))
            {
                Helper.ExecuteReader();
                if (Helper.Read())
                {
                    Assert.Equal("Test String", Helper.GetParameter<string>("StringValue1", ""));
                    Assert.Equal("Test String", Helper.GetParameter<string>("StringValue2", ""));
                    Assert.Equal(12345, Helper.GetParameter<long>("BigIntValue", 0));
                    Assert.Equal(true, Helper.GetParameter<bool>("BitValue", false));
                    Assert.Equal(1234.5678m, Helper.GetParameter<decimal>("DecimalValue", 0));
                    Assert.Equal(12345.6534f, Helper.GetParameter<float>("FloatValue", 0));
                    Assert.Equal(TempGuid, Helper.GetParameter<Guid>("GUIDValue", Guid.Empty));
                    Assert.Equal(new DateTime(1999, 12, 31), Helper.GetParameter<DateTime>("DateTimeValue", DateTime.Now));
                }
                else
                {
                    Assert.Fail("Nothing was inserted");
                }
            }
        }

        [Test]
        public void ClearParameters()
        {
            Guid TempGuid = Guid.NewGuid();
            using (Utilities.SQL.SQLHelper Helper = new Utilities.SQL.SQLHelper("insert into TestTable(StringValue1,StringValue2,BigIntValue,BitValue,DecimalValue,FloatValue,DateTimeValue,GUIDValue) VALUES (@StringValue1,@StringValue2,@BigIntValue,@BitValue,@DecimalValue,@FloatValue,@DateTimeValue,@GUIDValue)", "Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=SSPI;Pooling=false", CommandType.Text))
            {
                Helper.AddParameter<string>("@StringValue1", "Test");
                Helper.AddParameter<string>("@StringValue2", "Test");
                Helper.AddParameter<long>("@BigIntValue", 123);
                Helper.AddParameter<bool>("@BitValue", false);
                Helper.AddParameter<decimal>("@DecimalValue", 1234);
                Helper.AddParameter<float>("@FloatValue", 12345);
                Helper.AddParameter<Guid>("@GUIDValue", Guid.NewGuid());
                Helper.AddParameter<DateTime>("@DateTimeValue", new DateTime(1999, 1, 1));
                Helper.ClearParameters();
                Helper.AddParameter<string>("@StringValue1", "Test String");
                Helper.AddParameter<string>("@StringValue2", "Test String");
                Helper.AddParameter<long>("@BigIntValue", 12345);
                Helper.AddParameter<bool>("@BitValue", true);
                Helper.AddParameter<decimal>("@DecimalValue", 1234.5678m);
                Helper.AddParameter<float>("@FloatValue", 12345.6534f);
                Helper.AddParameter<Guid>("@GUIDValue", TempGuid);
                Helper.AddParameter<DateTime>("@DateTimeValue", new DateTime(1999, 12, 31));
                Helper.ExecuteNonQuery();
            }
            using (Utilities.SQL.SQLHelper Helper = new Utilities.SQL.SQLHelper("SELECT * FROM TestTable", "Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=SSPI;Pooling=false", CommandType.Text))
            {
                Helper.ExecuteReader();
                if (Helper.Read())
                {
                    Assert.Equal("Test String", Helper.GetParameter<string>("StringValue1", ""));
                    Assert.Equal("Test String", Helper.GetParameter<string>("StringValue2", ""));
                    Assert.Equal(12345, Helper.GetParameter<long>("BigIntValue", 0));
                    Assert.Equal(true, Helper.GetParameter<bool>("BitValue", false));
                    Assert.Equal(1234.5678m, Helper.GetParameter<decimal>("DecimalValue", 0));
                    Assert.Equal(12345.6534f, Helper.GetParameter<float>("FloatValue", 0));
                    Assert.Equal(TempGuid, Helper.GetParameter<Guid>("GUIDValue", Guid.Empty));
                    Assert.Equal(new DateTime(1999, 12, 31), Helper.GetParameter<DateTime>("DateTimeValue", DateTime.Now));
                }
                else
                {
                    Assert.Fail("Nothing was inserted");
                }
            }
        }

        [Test]
        public void Update()
        {

            Guid TempGuid = Guid.NewGuid();
            using (Utilities.SQL.SQLHelper Helper = new Utilities.SQL.SQLHelper("insert into TestTable(StringValue1,StringValue2,BigIntValue,BitValue,DecimalValue,FloatValue,DateTimeValue,GUIDValue) VALUES (@StringValue1,@StringValue2,@BigIntValue,@BitValue,@DecimalValue,@FloatValue,@DateTimeValue,@GUIDValue)", "Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=SSPI;Pooling=false", CommandType.Text))
            {
                Helper.AddParameter<string>("@StringValue1", "Test");
                Helper.AddParameter<string>("@StringValue2", "Test");
                Helper.AddParameter<long>("@BigIntValue", 123);
                Helper.AddParameter<bool>("@BitValue", false);
                Helper.AddParameter<decimal>("@DecimalValue", 1234);
                Helper.AddParameter<float>("@FloatValue", 12345);
                Helper.AddParameter<Guid>("@GUIDValue", Guid.NewGuid());
                Helper.AddParameter<DateTime>("@DateTimeValue", new DateTime(1999, 1, 1));
                Helper.ExecuteNonQuery();
            }
            using (Utilities.SQL.SQLHelper Helper = new Utilities.SQL.SQLHelper("update TestTable set StringValue1=@StringValue1,StringValue2=@StringValue2,BigIntValue=@BigIntValue,BitValue=@BitValue,DecimalValue=@DecimalValue,FloatValue=@FloatValue,DateTimeValue=@DateTimeValue,GUIDValue=@GUIDValue", "Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=SSPI;Pooling=false", CommandType.Text))
            {
                Helper.AddParameter<string>("@StringValue1", "Test String");
                Helper.AddParameter<string>("@StringValue2", "Test String");
                Helper.AddParameter<long>("@BigIntValue", 12345);
                Helper.AddParameter<bool>("@BitValue", true);
                Helper.AddParameter<decimal>("@DecimalValue", 1234.5678m);
                Helper.AddParameter<float>("@FloatValue", 12345.6534f);
                Helper.AddParameter<Guid>("@GUIDValue", TempGuid);
                Helper.AddParameter<DateTime>("@DateTimeValue", new DateTime(1999, 12, 31));
                Helper.ExecuteNonQuery();
            }
            using (Utilities.SQL.SQLHelper Helper = new Utilities.SQL.SQLHelper("SELECT * FROM TestTable", "Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=SSPI;Pooling=false", CommandType.Text))
            {
                Helper.ExecuteReader();
                if (Helper.Read())
                {
                    Assert.Equal("Test String", Helper.GetParameter<string>("StringValue1", ""));
                    Assert.Equal("Test String", Helper.GetParameter<string>("StringValue2", ""));
                    Assert.Equal(12345, Helper.GetParameter<long>("BigIntValue", 0));
                    Assert.Equal(true, Helper.GetParameter<bool>("BitValue", false));
                    Assert.Equal(1234.5678m, Helper.GetParameter<decimal>("DecimalValue", 0));
                    Assert.Equal(12345.6534f, Helper.GetParameter<float>("FloatValue", 0));
                    Assert.Equal(TempGuid, Helper.GetParameter<Guid>("GUIDValue", Guid.Empty));
                    Assert.Equal(new DateTime(1999, 12, 31), Helper.GetParameter<DateTime>("DateTimeValue", DateTime.Now));
                }
                else
                {
                    Assert.Fail("Nothing was inserted");
                }
            }
        }

        [Test]
        public void Delete()
        {
            Guid TempGuid = Guid.NewGuid();
            using (Utilities.SQL.SQLHelper Helper = new Utilities.SQL.SQLHelper("insert into TestTable(StringValue1,StringValue2,BigIntValue,BitValue,DecimalValue,FloatValue,DateTimeValue,GUIDValue) VALUES (@StringValue1,@StringValue2,@BigIntValue,@BitValue,@DecimalValue,@FloatValue,@DateTimeValue,@GUIDValue)", "Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=SSPI;Pooling=false", CommandType.Text))
            {
                Helper.AddParameter<string>("@StringValue1", "Test String");
                Helper.AddParameter<string>("@StringValue2", "Test String");
                Helper.AddParameter<long>("@BigIntValue", 12345);
                Helper.AddParameter<bool>("@BitValue", true);
                Helper.AddParameter<decimal>("@DecimalValue", 1234.5678m);
                Helper.AddParameter<float>("@FloatValue", 12345.6534f);
                Helper.AddParameter<Guid>("@GUIDValue", TempGuid);
                Helper.AddParameter<DateTime>("@DateTimeValue", new DateTime(1999, 12, 31));
                Helper.ExecuteNonQuery();
            }
            using (Utilities.SQL.SQLHelper Helper = new Utilities.SQL.SQLHelper("delete from TestTable where @ID=ID", "Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=SSPI;Pooling=false", CommandType.Text))
            {
                Helper.AddParameter<int>("@ID", 1);
                Helper.ExecuteNonQuery();
            }
            using (Utilities.SQL.SQLHelper Helper = new Utilities.SQL.SQLHelper("SELECT * FROM TestTable", "Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=SSPI;Pooling=false", CommandType.Text))
            {
                Helper.ExecuteReader();
                if (Helper.Read())
                {
                    Assert.Fail("Nothing was deleted");
                }
            }
        }

        [Test]
        public void Transaction()
        {
            Guid TempGuid = Guid.NewGuid();
            using (Utilities.SQL.SQLHelper Helper = new Utilities.SQL.SQLHelper("insert into TestTable(StringValue1,StringValue2,BigIntValue,BitValue,DecimalValue,FloatValue,DateTimeValue,GUIDValue) VALUES (@StringValue1,@StringValue2,@BigIntValue,@BitValue,@DecimalValue,@FloatValue,@DateTimeValue,@GUIDValue)", "Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=SSPI;Pooling=false", CommandType.Text))
            {
                Helper.BeginTransaction();
                Helper.AddParameter<string>("@StringValue1", "Test");
                Helper.AddParameter<string>("@StringValue2", "Test");
                Helper.AddParameter<long>("@BigIntValue", 123);
                Helper.AddParameter<bool>("@BitValue", false);
                Helper.AddParameter<decimal>("@DecimalValue", 1234);
                Helper.AddParameter<float>("@FloatValue", 12345);
                Helper.AddParameter<Guid>("@GUIDValue", Guid.NewGuid());
                Helper.AddParameter<DateTime>("@DateTimeValue", new DateTime(1999, 1, 1));
                Helper.ExecuteNonQuery();
                Helper.Rollback();
                Helper.BeginTransaction();
                Helper.AddParameter<string>("@StringValue1", "Test String");
                Helper.AddParameter<string>("@StringValue2", "Test String");
                Helper.AddParameter<long>("@BigIntValue", 12345);
                Helper.AddParameter<bool>("@BitValue", true);
                Helper.AddParameter<decimal>("@DecimalValue", 1234.5678m);
                Helper.AddParameter<float>("@FloatValue", 12345.6534f);
                Helper.AddParameter<Guid>("@GUIDValue", TempGuid);
                Helper.AddParameter<DateTime>("@DateTimeValue", new DateTime(1999, 12, 31));
                Helper.ExecuteNonQuery();
                Helper.Commit();
            }
            using (Utilities.SQL.SQLHelper Helper = new Utilities.SQL.SQLHelper("SELECT * FROM TestTable", "Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=SSPI;Pooling=false", CommandType.Text))
            {
                Helper.ExecuteReader();
                if (Helper.Read())
                {
                    Assert.Equal("Test String", Helper.GetParameter<string>("StringValue1", ""));
                    Assert.Equal("Test String", Helper.GetParameter<string>("StringValue2", ""));
                    Assert.Equal(12345, Helper.GetParameter<long>("BigIntValue", 0));
                    Assert.Equal(true, Helper.GetParameter<bool>("BitValue", false));
                    Assert.Equal(1234.5678m, Helper.GetParameter<decimal>("DecimalValue", 0));
                    Assert.Equal(12345.6534f, Helper.GetParameter<float>("FloatValue", 0));
                    Assert.Equal(TempGuid, Helper.GetParameter<Guid>("GUIDValue", Guid.Empty));
                    Assert.Equal(new DateTime(1999, 12, 31), Helper.GetParameter<DateTime>("DateTimeValue", DateTime.Now));
                }
                else
                {
                    Assert.Fail("Nothing was inserted");
                }
            }
            using (Utilities.SQL.SQLHelper Helper = new Utilities.SQL.SQLHelper("SELECT COUNT(*) as [ItemCount] FROM TestTable", "Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=SSPI;Pooling=false", CommandType.Text))
            {
                Helper.ExecuteReader();
                if (Helper.Read())
                {
                    Assert.Equal(1,Helper.GetParameter<int>("ItemCount",0));
                }
                else
                {
                    Assert.Fail("Nothing was inserted");
                }
            }
        }

        [Test]
        public void OutputParamter()
        {
            Guid TempGuid = Guid.NewGuid();
            using (Utilities.SQL.SQLHelper Helper = new Utilities.SQL.SQLHelper("insert into TestTable(StringValue1,StringValue2,BigIntValue,BitValue,DecimalValue,FloatValue,DateTimeValue,GUIDValue) VALUES (@StringValue1,@StringValue2,@BigIntValue,@BitValue,@DecimalValue,@FloatValue,@DateTimeValue,@GUIDValue)", "Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=SSPI;Pooling=false", CommandType.Text))
            {
                Helper.AddParameter<string>("@StringValue1", "Test String");
                Helper.AddParameter<string>("@StringValue2", "Test String");
                Helper.AddParameter<long>("@BigIntValue", 12345);
                Helper.AddParameter<bool>("@BitValue", true);
                Helper.AddParameter<decimal>("@DecimalValue", 1234.5678m);
                Helper.AddParameter<float>("@FloatValue", 12345.6534f);
                Helper.AddParameter<Guid>("@GUIDValue", TempGuid);
                Helper.AddParameter<DateTime>("@DateTimeValue", new DateTime(1999, 12, 31));
                Helper.ExecuteNonQuery();
            }
            using (Utilities.SQL.SQLHelper Helper = new Utilities.SQL.SQLHelper("SET @ASD=12345", "Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=SSPI;Pooling=false", CommandType.Text))
            {
                Helper.AddParameter<long>("@ASD", Direction: ParameterDirection.Output);
                Helper.ExecuteNonQuery();
                Assert.Equal(12345, Helper.GetParameter<long>("@ASD", 0, ParameterDirection.Output));
            }
        }

        public void Dispose()
        {
            using (Utilities.SQL.SQLHelper Helper = new Utilities.SQL.SQLHelper("ALTER DATABASE TestDatabase SET OFFLINE WITH ROLLBACK IMMEDIATE", "Data Source=localhost;Initial Catalog=master;Integrated Security=SSPI;Pooling=false", CommandType.Text))
            {
                Helper.ExecuteNonQuery();
                Helper.Command = "ALTER DATABASE TestDatabase SET ONLINE";
                Helper.ExecuteNonQuery();
                Helper.Command = "DROP DATABASE TestDatabase";
                Helper.ExecuteNonQuery();
            }
        }
    }
}