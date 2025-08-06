using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject hitVFX;
    [SerializeField] float speed = 10f;
    [SerializeField] int damage = 2;

    Rigidbody rb;

    const string PLAYER_TAG = "Player";

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        Instantiate(hitVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }    
}
