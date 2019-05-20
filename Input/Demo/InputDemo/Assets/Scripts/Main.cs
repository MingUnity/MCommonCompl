using UnityEngine;
using MingUnity.InputModule;

public class Main : MonoBehaviour
{
    public RectTransform image;

    private void Start()
    {
        MInput.Instance.enable = true;

        MInput.Instance.allowUIDetection = false;

        MInput.OnSwipeStart += MInput_OnSwipeStart;

        MInput.OnTouchDown += MInput_OnTouchDown;

        MInput.OnSimpleTap += MInput_OnSimpleTap;
    }

    private void MInput_OnSwipeStart(MGesture gesture)
    {
        Debug.LogFormat("<Ming> start swipe; on image:{0}", gesture.IsOverRectTransform(image));
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
