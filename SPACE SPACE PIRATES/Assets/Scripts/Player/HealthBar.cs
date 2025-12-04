using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fill;  

    public void SetHealth(float current, float max)
    {
        if (fill == null) return;
        fill.fillAmount = current / max;
    }
}
