using UnityEngine;

namespace MingUnity.Common
{
    public static class DataExtends
    {
        public static Vector2 ToVector2(this Float2 obj)
        {
            return new Vector2(obj.x, obj.y);
        }

        public static Vector3 ToVector3(this Float3 obj)
        {
            return new Vector3(obj.x, obj.y, obj.z);
        }

        public static Vector2 ToVector2(this Int2 obj)
        {
            return new Vector2(obj.x, obj.y);
        }

        public static Vector3 ToVector3(this Int3 obj)
        {
            return new Vector3(obj.x, obj.y, obj.z);
        }
    }
}
