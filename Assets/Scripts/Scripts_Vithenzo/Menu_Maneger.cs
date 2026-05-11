using UnityEngine;

public class Menu_Maneger : MonoBehaviour
{
    [Header("Configuração")]
    public GameObject Panel_Menu; // Arraste seu painel aqui no Inspector
    private bool menuAberto = false;

    void Start()
    {
        // Garante que o menu comece fechado e o mouse preso no início do jogo
        if (Panel_Menu != null) Panel_Menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Verifica se apertou o botão configurado (Esc)
        if (Input.GetButtonDown("Menu"))
        {
            AlternarMenu();
        }
    }

    public void AlternarMenu()
    {
        menuAberto = !menuAberto; // O "!" inverte o valor (Toggle)

        if (Panel_Menu != null)
        {
            Panel_Menu.SetActive(menuAberto);
        }

        if (menuAberto)
        {
            // Abriu o Menu: Para o tempo e libera o mouse
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // Fechou o Menu: Volta o tempo e esconde o mouse
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void SairDoJogo()
    {
        Debug.Log("Saindo");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Fecha no Editor
#endif
    }
}