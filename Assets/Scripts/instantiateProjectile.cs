using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateProjectile : MonoBehaviour
{
    
    public GameObject projectile;
    public float speed;
    private GameObject instantiatedProjectile;
    private GameObject stationaryProjectile;
    private Vector3 direction;
    private Vector3 position;
    public float projectileFlightTime;

    private void Start(){
            stationaryProjectile = Instantiate(projectile, position, Quaternion.LookRotation(direction));
    }

    void Update(){
        if(ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.skeleton.joints == null){
            return;
        }
        Vector3[] joints = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.skeleton.joints;
        // Draw a line between joints 5 and 7. See https://imgur.com/a/vdzYDOF or the
        // Manomotion SDK Pro Technical Documentation for which joints 5 and 7 are.
        Vector3 startJoint = CalculateNewPositionFromJoint(joints[5]);
        Vector3 endJoint = CalculateNewPositionFromJoint(joints[7]);
        Vector3 aimDirection = endJoint - startJoint;
        //Vector3 position = CalculateNewPositionFromJoint(joints[6]);

        if(stationaryProjectile){
        stationaryProjectile.transform.rotation = Quaternion.LookRotation(aimDirection);
        stationaryProjectile.transform.position = CalculateNewPositionFromJoint(joints[6]);
        }
    }
    
    public void Fire(Vector3 position, Vector3 aimDirection)
        {   
            //Debug.Log("FIRE");
            //Debug.Log(!instantiatedProjectile);
            if(!instantiatedProjectile){
            Destroy(stationaryProjectile);
            direction = aimDirection;
            //Debug.Log("Position: "+position+" Direction: "+direction+" Rotation: "+Quaternion.LookRotation(direction));
            instantiatedProjectile = Instantiate(projectile, position, Quaternion.LookRotation(direction));
            instantiatedProjectile.GetComponent<Rigidbody>().velocity = transform.TransformDirection(direction).normalized * speed;
            Handheld.Vibrate();
            Reload(projectileFlightTime);
            }
        }
    
    private void Reload(float time){
        Destroy(instantiatedProjectile, time);
        StartCoroutine(Wait(time));
    }

    private Vector3 CalculateNewPositionFromJoint(Vector3 joint)
    {
        return ManoUtils.Instance.CalculateNewPosition(new Vector3(joint.x, joint.y, joint.z), 0);
    }
    
    public IEnumerator Wait(float seconds)
        {   
            yield return new WaitForSeconds(seconds);
            stationaryProjectile = Instantiate(projectile);
            GetComponent<VoiceAim>().StartListening();
        }
}
