using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private GunInventory _gunInventory;
    [SerializeField] private Transform _handGunModelParent;
    private Transform _camera;
    [SerializeField] private GunElement _handGun;
    private float _shootTimer;
    private bool _isReloading;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main.transform;
        _handGun.Initialize();
        _shootTimer = _handGun.ShootRate;
        _handGun.OnReload.AddListener(() => StartCoroutine(Reload()));
        _gunInventory.AddWeapon(_handGun);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Switch"))
        {
            ChangeWeapon();
        }

        if (Input.GetButtonDown("Reload"))
        {
            if (_handGun.Ammunation <= 0)
                return;

            _handGun.OnReload.Invoke();
        }

        _shootTimer += Time.deltaTime;
        if (_isReloading)
            return;
        if (_shootTimer < _handGun.ShootRate)
            return;
        //Verifica se o player atirou
        if (!Input.GetButtonDown("Fire1"))
            return;
        if (!_handGun.UseAmmunation())//Se não tiver munição, não é possível atirar
            return;
        //Verifica se o player acertou algo
        if (!Physics.Raycast(_camera.position, _camera.forward, out RaycastHit target))
            return;
        //Verifica se o objeto acertado implementa IShootable
        if (!target.collider.TryGetComponent(out IShootable shootable))
            return;

        //Aciona o método do contrato IShootable
        shootable.Hitted(_handGun.Damage, target.point);
        _shootTimer = 0;
    }

    private void ChangeWeapon()
    {
        if (_gunInventory.Guns.Count <= 1)
            print("Funciona ksksks");
            return;
        //ChangeGunVisual();
    }

    IEnumerator Reload()
    {
        _isReloading = true;
        //Trava até ser verdadeiro
        //yield return new WaitUntil(() => _handGun.Ammunation > 0);
        //Trava enquanto for verdadeiro
        //yield return new WaitWhile(() => _handGun.Ammunation <= 0);
        yield return new WaitForSeconds(_handGun.ReloadTime);
        _handGun.Reload();
        _shootTimer = _handGun.ShootRate;//Deixa a arma já pronta para atirar
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
        Destroy(_handGunModelParent.GetChild(0).gameObject);
        GameObject gun = Instantiate(_handGun.GunModel, _handGunModelParent);
        gun.layer = LayerMask.NameToLayer("Gun");
        gun.transform.localPosition = new Vector3(0, 0, -gun.transform.localScale.z);
    }
}