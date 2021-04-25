using UnityEngine;
using UnityEngine.UI;

public class UIEcologyView : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public void SetValue(float value)
    {
        slider.value = value;
    }
}