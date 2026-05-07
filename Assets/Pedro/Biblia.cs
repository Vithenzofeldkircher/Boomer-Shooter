using UnityEngine;

public class Biblia : MonoBehaviour
{
    [SerializeField] private GameObject magic;
    [SerializeField] private Transform magicBornPlaceInSpace;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Switch"))
        {
            Instantiate(magic, magicBornPlaceInSpace);
        }
    }
}
