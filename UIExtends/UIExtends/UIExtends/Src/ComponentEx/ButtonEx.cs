using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    /// <summary>
    /// UGUI按钮扩展
    /// </summary>
    public class ButtonEx : Button
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
        /// 高亮态 文本颜色
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

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);

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

            IsNormal = state == SelectionState.Normal;
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
