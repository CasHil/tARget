using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    private GameObject cylinder;
    public bool laserAim; 
    public bool touchScreenAiming;
    public bool dualMode;
    public Material laserRed;

    [SerializeField]
    public Vector3 aimDirection;

    public Vector3 position;

    // Start is called before the first frame update
    private void Start()
    {
        cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Destroy(cylinder.GetComponent<Rigidbody>()); //The missile gots destroyed by the cylinders rigidbody so I took it away lol
        cylinder.GetComponent<Renderer>().material = laserRed;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3[] joints = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.skeleton.joints;

        // Draw a line between joints 5 and 7. See https://imgur.com/a/vdzYDOF or the
        // Manomotion SDK Pro Technical Documentation for which joints 5 and 7 are.
        Vector3 startJoint = CalculateNewPositionFromJoint(joints[5]);
        Vector3 endJoint = CalculateNewPositionFromJoint(joints[7]);

        aimDirection = endJoint - startJoint;
        position = startJoint + aimDirection * 2.0f;
        Vector3 scale = new Vector3(0.02f, aimDirection.magnitude * 2.0f, 0.02f);

        /*Debug.Log("Start joint: " + startJoint);
        Debug.Log("End joint: " + endJoint);
        Debug.Log("Aim direction: " + aimDirection);
        Debug.Log("Position: " + position);
        Debug.Log("Scale: " + scale);
        Debug.Log("======================= " + aimDirection);*/

        if (laserAim == true)
        {
            cylinder.transform.position = position;
            cylinder.transform.localScale = scale;
            cylinder.transform.up = aimDirection;
        }
        else
        {
            Destroy(cylinder);
        }
        // This is for testing, we can remove it when we don't want to fire by the touchscreen.
        if (touchScreenAiming == true)
        {   
            if(!dualMode)
            {
                GetComponent<VoiceAim>().enabled = false; 
            }
            if (Input.touchCount == 0)
            {
                return;
            }
            else
            {   
                GetComponent<VoiceAim>().StopListening();
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        GetComponent<instantiateProjectile>().Fire(position, aimDirection);
    }

    private Vector3 CalculateNewPositionFromJoint(Vector3 joint)
    {
        return ManoUtils.Instance.CalculateNewPosition(new Vector3(joint.x, joint.y, joint.z), 0);
    }
}