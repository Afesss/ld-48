using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlighIndicatorView : MonoBehaviour
{

    [Tooltip("MeshRenderer индикатора")]
    [SerializeField]
    private MeshRenderer meshRenderer;

    [Tooltip("Цвет при выделенном объекте")]
    [SerializeField]
    private Color selectedColor;

    [Tooltip("Цвет при наведении на объект")]
    [SerializeField]
    private Color hoveredColor;

    private Color defaultColor;

    private void Awake()
    {
        defaultColor = meshRenderer.material.color;
    }

    /// <summary>
    /// Устанавливает индикатор в состояние подсведки
    /// </summary>
    public void SetHover()
    {
        meshRenderer.material.color = hoveredColor;
    }

    /// <summary>
    /// Устанавливает индикатор в состояние выделения
    /// </summary>
    public void SetSelected()
    {
        meshRenderer.material.color = selectedColor;
    }

    /// <summary>
    /// Устанавливает индикатор в состояние по умолчанию
    /// </summary>
    public void SetDefault()
    {
        meshRenderer.material.color = defaultColor;
    }
}