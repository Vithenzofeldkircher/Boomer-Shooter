using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        return;
        Debug.Log("player");
        CheckPointManager.instance.SetCheckpoint(respawnPoint.position);
    }
}