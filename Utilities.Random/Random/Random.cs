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
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using Utilities.DataTypes;
using Utilities.DataTypes.ExtensionMethods;
#endregion

namespace Utilities.Random
{
    /// <summary>
    /// Utility class for handling random
    /// information.
    /// </summary>
    public class Random : System.Random
    {
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public Random()
            : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Seed">Seed value</param>
        public Random(int Seed)
            : base(Seed)
        {
        }

        #endregion

        #region Public Functions

        #region NextDate

        /// <summary>
        /// returns a random date/time for a specific date range.
        /// </summary>
        /// <param name="Start">Start time</param>
        /// <param name="End">End time</param>
        /// <returns>A random date/time between the start and end times</returns>
        public virtual DateTime NextDate(DateTime Start, DateTime End)
        {
            if (Start == null)
                throw new ArgumentNullException("Start");
            if (End == null)
                throw new ArgumentNullException("End");
            if (Start > End)
                throw new ArgumentException("The start value must be earlier than the end value");
            return Start + new TimeSpan((long)(new TimeSpan(End.Ticks - Start.Ticks).Ticks * NextDouble()));
        }

        #endregion

        #region NextString

        /// <summary>
        /// Returns a randomly generated string of a specified length, containing
        /// only a set of characters, and at max a specified number of non alpha numeric characters.
        /// </summary>
        /// <param name="Length">Length of the string</param>
        /// <param name="AllowedCharacters">Characters allowed in the string</param>
        /// <param name="NumberOfNonAlphaNumericsAllowed">Number of non alpha numeric characters allowed.</param>
        /// <returns>A randomly generated string of a specified length, containing only a set of characters, and at max a specified number of non alpha numeric characters.</returns>
        public virtual string NextString(int Length, string AllowedCharacters = ".", int NumberOfNonAlphaNumericsAllowed = int.MaxValue)
        {
            if (Length < 1)
                return "";
            StringBuilder TempBuilder = new StringBuilder();
            Regex Comparer = new Regex(AllowedCharacters);
            Regex AlphaNumbericComparer = new Regex("[0-9a-zA-z]");
            int Counter = 0;
            while (TempBuilder.Length < Length)
            {
                string TempValue = new string(Convert.ToChar(Convert.ToInt32(System.Math.Floor(94 * NextDouble() + 32))), 1);
                if (Comparer.IsMatch(TempValue))
                {
                    if (!AlphaNumbericComparer.IsMatch(TempValue) && NumberOfNonAlphaNumericsAllowed > Counter)
                    {
                        TempBuilder.Append(TempValue);
                        ++Counter;
                    }
                    else if (AlphaNumbericComparer.IsMatch(TempValue))
                    {
                        TempBuilder.Append(TempValue);
                    }
                }
            }
            return TempBuilder.ToString();
        }

        #endregion

        #region NextLoremIpsum

        /// <summary>
        /// Creates a Lorem Ipsum sentence.
        /// </summary>
        /// <param name="NumberOfWords">Number of words for the sentence</param>
        /// <returns>A string containing Lorem Ipsum text</returns>
        public virtual string NextLoremIpsum(int NumberOfWords)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append(Words[Next(Words.Length)].ToFirstCharacterUpperCase());
            for (int x = 1; x < NumberOfWords; ++x)
                Builder.Append(" ").Append(Words[Next(Words.Length)]);
            Builder.Append(".");
            return Builder.ToString();
        }

        /// <summary>
        /// Creates a Lorem Ipsum paragraph.
        /// </summary>
        /// <param name="NumberOfParagraphs">Number of paragraphs</param>
        /// <param name="MaxSentenceLength">Maximum sentence length</param>
        /// <param name="MinSentenceLength">Minimum sentence length</param>
        /// <param name="NumberOfSentences">Number of sentences per paragraph</param>
        /// <param name="HTMLFormatting">Determines if this should use HTML formatting or not</param>
        /// <returns>A string containing Lorem Ipsum text</returns>
        public virtual string NextLoremIpsum(int NumberOfParagraphs, int NumberOfSentences, int MinSentenceLength = 4, int MaxSentenceLength = 10, bool HTMLFormatting = false)
        {
            StringBuilder Builder = new StringBuilder();
            if (HTMLFormatting)
                Builder.Append("<p>");
            Builder.Append("Lorem ipsum dolor sit amet. ");
            for (int y = 1; y < NumberOfSentences; ++y)
                Builder.Append(NextLoremIpsum(Next(MinSentenceLength, MaxSentenceLength))).Append(" ");
            if (HTMLFormatting)
                Builder.Append("</p>");
            for (int x = 1; x < NumberOfParagraphs; ++x)
            {
                if (HTMLFormatting)
                    Builder.Append("<p>");
                for (int y = 0; y < NumberOfSentences; ++y)
                    Builder.Append(NextLoremIpsum(Next(MinSentenceLength, MaxSentenceLength))).Append(" ");
                if (HTMLFormatting)
                    Builder.Append("</p>");
                else
                    Builder.Append(System.Environment.NewLine).Append(System.Environment.NewLine);
            }
            return Builder.ToString();
        }

        #endregion

        #region NextBool

        /// <summary>
        /// Returns a random boolean value
        /// </summary>
        /// <returns>returns a boolean</returns>
        public virtual bool NextBool()
        {
            return Next(0, 2) == 1;
        }

        #endregion

        #region NextEnum

        /// <summary>
        /// Gets a random enum value
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <returns>A random value from an enum</returns>
        public virtual T NextEnum<T>()
        {
            Array Values = Enum.GetValues(typeof(T));
            int Index = Next(0, Values.Length);
            return (T)Values.GetValue(Index);
        }

        #endregion

        #region NextTimeSpan

        /// <summary>
        /// Randomly generates a new time span
        /// </summary>
        /// <param name="Start">Start time span</param>
        /// <param name="End">End time span</param>
        /// <returns>A time span between the start and end</returns>
        public virtual TimeSpan NextTimeSpan(TimeSpan Start, TimeSpan End)
        {
            if (Start == null)
                throw new ArgumentNullException("Start");
            if (End == null)
                throw new ArgumentNullException("End");
            if (Start > End)
                throw new ArgumentException("The start value must be earlier than the end value");
            return Start + new TimeSpan((long)(new TimeSpan(End.Ticks - Start.Ticks).Ticks * NextDouble()));
        }

        #endregion

        #region NextColor
        
        /// <summary>
        /// Returns a random color within a range
        /// </summary>
        /// <param name="MinColor">The inclusive minimum color (minimum for A, R, G, and B values). Defaults to Black.</param>
        /// <param name="MaxColor">The inclusive maximum color (max for A, R, G, and B values). Defaults to White</param>
        /// <returns>A random color between the min and max values</returns>
        public virtual Color NextColor(Color MinColor = default(Color), Color MaxColor = default(Color))
        {
            if (MinColor == default(Color))
                MinColor = Color.Black;
            if (MaxColor == default(Color))
                MaxColor = Color.White;
            return Color.FromArgb(Next(MinColor.A, MaxColor.A + 1),
                Next(MinColor.R, MaxColor.R + 1),
                Next(MinColor.G, MaxColor.G + 1),
                Next(MinColor.B, MaxColor.B + 1));
        }

        #endregion

        #endregion

        #region Private Variables

        private string[] Words = new string[] { "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod",
        "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua",
        "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita",
        "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet",
        "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod",
        "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua",
        "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita",
        "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet",
        "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod",
        "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua",
        "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita",
        "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "duis",
        "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie",
        "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eros", "et",
        "accumsan", "et", "iusto", "odio", "dignissim", "qui", "blandit", "praesent", "luptatum", "zzril", "delenit",
        "augue", "duis", "dolore", "te", "feugait", "nulla", "facilisi", "lorem", "ipsum", "dolor", "sit", "amet",
        "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet",
        "dolore", "magna", "aliquam", "erat", "volutpat", "ut", "wisi", "enim", "ad", "minim", "veniam", "quis",
        "nostrud", "exerci", "tation", "ullamcorper", "suscipit", "lobortis", "nisl", "ut", "aliquip", "ex", "ea",
        "commodo", "consequat", "duis", "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate",
        "velit", "esse", "molestie", "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at",
        "vero", "eros", "et", "accumsan", "et", "iusto", "odio", "dignissim", "qui", "blandit", "praesent", "luptatum",
        "zzril", "delenit", "augue", "duis", "dolore", "te", "feugait", "nulla", "facilisi", "nam", "liber", "tempor",
        "cum", "soluta", "nobis", "eleifend", "option", "congue", "nihil", "imperdiet", "doming", "id", "quod", "mazim",
        "placerat", "facer", "possim", "assum", "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing",
        "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam",
        "erat", "volutpat", "ut", "wisi", "enim", "ad", "minim", "veniam", "quis", "nostrud", "exerci", "tation",
        "ullamcorper", "suscipit", "lobortis", "nisl", "ut", "aliquip", "ex", "ea", "commodo", "consequat", "duis",
        "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie",
        "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eos", "et", "accusam",
        "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea",
        "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit",
        "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut",
        "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et",
        "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no",
        "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit",
        "amet", "consetetur", "sadipscing", "elitr", "at", "accusam", "aliquyam", "diam", "diam", "dolore", "dolores",
        "duo", "eirmod", "eos", "erat", "et", "nonumy", "sed", "tempor", "et", "et", "invidunt", "justo", "labore",
        "stet", "clita", "ea", "et", "gubergren", "kasd", "magna", "no", "rebum", "sanctus", "sea", "sed", "takimata",
        "ut", "vero", "voluptua", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit",
        "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut",
        "labore", "et", "dolore", "magna", "aliquyam", "erat", "consetetur", "sadipscing", "elitr", "sed", "diam",
        "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed",
        "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea",
        "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum" };

        #endregion
    }
}