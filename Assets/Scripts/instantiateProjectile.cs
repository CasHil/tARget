using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateProjectile : MonoBehaviour
{
    public GameObject projectile;
    public float speed = 20;
    private GameObject instantiatedProjectile;
    private GameObject stationaryProjectile;
    private Vector3 direction;
    private Vector3 position;

    private void Start()
    {
        stationaryProjectile = Instantiate(projectile, position, Quaternion.LookRotation(direction));
    }

    private void Update()
    {
        Vector3[] joints = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.skeleton.joints;

        // Draw a line between joints 5 and 7. See https://imgur.com/a/vdzYDOF or the
        // Manomotion SDK Pro Technical Documentation for which joints 5 and 7 are.
        Vector3 startJoint = joints[7];
        Vector3 endJoint = joints[8];
        Vector3 aimDirection = endJoint - startJoint;
        Vector3 position = CalculateNewPositionFromJoint(joints[7]);

        Debug.Log("tARget: Start joint: " + startJoint);
        Debug.Log("tARget: End joint: " + endJoint);
        Debug.Log("tARget: Aim direction: " + aimDirection);
        /*Debug.Log("tARget: Position: " + position);*/
        Debug.Log("tARget: =======================");

        stationaryProjectile.transform.position = position;
        //stationaryProjectile.transform.rotation = Quaternion.LookRotation(aimDirection);
        stationaryProjectile.transform.forward = aimDirection;
    }

    public void Fire(Vector3 position, Vector3 aimDirection)
    {
        if (!instantiatedProjectile)
        {
            Destroy(stationaryProjectile);
            direction = aimDirection;
            //Debug.Log("Position: "+position+" Direction: "+direction+" Rotation: "+Quaternion.LookRotation(direction));
            instantiatedProjectile = Instantiate(projectile, position, Quaternion.LookRotation(direction));
            instantiatedProjectile.GetComponent<Rigidbody>().velocity = transform.TransformDirection(direction).normalized * speed;
            Handheld.Vibrate();
            Reload(3f);
        }
    }

    private void Reload(float time)
    {
        Destroy(instantiatedProjectile, time);
        StartCoroutine(Wait(time));
    }

    private Vector3 CalculateNewPositionFromJoint(Vector3 joint)
    {
        return ManoUtils.Instance.CalculateNewPosition(new Vector3(joint.x, joint.y, joint.z), 0);
    }

    private Vector3 CalculateNewPositionWithDepth(Vector3 position)
    {
        return ManoUtils.Instance.CalculateNewPositionDepth(new Vector3(position.x, position.y, position.z), position.z);
    }

    public IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        stationaryProjectile = Instantiate(projectile);
    }
}