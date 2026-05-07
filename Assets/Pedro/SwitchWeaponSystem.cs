using UnityEngine;
using UnityEngine.Events;

public class SwitchWeaponSystem : MonoBehaviour
{
    [SerializeField] private UnityEvent toMeele;
    [SerializeField] private UnityEvent toBiblia;
    [SerializeField] private MeeleWeapon _meele;
    private Biblia _biblia;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_meele._isAnimation == true)
            return;
        if (Input.GetButtonDown("Switch"))
        {
            toMeele.Invoke();
        }
        if (Input.GetButtonDown("Biblia") && _biblia.collected == true)
        {
            toBiblia.Invoke();
        }
    }
}
