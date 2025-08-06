using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] ParticleSystem muzzleFlash;

    public void Shoot(WeaponSO weaponSO)
    {
        muzzleFlash.Play();
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayer, QueryTriggerInteraction.Ignore))
        {
            Instantiate(weaponSO.HitVFX, hit.point, Quaternion.identity);
            hit.collider.GetComponent<EneemyHealth>()?.TakeDamage(weaponSO.Damage);
        }
    }
}
