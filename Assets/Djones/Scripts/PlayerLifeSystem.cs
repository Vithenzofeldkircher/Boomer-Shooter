using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeSystem : MonoBehaviour, IStatusPlayer
{
    [SerializeField] private float _ImortalityTime = 0.5f;
    [SerializeField] private float _maxLife = 3;
    [SerializeField] private Slider _lifeBar;
    private float _ActualTime;
    private float _life;

    public void DamagePlayer(float damage)
    {
        if (_ActualTime > 0)
            return;

        _life -= damage;
        Debug.Log(_life);
        _ActualTime = _ImortalityTime;
        if (_life > 0)
            return;
         transform.position = CheckPointManager.instance.GetCheckpoint();
    }

    void Start()
    {
        _ActualTime =_ImortalityTime;
       _life = _maxLife;
    }

    void Update()
    {
        if (_ActualTime > 0)
        {
            _ActualTime -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.L)) 
        { 
         DamagePlayer(1.0f);
        }
    }
}
