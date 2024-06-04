using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxFury(float fury)
    {
        slider.maxValue = 100;
        slider.value = fury;
    }

    public void SetFury(float fury)
    {
        slider.value = fury;
    }
}
