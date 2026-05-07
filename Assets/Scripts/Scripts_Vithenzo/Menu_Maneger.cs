using UnityEngine;

public class Menu_Maneger : MonoBehaviour
{
    //Uma condição para chamar o panel com as opçoes
    [Header("Configuração")]
    public GameObject Panel_Menu;
    public bool Menu_Button_Press = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            Menu_Button_Press = !Menu_Button_Press;
            print("botão acionado"); // testar se ele ta reconhecendo o botão

            if(Panel_Menu != null)
            {
                Panel_Menu.SetActive(Menu_Button_Press);
            }

            if (Menu_Button_Press)
            {
                Cursor.lockState = CursorLockMode.None; // destrava o cursos
                Cursor.visible = true;
                Time.timeScale = 0;
            }

            else
            {
                Cursor.lockState = CursorLockMode.Locked; // trava o cursos
                Cursor.visible = false;
                Time.timeScale = 1;
            }
        }
    }
}
