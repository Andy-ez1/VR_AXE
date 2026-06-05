using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TestHover : MonoBehaviour
{
    void Awake()
    {
        var interactable = GetComponent<XRSimpleInteractable>();
        interactable.hoverEntered.AddListener((args) => Debug.Log("KUBS HOVER STRĀDĀ!"));
        interactable.selectEntered.AddListener((args) => Debug.Log("KUBS SELECT STRĀDĀ!"));
    }
}