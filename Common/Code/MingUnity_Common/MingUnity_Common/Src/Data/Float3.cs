namespace MingUnity.Common
{
    public struct Float3
    {
        public float x;

        public float y;

        public float z;

        public Float3(float x, float y, float z)
        {
            this.x = x;

            this.y = y;

            this.z = z;
        }

        public override bool Equals(object obj)
        {
            bool res = false;

            if (obj is Float3)
            {
                Float3 target = (Float3)obj;

                res = x == target.x && y == target.y && z == target.z;
            }

            return res;
        }

        public static bool operator ==(Float3 a, Float3 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Float3 a, Float3 b)
        {
            return !a.Equals(b);
        }

        public static Float3 operator +(Float3 a, Float3 b)
        {
            return new Float3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Float3 operator -(Float3 a, Float3 b)
        {
            return new Float3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"[Float3 x:{x} y:{y} z:{z}]";
        }
    }
}
