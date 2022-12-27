using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField]
    private FingerInfoGizmo fingerInfoGizmo;

    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int lengthOfLineRenderer = 20;
    public LineRenderer lineRenderer;

    // Start is called before the first frame update
    private void Start()
    {
        lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
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
        /*denormalizedPosition = DenormalizeJointValues(ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.skeleton.joints[8], Screen.width, Screen.height);*/
        Vector3[] joints = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.skeleton.joints;
        Vector3 jointIndexFinger1 = TranslateHandTrackingToWorldView(joints[6]);
        Debug.Log("Joint index 1: " + jointIndexFinger1);
        Vector3 jointIndexFinger2 = TranslateHandTrackingToWorldView(joints[7]);
        Debug.Log("Joint index 2: " + jointIndexFinger2);

        Vector3 aimDirection = (jointIndexFinger2 - jointIndexFinger1) * 10;
        Debug.Log("aimDirection: " + aimDirection);

        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        /*LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 1.0f;
        lineRenderer.endWidth = 1.0f;*/
        lineRenderer.SetPosition(0, jointIndexFinger1);
        lineRenderer.SetPosition(1, aimDirection);

        /*Vector3 ringPlacement = JointToHandTracking(ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.skeleton.joints[8]);

        Debug.Log("Finger info at: " + ringPlacement);
        GameObject.Find("Cube").transform.position = ringPlacement;*/
    }

    // The hand tracking on the bottom left of the screen starts at (0, 0), i.e. if a joint is in the bottom left, its position is (0, 0).
    // However, the world origin is in the middle of the screen. Therefore, you need to translate the object's width by (-0.5, -0.5).
    private Vector3 TranslateHandTrackingToWorldView(Vector3 position)
    {
        return new Vector3(position.x - 0.5f, position.y - 0.5f, position.z);
    }
}