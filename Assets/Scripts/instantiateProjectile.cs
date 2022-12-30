using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateProjectile : MonoBehaviour
{
    
    public GameObject projectile;
    public float speed = 20;
    private GameObject instantiatedProjectile = null;

    public void Fire(Vector3 position, Vector3 direction)
        {
            if(!instantiatedProjectile){
            //Debug.Log("Position: "+position+" Direction: "+direction+" Rotation: "+Quaternion.LookRotation(direction));
            instantiatedProjectile = Instantiate(projectile, position, Quaternion.LookRotation(direction));
            instantiatedProjectile.GetComponent<Rigidbody>().velocity = transform.TransformDirection(direction);
            Destroy(instantiatedProjectile, 3f);
            Handheld.Vibrate();
            //Wait(1f);  
            }
        }

        public IEnumerator Wait(float seconds)
        {   
            
            yield return new WaitForSeconds(seconds);
        }
}
