using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject hitVFXPrefab;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] int damage = 1;

    StarterAssetsInputs starterAssetsInputs;

    string SHOOT_ANIMATION = "Shoot";

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleShoot();
    }

    void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;

        muzzleFlash.Play();
        animator.Play(SHOOT_ANIMATION, 0, 0f);
        RaycastHit hit;
        starterAssetsInputs.ShootInput(false); // Reset shoot input after processing

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            Instantiate(hitVFXPrefab, hit.point, Quaternion.identity);
            hit.collider.GetComponent<EneemyHealth>()?.TakeDamage(damage);
        }
    }
}
