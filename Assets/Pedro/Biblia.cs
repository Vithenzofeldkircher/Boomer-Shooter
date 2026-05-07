using UnityEngine;

public class Biblia : MonoBehaviour
{
    [SerializeField] private GameObject _magic;
    [SerializeField] private Transform _magicBornPlaceInSpace;
    private bool _isHeald = false;
    public bool collected = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collected == false)
            return;

        if (_isHeald == false)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(_magic, _magicBornPlaceInSpace);
        }
    }
    public void IsCurrentWeapon()
    {
        collected = true;
        _isHeald = true;
    }
    public void IsNotCurrentWeapon()
    {
        _isHeald = false;
    }
}
