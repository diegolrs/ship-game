using UnityEngine;

public static class ExtendMethods
{
    /// <summary> Get Transform 2D rotation angle in degrees. 
    /// The value is clamped between (0, 360) instead of default (-180, 180). </summary>
    /// <returns> Return transform rotation z clamped between 0 and 360 degrees. </returns>
    public static float GetZRotationInDegrees(this Transform transform)
    {
        float zRotation = transform.rotation.eulerAngles.z;

        while(zRotation < 0)
            zRotation += 360;

        while(zRotation > 360)
            zRotation -= 360;

        return zRotation;
    }

    public static Vector2 Forward2d(this Transform transform)
    {
        float zRotation = transform.GetZRotationInDegrees() * Mathf.Deg2Rad;

        return new Vector2(){
                                x = Mathf.Cos(zRotation),
                                y = Mathf.Sin(zRotation)
                            };
    }

    public static void SetRotation(this Transform transform, Vector3 rotation)
    {
        transform.rotation = Quaternion.Euler( rotation );
    }

    public static T PickRandom<T>(this T[] array)
    {
        int randIndex = Random.Range(0, array.Length);
        return array[randIndex];
    }
}