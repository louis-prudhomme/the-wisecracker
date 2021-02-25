using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utils : MonoBehaviour
{
    private static Camera main = Camera.main;

    private static Camera Main()
    {
        if (main == null)
            main = Camera.main;
        return main;
    }

    private static RaycastHit hit;

    public static Vector3 MousePosition()
    {
        Physics.Raycast(Main().ScreenPointToRay(Input.mousePosition), out hit);
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

        var result = distanceXZ.normalized * distanceXZ.magnitude;
        result.y = distance.y + .5f * Mathf.Abs(Physics.gravity.y);

        return result;
    }

    public static float StandardLevelGroundPosition => SceneManager.GetActiveScene()
        .name.Equals("Level1")
        ? 0.352f
        : 1.27f;

    public static GameObject FindGameObject(string tag) => GameObject.FindGameObjectWithTag(tag);
    public static GameObject[] FindGameObjects(string tag) => GameObject.FindGameObjectsWithTag(tag);
    public sealed class Tags
    {
        public static readonly string GAME = "Game";
        public static readonly string PLAYER = "Player";
        public static readonly string RIOTER = "Rioter";

        public static readonly string RIOTERS_RETREAT = "RiotersRetreat";
        public static readonly string RIOTERS_GOAL = "RiotersGoal";

        public static readonly string RIOTERS_CONTAINER = "RiotersContainer";
        public static readonly string PROJECTILES_CONTAINER = "ProjectilesContainer";
    }
}
