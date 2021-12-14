using UnityEngine;

/* Abstract class extended by all Tank types */

public abstract class Tank : MonoBehaviour
{
    public GameObject directionHolder;

    public Rigidbody myRigidBody;
    public float speed = 5f;
    public float smoothMoveTime = 0.1f;
    protected Vector3 velocity;

    public float turnSpeed = 8f;
    protected float angle;
    protected float smoothInputMagnitude;
    protected float smoothMoveVelocity;

    public Projectile bullet;
    public GameObject ejectionPoint;
    public float bulletSpeed = 6f;
    public int numRicochets = 1;
    public int maxSimultaneousBullets = 5;
    protected int currentBullets = 0;

    protected virtual void Update()
    {
        GetMoveInput();
    }

    protected virtual void FixedUpdate()
    {
        // Tank Movement
        Move();

        // Firing nozzle targeting
        Vector3 targetPoint = GetTarget();
        LookAt(targetPoint);
    }

    // Tank gets its target using its specific method
    protected abstract Vector3 GetTarget();

    // Aims firing nozzle at target point
    protected void LookAt(Vector3 target)
    {
        directionHolder.transform.LookAt(target);
    }

    // Tank gets its move input using its specific method
    protected abstract void GetMoveInput();


    // Move in direction acquired in GetMoveInput
    protected void Move()
    {
        myRigidBody.MovePosition(myRigidBody.position + (velocity * Time.deltaTime));
        myRigidBody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
    }

    // Fires the Tank's Projectile in the direction it is looking at
    protected virtual void Shoot()
    {
        if (currentBullets < maxSimultaneousBullets)
        {
            Projectile newProjectile = Instantiate(bullet, ejectionPoint.transform.position, directionHolder.transform.rotation) as Projectile;
            newProjectile.SetAttributes(bulletSpeed, numRicochets);
            newProjectile.ProjectileDeath += DecrementActiveBullets;
            currentBullets++;
        }
    }

    protected void DecrementActiveBullets()
    {
        currentBullets--;
    }

    // Destroy tank after taking hit from Projectile
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
