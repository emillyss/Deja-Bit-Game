using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class PauseScript : MonoBehaviour
{
    public GameObject telaPause;
    public static bool pause;
    public AudioMixer audioMixer;

    void Start()
    {
        ResumeGame();
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (pause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        telaPause.SetActive(true);
        Time.timeScale = 0f;
        pause = true;
    }

    public void ResumeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        telaPause.SetActive(false);
        Time.timeScale = 1f;
        pause = false;
    }
    public void Retornar()
    {
        ResumeGame();
    }

    public void AtualizarVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void TelaInicial()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TelaInicial");
    }

    public void ReiniciarFase()
    {
        SceneManager.LoadScene("MainGame");
    }
}
