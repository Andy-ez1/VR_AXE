using UnityEngine;

public class DiscoLights : MonoBehaviour
{
    [Header("Gaismas ko mainīt")]
    public Light[] discoLights;

    [Header("Ātrums")]
    public float colorChangeSpeed = 2f;

    [Header("Krāsas")]
    public Color[] colors;

    private float timer = 0f;
    private bool discoActive = false;

    void Start()
    {
        if (colors == null || colors.Length == 0)
        {
            colors = new Color[]
            {
                Color.red, Color.magenta, Color.blue,
                Color.cyan, Color.green, Color.yellow
            };
        }

        SetLights(false);
    }

    void Update()
    {
        if (!discoActive) return;

        timer += Time.deltaTime * colorChangeSpeed;

        if (timer >= 1f)
        {
            timer = 0f;
            foreach (Light l in discoLights)
            {
                if (l != null)
                    l.color = colors[Random.Range(0, colors.Length)];
            }
        }
    }

    public void StartDisco()
    {
        discoActive = true;
        SetLights(true);
    }

    void SetLights(bool state)
    {
        if (discoLights == null) return;
        foreach (Light l in discoLights)
        {
            if (l != null) l.enabled = state;
        }
    }
}