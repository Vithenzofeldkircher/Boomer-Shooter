using System.Collections;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public float speed = 5f;
    public float detectionRange = 10f;
    private Transform enemy;
    [SerializeField] private float damage = 2;

    void Start()
    {
        transform.SetParent(null);
        StartCoroutine(MagicTimeOfLife());
        // Procura o player pela tag
        GameObject enemyObj = GameObject.FindGameObjectWithTag("Enemy");

        if (enemyObj != null)
        {
            enemy = enemyObj.transform;
        }
    }

    void Update()
    {
        if (enemy == null)
        {
            Destroy(gameObject);
            return;
        }

        float distance = Vector3.Distance(transform.position, enemy.position);

        if (distance <= detectionRange)
        {
            // Persegue
            transform.position = Vector3.MoveTowards(
                transform.position,
                enemy.position,
                speed * Time.deltaTime
            );
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IShootable target))
        {
            target.Hitted(damage);
            Destroy(gameObject);
        }
    }
    IEnumerator MagicTimeOfLife()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
