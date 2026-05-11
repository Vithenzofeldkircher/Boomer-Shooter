using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Cinemachine;

[System.Serializable]
public class GunInventory
{
    [SerializeField] private List<GunElement> _guns;


    public List<GunElement> Guns { get => _guns; }

    //Arrays [] possuem tamanho fixo
    //Arrays são usados em inventários visuais
    //armazenamento de referências fixas

    //Listas <> possuem tamanho dinâmico
    //Listas são boas para controle de inimigos

    public void AddWeapon(GunElement newGun)
    {
        Guns.Add(newGun);
    }
}

public class GunSystem : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private GunInventory _gunInventory;

    [Header("Weapon")]
    [SerializeField] private Transform _handGunModelParent;
    [SerializeField] private GunElement _handGun;

    [Header("Events")]
    [SerializeField] private UnityEvent _disactivateOtherWeapon;

    [Header("Scope")]
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    private float _normalFov;
    [SerializeField] private float _scopeFov = 10f;
    [SerializeField] private float _scopeSpeed = 10f;

    private Transform _camera;

    private float _shootTimer;

    private bool _isReloading;

    void Start()
    {
        _camera = Camera.main.transform;

        _normalFov = _virtualCamera.m_Lens.FieldOfView;

        _handGun.Initialize();

        _shootTimer = _handGun.ShootRate;

        _handGun.OnReload.AddListener(() => StartCoroutine(Reload()));
    }

    void Update()
    {
        float currentGunIndex = Input.GetAxis("Mouse ScrollWheel");

        if (currentGunIndex != 0)
        {
            _disactivateOtherWeapon.Invoke();

            ChangeWeapon(currentGunIndex);
        }

        if (Input.GetButtonDown("Reload"))
        {
            if (_handGun.Ammunation <= 0)
                return;

            _handGun.OnReload.Invoke();
        }

        if (_handGun.HasScope == true)
        {
            if (_virtualCamera == null)
                return;

            bool isScoping = Input.GetMouseButton(1);

            float targetFov = isScoping ? _scopeFov : _normalFov;

            var lens = _virtualCamera.m_Lens;

            lens.FieldOfView = Mathf.Lerp(
                lens.FieldOfView,
                targetFov,
                _scopeSpeed * Time.deltaTime
            );

            _virtualCamera.m_Lens = lens;
        }

        _shootTimer += Time.deltaTime;

        if (_isReloading)
            return;

        if (_shootTimer < _handGun.ShootRate)
            return;

        if (!Input.GetButtonDown("Fire1"))
            return;
        Debug.Log("Atirando");

        if (!_handGun.UseAmmunation())
            return;

        if (!Physics.Raycast(_camera.position, _camera.forward, out RaycastHit target))
            return;

        if (!target.collider.TryGetComponent(out IShootable shootable))
            return;

        shootable.Hitted(_handGun.Damage);

        _shootTimer = 0;
    }
    private void ChangeWeapon(float nextIndex)
    {
        if (_gunInventory.Guns.Count < 1)
            return;

        int currentIndex = _gunInventory.Guns.IndexOf(_handGun);

        currentIndex += (int)Mathf.Sign(nextIndex);

        if (currentIndex == _gunInventory.Guns.Count)
        {
            currentIndex = 0;
        }
        else if (currentIndex < 0)
        {
            currentIndex = _gunInventory.Guns.Count - 1;
        }

        _handGun = _gunInventory.Guns[currentIndex];

        ChangeGunVisual();
    }

    IEnumerator Reload()
    {
        _isReloading = true;

        yield return new WaitForSeconds(_handGun.ReloadTime);

        _handGun.Reload();

        _shootTimer = _handGun.ShootRate;

        _isReloading = false;
    }

    public void AddNewGun(GunElement newGun)
    {
        _handGun = newGun;

        _handGun.Initialize();

        _shootTimer = _handGun.ShootRate;

        _handGun.OnReload.AddListener(() => StartCoroutine(Reload()));

        _gunInventory.AddWeapon(newGun);

        ChangeGunVisual();
    }

    public void ChangeGunVisual()
    {
        Destroy(_handGunModelParent.GetChild(1).gameObject);

        GameObject gun = Instantiate(_handGun.GunModel, _handGunModelParent);

        gun.layer = LayerMask.NameToLayer("Gun");

        gun.transform.localPosition =
            new Vector3(0, 0, -gun.transform.localScale.z);
    }
}