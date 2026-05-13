using UnityEngine;
using System.Collections.Generic;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager instance;

    private Vector3 currentCheckpoint;

    public List<string> objetosDesativados = new List<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCheckpoint(Vector3 pos)
    {
        currentCheckpoint = pos;
    }

    public Vector3 GetCheckpoint()
    {
        return currentCheckpoint;
    }

    public void RegistrarObjeto(string id)
    {
        if (!objetosDesativados.Contains(id))
        {
            objetosDesativados.Add(id);
        }
    }

    public bool ObjetoJaFoiDesativado(string id)
    {
        return objetosDesativados.Contains(id);
    }
}
