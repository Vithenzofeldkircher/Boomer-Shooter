using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private Transform firePointLeft;
    [SerializeField] private     Transform firePointRight;

    [SerializeField] private float attackRange = 40f;

    [Range(0f, 100f)]
    [SerializeField] private float attackChance = 30f;

    [SerializeField] private float thinkRate = 0.5f;

    [SerializeField] private LineRenderer laser;

    [SerializeField] private Transform laserPoint;

    [SerializeField] private float laserDuration = 3f;

    [SerializeField] private float laserCooldown = 10f;

    [SerializeField] private float rotationSpeed = 3f;

    [SerializeField] private GameObject bloodEffect;

    [SerializeField] private Transform pointBloodEffect;
    private Transform player;
    private Animator animator;

    private float thinkTimer;
    private float laserTimer;

    private bool usingLaser = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        GameObject playerObj =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        thinkTimer = thinkRate;
        laserTimer = laserCooldown;

        if (laser != null)
        {
            laser.enabled = false;
        }
    }

    void Update()
    {
        if (player == null) return;

        RotateToPlayer();

        float distance =
            Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
            return;

        if (usingLaser)
            return;

        laserTimer -= Time.deltaTime;

        if (laserTimer <= 0f)
        {
            StartCoroutine(LaserAttack());

            laserTimer = laserCooldown;

            return;
        }

        thinkTimer -= Time.deltaTime;

        if (thinkTimer <= 0f)
        {
            TryAttack();

            thinkTimer = thinkRate;
        }
    }

    void RotateToPlayer()
    {
        Vector3 direction =
            player.position - transform.position;

        direction.y = 0f;

        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    void TryAttack()
    {
        float randomValue =
            Random.Range(0f, 100f);

        if (randomValue <= attackChance)
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        int randomHand = Random.Range(0, 2);

        if (randomHand == 0)
        {
            Shoot(firePointLeft);
        }
        else
        {
            Shoot(firePointRight);
        }
    }

    void Shoot(Transform firePoint)
    {
        if (firePoint == null || projectilePrefab == null)
            return;

        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        Vector3 direction =
            (player.position - firePoint.position).normalized;

        Projectile projectileScript =
            projectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            projectileScript.Initialize(direction);
        }
    }

    IEnumerator LaserAttack()
    {
        usingLaser = true;

        animator.SetTrigger("ChargeLaser");
        GameObject blood = Instantiate(bloodEffect, pointBloodEffect.position, Quaternion.LookRotation(pointBloodEffect.position - transform.position));
        Vector3 laserDirection =
            (player.position - laserPoint.position).normalized;

        laserDirection += new Vector3(
            Random.Range(-0.15f, 0.15f),
            0f,
            Random.Range(-0.15f, 0.15f)
        );

        laserDirection.Normalize();

        yield return new WaitForSeconds(0.8f);

        GameObject blood2 = Instantiate(bloodEffect, pointBloodEffect.position, Quaternion.LookRotation(pointBloodEffect.position - transform.position));

        animator.SetTrigger("Laser");

        laser.enabled = true;

        float timer = 0f;

        while (timer < laserDuration)
        {
            GameObject blood3 = Instantiate(bloodEffect, pointBloodEffect.position, Quaternion.LookRotation(pointBloodEffect.position - transform.position));
            timer += Time.deltaTime;

            RaycastHit hit;

            if (Physics.Raycast(
                laserPoint.position,
                laserDirection,
                out hit,
                100f))
            {
                laser.SetPosition(0, laserPoint.position);
                laser.SetPosition(1, hit.point);


                IStatusPlayer player = hit.collider.GetComponentInParent<IStatusPlayer>();
                if (player != null)
                {
                    player.DamagePlayer(2f);
                }                
                   
            }
            else
            {
                laser.SetPosition(0, laserPoint.position);

                laser.SetPosition(
                    1,
                    laserPoint.position + laserDirection * 100f
                );
            }

            yield return null;
        }

        laser.enabled = false;

        usingLaser = false;
    }
}