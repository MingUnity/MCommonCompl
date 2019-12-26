using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    /// <summary>
    /// Toggle扩展
    /// </summary>
    public class ToggleEx : Toggle
    {
        /// <summary>
        /// 文本
        /// </summary>
        public Text text;

        /// <summary>
        /// 普通态 文本颜色
        /// </summary>
        public Color normalTextColor = Color.white;

        /// <summary>
        /// 悬浮态 文本颜色
        /// </summary>
        public Color highlightedTextColor = Color.white;

        /// <summary>
        /// 点击态 文本颜色
        /// </summary>
        public Color pressedTextColor = Color.white;

        /// <summary>
        /// 禁用态 文本颜色
        /// </summary>
        public Color disabledTextColor = Color.white;

        /// <summary>
        /// 选中态 文本颜色
        /// </summary>
        public Color isOnTextColor = Color.white;

        /// <summary>
        /// 普通态事件
        /// </summary>
        public UnityEvent onNormal = new UnityEvent();

        /// <summary>
        /// 高亮态事件
        /// </summary>
        public UnityEvent onHighlighted = new UnityEvent();

        /// <summary>
        /// 点击态
        /// </summary>
        public UnityEvent onPressed = new UnityEvent();

        /// <summary>
        /// 禁用态 
        /// </summary>
        public UnityEvent onDisabled = new UnityEvent();

        /// <summary>
        /// 进入悬浮态
        /// </summary>
        public UnityEvent onHoverEnter = new UnityEvent();

        /// <summary>
        /// 退出悬浮态
        /// </summary>
        public UnityEvent onHoverExit = new UnityEvent();

        /// <summary>
        /// 是否为普通态
        /// </summary>
        public bool IsNormal { get; set; }

        protected override void Awake()
        {
            base.Awake();

            onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            OnValueChanged(isOn);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            OnValueChanged(false);
        }

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);

            if (!isOn)
            {
                switch (state)
                {
                    case SelectionState.Normal:
                        if (text != null)
                        {
                            text.color = normalTextColor;
                        }
                        onNormal.Invoke();
                        break;

                    case SelectionState.Highlighted:
                        if (text != null)
                        {
                            text.color = highlightedTextColor;
                        }
                        onHighlighted.Invoke();
                        break;

                    case SelectionState.Pressed:
                        if (text != null)
                        {
                            text.color = pressedTextColor;
                        }
                        onPressed.Invoke();
                        break;

                    case SelectionState.Disabled:
                        if (text != null)
                        {
                            text.color = disabledTextColor;
                        }
                        onDisabled.Invoke();
                        break;
                }
            }

            IsNormal = !isOn && state == SelectionState.Normal;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(bool value)
        {
            if (text != null)
            {
                if (value)
                {
                    text.color = isOnTextColor;
                }
                else
                {
                    text.color = normalTextColor;
                }
            }
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            onHoverEnter?.Invoke();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            onHoverExit?.Invoke();
        }
    }
}
