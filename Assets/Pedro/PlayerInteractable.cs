using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    private GunSystem _gunSystem;
    private IGunEquipped equipped;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gunSystem = GetComponentInParent<GunSystem>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.gameObject.TryGetComponent(out ICollectable collectable))
            return;

        switch (collider.gameObject.tag)
        {
            case "Gun":
                _gunSystem.AddNewGun((GunElement)collectable.Collect());
                break;
            case "Biblia":
                break;
            case "Armor":
                break;
            default:
                break;
        }
    }
}
