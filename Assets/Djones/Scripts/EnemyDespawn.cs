using UnityEngine;

public class EnemyDespawn : MonoBehaviour
{
    
    public string idObjeto;

    private void Start()
    {
        // Se já foi coletada, desativa
        if (CheckPointManager.instance.ObjetoJaFoiDesativado(idObjeto))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Marca como coletada
            CheckPointManager.instance.RegistrarObjeto(idObjeto);

            gameObject.SetActive(false);
        }
    }
}
