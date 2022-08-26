﻿using BuzzGUI.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace EnvelopeBlock
{
    /// <summary>
    /// Bunch of small static helper functions used around AudioBlock.
    /// </summary>
    public static class Utils
    {
        public static Brush[] WaveCanvasBurshes = new Brush[] { Brushes.Red, Brushes.DodgerBlue, Brushes.YellowGreen, Brushes.Orange, Brushes.Green, Brushes.Yellow, Brushes.DarkRed, Brushes.MediumBlue };
        public static void DrawText(Canvas canvas, string text)
        {
            Label lb = new Label() { FontSize = 13, Content = text, Foreground = Brushes.Black, Background = Brushes.Transparent, BorderThickness = new Thickness(0), Margin = new Thickness(0) };
            canvas.Children.Add(lb);
        }

        public static void DrawText(Canvas canvas, double x, double y, string text)
        {
            Label lb = new Label() { FontSize = 13, Content = text, Foreground = Brushes.Black, Background = Brushes.Transparent, BorderThickness = new Thickness(0), Margin = new Thickness(0) };

            Canvas.SetLeft(lb, x);
            Canvas.SetTop(lb, y);

            canvas.Children.Add(lb);
        }

        public static void DrawText(Canvas canvas, double x, double y, string text, Brush colorBrush)
        {
            Label lb = new Label() { FontSize = 13, Content = text, Foreground = colorBrush, Background = Brushes.Transparent, BorderThickness = new Thickness(0), Margin = new Thickness(0) };

            Canvas.SetLeft(lb, x);
            Canvas.SetTop(lb, y);

            canvas.Children.Add(lb);
        }

        public static void DrawBox(Canvas canvas)
        {
            Brush myBrush = new SolidColorBrush(Utils.ChangeColorBrightness(Global.Buzz.ThemeColors["SE Pattern Box"], -0.4f));

            DrawLine(canvas, canvas.Width, 0, canvas.Width, canvas.Height, myBrush, 1);
            DrawLine(canvas, canvas.Width, canvas.Height, 0, canvas.Height, myBrush, 1);

            myBrush = new SolidColorBrush(Utils.ChangeColorBrightness(Global.Buzz.ThemeColors["SE Pattern Box"], 0.4f));

            DrawLine(canvas, 0, canvas.Height - 1, 0, 0, myBrush, 2);
            DrawLine(canvas, 0, 0, canvas.Width - 1, 0, myBrush, 2);
        }

        public static void DrawLine(Canvas canvas, double X1, double Y1, double X2, double Y2, Brush brush, double strokeWidth)
        {
            Line myLine = new Line();

            myLine.Stroke = brush;
            myLine.SnapsToDevicePixels = true;
            myLine.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            myLine.StrokeThickness = strokeWidth;

            myLine.X1 = X1;
            myLine.Y1 = Y1;
            myLine.X2 = X2;
            myLine.Y2 = Y2;

            canvas.Children.Add(myLine);
        }

        public static void UpdateLine(Line myLine, double X1, double Y1, double X2, double Y2)
        {
            myLine.SnapsToDevicePixels = true;
            myLine.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

            myLine.X1 = X1;
            myLine.Y1 = Y1;
            myLine.X2 = X2;
            myLine.Y2 = Y2;
        }

        public static void UpdateLine(Line myLine, double X1, double Y1, double X2, double Y2, Brush brush, double strokeWidth)
        {
            myLine.Stroke = brush;
            myLine.SnapsToDevicePixels = true;
            myLine.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            myLine.StrokeThickness = strokeWidth;

            myLine.X1 = X1;
            myLine.Y1 = Y1;
            myLine.X2 = X2;
            myLine.Y2 = Y2;
        }
        public static Line CreateLine(Canvas canvas, double X1, double Y1, double X2, double Y2, Brush brush, double strokeWidth, EdgeMode edgeMode)
        {
            Line myLine = new Line();

            myLine.Stroke = brush;
            myLine.SnapsToDevicePixels = true;
            if (edgeMode == EdgeMode.Aliased)
                myLine.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            myLine.StrokeThickness = strokeWidth;

            myLine.X1 = X1;
            myLine.Y1 = Y1;
            myLine.X2 = X2;
            myLine.Y2 = Y2;

            canvas.Children.Add(myLine);

            return myLine;
        }


        public static Line CreateLine(Canvas canvas, double X1, double Y1, double X2, double Y2, Brush brush, double strokeWidth)
        {
            return CreateLine(canvas, X1, Y1, X2, Y2, brush, strokeWidth, EdgeMode.Aliased);
        }

        public static Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            red = red < 0 ? 0 : red;
            green = green < 0 ? 0 : green;
            blue = blue < 0 ? 0 : blue;

            red = red > 255 ? 255 : red;
            green = green > 255 ? 255 : green;
            blue = blue > 255 ? 255 : blue;

            return Color.FromArgb(color.A, (byte)red, (byte)green, (byte)blue);
        }

        /*
        public static Brush GetBrushForPattern(AudioBlock ab, int audioBlockIndex)
        {
            Brush brush;
            int color = ab.MachineState.AudioBlockInfoTable[audioBlockIndex].Color;

            if (color >= 0)
            {
                brush = new SolidColorBrush(Color.FromRgb((byte)((color & 0xFF0000) >> 16), (byte)((color & 0xFF00) >> 8), (byte)(color & 0xFF)));
            }
            else
            {
                brush = Utils.WaveCanvasBurshes[audioBlockIndex % Utils.WaveCanvasBurshes.Length];
            }
            return brush;
        }
        */

        public static LinearGradientBrush CreateLGBackgroundBrush(bool veritcalWave)
        {
            LinearGradientBrush lgBrush = new LinearGradientBrush();
            if (veritcalWave)
            {
                lgBrush.StartPoint = new Point(0, 0);
                lgBrush.EndPoint = new Point(0, 1);
            }
            else
            {
                lgBrush.StartPoint = new Point(0, 0);
                lgBrush.EndPoint = new Point(1, 0);
            }

            GradientStop bgGS1 = new GradientStop();
            bgGS1.Color = Global.Buzz.ThemeColors["SE Pattern Box"];
            bgGS1.Offset = 0.0;
            lgBrush.GradientStops.Add(bgGS1);

            GradientStop bgGS2 = new GradientStop();
            bgGS2.Color = ChangeColorBrightness(Global.Buzz.ThemeColors["SE Pattern Box"], -0.13f);
            bgGS2.Offset = 1.0;
            lgBrush.GradientStops.Add(bgGS2);

            return lgBrush;
        }

        /*
        public static BitmapImage ToBitmapImage(this System.Drawing.Bitmap bitmap, Rotation rotate)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.Rotation = rotate;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
        */
    }
}
