using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MeeleWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _hitbox;
    [SerializeField] private int _meeleDamage = 1;
    private bool _isAttaking = false;
    private IShootable _shootable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _hitbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && _isAttaking == false)
        {
            _isAttaking = true;
            _hitbox.SetActive(true);
            StartCoroutine(attackRate());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out IShootable target))
        return;

        target.Hitted(_meeleDamage);
        _hitbox.SetActive(false);
    }
    IEnumerator attackRate()
    {
        yield return new WaitForSeconds(2);
        _hitbox.SetActive(false);
        _isAttaking = false;
    }
}
