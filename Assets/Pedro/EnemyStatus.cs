using UnityEngine;

public class EnemyStatus : MonoBehaviour, IShootable
{
    [SerializeField] float _lifeMax = 2;
     private float _currentLife;
    public void Hitted(float damage)
    {
        _currentLife -= damage;

        if (_currentLife > 0)
            return;

        print("Matou");
        Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentLife = _lifeMax;
    }
}