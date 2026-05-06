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
        if (_agent.remainingDistance > _attackDistance )
            return;
        if (!_walkSystem._chasing)
            return;
        if (!_canAttack)
            return;
       StartCoroutine(PerformAttack());
    }

   IEnumerator PerformAttack()
    {
        _walkSystem._canWalk = false;
        _collider.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _collider.SetActive(false);
        _walkSystem._canWalk = true;
        _canAttack = false;
        _cooldown = _attackCooldown;
        print("atack");
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagebleEnemy player = other.gameObject.GetComponentInChildren<IDamagebleEnemy>();
        if (player == null)
            return;
        player.Hitted(_damagePerHit);
        print("dano");
    }
}
