using UnityEngine;

public class EnemyStatus : MonoBehaviour, IShootable
{
    [SerializeField] float _lifeMax = 2;
    private float _currentLife;
    private Vector3 _transform;
    public void Hitted(float damage)
    {
        _currentLife -= damage;

        if (_currentLife > 0)
            return;
            
        print("Matou");
        transform.position = _transform;
        gameObject.SetActive(false);
    }
    public void Respawn()
    {
        transform.position = _transform;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _transform = transform.position;
        _currentLife = _lifeMax;
    }
}