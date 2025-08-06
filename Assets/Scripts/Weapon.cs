using StarterAssets;
using Unity.Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] ParticleSystem muzzleFlash;

    CinemachineImpulseSource impulseSource;

    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shoot(WeaponSO weaponSO)
    {
        impulseSource.GenerateImpulse();
        muzzleFlash.Play();
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayer, QueryTriggerInteraction.Ignore))
        {
            Instantiate(weaponSO.HitVFX, hit.point, Quaternion.identity);
            hit.collider.GetComponent<EneemyHealth>()?.TakeDamage(weaponSO.Damage);
        }
    }
}
