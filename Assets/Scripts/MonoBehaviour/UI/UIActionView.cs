using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIActionView : MonoBehaviour
{
    [SerializeField]
    private GameObject actionPanel;

    [SerializeField]
    private Image actionIcon;

    [SerializeField]
    private Button actionButton;

    [SerializeField]
    private TextMeshProUGUI actionTitle;

    [SerializeField]
    private GameObject pricePanel;

    [SerializeField]
    private TextMeshProUGUI priceField;

    public void SetActive(bool state)
    {
        actionPanel.SetActive(state);
    }

    public void SetIcon(Sprite icon)
    {
        actionIcon.sprite = icon;
    }

    public void SetAction(Action action)
    {
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(() => action());
    }

    public void SetTitle(string title)
    {
        actionTitle.text = title;
    }

    public void SetPrice(int value)
    {
        pricePanel.SetActive(value > 0);
        priceField.text = value.ToString();
    }

}
