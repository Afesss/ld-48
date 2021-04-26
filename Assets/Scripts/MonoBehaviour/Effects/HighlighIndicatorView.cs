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

    [Tooltip("Курсор для состояния наведение на интерактивный объект")]
    [SerializeField]
    private Texture2D onOverCursor = default;


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
        if (onOverCursor != null)
            Cursor.SetCursor(onOverCursor, Vector2.zero, CursorMode.Auto);
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
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}