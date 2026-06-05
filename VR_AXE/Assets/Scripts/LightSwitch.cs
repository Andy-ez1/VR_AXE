using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LightSwitch : MonoBehaviour
{
    [Header("Slēdža svira")]
    public Transform leverSwitch;
    public Vector3 onRotation = new Vector3(-30, 0, 0);
    public Vector3 offRotation = new Vector3(30, 0, 0);

    [Header("Ventilators")]
    public CeilingFan ceilingFan;

    [Header("Taimeris")]
    public EscapeTimer gameTimer;

    [Header("Villain Audio")]
    public AudioSource villainAudio;   // Villain balss
    private bool villainSpoke = false; // Vai jau runāja

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
        if (leverSwitch != null)
            leverSwitch.localEulerAngles = offRotation;
    }

    void OnSwitch(SelectEnterEventArgs args)
    {
        isOn = !isOn;

        if (leverSwitch != null)
            leverSwitch.localEulerAngles = isOn ? onRotation : offRotation;

        if (ceilingFan != null)
        {
            if (isOn) ceilingFan.TurnOn();
            else ceilingFan.TurnOff();
        }

        if (gameTimer != null)
            gameTimer.SetLightsOn(isOn);

        // Villain runā TIKAI pirmo reizi kad ieslēdz gaismu
        if (isOn && !villainSpoke && villainAudio != null)
        {
            villainAudio.Play();
            villainSpoke = true;
        }

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