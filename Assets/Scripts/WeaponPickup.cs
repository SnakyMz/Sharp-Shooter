using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInChildren<ActiveWeapon>().SwitchWeapon(weaponSO);
            Destroy(gameObject);
        }
    }
}
