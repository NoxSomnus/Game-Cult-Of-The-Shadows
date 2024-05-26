using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuryBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxFury(float fury)
    {
        slider.maxValue = fury;
        slider.value = fury;
    }

    public void SetFury(float fury)
    {
        slider.value = fury;
    }
}
