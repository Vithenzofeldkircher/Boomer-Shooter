using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class walkEnemy : MonoBehaviour
{
    [SerializeField] private float _visionDistance = 3;
    private NavMeshAgent _agent;
    private Vector3 _EnemyTrasform;
    void Start()
    {
      _agent = GetComponent<NavMeshAgent>();    
    }

    void Update()
    {
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

         //   if(Physics.Linecast(hit, _visionDistance))


        }
    }
}
