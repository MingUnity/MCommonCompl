﻿using System;

namespace MingUnity.Common
{
    [Serializable]
    public struct Float2
    {
        public float x;

        public float y;

        public Float2(float x, float y)
        {
            this.x = x;

            this.y = y;
        }

        public override bool Equals(object obj)
        {
            bool res = false;

            if (obj is Float2)
            {
                Float2 target = (Float2)obj;

                res = x == target.x && y == target.y;
            }

            return res;
        }

        public static bool operator ==(Float2 a, Float2 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Float2 a, Float2 b)
        {
            return !a.Equals(b);
        }

        public static Float2 operator +(Float2 a, Float2 b)
        {
            return new Float2(a.x + b.x, a.y + b.y);
        }

        public static Float2 operator -(Float2 a, Float2 b)
        {
            return new Float2(a.x - b.x, a.y - b.y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"[Float2 x:{x} y:{y}]";
        }
    }
}