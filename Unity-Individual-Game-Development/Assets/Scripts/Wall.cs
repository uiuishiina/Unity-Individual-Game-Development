using UnityEngine;

public class Wall : MonoBehaviour, ICollisionResponse
{
    public float restitution = 0.9f;

    public Vector3 GetBounceVelocity(
        Rigidbody ball,
        Collision collision,
        Vector3 incomingVelocity)
    {
        Vector3 normal = collision.contacts[0].normal;
        var reflected = Vector3.Reflect(incomingVelocity, normal) * restitution;
        float randomAngle = Random.Range(-5f, 5f);
        Vector3 result = Quaternion.AngleAxis(randomAngle, Vector3.up) * reflected;

        return result * restitution;
    }
}