using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public GameObject turret;
    private GameObject cylinder;
    private GameObject target;
    // Start is called before the first frame update
    private void Start()
    {
        //ManomotionManager.Instance.ShouldCalculateSkeleton3D(true);
        //cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        turret = Instantiate(turret);
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
        Vector3 position = startJoint + aimDirection * 2.0f;
        Vector3 scale = new Vector3(0.02f, aimDirection.magnitude * 2.0f, 0.02f);
        
        /*
        //turret.transform.rotation = target.transform.rotation;
        turret.transform.rotation = Quaternion.LookRotation(aimDirection);
        turret.transform.position = CalculateNewPositionFromJoint(joints[6]);
        
        Debug.Log("Start joint: " + startJoint);
        Debug.Log("End joint: " + endJoint);
        Debug.Log("Aim direction: " + aimDirection);
        Debug.Log("Position: " + position);
        Debug.Log("Scale: " + scale);
        Debug.Log("======================= " + aimDirection);
        
        Destroy(cylinder.GetComponent<Rigidbody>()); //The missile gots destroyed by the cylinders rigidbody so I took it away lol
        cylinder.transform.position = position;
        cylinder.transform.localScale = scale;
        cylinder.transform.up = aimDirection;
        */

        // This is for testing, we can remove it when we don't want to fire by the touchscreen. 
        if(Input.touchCount == 0)
        {
            return;
        }
        else
        {   
            GetComponent<instantiateProjectile>().Fire(CalculateNewPositionFromJoint(joints[6]), aimDirection);
        }
    }
    private Vector3 CalculateNewPositionFromJoint(Vector3 joint)
    {
        return ManoUtils.Instance.CalculateNewPosition(new Vector3(joint.x, joint.y, joint.z), 0);
    }
}