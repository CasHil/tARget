using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField]
    private FingerInfoGizmo fingerInfoGizmo;

    private Vector3 denormalizedPosition;

    // Start is called before the first frame update
    private void Start()
    {
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
        Vector3 ringPlacement = JointToHandTracking(ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.skeleton.joints[8]);

        Debug.Log("Finger info at: " + ringPlacement);
        GameObject.Find("Cube").transform.position = ringPlacement;
    }

    // The hand tracking on the bottom left of the screen starts at (0, 0), i.e. if a joint is in the bottom left, its position is (0, 0).
    // However, the world origin is in the middle of the screen. Therefore, you need to translate the object's width by (-0.5, -0.5).
    private Vector3 JointToHandTracking(Vector3 position)
    {
        Vector3 calculatedJoint;
        /* calculatedJoint = new Vector3(position.x * width, position.y * height, position.z);*/

        calculatedJoint = new Vector3(position.x - 0.5f, position.y - 0.5f, position.z);
        return calculatedJoint;
    }
}