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
using System.Xml;
using Utilities.DataTypes.ExtensionMethods;
#endregion

namespace Utilities.FileFormats.BlogML
{
    /// <summary>
    /// Category class
    /// </summary>
    public class Category
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Element">XML element with the category info</param>
        public Category(XmlElement Element)
        {
            Element.ThrowIfNull("Element");
            ID = Element.Attributes["id"] != null ? Element.Attributes["id"].Value : "";
            REF = Element.Attributes["ref"] != null ? Element.Attributes["ref"].Value : "";
            foreach (XmlNode Children in Element.ChildNodes)
            {
                if (Children.Name.Equals("title", StringComparison.CurrentCultureIgnoreCase))
                    Title = Children.InnerText;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// ID of the category
        /// </summary>
        public virtual string ID { get; set; }

        /// <summary>
        /// Title of the cateogry (its name)
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// Determines if this is a reference to a category
        /// </summary>
        public virtual string REF { get; set; }

        #endregion
    }
}