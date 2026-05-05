using UnityEngine;
using UnityEngine.AI;

public class walkEnemy : MonoBehaviour
{
    [SerializeField] private float _visionDistance = 3;
    [SerializeField] private Transform[] _pathPoints;
    private int _pathIndex = 0;
    private float _perceptionTime = 2;
    private NavMeshAgent _agent;
    private bool _chasing;


    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if(_pathPoints.Length > 1)
        {
            _agent.SetDestination(_pathPoints[_pathIndex].position);
        }
    }

    void Update()
    {
        {
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f  &&!_chasing && _pathPoints.Length > 0)
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
            Ray ray = new Ray(gameObject.transform.position,gameObject.transform.forward);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit, _visionDistance))
                return;
            if (!hit.collider.CompareTag("Player"))
                return;
            _chasing = true;
            _agent.SetDestination(hit.transform.position);
            //_agent.remainingDistance 
            gameObject.transform.LookAt(hit.transform.position); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        float actualTime = _perceptionTime;
        if (!other.gameObject.CompareTag("Player"))
            return;
        Debug.Log("player");
        actualTime -= Time.deltaTime ;
        Debug.Log(actualTime);
        if (actualTime > 0)
            return;
        Debug.Log("chasing");
        _chasing = true;
    }
}
