using UnityEngine;
using StarterAssets;
using Cinemachine;
using TMPro;

public class ActiveWeapon : MonoBehaviour
{

    [SerializeField] WeaponSO startingWeapon;
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] Camera weaponCamera;
    [SerializeField] GameObject zoomVignette;
    [SerializeField] TMP_Text ammoText;

    WeaponSO currentWeaponSO;
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    FirstPersonController firstPersonController;
    Weapon currentWeapon;
    const string SHOOT_STRING = "Shoot";
    float timeSinceLastShot = 0f;
    float defaultFOV;
    float defualtRotationSpeed;
    int currentAmmo;


    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
        defualtRotationSpeed = firstPersonController.RotationSpeed;
    }

    void Start()
    {
        SwitchWeapon(startingWeapon);
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }

    void Update()
    {
        HandleShoot();
        HandleZoom();
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo>currentWeaponSO.MagazineSize) {
            currentAmmo = currentWeaponSO.MagazineSize;
        }
        ammoText.text = currentAmmo.ToString("D2");
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }

        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;
        this.currentWeaponSO = weaponSO;
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }

    // --- CHANGED SECTION 2 ---
    // Changed return type from 'bool' to 'void'
    private void HandleShoot()
    {
        timeSinceLastShot += Time.deltaTime;

        // This 'return' now only exits the HandleShoot method,
        // it does not stop HandleZoom from running in Update().
        if (!starterAssetsInputs.shoot) return;

        if (timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
            currentWeapon.Shoot(currentWeaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            timeSinceLastShot = 0f;
            AdjustAmmo(-1);
        }

        if (!currentWeaponSO.isAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }

        // No 'return true' is needed anymore.
    }


    void HandleZoom()
    {
        if (!currentWeaponSO.CanZoom) return;

        if (starterAssetsInputs.zoom)
        {
            playerFollowCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;
            zoomVignette.SetActive(true);
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
        }
        else
        {
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
            weaponCamera.fieldOfView = defaultFOV;
            zoomVignette.SetActive(false);
            firstPersonController.ChangeRotationSpeed(defualtRotationSpeed);
        }
    }
}