using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [Header("Configuração de Movimento")]
    public float speed = 5f;            // Velocidade de movimento
    public float stoppingDistance = 6f; // Distância mínima do player
    public float maxRange = 30f;        // Distância maxima que o inimigo persegue o player

    [Header("Configuração de Ataque")]
    public float attackCooldown = 5f; 
    public float timeUntilAttack = 1f;

    public Transform firePoint;
    private Transform player;

    public GameObject projectilePrefab;

    bool canShoot = true;
    void Start()
    {
        // Acha o player pela tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;
        if (!canShoot)
        {
            attackCooldown -= Time.deltaTime * timeUntilAttack;
            if (attackCooldown <= 0)
            {
                canShoot = true;
                attackCooldown = 5f;
            }

        }
        // Segue o player no ar

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= maxRange)
        {
            if (distance > stoppingDistance)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
                transform.LookAt(player); // Faz olhar pro player
            }

            else if (distance <= stoppingDistance)
            {
                if (projectilePrefab != null && player != null && canShoot  )
                {
                    GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                    Vector3 direction = (player.position - firePoint.position).normalized;
                    projectile.GetComponent<Projectile>().Initialize(direction);
                    transform.LookAt(player);
                    canShoot = false;
                }

            }
        }
    }
}