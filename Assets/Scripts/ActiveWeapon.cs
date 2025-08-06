using StarterAssets;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO startingWeaponSO;
    [SerializeField] GameObject zoomVignette;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] TMP_Text ammoText;

    WeaponSO currentWeaponSO;
    Weapon currentWeapon;
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    FirstPersonController firstPersonController;
    
    int currentAmmo;
    float timeSinceLastShot = 0f;
    float defaultFOV;
    float defaultRotationSpeed;

    string SHOOT_ANIMATION = "Shoot";

    void Awake()
    {
        firstPersonController = GetComponentInParent<FirstPersonController>();
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        defaultFOV = virtualCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }

    void Start()
    {
        SwitchWeapon(startingWeaponSO); // Initialize with the starting weapon
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

        if (timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
            timeSinceLastShot = 0f; // Reset cooldown
            animator.Play(SHOOT_ANIMATION, 0, 0f);
            currentWeapon.Shoot(currentWeaponSO);
            AdjustAmmo(-1); // Decrease ammo count
        }

        if (!currentWeaponSO.IsAutomatic)
        {
            // Reset shoot input after processing
            starterAssetsInputs.ShootInput(false); 
        }
    }

    void HandleZoom()
    {
        if (!currentWeaponSO.CanZoom) return;

        if (starterAssetsInputs.zoom)
        {
            zoomVignette.SetActive(true);
            virtualCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomFOV; // Set to zoom FOV
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
        }
        else
        {
            zoomVignette.SetActive(false);
            virtualCamera.m_Lens.FieldOfView = defaultFOV; // Reset to default FOV
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
        }
    }

    public void SwitchWeapon(WeaponSO newWeaponSO)
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject); // Destroy the current weapon
        }

        // Update the weaponSO reference to the new weapon
        currentWeaponSO = newWeaponSO;
        // Instantiate the new weapon prefab and assign it to currentWeapon
        currentWeapon = Instantiate(currentWeaponSO.WeaponPrefab, transform).GetComponent<Weapon>();
        // Set initial ammo to magazine size
        AdjustAmmo(currentWeaponSO.MagazineSize); 
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > currentWeaponSO.MagazineSize)
        {
            currentAmmo = currentWeaponSO.MagazineSize; // Cap at magazine size
        }
        ammoText.text = currentAmmo.ToString("D2");
    }
}
