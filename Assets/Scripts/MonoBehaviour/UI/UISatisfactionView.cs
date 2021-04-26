using UnityEngine;
using UnityEngine.UI;

public class UISatisfactionView : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private Sprite[] moodsIcon;

    private float step;
    private int currentIndex;

    private void Awake()
    {
        step = 1 / (float) moodsIcon.Length;
        currentIndex = 0;
    }

    public void SetValue(float value)
    {
        slider.value = value;
        var index = Mathf.FloorToInt(value/step);
        if (index != currentIndex && index < moodsIcon.Length)
        {
            currentIndex = index;
            iconImage.sprite = moodsIcon[currentIndex];
        }
    }
}