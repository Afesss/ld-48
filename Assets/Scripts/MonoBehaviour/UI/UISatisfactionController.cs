using System;
using UnityEngine;
using Zenject;

public class UISatisfactionController : MonoBehaviour
{
    [SerializeField]
    private UISatisfactionView view;

    private Dissatisfied dissatisfied;

    [Inject]
    private void Constructor(Dissatisfied dissatisfied)
    {
        this.dissatisfied = dissatisfied;
    }

    private void Awake()
    {
        view.SetValue(0);
        if (dissatisfied != null)
            dissatisfied.OnSatisfactionChange += OnSatisfactionChange;
    }

    private void OnSatisfactionChange()
    {
        view.SetValue(dissatisfied.GetRate());
    }

    private void OnDestroy()
    {
        if (dissatisfied != null)
            dissatisfied.OnSatisfactionChange -= OnSatisfactionChange;
    }
}