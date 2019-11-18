using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

    public UnityEvent onNormal = new UnityEvent();
    public UnityEvent onHighlighted = new UnityEvent();
    public UnityEvent onPressed = new UnityEvent();
    public UnityEvent onDisabled = new UnityEvent();

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
}
