using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ObjectSelectionController : MonoBehaviour
{
    public event Action OnObjectSelect;

    [SerializeField]
    private HighlighIndicatorView view;

    /// <summary>
    /// Состояние наведения
    /// </summary>
    public bool IsHovered { get; private set; }

    /// <summary>
    /// Состояние выделения
    /// </summary>
    public bool IsSelected { get; private set; }

    private bool isLocked = false;

    private SignalBus signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isLocked && IsSelected && !EventSystem.current.IsPointerOverGameObject())
            SetDeseleced();

        if (isLocked)
            isLocked = false;
    }

    /// <summary>
    /// Подсвечивает площадку
    /// </summary>
    public void SetHovered()
    {
        if (!IsHovered)
        {
            IsHovered = true;
            view.SetHover();
        }
    }

    /// <summary>
    /// Выделяет площадку
    /// </summary>
    public void SetSelected()
    {
        if (!IsSelected)
        {
            IsSelected = true;
            view.SetSelected();
            OnObjectSelect?.Invoke();
        }
        isLocked = true;
    }

    /// <summary>
    /// Снимает все выделения
    /// </summary>
    public void SetDefault()
    {
        IsHovered = false;
        IsSelected = false;
        view.SetDefault();
    }

    public void SetDeseleced()
    {
        signalBus.Fire(new DeselectBuildObjectSignal());
        SetDefault();
    }

}