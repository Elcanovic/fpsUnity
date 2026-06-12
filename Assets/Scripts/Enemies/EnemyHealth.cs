using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject RobotExplosionVFX;
    [SerializeField] int startingHealth = 3;
    int currentHealth;

    GameManager gameManager;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager.AdjustEnemiesLeft(1);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            gameManager.AdjustEnemiesLeft(-1);
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
         gameManager.AdjustEnemiesLeft(-1);
        if (RobotExplosionVFX != null)
        Instantiate(RobotExplosionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);;
    }
}
