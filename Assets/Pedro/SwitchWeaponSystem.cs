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
        if (Input.GetButtonDown("Switch") && _meele._isAnimation == false)
        {
            toMeele.Invoke();
        }
        else if (Input.GetButtonDown("Biblia") && _meele._isAnimation == false && _biblia.collected == true)
        {
            toBiblia.Invoke();
        }
    }
}
