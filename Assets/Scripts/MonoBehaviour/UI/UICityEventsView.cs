using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICityEventsView : MonoBehaviour
{
    public delegate void CityEventAction(int index);
    public event CityEventAction OnCityEventSelect;

    [SerializeField]
    private Button ShowEventsButton;

    [SerializeField]
    private Transform ListContainer;

    [SerializeField]
    private UICityEventElementView listElementPrefab;

    [SerializeField]
    private float slideSpeed = 10;

    private PoolingService<UICityEventElementView> elementPool;
    private State displayState = State.Closed;
    private List<UICityEventElementView> poolElements;

    private RectTransform listRect;
    private float listWidth;
    private float currentListPosition;

    private void Awake()
    {
        ShowEventsButton.onClick.AddListener(ToggleList);
        elementPool = new PoolingService<UICityEventElementView>(listElementPrefab, 8, ListContainer, true);
        poolElements = new List<UICityEventElementView>();
        listRect = ListContainer.gameObject.GetComponent<RectTransform>();
        listWidth = listRect.rect.width;
        currentListPosition = listWidth;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !RectTransformUtility.RectangleContainsScreenPoint(listRect, Input.mousePosition, null))
            Close();
    }

    public void RedrawList(CityEventSettings[] events)
    {
        ClearList();
        var pos = 0;
        for(var i = 0; i < events.Length; i++)
        {
            var e = events[i];
            if (e != null)
            {
                var element = elementPool.GetFreeElement();
                element.PlaceInPosition(pos);
                var rate = (int)(e.SatisfactionBonusRate * 100);
                element.Init(e.Title, rate, e.Price);
                var actionIndex = i;
                element.SetAction(()=> {
                    OnCityEventSelect?.Invoke(actionIndex);
                });
                poolElements.Add(element);
                pos++;
            }
        }
    }

    private void ClearList()
    {
        if (poolElements != null)
        {
            foreach (var element in poolElements)
                element.ReturnToPool();
            poolElements.Clear();
        }
    }

    public void Close()
    {
        displayState = State.Closing;
        StartCoroutine(CloseCoroutine());
    }

    public void Open()
    {
        displayState = State.Opening;
        StartCoroutine(OpenCoroutine());
    }

    private IEnumerator OpenCoroutine()
    {
        while(currentListPosition > 0)
        {
            currentListPosition -= Time.deltaTime * slideSpeed;
            if (currentListPosition < 0)
                currentListPosition = 0;
            listRect.offsetMax = new Vector2(currentListPosition, listRect.offsetMax.y);
            listRect.offsetMin = new Vector2(currentListPosition, listRect.offsetMin.y);
            yield return null;
        }
        displayState = State.Opened;
    }

    private IEnumerator CloseCoroutine()
    {
        while (currentListPosition < listWidth)
        {
            currentListPosition += Time.deltaTime * slideSpeed;
            if (currentListPosition > listWidth)
                currentListPosition = listWidth;
            listRect.offsetMax = new Vector2(currentListPosition, listRect.offsetMax.y);
            listRect.offsetMin = new Vector2(currentListPosition, listRect.offsetMin.y);
            yield return null;
        }
        displayState = State.Closed;
    }

    private void ToggleList()
    {
        if (displayState == State.Closed)
        {
            Open();
        }
        else if (displayState == State.Opened)
        {
            Close();
        }
    }

    private enum State
    {
        Opening,
        Opened,
        Closing,
        Closed
    }
}