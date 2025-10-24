using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup aduioMixerGroup;
    public static AudioManager instance;

    public AudioClip Bau, Alavanca, BotaoPressao, BotaoNormal, Porta, InimigoMorte, CaixaCaindo, CaixaDeTexto;

    private AudioSource audioSource;

    public void Awake()
    {
        instance = this;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = aduioMixerGroup;
        }
    }

    public void PlayBau()
    {
        audioSource.PlayOneShot(Bau);
    }

    public void PlayAlavanca()
    {
        audioSource.PlayOneShot(Alavanca);
    }

    public void PlayBotaoPressao()
    {
        audioSource.PlayOneShot(BotaoPressao);
    }

    public void PlayBotaoNormal()
    {
        audioSource.PlayOneShot(BotaoNormal);
    }

    public void PlayPorta()
    {
        audioSource.PlayOneShot(Porta);
    }

    public void PlayInimigoMorte()
    {
        audioSource.PlayOneShot(InimigoMorte);
    }

    public void PlayCaixaCaindo()
    {
        audioSource.PlayOneShot(CaixaCaindo);
    }

    public void PlayCaixaDeTexto()
    {
        audioSource.PlayOneShot(CaixaDeTexto);
    }
}
