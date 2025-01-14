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
using System.Drawing.Imaging;
using Utilities.Media.Image.Procedural;
using Utilities.Media.Image.ExtensionMethods;
#endregion

namespace Utilities.Media.Image
{
    /// <summary>
    /// Not recommended for use, fun class for distorting an image.
    /// </summary>
    public class OilPainting : IDisposable
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Image">Input image</param>
        /// <param name="Seed">Randomization seed</param>
        /// <param name="NumberOfPoints">Number of points for the painting</param>
        public OilPainting(Bitmap Image, int Seed, int NumberOfPoints)
        {
            if (Image == null)
                throw new ArgumentNullException("Image");
            _Image = new Bitmap(Image);
            _NumberOfPoints = NumberOfPoints;
            Map = new CellularMap(Seed, Image.Width, Image.Height, NumberOfPoints);
            SetupImage();
        }

        #endregion

        #region Private Functions

        private void SetupImage()
        {
            BitmapData ImageData = _Image.LockImage();
            int ImagePixelSize = ImageData.GetPixelSize();
            for (int i = 0; i < _NumberOfPoints; ++i)
            {
                int Red = 0;
                int Green = 0;
                int Blue = 0;
                int Counter = 0;
                for (int x = 0; x < _Image.Width; ++x)
                {
                    for (int y = 0; y < _Image.Height; ++y)
                    {
                        if (Map.ClosestPoint[x, y] == i)
                        {
                            Color Pixel = ImageData.GetPixel(x, y, ImagePixelSize);
                            Red += Pixel.R;
                            Green += Pixel.G;
                            Blue += Pixel.B;
                            ++Counter;
                        }
                    }
                }
                int Counter2 = 0;
                for (int x = 0; x < _Image.Width; ++x)
                {
                    for (int y = 0; y < _Image.Height; ++y)
                    {
                        if (Map.ClosestPoint[x, y] == i)
                        {
                            ImageData.SetPixel(x, y, Color.FromArgb(Red / Counter, Green / Counter, Blue / Counter), ImagePixelSize);
                            ++Counter2;
                            if (Counter2 == Counter)
                                break;
                        }
                    }
                    if (Counter2 == Counter)
                        break;
                }
            }
            _Image.UnlockImage(ImageData);
        }

        #endregion

        #region Properties

        private CellularMap Map = null;

        /// <summary>
        /// Final Bitmap Image
        /// </summary>
        public virtual Bitmap _Image { get; set; }

        private int _NumberOfPoints = 0;

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_Image != null)
            {
                _Image.Dispose();
                _Image = null;
            }
        }

        #endregion
    }
}