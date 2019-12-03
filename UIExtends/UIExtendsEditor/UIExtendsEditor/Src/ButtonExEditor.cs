using UnityEditor;
using UnityEditor.UI;
using UnityEngine.UI;

[CanEditMultipleObjects]
[CustomEditor(typeof(ButtonEx), true)]
public class ButtonExEditor : ButtonEditor
{
    private SerializedProperty _onNormal;
    private SerializedProperty _onHighlighted;
    private SerializedProperty _onPressed;
    private SerializedProperty _onDisabled;
    private SerializedProperty _onHoverEnter;
    private SerializedProperty _onHoverExit;

    private ButtonEx _src;

    protected override void OnEnable()
    {
        base.OnEnable();

        _onNormal = serializedObject.FindProperty("onNormal");
        _onHighlighted = serializedObject.FindProperty("onHighlighted");
        _onPressed = serializedObject.FindProperty("onPressed");
        _onDisabled = serializedObject.FindProperty("onDisabled");
        _onHoverEnter = serializedObject.FindProperty("onHoverEnter");
        _onHoverExit = serializedObject.FindProperty("onHoverExit");

        _src = target as ButtonEx;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (_src == null)
        {
            return;
        }

        EditorGUILayout.Space();

        _src.text = EditorGUILayout.ObjectField("Text", _src.text, typeof(Text), true) as Text;

        if (_src.text != null)
        {
            _src.normalTextColor = _src.text.color;
            _src.highlightedTextColor = EditorGUILayout.ColorField("HighlightedTextColor", _src.highlightedTextColor);
            _src.pressedTextColor = EditorGUILayout.ColorField("PressedTextColor", _src.pressedTextColor);
            _src.disabledTextColor = EditorGUILayout.ColorField("DisabledTextColor", _src.disabledTextColor);
        }

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_onNormal);
        EditorGUILayout.PropertyField(_onHighlighted);
        EditorGUILayout.PropertyField(_onPressed);
        EditorGUILayout.PropertyField(_onDisabled);
        EditorGUILayout.PropertyField(_onHoverEnter);
        EditorGUILayout.PropertyField(_onHoverExit);

        serializedObject.ApplyModifiedProperties();
    }
}
