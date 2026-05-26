using UnityEngine;
using NumericsVec3 = System.Numerics.Vector3;

public static class Vector3Utils
{
    public static NumericsVec3 ToCore(Vector3 v) => new(v.x, v.y, v.z);

    public static Vector3 ToUnity(NumericsVec3 v) => new(v.X, v.Y, v.Z);
}
