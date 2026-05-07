using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private int damagePerHit = 10;
    [SerializeField] private float attackDistance = 2f;

    [Header("References")]
    [SerializeField] private GameObject attackCollider;

    private NavMeshAgent agent;
    private WalkEnemy walkEnemy;

    private bool canAttack = true;
    private bool isAttacking;

    private void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        walkEnemy = GetComponentInParent<WalkEnemy>();

        attackCollider.SetActive(false);
    }

    private void Update()
    {
        if (!walkEnemy.IsChasing)
            return;

        if (agent.remainingDistance > attackDistance)
            return;

        if (!canAttack || isAttacking)
            return;

        StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;
        canAttack = false;

        walkEnemy.CanWalk = false;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        attackCollider.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        attackCollider.SetActive(false);
        walkEnemy.CanWalk = true;
        agent.isStopped = false;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagebleEnemy target =
            other.GetComponent<IDamagebleEnemy>();

        if (target == null)
            return;

        target.Hitted(damagePerHit);
    }
}
