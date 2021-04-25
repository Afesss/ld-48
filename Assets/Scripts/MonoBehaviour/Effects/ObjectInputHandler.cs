using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectInputHandler : MonoBehaviour
{
    [SerializeField]
    private ObjectSelectionController controller;

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            controller.SetHovered();
        }
        else
        {
            //controller.SetDefault();
        }
    }

    private void OnMouseExit()
    {
        if (controller.IsHovered && !controller.IsSelected)
            controller.SetDefault();
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            StartCoroutine(DoDelect());
        }
    }

    private IEnumerator DoDelect()
    {
        yield return new WaitForEndOfFrame();
        controller.SetSelected();
    }
}