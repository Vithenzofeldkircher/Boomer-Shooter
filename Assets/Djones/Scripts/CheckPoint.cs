using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if (!other.CompareTag("Player"))
        return;
        Debug.Log("player");
        CheckPointManager.instance.SetCheckpoint(respawnPoint.position);
        
    }
}