using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICityEventElementView : MonoBehaviour, IPoolObject
{
    [Tooltip("Заголовок")]
    [SerializeField]
    private TextMeshProUGUI titleField;

    [Tooltip("Удовлетворение")]
    [SerializeField]
    private TextMeshProUGUI satisfactionField;

    [Tooltip("Цена")]
    [SerializeField]
    private TextMeshProUGUI priceField;

    [Tooltip("Кнопка")]
    [SerializeField]
    private Button button;

    [Tooltip("Высота элемента меню")]
    [SerializeField]
    private float elementHeight = 40;

    [Tooltip("Отступ между элемента меню по высоте")]
    [SerializeField]
    private float elementHeightOffset = 5;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void Init(string title, int satisfaction, int price)
    {
        titleField.text = title;
        satisfactionField.text = $"+ {satisfaction.ToString()}%";
        priceField.text = price.ToString();
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    public void SetAction(Action action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(()=>action());
    }

    public void PlaceInPosition(int index)
    {
        _rectTransform.offsetMax = new Vector2(_rectTransform.offsetMax.x, -((elementHeight + elementHeightOffset) * index));
        _rectTransform.offsetMin = new Vector2(0, -(elementHeight * (index + 1) + elementHeightOffset * index));
    }
}