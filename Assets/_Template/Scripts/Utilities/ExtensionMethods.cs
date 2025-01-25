using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public static class StringExtensions
{
    public static string ToSnakeCase(this string value)
    {
        if (string.IsNullOrEmpty(value)) return value;

        var stringBuilder = new StringBuilder();
        for (int i = 0; i < value.Length; i++)
        {
            var currentChar = value[i];
            if (char.IsUpper(currentChar))
            {
                if (i > 0 && value[i - 1] != '_')
                {
                    stringBuilder.Append('_');
                }
                stringBuilder.Append(char.ToLower(currentChar));
            }
            else if (currentChar != ' ')
            {
                stringBuilder.Append(currentChar);
            }
        }

        return stringBuilder.ToString();
    }
}

public static class VectorExtensions
{
    public static Vector3 IgnoreAxis(this Vector3 v, Axis ignoreFlags)
    {
        if (ignoreFlags.HasFlag(Axis.X)) v.x = 0f;
        if (ignoreFlags.HasFlag(Axis.Y)) v.y = 0f;
        if (ignoreFlags.HasFlag(Axis.Z)) v.z = 0f;
        return v;
    }
    public static Vector3 IgnoreAxis(this Vector3 v, Axis ignoreFlags, Vector3 source)
    {
        if (ignoreFlags.HasFlag(Axis.X)) v.x = source.x;
        if (ignoreFlags.HasFlag(Axis.Y)) v.y = source.y;
        if (ignoreFlags.HasFlag(Axis.Z)) v.z = source.z;
        return v;
    }

    public static Vector3 WithX(this Vector3 v, float x) => new Vector3(x, v.y, v.z);
    public static Vector3 WithY(this Vector3 v, float y) => new Vector3(v.x, y, v.z);
    public static Vector3 WithZ(this Vector3 v, float z) => new Vector3(v.x, v.y, z);
    public static Vector2 WithX(this Vector2 v, float x) => new Vector2(x, v.y);
    public static Vector2 WithY(this Vector2 v, float y) => new Vector2(v.x, y);
    public static Vector3 WithZ(this Vector2 v, float z) => new Vector3(v.x, v.y, z);
    public static Vector3 XyToXz(this Vector2 v, float y = 0f) => new Vector3(v.x, y, v.y);
}

public static class VectorUtils
{
    public static Vector3 RandomXYZ(float minX, float minY, float mixZ)
    {
        return RandomXYZ(-minX, minX, -minY, minY, -mixZ, mixZ);
    }

    public static Vector3 RandomXYZ(float minX, float maxX, float minY, float maxY, float mixZ, float maxZ)
    {
        return new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(mixZ, maxZ));
    }

    public static Vector3 RandomXY(float min, float max)
    {
        return new Vector3(Random.Range(min, max), Random.Range(min, max), 0f);
    }

    public static Vector3 RandomXZ(float min, float max)
    {
        return new Vector3(Random.Range(min, max), 0f, Random.Range(min, max));
    }

    public static Vector3 RandomXY(float minX, float maxX, float minY, float maxY)
    {
        return new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
    }

    public static Vector3 RandomXZ(float minX, float maxX, float mixZ, float maxZ)
    {
        return new Vector3(Random.Range(minX, maxX), 0f, Random.Range(mixZ, maxZ));
    }

    public static Vector2 RandomInsideCircle(float radius)
    {
        return Random.insideUnitCircle * radius;
    }

    public static Vector3 RandomInsideSphere(float radius)
    {
        return Random.insideUnitSphere * radius;
    }

    public static Vector3 RandomInsideSphere(Vector3 center, float radius)
    {
        return center + RandomInsideSphere(radius);
    }

    public static Vector3 RandomBetween(Transform transformA, Transform transformB)
    {
        return new Vector3(Random.Range(transformA.position.x, transformB.position.x), Random.Range(transformA.position.y, transformB.position.y), Random.Range(transformA.position.z, transformB.position.z));
    }

    public static Vector3 RandomBetween(Vector3 positionA, Vector3 positionB)
    {
        return new Vector3(Random.Range(positionA.x, positionB.x), Random.Range(positionA.y, positionB.y), Random.Range(positionA.z, positionB.z));
    }

    public static bool ApproximatelyEquals(this Vector3 a, Vector3 b, float epsilon = 0.01f)
    {
        return
            Mathf.Abs(a.x - b.x) <= epsilon &&
            Mathf.Abs(a.z - b.z) <= epsilon;
    }
}

public static class LayerMaskExtensions
{
    public static bool Includes(this LayerMask mask, int layer)
    {
        return (mask.value & (1 << layer)) != 0;
    }
}

public static class FloatExtensions
{
    public static Task Awaiter(this float seconds) => Task.Delay(seconds.ToMilliseconds());
    public static int ToMilliseconds(this float seconds) => Mathf.RoundToInt(seconds * 1000);
    public static float AtLeast(this float value, float min) => Mathf.Max(value, min);
    public static float AtMost(this float value, float max) => Mathf.Min(value, max);
    public static float Clamp(this float value, float min, float max) => Mathf.Clamp(value, min, max);
    public static float Clamp01(this float value) => Mathf.Clamp01(value);
}

public static class IntExtensions
{
    public static int MoreThan(this int value, int min) => Mathf.Max(value, min);
    public static int LessThan(this int value, int max) => Mathf.Min(value, max);
    public static int Clamp(this int value, int min, int max) => Mathf.Clamp(value, min, max);
    public static int PlusOrMinus() => Random.Range(0, 2) == 0 ? -1 : 1;
}

public static class BoolExtensions
{
    public static TValue To<TValue>(this bool value, TValue valueIfTrue, TValue valueIfFalse)
    {
        return value ? valueIfTrue : valueIfFalse;
    }

    public static float ToFloat01(this bool value)
    {
        return value.To(1f, 0f);
    }
}


public static class TransformExtensions
{
    public static void DestroyChildren(this Transform transform)
    {
        var children = transform.Cast<Transform>().ToArray();

        foreach (var child in children)
        {
            Object.Destroy(child.gameObject);
        }
    }
}

public static class ArrayExtensions
{
    public static void Swap<T>(this T[] array, int indexA, int indexB)
    {
        T temp = array[indexA];
        array[indexA] = array[indexB];
        array[indexB] = temp;
    }

    public static int LastIndex<T>(this T[] array)
    {
        return array.Length - 1;
    }
}
