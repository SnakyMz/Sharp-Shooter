using Unity.Cinemachine;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] CinemachineCamera deathCamera;
    [SerializeField] Transform weaponCamera;
    [SerializeField] int health = 5;

    int currentHealth;
    int deathCameraPriiority = 20;

    void Awake()
    {
        currentHealth = health;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            weaponCamera.parent = null;
            deathCamera.Priority = deathCameraPriiority;
            Destroy(gameObject);
        }
    }
}
