using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _attackCooldown;
    [SerializeField] private int _damagePerHit;
    [SerializeField] private float _attackDistance;
    private float _cooldown;
    private NavMeshAgent _agent;
    [SerializeField] private GameObject _collider;
    private walkEnemy _walkSystem;
    private bool _canAttack;
    void Start()
    {
        _collider.GetComponent<BoxCollider>();
        _agent = GetComponentInParent<NavMeshAgent>();
        _walkSystem = GetComponentInParent<walkEnemy>();
        _collider.SetActive(false);
        _cooldown = _attackCooldown;
    }

    
    void Update()
    {
        if (_cooldown > 0)
        {
            _cooldown -= Time.deltaTime;
        }
        else
        {
            _canAttack = true;
        }  
        if (_agent.remainingDistance > _attackDistance && _walkSystem._chasing == false)
            return;
        if (!_canAttack)
            return;
        PerformAttack();
    }

   IEnumerator PerformAttack()
    {
        _walkSystem._canWalk = false;
        _collider.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _collider.SetActive(false);
        _cooldown = _attackCooldown;
        _walkSystem._canWalk = true;
        Debug.Log("attack");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out IDamagebleEnemy player))
            return;
        player.Hitted(_damagePerHit);
    }
}
