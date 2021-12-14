using UnityEngine;

/* Interace implemented by all Tank objects. Defines basic tank behaviors. */

public interface ITank
{
    // Tank gets its target using its specific method
    Vector3 GetTarget();

    // Aims firing nozzle at target point
    void LookAt(Vector3 target);

    // Tank gets its move input using its specific method
    void GetMoveInput();

    // Move in direction acquired in GetMoveInput
    void Move();

    // Fires the Tank's Projectile in the direction it is looking at
    void Shoot();

    // Lay mine at current position
    void LayMine();

    // Destroy tank after taking hit from Projectile
    void Die();
}
