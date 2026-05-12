using UnityEngine;
using UnityEngine.Events;

public class GunCollect : Item
{
    [SerializeField] private GunElement _attributes;
    [SerializeField] private UnityEvent _eventToWeaponToDisapear;
    MeeleWeapon meele;

    public override Element Collect()
    {
        _eventToWeaponToDisapear.Invoke();
        Destroy(gameObject);
        return _attributes;
    }
    protected override void Teste1()
    {
        
    }
}