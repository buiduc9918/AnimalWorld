using UnityEngine;
using UnityEngine.UI;

public class SliderScripts : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.minValue = 0;
        slider.maxValue = 100;
        slider.value = 41;
    }
    public void Point()
    {
        slider.value++;
        if (slider.value == slider.maxValue)
        {
            slider.value = 0;
        }
    }
}
