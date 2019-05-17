using UnityEngine;
using MingUnity.InputModule;

public class Main : MonoBehaviour
{
    private void Start()
    {
        MInput.Instance.enable = true;

        MInput.OnTouchDown += MInput_OnTouchDown;

        MInput.OnSimpleTap += MInput_OnSimpleTap;
    }

    private void MInput_OnSimpleTap(MGesture gesture)
    {
        Debug.Log("<Ming> tap");
    }

    private void MInput_OnTouchDown(MGesture gesture)
    {
        Debug.Log("<Ming> touch down");
    }
}
