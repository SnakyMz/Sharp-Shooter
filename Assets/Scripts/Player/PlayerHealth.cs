using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Image[] shieldBars;
    [SerializeField] CinemachineCamera deathCamera;
    [SerializeField] Transform weaponCamera;
    [Range(1,10)]
    [SerializeField] int health = 10;

    int currentHealth;
    int deathCameraPriiority = 20;

    void Awake()
    {
        currentHealth = health;
        AdjustShield();
    }

    void AdjustShield()
    {
        for (int i = 0; i < shieldBars.Length; i++)
        {
            if (i < currentHealth)
            {
                shieldBars[i].enabled = true;
            }
            else
            {
                shieldBars[i].enabled = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        AdjustShield();

        if (currentHealth <= 0)
        {
            weaponCamera.parent = null;
            deathCamera.Priority = deathCameraPriiority;
            Destroy(gameObject);
        }
    }
}
