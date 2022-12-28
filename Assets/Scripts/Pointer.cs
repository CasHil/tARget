using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    /*[SerializeField]
    private FingerInfoGizmo fingerInfoGizmo;*/

    public GameObject cylinder;

    // Start is called before the first frame update
    private void Start()
    {
        ManomotionManager.Instance.ShouldCalculateSkeleton3D(true);
        cylinder = GameObject.Find("Cylinder");
        /*if (fingerInfoGizmo == null)
        {
            try
            {
                fingerInfoGizmo = GameObject.Find("TryOnManager").GetComponent<FingerInfoGizmo>();
            }
            catch
            {
                Debug.Log("Failed to find TryOnManager");
            }
        }*/
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3[] joints = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.skeleton.joints;

        // Draw a line between joints 5 and 7. See https://imgur.com/a/vdzYDOF or the
        // Manomotion SDK Pro Technical Documentation for which joints 5 and 7 are.
        Vector3 startJoint = CalculateNewPositionFromJoint(joints[5]);
        Vector3 endJoint = CalculateNewPositionFromJoint(joints[7]);

        Vector3 aimDirection = endJoint - startJoint;
        Vector3 position = startJoint + aimDirection / 2.0f;
        Vector3 scale = new Vector3(0.02f, aimDirection.magnitude / 2.0f, 0.02f);

        /*
        Debug.Log("Start joint: " + startJoint);
        Debug.Log("End joint: " + endJoint);
        Debug.Log("Aim direction: " + aimDirection);
        Debug.Log("Position: " + position);
        Debug.Log("Scale: " + scale);
        Debug.Log("======================= " + aimDirection);
        */

        cylinder.transform.position = position;
        cylinder.transform.localScale = scale;
        cylinder.transform.up = aimDirection;
    }

    private Vector3 CalculateNewPositionFromJoint(Vector3 joint)
    {
        return ManoUtils.Instance.CalculateNewPosition(new Vector3(joint.x, joint.y, joint.z), 0);
    }
}