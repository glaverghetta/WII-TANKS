using UnityEngine;

/* Base class for all enemy tanks */

public abstract class EnemyTank : Tank
{
    public Color[] matColors = new Color[4];        // 0 = body, 1 = treads, 2 = cap, 3 = nozzle
    public GameObject cap;
    public GameObject nozzle;

    protected bool targetAcquired = false;
    protected Vector3 playerLocation;

    protected virtual void Start()
    {
        // Set colors of the tank
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        renderer.materials[0].color = matColors[0];
        renderer.materials[1].color = matColors[1];

        if(cap.GetComponent<MeshRenderer>() != null)
        {
            cap.GetComponent<MeshRenderer>().material.color = matColors[2];
        }

        if (nozzle.GetComponent<MeshRenderer>() != null)
        {
            nozzle.GetComponent<MeshRenderer>().material.color = matColors[3];
        }
    }

    protected override void Update()
    {
        playerLocation = GetPlayerPos();
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected Vector3 GetPlayerPos()
    {
        return GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).position;
    }

    public override void Die()
    {
        base.Die();
    }
}