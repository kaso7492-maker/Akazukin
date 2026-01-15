using UnityEngine;

public class TimeResetOnTitle : MonoBehaviour
{
    void Start()
    {
        TimeManagerScript.sharedRemainingTime = -1f;
    }
}
