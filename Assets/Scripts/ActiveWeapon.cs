using StarterAssets;
using Unity.Cinemachine;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] GameObject zoomVignette;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    Weapon currentWeapon;
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    
    float timeSinceLastShot = 0f;
    float defaultFOV = 40f;

    string SHOOT_ANIMATION = "Shoot";

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        defaultFOV = virtualCamera.m_Lens.FieldOfView;
        currentWeapon = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleShoot();
        HandleZoom();
    }

    void HandleShoot()
    {
        timeSinceLastShot += Time.deltaTime; // Increment cooldown timer

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

    void HandleZoom()
    {
        if (!weaponSO.CanZoom) return;

        if (starterAssetsInputs.zoom)
        {
            zoomVignette.SetActive(true);
            virtualCamera.m_Lens.FieldOfView = weaponSO.ZoomFOV; // Set to zoom FOV
        }
        else
        {
            zoomVignette.SetActive(false);
            virtualCamera.m_Lens.FieldOfView = defaultFOV; // Reset to default FOV
        }
    }

    public void SwitchWeapon(WeaponSO newWeaponSO)
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject); // Destroy the current weapon
        }

        // Update the weaponSO reference to the new weapon
        weaponSO = newWeaponSO;
        // Instantiate the new weapon prefab and assign it to currentWeapon
        currentWeapon = Instantiate(weaponSO.WeaponPrefab, transform).GetComponent<Weapon>();
    }
}
