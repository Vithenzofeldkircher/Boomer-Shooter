using System.Collections;
using UnityEngine;

public class Biblia : MonoBehaviour
{
    [Header("Os serialize Fields")]
    [SerializeField] private GameObject _magic;
    [SerializeField] private Transform _magicBornPlaceInSpace;
    private bool _isHeald = true;
    public bool _spellRateWating = false;

    void Update()
    {
        if (_isHeald == false)
            return;

        if (_spellRateWating == true)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(_magic, _magicBornPlaceInSpace);
            StartCoroutine(SpellRate());
            _spellRateWating = true;
        }
    }
    IEnumerator SpellRate()
    {
        yield return new WaitForSeconds(1.6f);
        _spellRateWating = false;
    }

    public void IsCurrentWeapon()
    {
        _isHeald = true;
    }
    public void IsNotCurrentWeapon()
    {
        _isHeald = false;
    }
}