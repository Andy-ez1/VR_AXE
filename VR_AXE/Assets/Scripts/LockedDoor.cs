using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class LockedDoor : MonoBehaviour
{
    [Header("Durvju HingeJoint")]
    public HingeJoint doorHinge;
    public float unlockedMax = 110f;

    [Header("Socket (lai atslēga paliek)")]
    public XRSocketInteractor keySocket;

    public void UnlockDoor()
    {
        if (doorHinge != null)
        {
            JointLimits limits = doorHinge.limits;
            limits.min = 0;
            limits.max = unlockedMax;
            doorHinge.limits = limits;
        }

        // Iesaldē atslēgu socketā - nevar izņemt
        if (keySocket != null)
            keySocket.socketActive = false;

        Debug.Log("Durvis atbloķētas, atslēga fiksēta!");
    }
}