using UnityEngine;

public class EneemyHealth : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] int health = 3;

    int currentHealth;

    void Awake()
    {
        currentHealth = health;
    }

    private void Start()
    {
        GameManager.Instance.UpdateEnemiesLeft(1);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        GameManager.Instance.UpdateEnemiesLeft(-1);
        Destroy(gameObject);
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
    }
}
