using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform turretHead;
    [SerializeField] Transform PlayerTargetPoint;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] float fireRate=2f;
    [SerializeField] int damage = 2;


    PlayerHealth player;


    void Start()
    {
        player = FindAnyObjectByType<PlayerHealth>();
        StartCoroutine(FireRoutine());
    }

    void Update()
    {
        turretHead.LookAt(PlayerTargetPoint);
        
    }

    IEnumerator FireRoutine()
    {
        while (player)
        {
            yield return new WaitForSeconds(fireRate);
            Projectile newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            newProjectile.transform.LookAt(PlayerTargetPoint);
            newProjectile.Init(damage);


        }
    }
}
