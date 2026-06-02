using UnityEngine;

public class CeilingFan : MonoBehaviour
{
    [Header("Griešana")]
    public RotateObject bladesRotator;   // Ievelc blades ar RotateObject skriptu

    [Header("Gaismas (4 spuldzes)")]
    public Light[] fanLights;

    private bool isOn = false;

    void Start()
    {
        SetLights(false);
        if (bladesRotator != null) bladesRotator.End(); // Sākumā negriežas
    }

    public void TurnOn()
    {
        isOn = true;
        SetLights(true);
        if (bladesRotator != null) bladesRotator.Begin(); // Sāc griezties
    }

    public void TurnOff()
    {
        isOn = false;
        SetLights(false);
        if (bladesRotator != null) bladesRotator.End(); // Beidz griezties
    }

    public void Toggle()
    {
        if (isOn) TurnOff();
        else TurnOn();
    }

    void SetLights(bool state)
    {
        if (fanLights == null) return;
        foreach (Light l in fanLights)
        {
            if (l != null) l.enabled = state;
        }
    }
}