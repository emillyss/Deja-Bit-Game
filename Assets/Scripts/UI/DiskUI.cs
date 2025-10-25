using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DiskUI : MonoBehaviour
{
    [Header("References")]
    public Image fillImage;                
    public Image backgroundImage;         
    public TextMeshProUGUI percentText;    
    public TextMeshProUGUI numericText;   

    [Header("Animation")]
    public float lerpSpeed = 6f;           

 
    float currentFill = 0f;
    Coroutine animCoroutine;

    void Awake()
    {
        if (fillImage == null)
            Debug.LogWarning("DiskUI: fillImage não setado.");

        UpdateVisualInstant(0f);
    }


    public void SetValues(float used, float capacity, string unit = "MB")
    {
        float targetFill = (capacity <= 0f) ? 0f : Mathf.Clamp01(used / capacity);


        if (numericText != null)
        {
            numericText.text = $"Memória Disponível: {Mathf.RoundToInt(capacity) - Mathf.RoundToInt(used)} / {Mathf.RoundToInt(capacity)} {unit}";
        }

        if (percentText != null)
        {
            int pct = (capacity <= 0f) ? 0 : Mathf.RoundToInt(targetFill * 100f);
            percentText.text = pct + "%";
        }


        if (animCoroutine != null) StopCoroutine(animCoroutine);
        animCoroutine = StartCoroutine(AnimateFill(currentFill, targetFill));
    }

    IEnumerator AnimateFill(float from, float to)
    {
        float t = 0f;
        float duration = Mathf.Max(0.01f, Mathf.Abs(to - from) / (lerpSpeed * 0.5f)); // heurística
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / duration;
            currentFill = Mathf.Lerp(from, to, Mathf.SmoothStep(0f, 1f, t));
            ApplyFill(currentFill);
            yield return null;
        }
        currentFill = to;
        ApplyFill(currentFill);
        animCoroutine = null;
    }

    void ApplyFill(float f)
    {
        if (fillImage != null) fillImage.fillAmount = f;
    }

    public void UpdateVisualInstant(float fill)
    {
        currentFill = Mathf.Clamp01(fill);
        ApplyFill(currentFill);
        if (percentText != null) percentText.text = Mathf.RoundToInt(currentFill * 100f) + "%";
    }
}

