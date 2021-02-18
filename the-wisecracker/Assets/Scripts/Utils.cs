using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    private static Camera main = Camera.main;
    private static RaycastHit hit;

    public static Vector3 MousePosition()
    {
        Physics.Raycast(main.ScreenPointToRay(Input.mousePosition), out hit);
        return hit.point;
    }

    public static Vector3 MousePosition(float yCorrection)
    {
        Vector3 corrected = Copy(MousePosition());
        corrected.y = yCorrection;
        return corrected;
    }

    public static Vector3 Copy(Vector3 v)
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

    public static Vector3 ComputeParabolic(Vector3 origin, Vector3 target)
    {
        var distance = target - origin;
        var distanceXZ = distance;
        distanceXZ.y = 0f;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = distanceXZ.magnitude;
        float Vy = distance.y + .5f * Mathf.Abs(Physics.gravity.y);

        var result = distanceXZ.normalized * distanceXZ.magnitude;
        result.y = distance.y + .5f * Mathf.Abs(Physics.gravity.y);

        return result;
    }
}
