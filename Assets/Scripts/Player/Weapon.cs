using Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    
    [SerializeField] ParticleSystem muzzleFlash;
    //[SerializeField] LayerMask interactionLayers;
    const string SHOOT_STRING = "Shoot";

    CinemachineImpulseSource impulseSource;

    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }


    public bool Shoot(WeaponSO weaponSO)
    {
        muzzleFlash.Play();
        impulseSource.GenerateImpulse();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);
            EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);

        }

        return true;
    }
}
