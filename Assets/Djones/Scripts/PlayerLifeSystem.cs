using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLifeSystem : MonoBehaviour, IStatusPlayer
{
    [SerializeField] private float _ImortalityTime = 0.5f;
    [SerializeField] private float _maxLife = 3;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _DeathPanel;
    [SerializeField] private Slider _lifeBar;
    private float _ActualTime;
    private float _life;
    private bool _passCheckPoint;

    [SerializeField] private List<GameObject> _enemysList;
    public void DamagePlayer(float damage)
    {
        if (_ActualTime > 0)
            return;

        _life -= damage;
        Debug.Log(_life);
        _ActualTime = _ImortalityTime;
        if (_life > 0)
            return;
        _DeathPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
    public void Respawn()
    {
        _player.transform.position = CheckPointManager.instance.GetCheckpoint();
        Debug.Log("teleport");
        for (int i = 0; i < _enemysList.Count; i++)
        {
            _enemysList[i].SetActive(true);
            EnemyStatus _enemy = _enemysList[i].GetComponent<EnemyStatus>();
            _enemy.Respawn();
        }
        _life = _maxLife;
        _DeathPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Return(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Checkpoint"))
            return;
         
    }
}
