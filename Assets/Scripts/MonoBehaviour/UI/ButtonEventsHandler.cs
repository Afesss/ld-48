using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Компонент смены курсора при наведении на кнопку
/// </summary>
public class ButtonEventsHandler : MonoBehaviour
{
    /// <summary>
    /// Курсор для состояния наведение на интерактивный объект
    /// </summary>
    [SerializeField]
    private Texture2D onOverCursor = default;

    private void Awake()
    {
        // находим все кнопки в окне и добавляем необходимые события
        Button[] buttons = gameObject.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();
            AddDeligate(ref trigger, EventTriggerType.PointerEnter, () => { OnMouseOver(); });
            AddDeligate(ref trigger, EventTriggerType.PointerExit, () => { OnMouseExit(); });
        }
    }

    /// <summary>
    /// Смена курсора при наведении мыши
    /// </summary>
    /// <param name="data">Данные события</param>
    public void OnMouseOver()
    {
        Cursor.SetCursor(onOverCursor, Vector2.zero, CursorMode.Auto);
    }

    /// <summary>
    /// Смена курсора при выходе мыши
    /// </summary>
    /// <param name="data">Данные события</param>
    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    /// <summary>
    /// Добавляет обработчик на указанный тип события
    /// </summary>
    /// <param name="trigger">Триггер события</param>
    /// <param name="eventType">Тип события</param>
    /// <param name="action">Обработчик события</param>
    private void AddDeligate(ref EventTrigger trigger, EventTriggerType eventType, UnityAction action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback.AddListener((data) => { action(); });
        trigger.triggers.Add(entry);
    }

}