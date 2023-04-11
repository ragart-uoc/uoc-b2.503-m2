using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * 6;
    }

    private void OnCollisionEnter (Collision collision)
    {
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>();
        if (health != null) {
            health.TakeDamage (10);
        }
        Destroy(gameObject);
    }
}