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
    
    #region Vector Rotation
    // Linear Algebra Concept: 
    // You can rotate a vector v in 90 degrees by doing v = (-v.y, v.x) 
    // To rotate 180/270 degrees, just apply this concept by rotating 90 degrees twice/three times
    public static Vector2 Rotate90Degress(Vector2 v2) => new Vector2(-v2.y, v2.x);
    public static Vector2 Rotate180Degress(Vector2 v2) => new Vector2(-v2.x, -v2.y);
    public static Vector2 Rotate270Degress(Vector2 v2) => new Vector2(v2.y, -v2.x);
    #endregion
}
