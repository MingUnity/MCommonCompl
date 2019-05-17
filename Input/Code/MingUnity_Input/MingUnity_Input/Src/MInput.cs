using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace MingUnity.InputModule
{
    /// <summary>
    /// 核心输入组件
    /// </summary>
    public class MInput : MonoBehaviour
    {
        #region private classes
        [System.Serializable]
        private class DoubleTap
        {
            public bool inDoubleTap = false;
            public bool inWait = false;
            public float time = 0;
            public int count = 0;
            public MFinger finger;

            public void Stop()
            {
                inDoubleTap = false;
                inWait = false;
                time = 0;
                count = 0;
            }
        }

        private class PickedObject
        {
            public GameObject pickedObj;
            public Camera pickedCamera;
            public bool isGUI;
        }
        #endregion

        #region Delegate
        public delegate void TouchCancelHandler(MGesture gesture);
        public delegate void Cancel2FingersHandler(MGesture gesture);
        public delegate void TouchStartHandler(MGesture gesture);
        public delegate void TouchDownHandler(MGesture gesture);
        public delegate void TouchUpHandler(MGesture gesture);
        public delegate void SimpleTapHandler(MGesture gesture);
        public delegate void DoubleTapHandler(MGesture gesture);
        public delegate void LongTapStartHandler(MGesture gesture);
        public delegate void LongTapHandler(MGesture gesture);
        public delegate void LongTapEndHandler(MGesture gesture);
        public delegate void DragStartHandler(MGesture gesture);
        public delegate void DragHandler(MGesture gesture);
        public delegate void DragEndHandler(MGesture gesture);
        public delegate void SwipeStartHandler(MGesture gesture);
        public delegate void SwipeHandler(MGesture gesture);
        public delegate void SwipeEndHandler(MGesture gesture);
        public delegate void TouchStart2FingersHandler(MGesture gesture);
        public delegate void TouchDown2FingersHandler(MGesture gesture);
        public delegate void TouchUp2FingersHandler(MGesture gesture);
        public delegate void SimpleTap2FingersHandler(MGesture gesture);
        public delegate void DoubleTap2FingersHandler(MGesture gesture);
        public delegate void LongTapStart2FingersHandler(MGesture gesture);
        public delegate void LongTap2FingersHandler(MGesture gesture);
        public delegate void LongTapEnd2FingersHandler(MGesture gesture);
        public delegate void TwistHandler(MGesture gesture);
        public delegate void TwistEndHandler(MGesture gesture);
        public delegate void PinchInHandler(MGesture gesture);
        public delegate void PinchOutHandler(MGesture gesture);
        public delegate void PinchEndHandler(MGesture gesture);
        public delegate void PinchHandler(MGesture gesture);
        public delegate void DragStart2FingersHandler(MGesture gesture);
        public delegate void Drag2FingersHandler(MGesture gesture);
        public delegate void DragEnd2FingersHandler(MGesture gesture);
        public delegate void SwipeStart2FingersHandler(MGesture gesture);
        public delegate void Swipe2FingersHandler(MGesture gesture);
        public delegate void SwipeEnd2FingersHandler(MGesture gesture);
        public delegate void MInuptIsReadyHandler();

        public delegate void OverUIElementHandler(MGesture gesture);
        public delegate void UIElementTouchUpHandler(MGesture gesture);
        #endregion

        #region Events
        /// <summary>
        /// Occurs when The system cancelled tracking for the touch, as when (for example) the user puts the device to her face.
        /// </summary>
        public static event TouchCancelHandler OnCancel;
        /// <summary>
        /// Occurs when the touch count is no longer egal to 2 and different to 0, after the begining of a two fingers gesture.
        /// </summary>
        public static event Cancel2FingersHandler OnCancel2Fingers;
        /// <summary>
        /// Occurs when a finger touched the screen.
        /// </summary>
        public static event TouchStartHandler OnTouchStart;
        /// <summary>
        /// Occurs as the touch is active.
        /// </summary>
        public static event TouchDownHandler OnTouchDown;
        /// <summary>
        /// Occurs when a finger was lifted from the screen.
        /// </summary>
        public static event TouchUpHandler OnTouchUp;
        /// <summary>
        /// Occurs when a finger was lifted from the screen, and the time elapsed since the beginning of the touch is less than the time required for the detection of a long tap.
        /// </summary>
        public static event SimpleTapHandler OnSimpleTap;
        /// <summary>
        /// Occurs when the number of taps is egal to 2 in a short time.
        /// </summary>
        public static event DoubleTapHandler OnDoubleTap;
        /// <summary>
        /// Occurs when a finger is touching the screen,  but hasn't moved  since the time required for the detection of a long tap.
        /// </summary>
        public static event LongTapStartHandler OnLongTapStart;
        /// <summary>
        /// Occurs as the touch is active after a LongTapStart
        /// </summary>
        public static event LongTapHandler OnLongTap;
        /// <summary>
        /// Occurs when a finger was lifted from the screen, and the time elapsed since the beginning of the touch is more than the time required for the detection of a long tap.
        /// </summary>
        public static event LongTapEndHandler OnLongTapEnd;
        /// <summary>
        /// Occurs when a drag start. A drag is a swipe on a pickable object
        /// </summary>
        public static event DragStartHandler OnDragStart;
        /// <summary>
        /// Occurs as the drag is active.
        /// </summary>
        public static event DragHandler OnDrag;
        /// <summary>
        /// Occurs when a finger that raise the drag event , is lifted from the screen.
        /// </summary>/
        public static event DragEndHandler OnDragEnd;
        /// <summary>
        /// Occurs when swipe start.
        /// </summary>
        public static event SwipeStartHandler OnSwipeStart;
        /// <summary>
        /// Occurs as the  swipe is active.
        /// </summary>
        public static event SwipeHandler OnSwipe;
        /// <summary>
        /// Occurs when a finger that raise the swipe event , is lifted from the screen.
        /// </summary>
        public static event SwipeEndHandler OnSwipeEnd;
        /// <summary>
        /// Like OnTouchStart but for a 2 fingers gesture.
        /// </summary>
        public static event TouchStart2FingersHandler OnTouchStart2Fingers;
        /// <summary>
        /// Like OnTouchDown but for a 2 fingers gesture.
        /// </summary>
        public static event TouchDown2FingersHandler OnTouchDown2Fingers;
        /// <summary>
        /// Like OnTouchUp but for a 2 fingers gesture.
        /// </summary>
        public static event TouchUp2FingersHandler OnTouchUp2Fingers;
        /// <summary>
        /// Like OnSimpleTap but for a 2 fingers gesture.
        /// </summary>
        public static event SimpleTap2FingersHandler OnSimpleTap2Fingers;
        /// <summary>
        /// Like OnDoubleTap but for a 2 fingers gesture.
        /// </summary>
        public static event DoubleTap2FingersHandler OnDoubleTap2Fingers;
        /// <summary>
        /// Like OnLongTapStart but for a 2 fingers gesture.
        /// </summary>
        public static event LongTapStart2FingersHandler OnLongTapStart2Fingers;
        /// <summary>
        /// Like OnLongTap but for a 2 fingers gesture.
        /// </summary>
        public static event LongTap2FingersHandler OnLongTap2Fingers;
        /// <summary>
        /// Like OnLongTapEnd but for a 2 fingers gesture.
        /// </summary>
        public static event LongTapEnd2FingersHandler OnLongTapEnd2Fingers;
        /// <summary>
        /// Occurs when a twist gesture start
        /// </summary>
        public static event TwistHandler OnTwist;
        /// <summary>
        /// Occurs as the twist gesture is active.
        /// </summary>
        public static event TwistEndHandler OnTwistEnd;
        /// <summary>
        /// Occurs as the pinch  gesture is active.
        /// </summary>
        public static event PinchHandler OnPinch;
        /// <summary>
        /// Occurs as the pinch in gesture is active.
        /// </summary>
        public static event PinchInHandler OnPinchIn;
        /// <summary>
        /// Occurs as the pinch out gesture is active.
        /// </summary>
        public static event PinchOutHandler OnPinchOut;
        /// <summary>
        /// Occurs when the 2 fingers that raise the pinch event , are lifted from the screen.
        /// </summary>
        public static event PinchEndHandler OnPinchEnd;
        /// <summary>
        /// Like OnDragStart but for a 2 fingers gesture.
        /// </summary>
        public static event DragStart2FingersHandler OnDragStart2Fingers;
        /// <summary>
        /// Like OnDrag but for a 2 fingers gesture.
        /// </summary>
        public static event Drag2FingersHandler OnDrag2Fingers;
        /// <summary>
        /// Like OnDragEnd2Fingers but for a 2 fingers gesture.
        /// </summary>
        public static event DragEnd2FingersHandler OnDragEnd2Fingers;
        /// <summary>
        /// Like OnSwipeStart but for a 2 fingers gesture.
        /// </summary>
        public static event SwipeStart2FingersHandler OnSwipeStart2Fingers;
        /// <summary>
        /// Like OnSwipe but for a 2 fingers gesture.
        /// </summary>
        public static event Swipe2FingersHandler OnSwipe2Fingers;
        /// <summary>
        /// Like OnSwipeEnd but for a 2 fingers gesture.
        /// </summary>
        public static event SwipeEnd2FingersHandler OnSwipeEnd2Fingers;
        /// <summary>
        /// Occurs when  input is ready.
        /// </summary>
        public static event MInuptIsReadyHandler OnMInputIsReady;
        /// <summary>
        /// Occurs when current touch is over user interface element.
        /// </summary>
        public static event OverUIElementHandler OnOverUIElement;

        public static event UIElementTouchUpHandler OnUIElementTouchUp;
        #endregion

        #region Enumerations

        public enum GesturePriority { Tap, Slips };

        public enum DoubleTapDetection { BySystem, ByTime }

        public enum GestureType { Tap, Drag, Swipe, None, LongTap, Pinch, Twist, Cancel, Acquisition };
        /// <summary>
        /// Represents the different directions for a swipe or drag gesture (Left, Right, Up, Down, Other)
        /// 
        /// The direction is influenced by the swipe Tolerance parameter Look at SetSwipeTolerance( float tolerance)
        /// <br><br>
        /// This enumeration is used on Gesture class
        /// </summary>
        public enum SwipeDirection { None, Left, Right, Up, Down, UpLeft, UpRight, DownLeft, DownRight, Other, All };

        public enum TwoFingerPickMethod { Finger, Average };

        public enum EvtType
        {
            None, OnTouchStart, OnTouchDown, OnTouchUp, OnSimpleTap, OnDoubleTap, OnLongTapStart, OnLongTap,
            OnLongTapEnd, OnDragStart, OnDrag, OnDragEnd, OnSwipeStart, OnSwipe, OnSwipeEnd, OnTouchStart2Fingers, OnTouchDown2Fingers, OnTouchUp2Fingers, OnSimpleTap2Fingers,
            OnDoubleTap2Fingers, OnLongTapStart2Fingers, OnLongTap2Fingers, OnLongTapEnd2Fingers, OnTwist, OnTwistEnd, OnPinch, OnPinchIn, OnPinchOut, OnPinchEnd, OnDragStart2Fingers,
            OnDrag2Fingers, OnDragEnd2Fingers, OnSwipeStart2Fingers, OnSwipe2Fingers, OnSwipeEnd2Fingers, OnMInputIsReady, OnCancel, OnCancel2Fingers, OnOverUIElement, OnUIElementTouchUp
        }

        #endregion

        #region Public members
        private static MInput _instance;
        public static MInput Instance
        {
            get
            {
                if (!_instance)
                {

                    // check if an ObjectPoolManager is already available in the scene graph
                    _instance = FindObjectOfType(typeof(MInput)) as MInput;

                    // nope, create a new one
                    if (!_instance)
                    {
                        GameObject obj = new GameObject("M_InputModule");
                        _instance = obj.AddComponent<MInput>();
                    }
                }

                return _instance;
            }
        }

        private MGesture _currentGesture = new MGesture();
        public static MGesture current
        {
            get
            {
                return MInput.Instance._currentGesture;
            }
        }

        private List<MGesture> _currentGestures = new List<MGesture>();

        public bool enable;             // Enables or disables input
        public bool enableRemote;           // Enables or disables Unity remote

        // General gesture properties
        public GesturePriority gesturePriority;
        public float StationaryTolerance;// 
        public float longTapTime;           // The time required for the detection of a long tap.
        public float swipeTolerance;        // Determines the accuracy of detecting a drag movement 0 => no precision 1=> high precision.
        public float minPinchLength;            // The minimum length for a pinch detection.
        public float minTwistAngle;         // The minimum angle for a twist detection.
        public DoubleTapDetection doubleTapDetection;
        public float doubleTapTime;
        public bool alwaysSendSwipe;
        //public bool isDpi;

        // Two finger gesture
        public bool enable2FingersGesture; // Enables 2 fingers gesture.
        public bool enableTwist;            // Enables or disables recognition of the twist
        public bool enablePinch;            // Enables or disables recognition of the Pinch
        public bool enable2FingersSwipe;    // Enables or disables recognition of 2 fingers swipe
        public TwoFingerPickMethod twoFingerPickMethod;

        // Auto selection
        public List<MCamera> touchCameras;  // The  cameras
        public bool autoSelect;                             // Enables or disables auto select
        public LayerMask pickableLayers3D;                          // Layer detectable by default
        public bool enable2D;                               // Enables or disables auto select on 2D
        public LayerMask pickableLayers2D;
        public bool autoUpdatePickedObject;

        // Unity UI
        public bool allowUIDetection;
        public bool enableUIMode;
        public bool autoUpdatePickedUI;

        // NGUI
        public bool enabledNGuiMode;    // True = no events are send when touch is hover an NGui panel
        public LayerMask nGUILayers;
        public List<Camera> nGUICameras;

        // Second Finger
        public bool enableSimulation;
        public KeyCode twistKey;
        public KeyCode swipeKey;

        // Inspector
        public bool showGuiInspector = false;
        public bool showSelectInspector = false;
        public bool showGestureInspector = false;
        public bool showTwoFingerInspector = false;
        public bool showSecondFingerInspector = false;
        #endregion

        #region Private members	
        private MTouchInput input = new MTouchInput();
        private MFinger[] fingers = new MFinger[100];                 // The informations of the touch for finger 1.
        public Texture secondFingerTexture;                         // The texture to display the simulation of the second finger.
        private MDoubleFingerGesture twoFinger = new MDoubleFingerGesture();
        private int oldTouchCount = 0;
        private DoubleTap[] singleDoubleTap = new DoubleTap[100];
        private MFinger[] tmpArray = new MFinger[100];
        private PickedObject pickedObject = new PickedObject();

        // Unity UI
        private List<RaycastResult> uiRaycastResultCache = new List<RaycastResult>();
        private PointerEventData uiPointerEventData;
        private EventSystem uiEventSystem;

        #endregion

        #region Constructor
        public MInput()
        {
            enable = true;

            allowUIDetection = true;
            enableUIMode = true;
            autoUpdatePickedUI = false;

            enabledNGuiMode = false;
            nGUICameras = new List<Camera>();

            autoSelect = true;
            touchCameras = new List<MCamera>();
            pickableLayers3D = 1 << 0;
            enable2D = false;
            pickableLayers2D = 1 << 0;

            gesturePriority = GesturePriority.Tap;
            StationaryTolerance = 15;
            longTapTime = 1;
            doubleTapDetection = DoubleTapDetection.BySystem;
            doubleTapTime = 0.3f;
            swipeTolerance = 0.85f;
            alwaysSendSwipe = false;

            enable2FingersGesture = true;
            twoFingerPickMethod = TwoFingerPickMethod.Finger;
            enable2FingersSwipe = true;
            enablePinch = true;
            minPinchLength = 0f;
            enableTwist = true;
            minTwistAngle = 0f;

            enableSimulation = true;
            twistKey = KeyCode.LeftAlt;
            swipeKey = KeyCode.LeftControl;


        }
        #endregion

        #region MonoBehaviour Callback
        void OnEnable()
        {
            if (Application.isPlaying && Application.isEditor)
            {
                Init();
            }
        }

        void Awake()
        {
            Init();
        }

        void Start()
        {

            for (int i = 0; i < 100; i++)
            {
                singleDoubleTap[i] = new DoubleTap();
            }
            int index = touchCameras.FindIndex(
                delegate (MCamera c)
                {
                    return c.camera == Camera.main;
                }
            );

            if (index < 0)
                touchCameras.Add(new MCamera(Camera.main, false));

            // Fire ready event
            if (OnMInputIsReady != null)
            {
                OnMInputIsReady();
            }

            // Current gesture
            _currentGestures.Add(new MGesture());
        }

        void Init()
        {

            // The texture to display the simulation of the second finger.
#if ((!UNITY_ANDROID && !UNITY_IOS && !UNITY_TVOS && !UNITY_WINRT && !UNITY_BLACKBERRY) || UNITY_EDITOR)
            if (secondFingerTexture == null && enableSimulation)
            {
                secondFingerTexture = Resources.Load("secondFinger") as Texture;
            }
#endif
        }

        // Display the simulation of the second finger
#if ((!UNITY_ANDROID && !UNITY_IOS && !UNITY_TVOS && !UNITY_WINRT && !UNITY_BLACKBERRY) || UNITY_EDITOR)
        void OnGUI()
        {
            if (enableSimulation && !enableRemote)
            {
                Vector2 finger = input.GetSecondFingerPosition();
                if (finger != new Vector2(-1, -1))
                {
                    GUI.DrawTexture(new Rect(finger.x - 16, Screen.height - finger.y - 16, 32, 32), secondFingerTexture);
                }
            }
        }
#endif

        void OnDrawGizmos()
        {
        }

        // Non comments.
        void Update()
        {


            if (enable && MInput.Instance == this)
            {

                //#if (UNITY_EDITOR )
                if (Application.isPlaying && Input.touchCount > 0)
                {
                    enableRemote = true;
                }

                if (Application.isPlaying && Input.touchCount == 0)
                {
                    enableRemote = false;
                }
                //#endif

                int i;

                // How many finger do we have ?
                int count = input.TouchCount();

                // Reset after two finger gesture;
                if (oldTouchCount == 2 && count != 2 && count > 0)
                {
                    CreateGesture2Finger(EvtType.OnCancel2Fingers, Vector2.zero, Vector2.zero, Vector2.zero, 0, SwipeDirection.None, 0, Vector2.zero, 0, 0, 0);
                }

                // Get touches		
                //#if (((UNITY_ANDROID || UNITY_IOS || UNITY_WINRT || UNITY_BLACKBERRY || UNITY_TVOS) && !UNITY_EDITOR))
#if (((UNITY_ANDROID || UNITY_IOS || UNITY_BLACKBERRY || UNITY_TVOS || UNITY_PSP2) && !UNITY_EDITOR))
				UpdateTouches(true, count);
#else
                UpdateTouches(false, count);
#endif

                // two fingers gesture
                twoFinger.oldPickedObject = twoFinger.pickedObject;
                if (enable2FingersGesture)
                {
                    if (count == 2)
                    {
                        TwoFinger();
                    }
                }

                // Other fingers gesture
                for (i = 0; i < 100; i++)
                {
                    if (fingers[i] != null)
                    {
                        OneFinger(i);
                    }
                }

                oldTouchCount = count;
            }
        }


        void LateUpdate()
        {

            // single gesture
            if (_currentGestures.Count > 1)
            {
                _currentGestures.RemoveAt(0);
            }
            else
            {
                _currentGestures[0] = null;// new Gesture();
            }
            _currentGesture = _currentGestures[0];


        }



        void UpdateTouches(bool realTouch, int touchCount)
        {

            fingers.CopyTo(tmpArray, 0);


            if (realTouch || enableRemote)
            {
                ResetTouches();
                for (var i = 0; i < touchCount; ++i)
                {
                    Touch touch = Input.GetTouch(i);

                    int t = 0;
                    while (t < 100 && fingers[i] == null)
                    {
                        if (tmpArray[t] != null)
                        {
                            if (tmpArray[t].fingerIndex == touch.fingerId)
                            {
                                fingers[i] = tmpArray[t];
                            }
                        }
                        t++;
                    }

                    if (fingers[i] == null)
                    {
                        fingers[i] = new MFinger();
                        fingers[i].fingerIndex = touch.fingerId;
                        fingers[i].gesture = GestureType.None;
                        fingers[i].phase = TouchPhase.Began;
                    }
                    else
                    {
                        fingers[i].phase = touch.phase;
                    }

                    if (fingers[i].phase != TouchPhase.Began)
                    {
                        fingers[i].deltaPosition = touch.position - fingers[i].position;
                    }
                    else
                    {
                        fingers[i].deltaPosition = Vector2.zero;
                    }

                    fingers[i].position = touch.position;
                    //fingers[i].deltaPosition = touch.deltaPosition;
                    fingers[i].tapCount = touch.tapCount;
                    fingers[i].deltaTime = touch.deltaTime;

                    fingers[i].touchCount = touchCount;

                    fingers[i].altitudeAngle = touch.altitudeAngle;
                    fingers[i].azimuthAngle = touch.azimuthAngle;
                    fingers[i].maximumPossiblePressure = touch.maximumPossiblePressure;
                    fingers[i].pressure = touch.pressure;
                    fingers[i].radius = touch.radius;
                    fingers[i].radiusVariance = touch.radiusVariance;
                    fingers[i].touchType = touch.type;

                }
            }
            else
            {
                int i = 0;
                while (i < touchCount)
                {
                    fingers[i] = input.GetMouseTouch(i, fingers[i]) as MFinger;
                    fingers[i].touchCount = touchCount;
                    i++;
                }
            }

        }

        void ResetTouches()
        {
            for (int i = 0; i < 100; i++)
            {
                fingers[i] = null;
            }
        }
        #endregion

        #region One finger Private methods
        private void OneFinger(int fingerIndex)
        {

            // A tap starts ?
            if (fingers[fingerIndex].gesture == GestureType.None)
            {

                if (!singleDoubleTap[fingerIndex].inDoubleTap)
                {
                    singleDoubleTap[fingerIndex].inDoubleTap = true;
                    singleDoubleTap[fingerIndex].time = 0;
                    singleDoubleTap[fingerIndex].count = 1;
                }

                fingers[fingerIndex].startTimeAction = Time.realtimeSinceStartup;
                fingers[fingerIndex].gesture = GestureType.Acquisition;
                fingers[fingerIndex].startPosition = fingers[fingerIndex].position;

                // do we touch a pickable gameobject ?
                if (autoSelect)
                {
                    if (GetPickedGameObject(fingers[fingerIndex]))
                    {
                        fingers[fingerIndex].pickedObject = pickedObject.pickedObj;
                        fingers[fingerIndex].isGuiCamera = pickedObject.isGUI;
                        fingers[fingerIndex].pickedCamera = pickedObject.pickedCamera;
                    }
                }

                // UnityGUI
                if (allowUIDetection)
                {
                    fingers[fingerIndex].isOverGui = IsScreenPositionOverUI(fingers[fingerIndex].position);
                    fingers[fingerIndex].pickedUIElement = GetFirstUIElementFromCache();
                }

                // we notify a touch
                CreateGesture(fingerIndex, EvtType.OnTouchStart, fingers[fingerIndex], SwipeDirection.None, 0, Vector2.zero);
            }

            if (singleDoubleTap[fingerIndex].inDoubleTap) singleDoubleTap[fingerIndex].time += Time.deltaTime;

            // Calculates the time since the beginning of the action.
            fingers[fingerIndex].actionTime = Time.realtimeSinceStartup - fingers[fingerIndex].startTimeAction;


            // touch canceled?
            if (fingers[fingerIndex].phase == TouchPhase.Canceled)
            {
                fingers[fingerIndex].gesture = GestureType.Cancel;
            }

            if (fingers[fingerIndex].phase != TouchPhase.Ended && fingers[fingerIndex].phase != TouchPhase.Canceled)
            {

                // Are we stationary  for a long tap
                if (fingers[fingerIndex].phase == TouchPhase.Stationary &&
                    fingers[fingerIndex].actionTime >= longTapTime && fingers[fingerIndex].gesture == GestureType.Acquisition)
                {

                    fingers[fingerIndex].gesture = GestureType.LongTap;
                    CreateGesture(fingerIndex, EvtType.OnLongTapStart, fingers[fingerIndex], SwipeDirection.None, 0, Vector2.zero);
                }

                // Let's move us?
                if (((fingers[fingerIndex].gesture == GestureType.Acquisition || fingers[fingerIndex].gesture == GestureType.LongTap) && fingers[fingerIndex].phase == TouchPhase.Moved && gesturePriority == GesturePriority.Slips)
                    || ((fingers[fingerIndex].gesture == GestureType.Acquisition || fingers[fingerIndex].gesture == GestureType.LongTap) && (FingerInTolerance(fingers[fingerIndex]) == false) && gesturePriority == GesturePriority.Tap))
                {

                    //  long touch => cancel
                    if (fingers[fingerIndex].gesture == GestureType.LongTap)
                    {
                        fingers[fingerIndex].gesture = GestureType.Cancel;
                        CreateGesture(fingerIndex, EvtType.OnLongTapEnd, fingers[fingerIndex], SwipeDirection.None, 0, Vector2.zero);
                        // Init the touch to start
                        fingers[fingerIndex].gesture = GestureType.Acquisition;
                    }
                    else
                    {
                        fingers[fingerIndex].oldSwipeType = SwipeDirection.None;

                        // If an object is selected we drag
                        if (fingers[fingerIndex].pickedObject)
                        {
                            fingers[fingerIndex].gesture = GestureType.Drag;
                            CreateGesture(fingerIndex, EvtType.OnDragStart, fingers[fingerIndex], SwipeDirection.None, 0, Vector2.zero);

                            if (alwaysSendSwipe)
                            {
                                CreateGesture(fingerIndex, EvtType.OnSwipeStart, fingers[fingerIndex], SwipeDirection.None, 0, Vector2.zero);
                            }
                        }
                        // If not swipe
                        else
                        {
                            fingers[fingerIndex].gesture = GestureType.Swipe;
                            CreateGesture(fingerIndex, EvtType.OnSwipeStart, fingers[fingerIndex], SwipeDirection.None, 0, Vector2.zero);
                        }

                    }
                }

                // Gesture update
                EvtType message = EvtType.None;

                switch (fingers[fingerIndex].gesture)
                {
                    case GestureType.LongTap:
                        message = EvtType.OnLongTap;
                        break;
                    case GestureType.Drag:
                        message = EvtType.OnDrag;
                        break;
                    case GestureType.Swipe:
                        message = EvtType.OnSwipe;
                        break;
                }

                // Send gesture
                SwipeDirection currentSwipe = SwipeDirection.None;
                currentSwipe = GetSwipe(new Vector2(0, 0), fingers[fingerIndex].deltaPosition);
                if (message != EvtType.None)
                {

                    fingers[fingerIndex].oldSwipeType = currentSwipe;
                    CreateGesture(fingerIndex, message, fingers[fingerIndex], currentSwipe, 0, fingers[fingerIndex].deltaPosition);

                    if (message == EvtType.OnDrag && alwaysSendSwipe)
                    {
                        CreateGesture(fingerIndex, EvtType.OnSwipe, fingers[fingerIndex], currentSwipe, 0, fingers[fingerIndex].deltaPosition);
                    }
                }

                // TouchDown
                CreateGesture(fingerIndex, EvtType.OnTouchDown, fingers[fingerIndex], currentSwipe, 0, fingers[fingerIndex].deltaPosition);
            }
            else
            {
                // End of the touch		
                switch (fingers[fingerIndex].gesture)
                {
                    // tap
                    case GestureType.Acquisition:

                        if (doubleTapDetection == DoubleTapDetection.BySystem)
                        {
                            if (FingerInTolerance(fingers[fingerIndex]))
                            {
                                if (fingers[fingerIndex].tapCount < 2)
                                {
                                    CreateGesture(fingerIndex, EvtType.OnSimpleTap, fingers[fingerIndex], SwipeDirection.None, 0, Vector2.zero);
                                }
                                else
                                {
                                    CreateGesture(fingerIndex, EvtType.OnDoubleTap, fingers[fingerIndex], SwipeDirection.None, 0, Vector2.zero);
                                }

                            }
                        }
                        else
                        {
                            if (!singleDoubleTap[fingerIndex].inWait)
                            {
                                singleDoubleTap[fingerIndex].finger = fingers[fingerIndex];
                                StartCoroutine(SingleOrDouble(fingerIndex));
                            }
                            else
                            {
                                singleDoubleTap[fingerIndex].count++;
                            }
                        }

                        break;
                    // long tap
                    case GestureType.LongTap:
                        CreateGesture(fingerIndex, EvtType.OnLongTapEnd, fingers[fingerIndex], SwipeDirection.None, 0, Vector2.zero);
                        break;
                    // drag
                    case GestureType.Drag:
                        CreateGesture(fingerIndex, EvtType.OnDragEnd, fingers[fingerIndex], GetSwipe(fingers[fingerIndex].startPosition, fingers[fingerIndex].position), (fingers[fingerIndex].startPosition - fingers[fingerIndex].position).magnitude, fingers[fingerIndex].position - fingers[fingerIndex].startPosition);
                        if (alwaysSendSwipe)
                        {
                            CreateGesture(fingerIndex, EvtType.OnSwipeEnd, fingers[fingerIndex], GetSwipe(fingers[fingerIndex].startPosition, fingers[fingerIndex].position), (fingers[fingerIndex].position - fingers[fingerIndex].startPosition).magnitude, fingers[fingerIndex].position - fingers[fingerIndex].startPosition);
                        }
                        break;
                    // swipe
                    case GestureType.Swipe:
                        CreateGesture(fingerIndex, EvtType.OnSwipeEnd, fingers[fingerIndex], GetSwipe(fingers[fingerIndex].startPosition, fingers[fingerIndex].position), (fingers[fingerIndex].position - fingers[fingerIndex].startPosition).magnitude, fingers[fingerIndex].position - fingers[fingerIndex].startPosition);
                        break;

                    // cancel
                    case GestureType.Cancel:
                        CreateGesture(fingerIndex, EvtType.OnCancel, fingers[fingerIndex], SwipeDirection.None, 0, Vector2.zero);
                        break;

                }
                CreateGesture(fingerIndex, EvtType.OnTouchUp, fingers[fingerIndex], SwipeDirection.None, 0, Vector2.zero);
                fingers[fingerIndex] = null;

            }

        }

        IEnumerator SingleOrDouble(int fingerIndex)
        {
            singleDoubleTap[fingerIndex].inWait = true;
            float time2Wait = doubleTapTime - singleDoubleTap[fingerIndex].finger.actionTime;
            if (time2Wait < 0) time2Wait = 0;
            yield return new WaitForSeconds(time2Wait);


            if (singleDoubleTap[fingerIndex].count < 2)
            {
                //CreateGesture( fingerIndex, EvtType.OnSimpleTap,singleDoubleTap[fingerIndex].finger, SwipeDirection.None,0,Vector2.zero);
                CreateGesture(fingerIndex, EvtType.OnSimpleTap, singleDoubleTap[fingerIndex].finger, SwipeDirection.None, 0, singleDoubleTap[fingerIndex].finger.deltaPosition);
            }
            else
            {
                //CreateGesture( fingerIndex, EvtType.OnDoubleTap,singleDoubleTap[fingerIndex].finger, SwipeDirection.None,0,Vector2.zero);
                CreateGesture(fingerIndex, EvtType.OnDoubleTap, singleDoubleTap[fingerIndex].finger, SwipeDirection.None, 0, singleDoubleTap[fingerIndex].finger.deltaPosition);
            }

            //fingers[fingerIndex]=null;
            singleDoubleTap[fingerIndex].Stop();
            StopCoroutine("SingleOrDouble");
        }

        private void CreateGesture(int touchIndex, EvtType message, MFinger finger, SwipeDirection swipe, float swipeLength, Vector2 swipeVector)
        {

            bool firingEvent = true;

            if (autoUpdatePickedUI && allowUIDetection)
            {
                finger.isOverGui = IsScreenPositionOverUI(finger.position);
                finger.pickedUIElement = GetFirstUIElementFromCache();
            }

            // NGui
            if (enabledNGuiMode && message == EvtType.OnTouchStart)
            {
                finger.isOverGui = finger.isOverGui || IsTouchOverNGui(finger.position);
            }

            // firing event ?
            if ((enableUIMode || enabledNGuiMode))
            {
                firingEvent = !finger.isOverGui;
            }

            // The new gesture
            MGesture gesture = finger.GetGesture();

            // Auto update picked object
            if (autoUpdatePickedObject && autoSelect)
            {
                if (message != EvtType.OnDrag && message != EvtType.OnDragEnd && message != EvtType.OnDragStart)
                {
                    if (GetPickedGameObject(finger))
                    {
                        gesture.pickedObject = pickedObject.pickedObj;
                        gesture.pickedCamera = pickedObject.pickedCamera;
                        gesture.isGuiCamera = pickedObject.isGUI;
                    }
                    else
                    {
                        gesture.pickedObject = null;
                        gesture.pickedCamera = null;
                        gesture.isGuiCamera = false;
                    }
                }
            }

            gesture.swipe = swipe;
            gesture.swipeLength = swipeLength;
            gesture.swipeVector = swipeVector;

            gesture.deltaPinch = 0;
            gesture.twistAngle = 0;


            // Firing event
            if (firingEvent)
            {
                RaiseEvent(message, gesture);
            }
            else if (finger.isOverGui)
            {
                if (message == EvtType.OnTouchUp)
                {
                    RaiseEvent(EvtType.OnUIElementTouchUp, gesture);
                }
                else
                {
                    RaiseEvent(EvtType.OnOverUIElement, gesture);
                }
            }

        }
        #endregion

        #region Two finger private methods
        private void TwoFinger()
        {

            bool move = false;

            // A touch starts
            if (twoFinger.currentGesture == GestureType.None)
            {

                if (!singleDoubleTap[99].inDoubleTap)
                {
                    singleDoubleTap[99].inDoubleTap = true;
                    singleDoubleTap[99].time = 0;
                    singleDoubleTap[99].count = 1;
                }

                twoFinger.finger0 = GetTwoFinger(-1);
                twoFinger.finger1 = GetTwoFinger(twoFinger.finger0);

                twoFinger.startTimeAction = Time.realtimeSinceStartup;
                twoFinger.currentGesture = GestureType.Acquisition;

                fingers[twoFinger.finger0].startPosition = fingers[twoFinger.finger0].position;
                fingers[twoFinger.finger1].startPosition = fingers[twoFinger.finger1].position;

                fingers[twoFinger.finger0].oldPosition = fingers[twoFinger.finger0].position;
                fingers[twoFinger.finger1].oldPosition = fingers[twoFinger.finger1].position;


                twoFinger.oldFingerDistance = Mathf.Abs(Vector2.Distance(fingers[twoFinger.finger0].position, fingers[twoFinger.finger1].position));
                twoFinger.startPosition = new Vector2((fingers[twoFinger.finger0].position.x + fingers[twoFinger.finger1].position.x) / 2, (fingers[twoFinger.finger0].position.y + fingers[twoFinger.finger1].position.y) / 2);
                twoFinger.position = twoFinger.startPosition;
                twoFinger.oldStartPosition = twoFinger.startPosition;
                twoFinger.deltaPosition = Vector2.zero;
                twoFinger.startDistance = twoFinger.oldFingerDistance;

                // do we touch a pickable gameobject ?
                if (autoSelect)
                {
                    if (GetTwoFingerPickedObject())
                    {
                        twoFinger.pickedObject = pickedObject.pickedObj;
                        twoFinger.pickedCamera = pickedObject.pickedCamera;
                        twoFinger.isGuiCamera = pickedObject.isGUI;
                    }
                    else
                    {
                        twoFinger.ClearPickedObjectData();
                    }
                }

                // UnityGUI
                if (allowUIDetection)
                {
                    if (GetTwoFingerPickedUIElement())
                    {
                        twoFinger.pickedUIElement = pickedObject.pickedObj;
                        twoFinger.isOverGui = true;
                    }
                    else
                    {
                        twoFinger.ClearPickedUIData();
                    }
                }

                // we notify the touch
                CreateGesture2Finger(EvtType.OnTouchStart2Fingers, twoFinger.startPosition, twoFinger.startPosition, twoFinger.deltaPosition, twoFinger.timeSinceStartAction, SwipeDirection.None, 0, Vector2.zero, 0, 0, twoFinger.oldFingerDistance);
            }

            if (singleDoubleTap[99].inDoubleTap) singleDoubleTap[99].time += Time.deltaTime;

            // Calculates the time since the beginning of the action.
            twoFinger.timeSinceStartAction = Time.realtimeSinceStartup - twoFinger.startTimeAction;

            // Position & deltaPosition
            twoFinger.position = new Vector2((fingers[twoFinger.finger0].position.x + fingers[twoFinger.finger1].position.x) / 2, (fingers[twoFinger.finger0].position.y + fingers[twoFinger.finger1].position.y) / 2);
            twoFinger.deltaPosition = twoFinger.position - twoFinger.oldStartPosition;
            twoFinger.fingerDistance = Mathf.Abs(Vector2.Distance(fingers[twoFinger.finger0].position, fingers[twoFinger.finger1].position));

            // Cancel
            if (fingers[twoFinger.finger0].phase == TouchPhase.Canceled || fingers[twoFinger.finger1].phase == TouchPhase.Canceled)
            {
                twoFinger.currentGesture = GestureType.Cancel;
            }

            // Let's go
            if (fingers[twoFinger.finger0].phase != TouchPhase.Ended && fingers[twoFinger.finger1].phase != TouchPhase.Ended && twoFinger.currentGesture != GestureType.Cancel)
            {


                // Are we stationary ?
                if (twoFinger.currentGesture == GestureType.Acquisition && twoFinger.timeSinceStartAction >= longTapTime && FingerInTolerance(fingers[twoFinger.finger0]) && FingerInTolerance(fingers[twoFinger.finger1]))
                {
                    twoFinger.currentGesture = GestureType.LongTap;
                    // we notify the beginning of a longtouch
                    CreateGesture2Finger(EvtType.OnLongTapStart2Fingers, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition, twoFinger.timeSinceStartAction, SwipeDirection.None, 0, Vector2.zero, 0, 0, twoFinger.fingerDistance);
                }

                // Let's move us ?
                if (((FingerInTolerance(fingers[twoFinger.finger0]) == false || FingerInTolerance(fingers[twoFinger.finger1]) == false) && gesturePriority == GesturePriority.Tap)
                || ((fingers[twoFinger.finger0].phase == TouchPhase.Moved || fingers[twoFinger.finger1].phase == TouchPhase.Moved) && gesturePriority == GesturePriority.Slips))
                {
                    move = true;
                }

                // we move
                if (move && twoFinger.currentGesture != GestureType.Tap)
                {
                    Vector2 currentDistance = fingers[twoFinger.finger0].position - fingers[twoFinger.finger1].position;
                    Vector2 previousDistance = fingers[twoFinger.finger0].oldPosition - fingers[twoFinger.finger1].oldPosition;
                    float currentDelta = currentDistance.magnitude - previousDistance.magnitude;


                    #region drag & swipe
                    if (enable2FingersSwipe)
                    {
                        float dot = Vector2.Dot(fingers[twoFinger.finger0].deltaPosition.normalized, fingers[twoFinger.finger1].deltaPosition.normalized);

                        if (dot > 0)
                        {

                            if (twoFinger.oldGesture == GestureType.LongTap)
                            {
                                CreateStateEnd2Fingers(twoFinger.currentGesture, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition, twoFinger.timeSinceStartAction, false, twoFinger.fingerDistance);
                                twoFinger.startTimeAction = Time.realtimeSinceStartup;
                            }

                            if (twoFinger.pickedObject && !twoFinger.dragStart && !alwaysSendSwipe)
                            {

                                twoFinger.currentGesture = GestureType.Drag;

                                CreateGesture2Finger(EvtType.OnDragStart2Fingers, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition, twoFinger.timeSinceStartAction, SwipeDirection.None, 0, Vector2.zero, 0, 0, twoFinger.fingerDistance);
                                CreateGesture2Finger(EvtType.OnSwipeStart2Fingers, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition, twoFinger.timeSinceStartAction, SwipeDirection.None, 0, Vector2.zero, 0, 0, twoFinger.fingerDistance);
                                twoFinger.dragStart = true;
                            }
                            else if (!twoFinger.pickedObject && !twoFinger.swipeStart)
                            {

                                twoFinger.currentGesture = GestureType.Swipe;

                                CreateGesture2Finger(EvtType.OnSwipeStart2Fingers, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition, twoFinger.timeSinceStartAction, SwipeDirection.None, 0, Vector2.zero, 0, 0, twoFinger.fingerDistance);
                                twoFinger.swipeStart = true;
                            }

                        }
                        else
                        {
                            if (dot < 0)
                            {
                                twoFinger.dragStart = false;
                                twoFinger.swipeStart = false;
                            }
                        }

                        //
                        if (twoFinger.dragStart)
                        {
                            CreateGesture2Finger(EvtType.OnDrag2Fingers, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition, twoFinger.timeSinceStartAction, GetSwipe(twoFinger.oldStartPosition, twoFinger.position), 0, twoFinger.deltaPosition, 0, 0, twoFinger.fingerDistance);
                            CreateGesture2Finger(EvtType.OnSwipe2Fingers, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition, twoFinger.timeSinceStartAction, GetSwipe(twoFinger.oldStartPosition, twoFinger.position), 0, twoFinger.deltaPosition, 0, 0, twoFinger.fingerDistance);
                        }

                        if (twoFinger.swipeStart)
                        {
                            CreateGesture2Finger(EvtType.OnSwipe2Fingers, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition, twoFinger.timeSinceStartAction, GetSwipe(twoFinger.oldStartPosition, twoFinger.position), 0, twoFinger.deltaPosition, 0, 0, twoFinger.fingerDistance);
                        }

                    }
                    #endregion

                    DetectPinch(currentDelta);
                    DetecTwist(previousDistance, currentDistance, currentDelta);
                }
                else
                {
                    // Long tap update
                    if (twoFinger.currentGesture == GestureType.LongTap)
                    {
                        CreateGesture2Finger(EvtType.OnLongTap2Fingers, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition, twoFinger.timeSinceStartAction, SwipeDirection.None, 0, Vector2.zero, 0, 0, twoFinger.fingerDistance);
                    }
                }

                CreateGesture2Finger(EvtType.OnTouchDown2Fingers, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition, twoFinger.timeSinceStartAction, GetSwipe(twoFinger.oldStartPosition, twoFinger.position), 0, twoFinger.deltaPosition, 0, 0, twoFinger.fingerDistance);

                fingers[twoFinger.finger0].oldPosition = fingers[twoFinger.finger0].position;
                fingers[twoFinger.finger1].oldPosition = fingers[twoFinger.finger1].position;

                twoFinger.oldFingerDistance = twoFinger.fingerDistance;
                twoFinger.oldStartPosition = twoFinger.position;
                twoFinger.oldGesture = twoFinger.currentGesture;

            }
            else
            {

                if (twoFinger.currentGesture != GestureType.Acquisition && twoFinger.currentGesture != GestureType.Tap)
                {
                    CreateStateEnd2Fingers(twoFinger.currentGesture, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition, twoFinger.timeSinceStartAction, true, twoFinger.fingerDistance);
                    twoFinger.currentGesture = GestureType.None;
                    twoFinger.pickedObject = null;
                    twoFinger.swipeStart = false;
                    twoFinger.dragStart = false;

                }
                else
                {
                    twoFinger.currentGesture = GestureType.Tap;
                    CreateStateEnd2Fingers(twoFinger.currentGesture, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition, twoFinger.timeSinceStartAction, true, twoFinger.fingerDistance);
                }
            }
        }




        private void DetectPinch(float currentDelta)
        {
            #region Pinch
            if (enablePinch)
            {

                if ((Mathf.Abs(twoFinger.fingerDistance - twoFinger.startDistance) >= minPinchLength && twoFinger.currentGesture != GestureType.Pinch) || twoFinger.currentGesture == GestureType.Pinch)
                {

                    if (currentDelta != 0 && twoFinger.oldGesture == GestureType.LongTap)
                    {
                        CreateStateEnd2Fingers(twoFinger.currentGesture, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition, twoFinger.timeSinceStartAction, false, twoFinger.fingerDistance);
                        twoFinger.startTimeAction = Time.realtimeSinceStartup;
                    }

                    twoFinger.currentGesture = GestureType.Pinch;

                    if (currentDelta > 0)
                    {
                        CreateGesture2Finger(EvtType.OnPinchOut, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition, twoFinger.timeSinceStartAction, GetSwipe(twoFinger.startPosition, twoFinger.position), 0, Vector2.zero, 0, Mathf.Abs(twoFinger.fingerDistance - twoFinger.oldFingerDistance), twoFinger.fingerDistance);
                    }

                    if (currentDelta < 0)
                    {
                        CreateGesture2Finger(EvtType.OnPinchIn, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition, twoFinger.timeSinceStartAction, GetSwipe(twoFinger.startPosition, twoFinger.position), 0, Vector2.zero, 0, Mathf.Abs(twoFinger.fingerDistance - twoFinger.oldFingerDistance), twoFinger.fingerDistance);
                    }

                    if (currentDelta < 0 || currentDelta > 0)
                    {
                        CreateGesture2Finger(EvtType.OnPinch, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition, twoFinger.timeSinceStartAction, GetSwipe(twoFinger.startPosition, twoFinger.position), 0, Vector2.zero, 0, currentDelta, twoFinger.fingerDistance);
                    }

                }

                twoFinger.lastPinch = currentDelta > 0 ? currentDelta : twoFinger.lastPinch;
            }
            #endregion
        }

        private void DetecTwist(Vector2 previousDistance, Vector2 currentDistance, float currentDelta)
        {
            #region Twist
            if (enableTwist)
            {

                float twistAngle = Vector2.Angle(previousDistance, currentDistance);

                //Debug.Log( twistAngle);
                if (previousDistance == currentDistance)
                    twistAngle = 0;

                if (Mathf.Abs(twistAngle) >= minTwistAngle && (twoFinger.currentGesture != GestureType.Twist) || twoFinger.currentGesture == GestureType.Twist)
                {

                    if (twoFinger.oldGesture == GestureType.LongTap)
                    {
                        CreateStateEnd2Fingers(twoFinger.currentGesture, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition, twoFinger.timeSinceStartAction, false, twoFinger.fingerDistance);
                        twoFinger.startTimeAction = Time.realtimeSinceStartup;
                    }

                    twoFinger.currentGesture = GestureType.Twist;

                    if (twistAngle != 0)
                    {
                        twistAngle *= Mathf.Sign(Vector3.Cross(previousDistance, currentDistance).z);
                    }

                    CreateGesture2Finger(EvtType.OnTwist, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition, twoFinger.timeSinceStartAction, SwipeDirection.None, 0, Vector2.zero, twistAngle, 0, twoFinger.fingerDistance);
                }

                twoFinger.lastTwistAngle = twistAngle != 0 ? twistAngle : twoFinger.lastTwistAngle;
            }
            #endregion
        }


        private void CreateStateEnd2Fingers(GestureType gesture, Vector2 startPosition, Vector2 position, Vector2 deltaPosition, float time, bool realEnd, float fingerDistance, float twist = 0, float pinch = 0)
        {


            switch (gesture)
            {
                // Tap
                case GestureType.Tap:
                case GestureType.Acquisition:

                    if (doubleTapDetection == DoubleTapDetection.BySystem)
                    {

                        if (fingers[twoFinger.finger0].tapCount < 2 && fingers[twoFinger.finger1].tapCount < 2)
                        {
                            CreateGesture2Finger(EvtType.OnSimpleTap2Fingers, startPosition, position, deltaPosition,
                                                 time, SwipeDirection.None, 0, Vector2.zero, 0, 0, fingerDistance);
                        }
                        else
                        {
                            CreateGesture2Finger(EvtType.OnDoubleTap2Fingers, startPosition, position, deltaPosition,
                                                 time, SwipeDirection.None, 0, Vector2.zero, 0, 0, fingerDistance);
                        }
                        twoFinger.currentGesture = GestureType.None;
                        twoFinger.pickedObject = null;
                        twoFinger.swipeStart = false;
                        twoFinger.dragStart = false;
                        singleDoubleTap[99].Stop();
                        StopCoroutine("SingleOrDouble2Fingers");

                    }
                    else
                    {
                        if (!singleDoubleTap[99].inWait)
                        {
                            StartCoroutine("SingleOrDouble2Fingers");
                        }
                        else
                        {
                            singleDoubleTap[99].count++;
                        }
                    }
                    break;

                // Long tap
                case GestureType.LongTap:
                    CreateGesture2Finger(EvtType.OnLongTapEnd2Fingers, startPosition, position, deltaPosition,
                                         time, SwipeDirection.None, 0, Vector2.zero, 0, 0, fingerDistance);
                    break;

                // Pinch 
                case GestureType.Pinch:
                    CreateGesture2Finger(EvtType.OnPinchEnd, startPosition, position, deltaPosition,
                                         time, SwipeDirection.None, 0, Vector2.zero, 0, twoFinger.lastPinch, fingerDistance);
                    break;

                // twist
                case GestureType.Twist:
                    CreateGesture2Finger(EvtType.OnTwistEnd, startPosition, position, deltaPosition,
                                         time, SwipeDirection.None, 0, Vector2.zero, twoFinger.lastTwistAngle, 0, fingerDistance);
                    break;
            }

            if (realEnd)
            {
                // Drag
                if (twoFinger.dragStart)
                {
                    CreateGesture2Finger(EvtType.OnDragEnd2Fingers, startPosition, position, deltaPosition,
                                         time, GetSwipe(startPosition, position), (position - startPosition).magnitude, position - startPosition, 0, 0, fingerDistance);
                };

                // Swipe
                if (twoFinger.swipeStart)
                {
                    CreateGesture2Finger(EvtType.OnSwipeEnd2Fingers, startPosition, position, deltaPosition,
                                         time, GetSwipe(startPosition, position), (position - startPosition).magnitude, position - startPosition, 0, 0, fingerDistance);
                }

                CreateGesture2Finger(EvtType.OnTouchUp2Fingers, startPosition, position, deltaPosition, time, SwipeDirection.None, 0, Vector2.zero, 0, 0, fingerDistance);
            }
        }

        IEnumerator SingleOrDouble2Fingers()
        {
            singleDoubleTap[99].inWait = true;

            yield return new WaitForSeconds(doubleTapTime);

            if (singleDoubleTap[99].count < 2)
            {

                CreateGesture2Finger(EvtType.OnSimpleTap2Fingers, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition,
                                     twoFinger.timeSinceStartAction, SwipeDirection.None, 0, Vector2.zero, 0, 0, twoFinger.fingerDistance);

            }
            else
            {
                CreateGesture2Finger(EvtType.OnDoubleTap2Fingers, twoFinger.startPosition, twoFinger.position, twoFinger.deltaPosition,
                                     twoFinger.timeSinceStartAction, SwipeDirection.None, 0, Vector2.zero, 0, 0, twoFinger.fingerDistance);
            }

            twoFinger.currentGesture = GestureType.None;
            twoFinger.pickedObject = null;
            twoFinger.swipeStart = false;
            twoFinger.dragStart = false;
            singleDoubleTap[99].Stop();
            StopCoroutine("SingleOrDouble2Fingers");
        }

        private void CreateGesture2Finger(EvtType message, Vector2 startPosition, Vector2 position, Vector2 deltaPosition,
                                           float actionTime, SwipeDirection swipe, float swipeLength, Vector2 swipeVector, float twist, float pinch, float twoDistance)
        {

            bool firingEvent = true;
            MGesture gesture = new MGesture();
            gesture.isOverGui = false;

            // NGui
            if (enabledNGuiMode && message == EvtType.OnTouchStart2Fingers)
            {
                gesture.isOverGui = gesture.isOverGui || (IsTouchOverNGui(twoFinger.position) && IsTouchOverNGui(twoFinger.position));
            }

            gesture.touchCount = 2;
            gesture.fingerIndex = -1;
            gesture.startPosition = startPosition;
            gesture.position = position;
            gesture.deltaPosition = deltaPosition;

            gesture.actionTime = actionTime;
            gesture.deltaTime = Time.deltaTime;

            gesture.swipe = swipe;
            gesture.swipeLength = swipeLength;
            gesture.swipeVector = swipeVector;

            gesture.deltaPinch = pinch;
            gesture.twistAngle = twist;
            gesture.twoFingerDistance = twoDistance;

            gesture.pickedObject = twoFinger.pickedObject;
            gesture.pickedCamera = twoFinger.pickedCamera;
            gesture.isGuiCamera = twoFinger.isGuiCamera;

            if (autoUpdatePickedObject)
            {
                if (message != EvtType.OnDrag && message != EvtType.OnDragEnd && message != EvtType.OnTwist && message != EvtType.OnTwistEnd && message != EvtType.OnPinch && message != EvtType.OnPinchEnd
                 && message != EvtType.OnPinchIn && message != EvtType.OnPinchOut)
                {

                    if (GetTwoFingerPickedObject())
                    {
                        gesture.pickedObject = pickedObject.pickedObj;
                        gesture.pickedCamera = pickedObject.pickedCamera;
                        gesture.isGuiCamera = pickedObject.isGUI;
                    }
                    else
                    {
                        twoFinger.ClearPickedObjectData();
                    }
                }
            }

            gesture.pickedUIElement = twoFinger.pickedUIElement;
            gesture.isOverGui = twoFinger.isOverGui;


            if (allowUIDetection && autoUpdatePickedUI)
            {
                if (message != EvtType.OnDrag && message != EvtType.OnDragEnd && message != EvtType.OnTwist && message != EvtType.OnTwistEnd && message != EvtType.OnPinch && message != EvtType.OnPinchEnd
                    && message != EvtType.OnPinchIn && message != EvtType.OnPinchOut)
                {
                    if (message == EvtType.OnSimpleTap2Fingers)

                        if (GetTwoFingerPickedUIElement())
                        {
                            gesture.pickedUIElement = pickedObject.pickedObj;
                            gesture.isOverGui = true;
                        }
                        else
                        {
                            twoFinger.ClearPickedUIData();
                        }
                }
            }



            // Firing event ?
            if ((enableUIMode || (enabledNGuiMode && allowUIDetection)))
            {
                firingEvent = !gesture.isOverGui;
            }

            // Firing event
            if (firingEvent)
            {
                RaiseEvent(message, gesture);
            }
            else if (gesture.isOverGui)
            {
                if (message == EvtType.OnTouchUp2Fingers)
                {
                    RaiseEvent(EvtType.OnUIElementTouchUp, gesture);
                }
                else
                {
                    RaiseEvent(EvtType.OnOverUIElement, gesture);
                }
            }
        }

        private int GetTwoFinger(int index)
        {

            int i = index + 1;
            bool find = false;

            while (i < 10 && !find)
            {
                if (fingers[i] != null)
                {
                    if (i >= index)
                    {
                        find = true;
                    }
                }
                i++;
            }
            i--;

            return i;
        }

        private bool GetTwoFingerPickedObject()
        {

            bool returnValue = false;

            if (twoFingerPickMethod == TwoFingerPickMethod.Finger)
            {
                if (GetPickedGameObject(fingers[twoFinger.finger0], false))
                {
                    GameObject tmp = pickedObject.pickedObj;
                    if (GetPickedGameObject(fingers[twoFinger.finger1], false))
                    {
                        if (tmp == pickedObject.pickedObj)
                        {
                            returnValue = true;
                        }
                    }
                }
            }

            else
            {
                if (GetPickedGameObject(fingers[twoFinger.finger0], true))
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        private bool GetTwoFingerPickedUIElement()
        {

            bool returnValue = false;

            if (fingers[twoFinger.finger0] == null)
            {
                return false;
            }

            if (twoFingerPickMethod == TwoFingerPickMethod.Finger)
            {
                if (IsScreenPositionOverUI(fingers[twoFinger.finger0].position))
                {
                    GameObject tmp = GetFirstUIElementFromCache();

                    if (IsScreenPositionOverUI(fingers[twoFinger.finger1].position))
                    {
                        GameObject tmp2 = GetFirstUIElementFromCache();

                        if (tmp2 == tmp || tmp2.transform.IsChildOf(tmp.transform) || tmp.transform.IsChildOf(tmp2.transform))
                        {
                            pickedObject.pickedObj = tmp;
                            pickedObject.isGUI = true;
                            returnValue = true;
                        }
                    }
                }
            }
            else
            {
                if (IsScreenPositionOverUI(twoFinger.position))
                {
                    pickedObject.pickedObj = GetFirstUIElementFromCache();
                    pickedObject.isGUI = true;
                    returnValue = true;
                }
            }

            return returnValue;
        }

        #endregion

        #region General private methods
        private void RaiseEvent(EvtType evnt, MGesture gesture)
        {

            gesture.type = evnt;

            switch (evnt)
            {
                case EvtType.OnCancel:
                    if (OnCancel != null)
                        OnCancel(gesture);
                    break;
                case EvtType.OnCancel2Fingers:
                    if (OnCancel2Fingers != null)
                        OnCancel2Fingers(gesture);
                    break;
                case EvtType.OnTouchStart:
                    if (OnTouchStart != null)
                        OnTouchStart(gesture);
                    break;
                case EvtType.OnTouchDown:
                    if (OnTouchDown != null)
                        OnTouchDown(gesture);
                    break;
                case EvtType.OnTouchUp:
                    if (OnTouchUp != null)
                        OnTouchUp(gesture);
                    break;
                case EvtType.OnSimpleTap:
                    if (OnSimpleTap != null)
                        OnSimpleTap(gesture);
                    break;
                case EvtType.OnDoubleTap:
                    if (OnDoubleTap != null)
                        OnDoubleTap(gesture);
                    break;
                case EvtType.OnLongTapStart:
                    if (OnLongTapStart != null)
                        OnLongTapStart(gesture);
                    break;
                case EvtType.OnLongTap:
                    if (OnLongTap != null)
                        OnLongTap(gesture);
                    break;
                case EvtType.OnLongTapEnd:
                    if (OnLongTapEnd != null)
                        OnLongTapEnd(gesture);
                    break;
                case EvtType.OnDragStart:
                    if (OnDragStart != null)
                        OnDragStart(gesture);
                    break;
                case EvtType.OnDrag:
                    if (OnDrag != null)
                        OnDrag(gesture);
                    break;
                case EvtType.OnDragEnd:
                    if (OnDragEnd != null)
                        OnDragEnd(gesture);
                    break;
                case EvtType.OnSwipeStart:
                    if (OnSwipeStart != null)
                        OnSwipeStart(gesture);
                    break;
                case EvtType.OnSwipe:
                    if (OnSwipe != null)
                        OnSwipe(gesture);
                    break;
                case EvtType.OnSwipeEnd:
                    if (OnSwipeEnd != null)
                        OnSwipeEnd(gesture);
                    break;
                case EvtType.OnTouchStart2Fingers:
                    if (OnTouchStart2Fingers != null)
                        OnTouchStart2Fingers(gesture);
                    break;
                case EvtType.OnTouchDown2Fingers:
                    if (OnTouchDown2Fingers != null)
                        OnTouchDown2Fingers(gesture);
                    break;
                case EvtType.OnTouchUp2Fingers:
                    if (OnTouchUp2Fingers != null)
                        OnTouchUp2Fingers(gesture);
                    break;
                case EvtType.OnSimpleTap2Fingers:
                    if (OnSimpleTap2Fingers != null)
                        OnSimpleTap2Fingers(gesture);
                    break;
                case EvtType.OnDoubleTap2Fingers:
                    if (OnDoubleTap2Fingers != null)
                        OnDoubleTap2Fingers(gesture);
                    break;
                case EvtType.OnLongTapStart2Fingers:
                    if (OnLongTapStart2Fingers != null)
                        OnLongTapStart2Fingers(gesture);
                    break;
                case EvtType.OnLongTap2Fingers:
                    if (OnLongTap2Fingers != null)
                        OnLongTap2Fingers(gesture);
                    break;
                case EvtType.OnLongTapEnd2Fingers:
                    if (OnLongTapEnd2Fingers != null)
                        OnLongTapEnd2Fingers(gesture);
                    break;
                case EvtType.OnTwist:
                    if (OnTwist != null)
                        OnTwist(gesture);
                    break;
                case EvtType.OnTwistEnd:
                    if (OnTwistEnd != null)
                        OnTwistEnd(gesture);
                    break;
                case EvtType.OnPinch:
                    if (OnPinch != null)
                        OnPinch(gesture);
                    break;
                case EvtType.OnPinchIn:
                    if (OnPinchIn != null)
                        OnPinchIn(gesture);
                    break;
                case EvtType.OnPinchOut:
                    if (OnPinchOut != null)
                        OnPinchOut(gesture);
                    break;
                case EvtType.OnPinchEnd:
                    if (OnPinchEnd != null)
                        OnPinchEnd(gesture);
                    break;
                case EvtType.OnDragStart2Fingers:
                    if (OnDragStart2Fingers != null)
                        OnDragStart2Fingers(gesture);
                    break;
                case EvtType.OnDrag2Fingers:
                    if (OnDrag2Fingers != null)
                        OnDrag2Fingers(gesture);
                    break;
                case EvtType.OnDragEnd2Fingers:
                    if (OnDragEnd2Fingers != null)
                        OnDragEnd2Fingers(gesture);
                    break;
                case EvtType.OnSwipeStart2Fingers:
                    if (OnSwipeStart2Fingers != null)
                        OnSwipeStart2Fingers(gesture);
                    break;
                case EvtType.OnSwipe2Fingers:
                    if (OnSwipe2Fingers != null)
                        OnSwipe2Fingers(gesture);
                    break;
                case EvtType.OnSwipeEnd2Fingers:
                    if (OnSwipeEnd2Fingers != null)
                        OnSwipeEnd2Fingers(gesture);
                    break;
                case EvtType.OnOverUIElement:
                    if (OnOverUIElement != null)
                    {
                        OnOverUIElement(gesture);
                    }
                    break;
                case EvtType.OnUIElementTouchUp:
                    if (OnUIElementTouchUp != null)
                    {
                        OnUIElementTouchUp(gesture);
                    }
                    break;
            }

            // Direct Acces 
            int result = _currentGestures.FindIndex(delegate (MGesture obj)
            {
                return obj != null && obj.type == gesture.type && obj.fingerIndex == gesture.fingerIndex;
            }
            );

            if (result > -1)
            {
                _currentGestures[result].touchCount = gesture.touchCount;
                _currentGestures[result].position = gesture.position;
                _currentGestures[result].actionTime = gesture.actionTime;
                _currentGestures[result].pickedCamera = gesture.pickedCamera;
                _currentGestures[result].pickedObject = gesture.pickedObject;
                _currentGestures[result].pickedUIElement = gesture.pickedUIElement;
                _currentGestures[result].isOverGui = gesture.isOverGui;
                _currentGestures[result].isGuiCamera = gesture.isGuiCamera;

                // Update delta from current
                _currentGestures[result].deltaPinch += gesture.deltaPinch;
                _currentGestures[result].deltaPosition += gesture.deltaPosition;

                _currentGestures[result].deltaTime += gesture.deltaTime;
                _currentGestures[result].twistAngle += gesture.twistAngle;
            }

            if (result == -1)
            {
                _currentGestures.Add((MGesture)gesture.Clone());
                if (_currentGestures.Count > 0)
                {
                    _currentGesture = _currentGestures[0];
                }
            }


        }

        private bool GetPickedGameObject(MFinger finger, bool isTowFinger = false)
        {

            if (finger == null && !isTowFinger)
            {
                return false;
            }

            pickedObject.isGUI = false;
            pickedObject.pickedObj = null;
            pickedObject.pickedCamera = null;

            if (touchCameras.Count > 0)
            {
                for (int i = 0; i < touchCameras.Count; i++)
                {
                    if (touchCameras[i].camera != null && touchCameras[i].camera.enabled)
                    {

                        Vector2 pos = Vector2.zero;
                        if (!isTowFinger)
                        {
                            pos = finger.position;
                        }
                        else
                        {
                            pos = twoFinger.position;
                        }


                        if (GetGameObjectAt(pos, touchCameras[i].camera, touchCameras[i].guiCamera))
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                Debug.LogWarning("No camera is assigned to MInput");
            }
            return false;
        }

        private bool GetGameObjectAt(Vector2 position, Camera cam, bool isGuiCam)
        {

            Ray ray = cam.ScreenPointToRay(position);
            RaycastHit hit;

            if (enable2D)
            {

                LayerMask mask2d = pickableLayers2D;
                RaycastHit2D[] hit2D = new RaycastHit2D[1];
                if (Physics2D.GetRayIntersectionNonAlloc(ray, hit2D, float.PositiveInfinity, mask2d) > 0)
                {
                    pickedObject.pickedCamera = cam;
                    pickedObject.isGUI = isGuiCam;
                    pickedObject.pickedObj = hit2D[0].collider.gameObject;
                    return true;
                }
            }

            LayerMask mask = pickableLayers3D;

            if (Physics.Raycast(ray, out hit, float.MaxValue, mask))
            {
                pickedObject.pickedCamera = cam;
                pickedObject.isGUI = isGuiCam;
                pickedObject.pickedObj = hit.collider.gameObject;
                return true;
            }

            return false;
        }

        private SwipeDirection GetSwipe(Vector2 start, Vector2 end)
        {

            Vector2 linear;
            linear = (end - start).normalized;

            if (Vector2.Dot(linear, Vector2.up) >= swipeTolerance)
                return SwipeDirection.Up;

            if (Vector2.Dot(linear, -Vector2.up) >= swipeTolerance)
                return SwipeDirection.Down;

            if (Vector2.Dot(linear, Vector2.right) >= swipeTolerance)
                return SwipeDirection.Right;

            if (Vector2.Dot(linear, -Vector2.right) >= swipeTolerance)
                return SwipeDirection.Left;

            if (Vector2.Dot(linear, new Vector2(0.5f, 0.5f).normalized) >= swipeTolerance)
                return SwipeDirection.UpRight;

            if (Vector2.Dot(linear, new Vector2(0.5f, -0.5f).normalized) >= swipeTolerance)
                return SwipeDirection.DownRight;

            if (Vector2.Dot(linear, new Vector2(-0.5f, 0.5f).normalized) >= swipeTolerance)
                return SwipeDirection.UpLeft;

            if (Vector2.Dot(linear, new Vector2(-0.5f, -0.5f).normalized) >= swipeTolerance)
                return SwipeDirection.DownLeft;

            return SwipeDirection.Other;
        }

        private bool FingerInTolerance(MFinger finger)
        {

            if ((finger.position - finger.startPosition).sqrMagnitude <= (StationaryTolerance * StationaryTolerance))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsTouchOverNGui(Vector2 position, bool isTwoFingers = false)
        {

            bool returnValue = false;

            if (enabledNGuiMode)
            {

                LayerMask mask = nGUILayers;
                RaycastHit hit;

                int i = 0;
                while (!returnValue && i < nGUICameras.Count)
                {
                    Vector2 pos = Vector2.zero;
                    if (!isTwoFingers)
                    {
                        pos = position;//fingers[touchIndex].position;
                    }
                    else
                    {
                        pos = twoFinger.position;
                    }
                    Ray ray = nGUICameras[i].ScreenPointToRay(pos);

                    returnValue = Physics.Raycast(ray, out hit, float.MaxValue, mask);
                    i++;
                }

            }

            return returnValue;

        }

        private MFinger GetFinger(int finderId)
        {
            int t = 0;

            MFinger fing = null;

            while (t < 10 && fing == null)
            {
                if (fingers[t] != null)
                {
                    if (fingers[t].fingerIndex == finderId)
                    {
                        fing = fingers[t];
                    }
                }
                t++;
            }

            return fing;
        }
        #endregion

        #region Unity UI
        private bool IsScreenPositionOverUI(Vector2 position)
        {

            uiEventSystem = EventSystem.current;
            if (uiEventSystem != null)
            {

                uiPointerEventData = new PointerEventData(uiEventSystem);
                uiPointerEventData.position = position;

                uiEventSystem.RaycastAll(uiPointerEventData, uiRaycastResultCache);
                if (uiRaycastResultCache.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private GameObject GetFirstUIElementFromCache()
        {

            if (uiRaycastResultCache.Count > 0)
            {
                return uiRaycastResultCache[0].gameObject;
            }
            else
            {
                return null;
            }
        }

        private GameObject GetFirstUIElement(Vector2 position)
        {

            if (IsScreenPositionOverUI(position))
            {
                return GetFirstUIElementFromCache();
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Static Method
        // Unity UI compatibility
        public static bool IsFingerOverUIElement(int fingerIndex)
        {
            if (MInput.Instance != null)
            {
                MFinger finger = MInput.Instance.GetFinger(fingerIndex);
                if (finger != null)
                {
                    return MInput.Instance.IsScreenPositionOverUI(finger.position);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static GameObject GetCurrentPickedUIElement(int fingerIndex, bool isTwoFinger)
        {
            if (MInput.Instance != null)
            {
                MFinger finger = MInput.Instance.GetFinger(fingerIndex);
                if (finger != null || isTwoFinger)
                {
                    Vector2 pos = Vector2.zero;
                    if (!isTwoFinger)
                    {
                        pos = finger.position;
                    }
                    else
                    {
                        pos = MInput.Instance.twoFinger.position;
                    }
                    return MInput.Instance.GetFirstUIElement(pos);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static GameObject GetCurrentPickedObject(int fingerIndex, bool isTwoFinger)
        {

            if (MInput.Instance != null)
            {
                MFinger finger = MInput.Instance.GetFinger(fingerIndex);

                if ((finger != null || isTwoFinger) && MInput.Instance.GetPickedGameObject(finger, isTwoFinger))
                {
                    return MInput.Instance.pickedObject.pickedObj;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        public static GameObject GetGameObjectAt(Vector2 position, bool isTwoFinger = false)
        {


            if (MInput.Instance != null)
            {

                if (isTwoFinger) position = MInput.Instance.twoFinger.position;
                if (MInput.Instance.touchCameras.Count > 0)
                {
                    for (int i = 0; i < MInput.Instance.touchCameras.Count; i++)
                    {
                        if (MInput.Instance.touchCameras[i].camera != null && MInput.Instance.touchCameras[i].camera.enabled)
                        {
                            if (MInput.Instance.GetGameObjectAt(position, MInput.Instance.touchCameras[i].camera, MInput.Instance.touchCameras[i].guiCamera))
                            {
                                return MInput.Instance.pickedObject.pickedObj;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }

            return null;
        }


        public static int GetTouchCount()
        {
            if (MInput.Instance)
            {
                return MInput.Instance.input.TouchCount();
            }
            else
            {
                return 0;
            }
        }

        public static void ResetTouch(int fingerIndex)
        {
            if (MInput.Instance)
                MInput.Instance.GetFinger(fingerIndex).gesture = GestureType.None;
        }


        public static void SetEnabled(bool enable)
        {
            MInput.Instance.enable = enable;
            if (enable)
            {
                MInput.Instance.ResetTouches();
            }
        }
        public static bool GetEnabled()
        {
            if (MInput.Instance)
                return MInput.Instance.enable;
            else
                return false;
        }

        public static void SetEnableUIDetection(bool enable)
        {
            if (MInput.Instance != null)
            {
                MInput.Instance.allowUIDetection = enable;
            }
        }
        public static bool GetEnableUIDetection()
        {
            if (MInput.Instance)
            {
                return MInput.Instance.allowUIDetection;
            }
            else
            {
                return false;
            }
        }

        public static void SetUICompatibily(bool value)
        {
            if (MInput.Instance != null)
            {
                MInput.Instance.enableUIMode = value;
            }
        }
        public static bool GetUIComptability()
        {
            if (MInput.Instance != null)
            {
                return MInput.Instance.enableUIMode;
            }
            else
            {
                return false;
            }
        }

        public static void SetAutoUpdateUI(bool value)
        {
            if (MInput.Instance)
                MInput.Instance.autoUpdatePickedUI = value;
        }
        public static bool GetAutoUpdateUI()
        {
            if (MInput.Instance)
                return MInput.Instance.autoUpdatePickedUI;
            else
                return false;
        }

        public static void SetNGUICompatibility(bool value)
        {
            if (MInput.Instance)
                MInput.Instance.enabledNGuiMode = value;
        }
        public static bool GetNGUICompatibility()
        {
            if (MInput.Instance)
                return MInput.Instance.enabledNGuiMode;
            else
                return false;
        }


        public static void SetEnableAutoSelect(bool value)
        {
            if (MInput.Instance)
                MInput.Instance.autoSelect = value;
        }
        public static bool GetEnableAutoSelect()
        {
            if (MInput.Instance)
                return MInput.Instance.autoSelect;
            else
                return false;
        }

        public static void SetAutoUpdatePickedObject(bool value)
        {
            if (MInput.Instance)
                MInput.Instance.autoUpdatePickedObject = value;
        }
        public static bool GetAutoUpdatePickedObject()
        {
            if (MInput.Instance)
                return MInput.Instance.autoUpdatePickedObject;
            else
                return false;
        }

        public static void Set3DPickableLayer(LayerMask mask)
        {
            if (MInput.Instance)
                MInput.Instance.pickableLayers3D = mask;
        }
        public static LayerMask Get3DPickableLayer()
        {
            if (MInput.Instance)
                return MInput.Instance.pickableLayers3D;
            else
                return LayerMask.GetMask("Default");
        }

        public static void AddCamera(Camera cam, bool guiCam = false)
        {
            if (MInput.Instance)
                MInput.Instance.touchCameras.Add(new MCamera(cam, guiCam));
        }
        public static void RemoveCamera(Camera cam)
        {
            if (MInput.Instance)
            {

                int result = MInput.Instance.touchCameras.FindIndex(
                    delegate (MCamera c)
                    {
                        return c.camera == cam;
                    }
                );

                if (result > -1)
                {
                    MInput.Instance.touchCameras[result] = null;
                    MInput.Instance.touchCameras.RemoveAt(result);

                }

            }

        }

        public static Camera GetCamera(int index = 0)
        {
            if (MInput.Instance)
            {
                if (index < MInput.Instance.touchCameras.Count)
                {
                    return MInput.Instance.touchCameras[index].camera;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static void SetEnable2DCollider(bool value)
        {
            if (MInput.Instance)
                MInput.Instance.enable2D = value;
        }
        public static bool GetEnable2DCollider()
        {
            if (MInput.Instance)
                return MInput.Instance.enable2D;
            else
                return false;
        }

        public static void Set2DPickableLayer(LayerMask mask)
        {
            if (MInput.Instance)
                MInput.Instance.pickableLayers2D = mask;
        }
        public static LayerMask Get2DPickableLayer()
        {
            if (MInput.Instance)
                return MInput.Instance.pickableLayers2D;
            else
                return LayerMask.GetMask("Default");
        }


        public static void SetGesturePriority(GesturePriority value)
        {
            if (MInput.Instance)
                MInput.Instance.gesturePriority = value;
        }
        public static GesturePriority GetGesturePriority()
        {
            if (MInput.Instance)
                return MInput.Instance.gesturePriority;
            else
                return GesturePriority.Tap;
        }

        public static void SetStationaryTolerance(float tolerance)
        {
            if (MInput.Instance)
                MInput.Instance.StationaryTolerance = tolerance;
        }
        public static float GetStationaryTolerance()
        {
            if (MInput.Instance)
                return MInput.Instance.StationaryTolerance;
            else
                return -1;
        }

        public static void SetLongTapTime(float time)
        {
            if (MInput.Instance)
                MInput.Instance.longTapTime = time;
        }
        public static float GetlongTapTime()
        {
            if (MInput.Instance)
                return MInput.Instance.longTapTime;
            else
                return -1;
        }

        public static void SetDoubleTapTime(float time)
        {
            if (MInput.Instance)
                MInput.Instance.doubleTapTime = time;
        }
        public static float GetDoubleTapTime()
        {
            if (MInput.Instance)
                return MInput.Instance.doubleTapTime;
            else
                return -1;
        }

        public static void SetDoubleTapMethod(DoubleTapDetection detection)
        {
            if (MInput.Instance)
                MInput.Instance.doubleTapDetection = detection;
        }
        public static MInput.DoubleTapDetection GetDoubleTapMethod()
        {
            if (MInput.Instance)
                return MInput.Instance.doubleTapDetection;
            else
                return MInput.DoubleTapDetection.BySystem;
        }

        public static void SetSwipeTolerance(float tolerance)
        {
            if (MInput.Instance)
                MInput.Instance.swipeTolerance = tolerance;
        }
        public static float GetSwipeTolerance()
        {
            if (MInput.Instance)
                return MInput.Instance.swipeTolerance;
            else
                return -1;
        }


        public static void SetEnable2FingersGesture(bool enable)
        {
            if (MInput.Instance)
                MInput.Instance.enable2FingersGesture = enable;
        }
        public static bool GetEnable2FingersGesture()
        {
            if (MInput.Instance)
                return MInput.Instance.enable2FingersGesture;
            else
                return false;
        }

        public static void SetTwoFingerPickMethod(MInput.TwoFingerPickMethod pickMethod)
        {
            if (MInput.Instance)
                MInput.Instance.twoFingerPickMethod = pickMethod;
        }
        public static MInput.TwoFingerPickMethod GetTwoFingerPickMethod()
        {
            if (MInput.Instance)
                return MInput.Instance.twoFingerPickMethod;
            else
                return MInput.TwoFingerPickMethod.Finger;
        }

        public static void SetEnablePinch(bool enable)
        {
            if (MInput.Instance)
                MInput.Instance.enablePinch = enable;
        }
        public static bool GetEnablePinch()
        {
            if (MInput.Instance)
                return MInput.Instance.enablePinch;
            else
                return false;
        }

        public static void SetMinPinchLength(float length)
        {
            if (MInput.Instance)
                MInput.Instance.minPinchLength = length;
        }
        public static float GetMinPinchLength()
        {
            if (MInput.Instance)
                return MInput.Instance.minPinchLength;
            else
                return -1;
        }

        public static void SetEnableTwist(bool enable)
        {
            if (MInput.Instance)
                MInput.Instance.enableTwist = enable;
        }
        public static bool GetEnableTwist()
        {
            if (MInput.Instance)
                return MInput.Instance.enableTwist;
            else
                return false;
        }

        public static void SetMinTwistAngle(float angle)
        {
            if (MInput.Instance)
                MInput.Instance.minTwistAngle = angle;
        }
        public static float GetMinTwistAngle()
        {
            if (MInput.Instance)
                return MInput.Instance.minTwistAngle;
            else
                return -1;
        }

        public static bool GetSecondeFingerSimulation()
        {

            if (MInput.Instance != null)
            {
                return MInput.Instance.enableSimulation;
            }
            else
            {
                return false;
            }
        }
        public static void SetSecondFingerSimulation(bool value)
        {
            if (MInput.Instance != null)
            {
                MInput.Instance.enableSimulation = value;
            }
        }
        #endregion

    }
}