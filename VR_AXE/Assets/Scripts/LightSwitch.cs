using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LightSwitch : MonoBehaviour
{
    [Header("Slēdža svira (animācija)")]
    public Transform leverSwitch;          // Kustīgā svira
    public Vector3 onRotation = new Vector3(180, 0, 0);   // Pozīcija ieslēgts
    public Vector3 offRotation = new Vector3(0, 0, 0);   // Pozīcija izslēgts

    [Header("Ventilators")]
    public CeilingFan ceilingFan;

    [Header("Taimeris")]
    public EscapeTimer gameTimer;

    [Header("Haptika")]
    public float hapticAmplitude = 0.6f;
    public float hapticDuration = 0.1f;

    private bool isOn = false;
    private XRSimpleInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnSwitch);
    }

    void Start()
    {
        // Sākuma pozīcija - izslēgts
        if (leverSwitch != null)
            leverSwitch.localEulerAngles = offRotation;
    }

    void OnSwitch(SelectEnterEventArgs args)
    {
        isOn = !isOn;

        // Animē sviru
        if (leverSwitch != null)
            leverSwitch.localEulerAngles = isOn ? onRotation : offRotation;

        // Ventilators + gaisma
        if (ceilingFan != null)
        {
            if (isOn) ceilingFan.TurnOn();
            else ceilingFan.TurnOff();
        }

        // Taimera ātrums
        if (gameTimer != null)
            gameTimer.SetLightsOn(isOn);

        // Haptika
        SendHaptics(args.interactorObject);
    }

    void SendHaptics(UnityEngine.XR.Interaction.Toolkit.Interactors.IXRInteractor interactor)
    {
        if (interactor is UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor controllerInteractor)
            controllerInteractor.SendHapticImpulse(hapticAmplitude, hapticDuration);
    }

    void OnDestroy()
    {
        interactable.selectEntered.RemoveListener(OnSwitch);
    }
}