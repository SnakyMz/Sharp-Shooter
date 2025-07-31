using StarterAssets;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;

    Weapon currentWeapon;
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    
    float timeSinceLastShot = 0f;

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
        timeSinceLastShot += Time.deltaTime;
        HandleShoot();
    }

    void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;

        if (timeSinceLastShot >= weaponSO.FireRate)
        {
            timeSinceLastShot = 0f; // Reset cooldown
            animator.Play(SHOOT_ANIMATION, 0, 0f);
            currentWeapon.Shoot(weaponSO);
        }

        if (!weaponSO.IsAutomatic)
        {
            // Reset shoot input after processing
            starterAssetsInputs.ShootInput(false); 
        }
    }

    public void SwitchWeapon(WeaponSO newWeapon)
    {
        Debug.Log("New weapon picked up: " + newWeapon.name);
    }
}
