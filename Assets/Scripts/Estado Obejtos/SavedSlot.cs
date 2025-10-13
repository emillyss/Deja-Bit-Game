using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SavedSlot : MonoBehaviour
{
    public Image iconImage;     
    public TextMeshProUGUI labelText;     
    public Button restoreButton;
    public Button deleteButton;

    // Setup chamado logo após instanciar o prefab
    public void Setup(string label, Action onRestore, Sprite icon = null, Action onDelete = null)
    {
        if (labelText != null) labelText.text = label;

        if (iconImage != null)
        {
            if (icon != null)
            {
                iconImage.sprite = icon;
                iconImage.enabled = true;
            }
            else
            {
                iconImage.enabled = false; // esconde se não tiver
            }
        }

        if (restoreButton != null)
        {
            restoreButton.onClick.RemoveAllListeners();
            restoreButton.onClick.AddListener(() => onRestore?.Invoke());
        }

        // configura o botão deletar (se existir)
        if (deleteButton != null)
        {
            deleteButton.onClick.RemoveAllListeners();
            deleteButton.onClick.AddListener(() => onDelete?.Invoke());
        }
    }
}

