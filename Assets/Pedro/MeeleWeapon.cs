using UnityEngine;

public class MeeleWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _hitbox;
    [SerializeField] private Transform _playerRotation;
    private IShootable _shootable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _shootable.Hitted(1);
        }
    }
}
