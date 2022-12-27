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

    // Start is called before the first frame update
    private void Start()
    {
        lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        ManomotionManager.Instance.ShouldCalculateSkeleton3D(true);
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

        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        lineRenderer.SetPosition(0, jointIndexFinger1);
        lineRenderer.SetPosition(1, aimDirection);
    }

    // The hand tracking on the bottom left of the screen starts at (0, 0), i.e. if a joint is in the bottom left, its position is (0, 0).
    // However, the world origin is in the middle of the screen. Therefore, you need to translate the object's width by (-0.5, -0.5).
    private Vector3 TranslateHandTrackingToWorldView(Vector3 position)
    {
        return new Vector3(position.x - 0.5f, position.y - 0.5f, position.z);
    }

    private Vector3 CalculateNewPositionFromJoint(Vector3 joint)
    {
        return ManoUtils.Instance.CalculateNewPositionSkeletonPosition(new Vector3(joint.x, joint.y, joint.z), joint.z);
    }
}