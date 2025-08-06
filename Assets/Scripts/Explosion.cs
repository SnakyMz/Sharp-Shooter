using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;
    [SerializeField] int damage = 3;

    const string PLAYER_TAG = "Player";

    private void Start()
    {
        Explode();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in hits)
        {
            PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();

            if (!playerHealth) continue;

            playerHealth.TakeDamage(damage);

            break;
        }
    }
}
