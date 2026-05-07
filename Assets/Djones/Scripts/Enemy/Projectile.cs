
using UnityEngine;
using UnityEngine.Animations;

public class Projectile : MonoBehaviour, IProjectile
{
    public float speed = 10f;
    public float lifeTime = 5f;

    private Vector3 direction;

    public void Initialize(Vector3 dir)
    {
        direction = dir;
        Destroy(gameObject, lifeTime);  // Destroi após alguns segundos
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           Destroy(gameObject);
        }
    }
}