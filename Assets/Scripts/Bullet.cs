using UnityEngine;
 
public class Bullet : MonoBehaviour {
 
    private void Start() {
        GetComponent<Rigidbody>().velocity = transform.forward * 6;
    }
 
    private void OnCollisionEnter() {
        Destroy (gameObject);
    }
}