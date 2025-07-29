using StarterAssets;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;

    Weapon currentWeapon;
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;

    string SHOOT_ANIMATION = "Shoot";

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleShoot();
    }

    void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;

        animator.Play(SHOOT_ANIMATION, 0, 0f);
        starterAssetsInputs.ShootInput(false); // Reset shoot input after processing
        currentWeapon.Shoot(weaponSO);
    }
}
