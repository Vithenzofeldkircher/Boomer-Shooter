
using UnityEngine;
using UnityEngine.Animations;

public class Projectile : MonoBehaviour, IProjectile
{
    public float speed = 10f;
    public float lifeTime = 5f;
    public float damage = 1;

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
    private void OnTriggerEnter(Collider other)
    {
          IStatusPlayer player = other.gameObject.GetComponentInChildren<IStatusPlayer>();
          if (player == null)
              return;
 
          player.DamagePlayer(damage);
          Destroy(gameObject);
    }


}