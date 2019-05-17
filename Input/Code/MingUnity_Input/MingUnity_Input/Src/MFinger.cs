using UnityEngine;

namespace MingUnity.InputModule
{
    // Represents informations on Finger for touch
    // Internal use only, DO NOT USE IT
    public class MFinger : MFingerBase
    {
        public float startTimeAction;
        public Vector2 oldPosition;
        public int tapCount;                // Number of taps.
        public TouchPhase phase;            // Describes the phase of the touch.
        public MInput.GestureType gesture;
        public MInput.SwipeDirection oldSwipeType;
    }
}





