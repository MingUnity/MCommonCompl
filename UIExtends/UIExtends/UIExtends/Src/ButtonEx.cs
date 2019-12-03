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
        [SerializeField]
        private Text _text;

        /// <summary>
        /// 普通态 文本颜色
        /// </summary>
        [SerializeField]
        private Color _normalTextColor = Color.white;

        /// <summary>
        /// 高亮态 文本颜色
        /// </summary>
        [SerializeField]
        private Color _highlightedTextColor = Color.white;

        /// <summary>
        /// 点击态 文本颜色
        /// </summary>
        [SerializeField]
        private Color _pressedTextColor = Color.white;

        /// <summary>
        /// 禁用态 文本颜色
        /// </summary>
        [SerializeField]
        private Color _disabledTextColor = Color.white;

        /// <summary>
        /// 普通态事件
        /// </summary>
        [SerializeField]
        public UnityEvent onNormal = new UnityEvent();

        /// <summary>
        /// 高亮态事件
        /// </summary>
        [SerializeField]
        public UnityEvent onHighlighted = new UnityEvent();

        /// <summary>
        /// 点击态
        /// </summary>
        [SerializeField]
        public UnityEvent onPressed = new UnityEvent();

        /// <summary>
        /// 禁用态 
        /// </summary>
        [SerializeField]
        public UnityEvent onDisabled = new UnityEvent();

        /// <summary>
        /// 进入悬浮态
        /// </summary>
        [SerializeField]
        public UnityEvent onHoverEnter = new UnityEvent();

        /// <summary>
        /// 退出悬浮态
        /// </summary>
        [SerializeField]
        public UnityEvent onHoverExit = new UnityEvent();

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);

            switch (state)
            {
                case SelectionState.Normal:
                    if (_text != null)
                    {
                        _text.color = _normalTextColor;

                    }
                    onNormal.Invoke();
                    break;

                case SelectionState.Highlighted:
                    if (_text != null)
                    {
                        _text.color = _highlightedTextColor;
                    }
                    onHighlighted.Invoke();
                    break;

                case SelectionState.Pressed:
                    if (_text != null)
                    {
                        _text.color = _pressedTextColor;
                    }
                    onPressed.Invoke();
                    break;

                case SelectionState.Disabled:
                    if (_text != null)
                    {
                        _text.color = _disabledTextColor;
                    }
                    onDisabled.Invoke();
                    break;
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
