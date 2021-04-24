using UnityEngine;
using UnityEngine.UI;

public class UIStorageView : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public void SetValueRate(float rate)
    {
        slider.value = rate;
    }
}