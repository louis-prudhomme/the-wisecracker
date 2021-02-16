using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    private static Camera main = Camera.main;
    public static Vector3 MousePosition()
    {
        return MousePosition(main.nearClipPlane);
    }
    public static Vector3 MousePosition(float correction)
    {
        var v = main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            1));
        v.y = correction;
        return v;
    }

    public static Vector3 Copy(Vector3 v)
    {
        return new Vector3(
            v.x,
            v.y,
            v.z);
    }

    public static Vector3 Copy(Vector3 v, float y)
    {
        return new Vector3(
            v.x,
            v.y,
            v.z);
    }

    public static Quaternion Copy(Quaternion q)
    {
        return new Quaternion(
            q.x,
            q.y,
            q.z,
            q.w);
    }
}
