using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void Entrar()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void Sair()
    {
        Application.Quit();
    }
}
