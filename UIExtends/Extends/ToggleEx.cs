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

    public UnityEvent onNormal = new UnityEvent();
    public UnityEvent onHighlighted = new UnityEvent();
    public UnityEvent onPressed = new UnityEvent();
    public UnityEvent onDisabled = new UnityEvent();

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
}

