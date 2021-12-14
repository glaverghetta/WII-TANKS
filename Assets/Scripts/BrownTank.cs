using UnityEngine;

/*  Class for Brown Tanks 
    They are immobile
    They rotate their firing nozzle up and down in a 90 deg angle, and if the player is in their line of sight will pause for 1 second and then fire
 */

public class BrownTank : EnemyTank
{
    private Vector3 startingRotation;
    private bool rotationPositive = true;

    protected override void Start()
    {
        startingRotation = transform.rotation.eulerAngles;
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (!targetAcquired)
        {
            GetTarget();
            LookForPlayer();
        }
    }

    protected override void FixedUpdate()
    {
    
    }

    protected override Vector3 GetTarget()
    {
        float newYRot = directionHolder.transform.rotation.eulerAngles.y;
        if(rotationPositive)
        {
            newYRot += Time.deltaTime * turnSpeed;
        }
        else
        {
            newYRot -= Time.deltaTime * turnSpeed;
        }

        if(newYRot >= startingRotation.y + 45)
        {
            rotationPositive = false;
        }
        else if(newYRot <= startingRotation.y - 45)
        {
            rotationPositive = true;
        }

        directionHolder.transform.rotation = Quaternion.Euler(startingRotation.x, newYRot, startingRotation.z);

        return directionHolder.transform.forward;
    }


    protected override void GetMoveInput()
    {
        velocity = Vector3.zero;
    }

    /* Sets value of targetAcquired variable */
    private void LookForPlayer()
    {
        // Create ray with origin at direction holder's position and moving outward from the direction holder
        Ray ray = new Ray
        {
            origin = transform.GetChild(0).transform.position,
            direction = transform.GetChild(0).transform.forward
        };

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // If ray hit player tank, set targetAcquired to true. Shoot after 1 second.
            if (hit.collider.tag.Equals("Player"))
            {
                targetAcquired = true;
                Invoke("Shoot", 1);
            }
            else
            {
                targetAcquired = false;
            }
        }
    }

    protected override void Shoot()
    {
        base.Shoot();
        targetAcquired = false;
    }
}
