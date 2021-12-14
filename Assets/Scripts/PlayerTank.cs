using UnityEngine;

/* The tank controlled by the player. */

public class PlayerTank : Tank
{

    public bool godMode = false;

    void Start()
    {
        
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override Vector3 GetTarget()
    {
        // Cast ray from camera to mouse cursor
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, directionHolder.transform.position);
        float rayDistance;
        Vector3 lookPoint = Vector3.zero;

        // If ray intersected ground plane, rayDistance gets assigned the distance the ray travelled
        if(groundPlane.Raycast(ray, out rayDistance))
        {
            // Get the point at rayDistance units along the ray
            Vector3 point = ray.GetPoint(rayDistance);
            lookPoint = point;
            
        }

        return lookPoint;
    }

    protected override void GetMoveInput()
    {
        Vector3 inputDirection = Vector3.zero;

        // TODO: provide a way to turn off movement, pausing for example
        if(true)
        {
            inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        }

        float inputMagnitude = inputDirection.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);
        

        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed * inputMagnitude);

        velocity = transform.forward * speed * smoothInputMagnitude;
       
    }

    void OnCollisionEnter()
    {

    }

    public void LayMine()
    {

    }

    public override void Die()
    {
        if (!godMode)
        {
            base.Die();
        }
    }
}
