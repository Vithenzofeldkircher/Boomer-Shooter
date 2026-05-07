using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene_Manager : MonoBehaviour
{
    [Header("Configuração")]
    public GameObject Painel_Menu;
    public bool Button_Press = false;

    public void Continue()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void Back()
    {
        SceneManager.LoadScene("Start_Scene");
    }

    public void Sair()
    {
        print("saida com sucesso");
        Application.Quit();
    }

    public void Menu_Back()
    {
        Button_Press = !Button_Press;

        if (Painel_Menu != null)
        {
            Painel_Menu.SetActive(Button_Press);
        }
    }
}
