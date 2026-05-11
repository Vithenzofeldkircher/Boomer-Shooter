using UnityEngine;
using UnityEngine.Events;

public class GunCollect : Item
{
    [SerializeField] private GunElement _attributes;
    [SerializeField] private UnityEvent _eventToWeaponToDisapear;
    public override Element Collect()
    {
        Destroy(gameObject);
        _eventToWeaponToDisapear.Invoke();
        return _attributes;
    }

    protected override void Teste1()
    {
        
    }
}