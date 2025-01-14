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
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using Utilities.IO.ExtensionMethods;
using System.Linq;
using System.Web;
using System.Globalization;
using Utilities.DataTypes.ExtensionMethods;
using System.IO.Compression;
#endregion

namespace Utilities.Web.ExtensionMethods
{
    /// <summary>
    /// Set of HTML related extensions
    /// </summary>
    public static class HTMLExtensions
    {
        #region AbsoluteRoot

        /// <summary>
        /// Returns the absolute root
        /// </summary>
        public static Uri AbsoluteRoot(this HttpContext Context)
        {
            Context.ThrowIfNull("Context");
            if (Context.Items["absoluteurl"] == null)
                Context.Items["absoluteurl"] = new Uri(Context.Request.Url.GetLeftPart(UriPartial.Authority) + Context.RelativeRoot());
            return Context.Items["absoluteurl"] as Uri;
        }


        #endregion

        #region AddScriptFile

        /// <summary>
        /// Adds a script file to the header of the current page
        /// </summary>
        /// <param name="File">Script file</param>
        /// <param name="Directory">Script directory</param>
        public static void AddScriptFile(this System.Web.UI.Page Page, FileInfo File)
        {
            File.ThrowIfNull("File");
            if (!File.Exists)
                throw new ArgumentException("File does not exist");
            if (!Page.ClientScript.IsClientScriptIncludeRegistered(typeof(System.Web.UI.Page), File.FullName))
                Page.ClientScript.RegisterClientScriptInclude(typeof(System.Web.UI.Page), File.FullName, File.FullName);
        }

        #endregion

        #region ContainsHTML

        /// <summary>
        /// Decides if the string contains HTML
        /// </summary>
        /// <param name="Input">Input string to check</param>
        /// <returns>false if it does not contain HTML, true otherwise</returns>
        public static bool ContainsHTML(this string Input)
        {
            return string.IsNullOrEmpty(Input) ? false : STRIP_HTML_REGEX.IsMatch(Input);
        }

        /// <summary>
        /// Decides if the file contains HTML
        /// </summary>
        /// <param name="Input">Input file to check</param>
        /// <returns>false if it does not contain HTML, true otherwise</returns>
        public static bool ContainsHTML(this FileInfo Input)
        {
            Input.ThrowIfNull("Input");
            return Input.Exists ? Input.Read().ContainsHTML() : false;
        }

        #endregion

        #region HTTPCompress

        /// <summary>
        /// Adds HTTP compression to the current context
        /// </summary>
        /// <param name="Context">Current context</param>
        public static void HTTPCompress(this HttpContext Context)
        {
            Context.ThrowIfNull("Context");
            if (Context.Request.UserAgent != null && Context.Request.UserAgent.Contains("MSIE 6"))
                return;

            if (Context.IsEncodingAccepted(GZIP))
            {
                Context.Response.Filter = new GZipStream(Context.Response.Filter, CompressionMode.Compress);
                Context.SetEncoding(GZIP);
            }
            else if (Context.IsEncodingAccepted(DEFLATE))
            {
                Context.Response.Filter = new DeflateStream(Context.Response.Filter, CompressionMode.Compress);
                Context.SetEncoding(DEFLATE);
            }
        }

        #endregion

        #region IsEncodingAccepted

        /// <summary>
        /// Checks the request headers to see if the specified
        /// encoding is accepted by the client.
        /// </summary>
        public static bool IsEncodingAccepted(this HttpContext Context, string Encoding)
        {
            if (Context == null)
                return false;
            return Context.Request.Headers["Accept-encoding"] != null && Context.Request.Headers["Accept-encoding"].Contains(Encoding);
        }

        #endregion

        #region RelativeRoot

        /// <summary>
        /// Gets the relative root of the web site
        /// </summary>
        /// <param name="Context">Current context</param>
        /// <returns>The relative root of the web site</returns>
        public static string RelativeRoot(this HttpContext Context)
        {
            return VirtualPathUtility.ToAbsolute("~/");
        }
        
        #endregion

        #region RemoveURLIllegalCharacters

        /// <summary>
        /// Removes illegal characters (used in uri's, etc.)
        /// </summary>
        /// <param name="Input">string to be converted</param>
        /// <returns>A stripped string</returns>
        public static string RemoveURLIllegalCharacters(this string Input)
        {
            if (string.IsNullOrEmpty(Input))
                return "";
            Input = Input.Replace(":", string.Empty)
                        .Replace("/", string.Empty)
                        .Replace("?", string.Empty)
                        .Replace("#", string.Empty)
                        .Replace("[", string.Empty)
                        .Replace("]", string.Empty)
                        .Replace("@", string.Empty)
                        .Replace(".", string.Empty)
                        .Replace("\"", string.Empty)
                        .Replace("&", string.Empty)
                        .Replace("'", string.Empty)
                        .Replace(" ", "-");
            Input = RemoveExtraHyphen(Input);
            Input = RemoveDiacritics(Input);
            return HttpUtility.UrlEncode(Input).Replace("%", string.Empty);
        }

        #endregion

        #region SetEncoding

        /// <summary>
        /// Adds the specified encoding to the response headers.
        /// </summary>
        /// <param name="Encoding">Encoding to set</param>
        public static void SetEncoding(this HttpContext Context, string Encoding)
        {
            Context.ThrowIfNull("Context");
            Context.Response.AppendHeader("Content-encoding", Encoding);
        }

        #endregion

        #region StripHTML

        /// <summary>
        /// Removes HTML elements from a string
        /// </summary>
        /// <param name="HTML">HTML laiden string</param>
        /// <returns>HTML-less string</returns>
        public static string StripHTML(this string HTML)
        {
            if (string.IsNullOrEmpty(HTML))
                return "";
            HTML = STRIP_HTML_REGEX.Replace(HTML, string.Empty);
            return HTML.Replace("&nbsp;", " ")
                       .Replace("&#160;", string.Empty);
        }

        /// <summary>
        /// Removes HTML elements from a string
        /// </summary>
        /// <param name="HTML">HTML laiden file</param>
        /// <returns>HTML-less string</returns>
        public static string StripHTML(this FileInfo HTML)
        {
            HTML.ThrowIfNull("HTML");
            if (!HTML.Exists)
                throw new ArgumentException("File does not exist");
            return HTML.Read().StripHTML();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// Removes extra hyphens from a string
        /// </summary>
        /// <param name="Input">string to be stripped</param>
        /// <returns>Stripped string</returns>
        private static string RemoveExtraHyphen(string Input)
        {
            while (Input.Contains("--"))
                Input = Input.Replace("--", "-");
            return Input;
        }

        /// <summary>
        /// Removes special characters (Diacritics) from the string
        /// </summary>
        /// <param name="Input">String to strip</param>
        /// <returns>Stripped string</returns>
        private static string RemoveDiacritics(string Input)
        {
            string Normalized = Input.Normalize(NormalizationForm.FormD);
            StringBuilder Builder = new StringBuilder();
            for (int i = 0; i < Normalized.Length; i++)
            {
                Char TempChar = Normalized[i];
                if (CharUnicodeInfo.GetUnicodeCategory(TempChar) != UnicodeCategory.NonSpacingMark)
                    Builder.Append(TempChar);
            }
            return Builder.ToString();
        }

        #endregion

        #region Variables
        private static readonly Regex STRIP_HTML_REGEX = new Regex("<[^>]*>", RegexOptions.Compiled);
        #endregion

        #region Constants
        private const string GZIP = "gzip";
        private const string DEFLATE = "deflate";
        #endregion
    }
}