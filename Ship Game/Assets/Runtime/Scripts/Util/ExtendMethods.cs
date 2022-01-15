using UnityEngine;

public static class ExtendMethods
{
    public static Vector2 Forward2d(this Transform transform)
    {
        float zRotation = transform.rotation.eulerAngles.z;

        // Clamp between (0, 360) instead of default (-180, 180)
        while(zRotation < 0)
            zRotation += 360;

        while(zRotation > 360)
            zRotation -= 360;

        return new Vector2(){
                                x = Mathf.Cos(zRotation * Mathf.Deg2Rad),
                                y = Mathf.Sin(zRotation* Mathf.Deg2Rad)
                            };
    }
}