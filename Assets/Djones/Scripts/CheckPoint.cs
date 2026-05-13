using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        return;
        CheckPointManager.instance.SetCheckpoint(respawnPoint.position);
    }
}