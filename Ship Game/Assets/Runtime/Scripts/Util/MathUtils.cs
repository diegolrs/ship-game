using UnityEngine;

public static class MathUtils
{
    // Sin(a + b) = sin(a)*cos(b) + sin(b)*cos(a)
    public static float SinOfSum(float a, float b)
    {
        return Mathf.Sin(a) * Mathf.Cos(b) + Mathf.Sin(b) * Mathf.Cos(a);
    }

    // Cos(a + b) = cos(a)*cos(b) - sin(a)*sin(b)
    public static float CosOfSum(float a, float b)
    {
        return Mathf.Cos(a) * Mathf.Cos(b) - Mathf.Sin(a) * Mathf.Sin(b);
    }

    public static float LerpClamped(float init, float end, float t)
    {
        t = Mathf.Clamp(t, 0, 1);
        return init + t*(end - init);
    }
}