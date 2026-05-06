using UnityEngine;
using UnityEngine.AI;

public class walkEnemy : MonoBehaviour
{
    [SerializeField] private float _visionDistance = 3;
    [SerializeField] private float _perceptionTime = 1.5f;
    [SerializeField] private Transform[] _pathPoints;
    private int _pathIndex = 0;
    private float actualTime;
    private NavMeshAgent _agent;
    [HideInInspector] public bool _chasing;
    [HideInInspector] public bool _canWalk = true;


    void Start()
    {
        actualTime = _perceptionTime;
        _agent = GetComponent<NavMeshAgent>();
        if(_pathPoints.Length > 1)
        {
            _agent.SetDestination(_pathPoints[_pathIndex].position);
        }
    }

    void Update()
    {
        Debug.Log("Start");
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f && !_chasing && _pathPoints.Length > 0)
        {

            _pathIndex++;
            if (_pathIndex < _pathPoints.Length)
            {
                _agent.SetDestination(_pathPoints[_pathIndex].position);
            }
            else
            {
                _pathIndex = 0;
            }
        }
        _chasing = false;
        Ray ray = new Ray(gameObject.transform.position, gameObject.transform.forward);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, _visionDistance))
            return;
        if (!hit.collider.CompareTag("Player"))
            return;
        _chasing = true;
        _agent.SetDestination(hit.transform.position);
        //_agent.remainingDistance 
        gameObject.transform.LookAt(hit.transform.position);
        _agent.isStopped = !_canWalk;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (!_canWalk) 
            return;
        if (!other.gameObject.CompareTag("Player"))
            return;

        actualTime -= Time.deltaTime ;
        if (actualTime > 0)
            return;
 
        _agent.SetDestination(other.transform.position);
        _chasing = true;
    }
}
