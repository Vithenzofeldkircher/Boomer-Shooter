using UnityEngine;

public class Biblia : MonoBehaviour
{
    [SerializeField] private GameObject magic;
    [SerializeField] private Transform magicBornPlaceInSpace;
    private bool isHeald = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && isHeald == true)
        {
            Instantiate(magic, magicBornPlaceInSpace);
        }
    }
    public void IsCurrentWeapon()
    {
        isHeald = true;
    }
    public void IsNotCurrentWeapon()
    {
        isHeald = false;
    }
}
