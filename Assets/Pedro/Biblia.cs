using UnityEngine;

public class Biblia : MonoBehaviour
{
    [SerializeField] private GameObject _magic;
    [SerializeField] private Transform _magicBornPlaceInSpace;
    public float detectionRange = 10f;
    private bool _isHeald = false;
    public bool collected = false;
    public bool _isEnemyClose = true;
    private Transform enemy;
    void Start()
    {
        GameObject enemyObj = GameObject.FindGameObjectWithTag("Enemy");

        if (enemyObj != null)
        {
            enemy = enemyObj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (collected == false)
            return;

        if (_isHeald == false)
            return;

        if (Input.GetButtonDown("Fire1") && _isEnemyClose == false)
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
