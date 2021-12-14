using UnityEngine;

public class GrayTank : EnemyTank
{
    

    protected override void Start()
    { 
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override Vector3 GetTarget()
    {
        return playerLocation;
    }

    protected override void GetMoveInput()
    {
        
    }
}
