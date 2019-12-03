using UnityEditor;
using UnityEditor.UI;
using UnityEngine.UI;

[CanEditMultipleObjects]
[CustomEditor(typeof(ButtonEx), true)]
public class ButtonExEditor : ButtonEditor
{
    private SerializedProperty _text;
    private SerializedProperty _clrNormal;
    private SerializedProperty _clrHighlighted;
    private SerializedProperty _clrPressed;
    private SerializedProperty _clrDisabled;
    private SerializedProperty _onNormal;
    private SerializedProperty _onHighlighted;
    private SerializedProperty _onPressed;
    private SerializedProperty _onDisabled;
    private SerializedProperty _onHoverEnter;
    private SerializedProperty _onHoverExit;

    protected override void OnEnable()
    {
        base.OnEnable();

        _text = serializedObject.FindProperty("_text");
        _clrNormal = serializedObject.FindProperty("_normalTextColor");
        _clrHighlighted = serializedObject.FindProperty("_highlightedTextColor");
        _clrPressed = serializedObject.FindProperty("_pressedTextColor");
        _clrDisabled = serializedObject.FindProperty("_disabledTextColor");
        _onNormal = serializedObject.FindProperty("onNormal");
        _onHighlighted = serializedObject.FindProperty("onHighlighted");
        _onPressed = serializedObject.FindProperty("onPressed");
        _onDisabled = serializedObject.FindProperty("onDisabled");
        _onHoverEnter = serializedObject.FindProperty("onHoverEnter");
        _onHoverExit = serializedObject.FindProperty("onHoverExit");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_text);

        if (_text.objectReferenceValue != null)
        {
            EditorGUILayout.PropertyField(_clrNormal);
            EditorGUILayout.PropertyField(_clrHighlighted);
            EditorGUILayout.PropertyField(_clrPressed);
            EditorGUILayout.PropertyField(_clrDisabled);
        }

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_onNormal);
        EditorGUILayout.PropertyField(_onHighlighted);
        EditorGUILayout.PropertyField(_onPressed);
        EditorGUILayout.PropertyField(_onDisabled);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_onHoverEnter);
        EditorGUILayout.PropertyField(_onHoverExit);

        serializedObject.ApplyModifiedProperties();
    }
}
