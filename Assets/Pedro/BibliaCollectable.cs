using UnityEngine;
using UnityEngine.Events;

public class BibliaCollectable : MonoBehaviour
{
    private Biblia biblia;
    [SerializeField] private UnityEvent bibliaCollected;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            bibliaCollected.Invoke();
            Destroy(gameObject);
        }
    }
}
