using StarterAssets;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class walkEnemy : MonoBehaviour
{
    [SerializeField] private float _visionDistance = 3;
    private NavMeshAgent _agent;

    void Start()
    {
       _agent = GetComponent<NavMeshAgent>();    
    }

    void Update()
    {
        {
            Ray ray = new Ray(gameObject.transform.position,gameObject.transform.forward);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit, _visionDistance))
                return;
            if (!hit.collider.CompareTag("Player"))
                return;
                _agent.SetDestination(hit.transform.position);
            //_agent.remainingDistance 
            gameObject.transform.rotation = Quaternion.LookRotation(gameObject.transform.position, hit.transform.position).normalized ;
        }
    }
}
