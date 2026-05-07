using UnityEngine;
using UnityEngine.AI;

public class WalkEnemy : MonoBehaviour
{
    [Header("Vision")]
    [SerializeField] private float visionDistance = 5f;

    [Header("Patrol")]
    [SerializeField] private Transform[] pathPoints;
    [SerializeField] private float pointReachDistance = 0.5f;

    private NavMeshAgent agent;
    private int currentPoint;

    public bool IsChasing { get; private set; }
    public bool CanWalk { get; set; } = true;

    private Animator _animator;

    private Transform player;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            player = playerObj.transform;

        GoToNextPoint();
    }

    private void Update()
    {
        if (!CanWalk)
        {
            _animator.SetBool("walking", false);
            agent.isStopped = true;
            return;
            
        }

        agent.isStopped = false;
        DetectPlayer();

        if (IsChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void DetectPlayer()
    {
        IsChasing = false;
        if (player == null)
            return;

        Vector3 direction = (player.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, visionDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                IsChasing = true;
            }
        }
    }

    private void ChasePlayer()
    {
        _animator.SetBool("walking", true);
        agent.SetDestination(player.position);
    }

    private void Patrol()
    {
        if (pathPoints.Length == 0)
            return;
        if (agent.pathPending)
            return;

        if (agent.remainingDistance <= pointReachDistance)
        {
            GoToNextPoint();
            _animator.SetBool("walking", true);
        }
    }

    private void GoToNextPoint()
    {
        if (pathPoints.Length == 0)
            return;

        agent.SetDestination(pathPoints[currentPoint].position);
        currentPoint = (currentPoint + 1) % pathPoints.Length;
    }
}