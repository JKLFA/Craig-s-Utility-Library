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

#endregion

using System;
namespace Utilities.Environment.DataTypes
{
    /// <summary>
    /// Represents a computer
    /// </summary>
    public class Computer
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name">Computer Name</param>
        /// <param name="UserName">User name</param>
        /// <param name="Password">Password</param>
        public Computer(string Name, string UserName = "", string Password = "")
        {
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentNullException("Name");
            this.Name = Name;
            this.UserName = UserName;
            this.Password = Password;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Computer Name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// BIOS info
        /// </summary>
        public virtual BIOS BIOS
        {
            get
            {
                if (_BIOS == null)
                {
                    _BIOS = new BIOS(Name, UserName, Password);
                }
                return _BIOS;
            }
        }

        protected BIOS _BIOS = null;

        /// <summary>
        /// Application info
        /// </summary>
        public virtual Applications Applications
        {
            get
            {
                if (_Applications == null)
                {
                    _Applications = new Applications(Name, UserName, Password);
                }
                return _Applications;
            }
        }

        protected Applications _Applications = null;

        /// <summary>
        /// Network info
        /// </summary>
        public virtual Network Network
        {
            get
            {
                if (_Network == null)
                {
                    _Network = new Network(Name, UserName, Password);
                }
                return _Network;
            }
        }

        protected Network _Network = null;

        /// <summary>
        /// Operating system info
        /// </summary>
        public virtual OperatingSystem OperatingSystem
        {
            get
            {
                if (_OperatingSystem == null)
                {
                    _OperatingSystem = new OperatingSystem(Name, UserName, Password);
                }
                return _OperatingSystem;
            }
        }

        protected OperatingSystem _OperatingSystem = null;

        /// <summary>
        /// Holds a list of users that have logged into the machine recently
        /// </summary>
        public virtual User LatestUsers
        {
            get
            {
                if (_User == null)
                {
                    _User = new User(Name, UserName, Password);
                }
                return _User;
            }
        }

        protected User _User = null;


        #endregion
    }
}