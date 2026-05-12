using System.Collections;
using UnityEngine;

public class MeeleWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _hitbox;
    //[SerializeField] private GameObject _meeleCode;
    [SerializeField] private int _meeleDamage = 1; 
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject _crossVisual;
    private bool _equipped = true;
    private bool _isAttaking = false;
    public bool _isAnimation = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _hitbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_equipped == false)
            return;
        if (Input.GetButtonDown("Fire1") && _isAttaking == false)
        {
            Debug.Log(animator);
            _isAnimation = true;
            animator.SetBool("IsAttacking", true);
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
    public void Equipped()
    {
        _equipped = true;
        _crossVisual.SetActive(true);
        //_meeleCode.SetActive(true);
        Vector3 pos = _crossVisual.transform.position;
        _crossVisual.transform.position = pos;
        pos.x = 0.45f;

    }
    public void Desequipped()
    {
        _equipped = false;
        _crossVisual.SetActive(false);
        //_meeleCode.SetActive(false);
        Vector3 pos = _crossVisual.transform.position;
        _crossVisual.transform.position = pos;
        pos.x = 0.45f;
    }
    IEnumerator attackRate()
    {
        yield return new WaitForSeconds(2);
        Debug.Log(animator);
        _isAnimation = false;
        animator.SetBool("IsAttacking", false);
        _hitbox.SetActive(false);
        _isAttaking = false;
    }
}
