using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField]
    private FingerInfoGizmo fingerInfoGizmo;

    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public LineRenderer lineRenderer;
    public GameObject cylinder;

    // Start is called before the first frame update
    private void Start()
    {
        /* lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();*/
        ManomotionManager.Instance.ShouldCalculateSkeleton3D(true);
        cylinder = GameObject.Find("Cylinder");
        if (fingerInfoGizmo == null)
        {
            try
            {
                fingerInfoGizmo = GameObject.Find("TryOnManager").GetComponent<FingerInfoGizmo>();
            }
            catch
            {
                Debug.Log("Failed to find TryOnManager");
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        /*        fingerInfoGizmo.ShowFingerInformation();*/
        Vector3[] joints = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.skeleton.joints;
        Vector3 jointIndexFinger1 = CalculateNewPositionFromJoint(joints[5]);
        Debug.Log("Joint index 1: " + jointIndexFinger1);
        Vector3 jointIndexFinger2 = CalculateNewPositionFromJoint(joints[7]);
        Debug.Log("Joint index 2: " + jointIndexFinger2);

        Vector3 aimDirection = (jointIndexFinger2 - jointIndexFinger1);
        Debug.Log("aimDirection: " + aimDirection);
        Vector3 position = jointIndexFinger1 + aimDirection / 2.0f;
        Vector3 scale = new Vector3(0.02f, aimDirection.magnitude / 2.0f, 0.02f);
        // Vector3 scale = new Vector3(0.02f, 0.02f, 0.02f);
        Debug.Log("position: " + position);
        Debug.Log("scale: " + scale);
        Debug.Log("======================= " + aimDirection);
        /*lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        lineRenderer.SetPosition(0, jointIndexFinger1);
        lineRenderer.SetPosition(1, aimDirection);*/
        cylinder.transform.position = position;
        /* cylinder.transform.rotation = Quaternion.Euler(Mathf.Asin((jointIndexFinger2.x - jointIndexFinger1.x)));*/
        cylinder.transform.localScale = scale;
        /*var offset = end - start;
        var scale = new Vector3(width, offset.magnitude / 2.0f, width);
        var position = start + (offset / 2.0f);

        var cylinder = Instantiate(cylinderPrefab, position, Quaternion.identity);*/
        cylinder.transform.up = aimDirection;
    }

    // The hand tracking on the bottom left of the screen starts at (0, 0), i.e. if a joint is in the bottom left, its position is (0, 0).
    // However, the world origin is in the middle of the screen. Therefore, you need to translate the object's width by (-0.5, -0.5).
    private Vector3 TranslateHandTrackingToWorldView(Vector3 position)
    {
        return new Vector3(position.x - 0.5f, position.y - 0.5f, position.z);
    }

    private Vector3 CalculateNewPositionFromJoint(Vector3 joint)
    {
        return ManoUtils.Instance.CalculateNewPosition(new Vector3(joint.x, joint.y, joint.z), 0);
    }
}