using UnityEngine;

public interface ICollisionResponse
{
    Vector3 GetBounceVelocity(
        Rigidbody ball,
        Collision collision,
        Vector3 incomingVelocity);
}

public class Boll : MonoBehaviour
{
    private Rigidbody rb_;

    private void Awake() {
        rb_ = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.TryGetComponent<ICollisionResponse>(out var response)) {
            rb_.linearVelocity = response.GetBounceVelocity(
                rb_,collision,rb_.linearVelocity
                );
        }

        if (collision.contacts[0].otherCollider.CompareTag("Racket"))
        {
            rb_.linearVelocity = Vector3.zero;
            rb_.angularVelocity = Vector3.zero;
            rb_.AddForce(new Vector3(0, 0.25f, 3f), ForceMode.Impulse);
        }
        else if(collision.contacts[0].otherCollider.CompareTag("Dead"))
        {
            gameObject.SetActive(false);
        }
    }
}
