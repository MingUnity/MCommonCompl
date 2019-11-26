using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Toggle扩展
/// </summary>
public class ToggleEx : Toggle
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
    /// 悬浮态 文本颜色
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
    /// 选中态 文本颜色
    /// </summary>
    [SerializeField]
    private Color _isOnTextColor = Color.white;

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

    protected override void Awake()
    {
        base.Awake();

        onValueChanged.AddListener(OnValueChanged);
    }

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);

        if (!isOn)
        {
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
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(bool value)
    {
        if (_text != null)
        {
            if (value)
            {
                _text.color = _isOnTextColor;
            }
            else
            {
                _text.color = _normalTextColor;
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

