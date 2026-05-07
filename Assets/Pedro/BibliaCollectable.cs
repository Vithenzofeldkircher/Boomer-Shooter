using UnityEngine;

public class BibliaCollectable : MonoBehaviour
{
    private Biblia biblia;
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
            biblia.collected = true;
            Destroy(gameObject);
        }
    }
}
