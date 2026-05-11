using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [Header("Configuração de Movimento")]
    public float speed = 5f;
    public float stoppingDistance = 6f;
    public float maxRange = 30f;

    [Header("Configuração de Ataque")]
    public float attackCooldown = 5f;

    public Transform firePoint;
    public GameObject projectilePrefab;

    private Transform player;
    private Animator animator;

    private bool canShoot = true;
    private float currentCooldown;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Procura o player pela tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        currentCooldown = attackCooldown;
    }

    void Update()
    {
        if (player == null) return;

        // Controle do cooldown
        if (!canShoot)
        {
            currentCooldown -= Time.deltaTime;

            if (currentCooldown <= 0f)
            {
                canShoot = true;
                currentCooldown = attackCooldown;
            }
        }

        // Distância até o player
        float distance = Vector3.Distance(transform.position, player.position);

        // Player dentro do alcance
        if (distance <= maxRange)
        {
            animator.SetBool("Chasing", true);

            // Seguir player
            if (distance > stoppingDistance)
            {
                animator.SetBool("Attacking", false);

                Vector3 direction = (player.position - transform.position).normalized;

                transform.position += direction * speed * Time.deltaTime;

                transform.LookAt(player);
            }
            // Atacar player
            else
            {
                transform.LookAt(player);

                if (projectilePrefab != null && canShoot)
                {
                    animator.SetBool("Chasing", false);
                    animator.SetBool("Attacking", true);

                    GameObject projectile = Instantiate(
                        projectilePrefab,
                        firePoint.position,
                        Quaternion.identity
                    );

                    Vector3 direction =
                        (player.position - firePoint.position).normalized;

                    projectile
                        .GetComponent<Projectile>()
                        .Initialize(direction);

                    canShoot = false;
                }
            }
        }
        else
        {
            animator.SetBool("Chasing", false);
            animator.SetBool("Attacking", false);
        }
    }
}