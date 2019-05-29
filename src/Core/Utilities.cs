﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace ArtillerySeries.src
{
    public static class Utilities
    {
        /*
         * To add:  
         *  PhysicsEngine.Clamp
         *  Color Roughly
         *  Color RoughlyValued
         *  
         * 
         */

        private static Random _random = new Random();

        public static Vector RandomVector(float magnitude)
        {
            return new Vector()
            {
                X = (float)_random.NextDouble() * magnitude,
                Y = (float)_random.NextDouble() * magnitude
            };
        }

        public static Point2D RandomPoint2D(float magnitude)
        {
            return new Point2D()
            {
                X = (float)_random.NextDouble() * magnitude,
                Y = (float)_random.NextDouble() * magnitude
            };
        }
        public static double RandDoubleBetween(double min, double max)
        {
            return _random.NextDouble() * (max - min) + min;
        }

        public static float RandFloatBetween(float min, float max)
        {
            return (float)RandDoubleBetween(min, max);
        }

        public static Point2D AddPoint2D(Point2D a, Point2D b)
        {
            return new Point2D()
            {
                X = a.X + b.X,
                Y = a.Y + b.Y,
            };
        }

        public static Vector Normalise(Vector vector)
        {
            float x = vector.X;
            float y = vector.Y;
            float magnitude = (float)Math.Sqrt(x * x + y * y);
            vector.X /= magnitude;
            vector.Y /= magnitude;
            return vector;
        }

        public static float VectorDirection(Point2D to, Point2D from)
        {
            return VectorDirection(new Vector(to), new Vector(from));
        }

        public static float VectorDirection(Vector to, Vector from)
        {

            return VectorDirection(new Vector(from.X - to.X, from.Y - to.Y));
        }

        public static float VectorDirection(Point2D vector)
        {
            return VectorDirection(new Vector(vector));
        }

        public static float VectorDirection(Vector vector)
        {
            return (float)(Math.Tan((vector.Y / vector.X)) % (2 * Math.PI));
        }
        public static Vector ZeroVector()
        {
            return new Vector();
        }
        public static Point2D ZeroPoint2D()
        {
            return new Point2D()
            {
                X = 0,
                Y = 0
            };
        }

        public static void DrawTextCentre(string text, Color color, float x, float y)
        {
            SwinGame.DrawText(text, color, x - (text.Length * 3.5f), y);
        }

        public static void DrawTextCentre(string text, Color color, Vector pt)
        {
            DrawTextCentre(text, color, pt.X, pt.Y);
        }


        public static float Rad(float deg)
        {
            return deg * (float)Math.PI / 180;
        }

        public static float Deg(float rad)
        {
            return (rad * 180 / (float)Math.PI) % 360f;
        }

        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }

        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }

        public static bool WithinBoundary(Vector pos, Rectangle boundary)
        {
            if (SwinGame.PointInRect(pos.ToPoint2D, boundary))
                return true;

            return false;
        }
    }
}
