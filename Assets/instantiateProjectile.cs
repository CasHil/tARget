using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateProjectile : MonoBehaviour
{
    
    //The speed of the projectile
    public float speed = 10f;

    //The direction the projectile will move in
    public Vector3 direction;

    //The distance the projectile will travel
    public float distance = 10f;

    //The layerMask for the raycast
    public LayerMask layerMask;

    //The current distance traveled
    private float currentDistance;

    //The raycast hit
    private RaycastHit hit;

    // Use this for initialization
    void Start ()
    {
        //Calculate the direction vector
        direction = direction.normalized;

        //Set the current distance to 0
        currentDistance = 0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Check if the projectile has traveled it's maximum distance
        if (currentDistance < distance)
        {
            //Calculate the new position
            Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

            //Check if the projectile will hit something
            if (Physics.Raycast(transform.position, direction, out hit, speed * Time.deltaTime, layerMask))
            {
                //Set the new position to the hit point
                newPosition = hit.point;

                //Call the OnHit method
                OnHit();
            }

            //Set the new position
            transform.position = newPosition;

            //Increase the current distance
            currentDistance += speed * Time.deltaTime;
        }
        else
        {
            //Destroy the projectile
            Destroy(gameObject);
        }
	}

    //This method is called when the projectile hits something
    void OnHit()
    {
        //Do something here
    }
}
