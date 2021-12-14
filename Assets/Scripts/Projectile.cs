using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;

    public event System.Action ProjectileDeath; // Event called when Projectile is destroyed

    private float speed = 4f;
    private int numRicochets = 0;


    void Start()
    {
        
    }

    public void SetAttributes(float newSpeed, int newNumRicochets)
    {
        speed = newSpeed;
        numRicochets = newNumRicochets;
    }


    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    void CheckCollisions(float moveDistance)
    {
        // Cast ray starting from current Projectile position and moving forward
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // If the ray hit a valid target, call OnProjectileHit
        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnProjectileHit(hit.collider, hit);
        }
    }

    void OnProjectileHit(Collider c, RaycastHit hit)
    {
        // If the object the projectile hit is a Tank, call the Tank's Die method
        Tank tank = c.GetComponent<Tank>();
        if(tank != null)        // Hit tank
        {
            tank.Die();
            OnProjectileDeath();
        }
        else if(numRicochets > 0)   // Hit wall but still has ricochets left
        {
            Vector3 reflectDir = Vector3.Reflect(transform.forward, hit.normal);
            float rotation = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));
            numRicochets--;
        }
        else                        // Hit wall and no ricochets left
        {
            OnProjectileDeath();
        }
    }

    void OnProjectileDeath()
    {
        if(ProjectileDeath != null)
        {
            ProjectileDeath();
        }
        Destroy(gameObject);
    }
}
