using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    [SerializeField] string nomeCena = "MainGame2D";
    [SerializeField] GameObject telaPause;
    [SerializeField] AudioMixer audioMixer;
    public void Entrar()
    {
        SceneManager.LoadScene(nomeCena);
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void Configuracao()
    {
        telaPause.SetActive(true);
    }

    public void Retornar()
    {
        telaPause.SetActive(false);
    }

    public void AtualizarVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
