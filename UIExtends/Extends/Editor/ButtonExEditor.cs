using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
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

    protected override void OnEnable()
    {
        base.OnEnable();

        _text = serializedObject.FindProperty("_text");
        _clrNormal = serializedObject.FindProperty("_normalTextColor");
        _clrHighlighted = serializedObject.FindProperty("_highlightedTextColor");
        _clrPressed = serializedObject.FindProperty("_pressedTextColor");
        _clrDisabled = serializedObject.FindProperty("_disabledTextColor");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_text);

        if (_text.objectReferenceValue != null)
        {
            if (!Application.isPlaying)
            {
                Text text = _text.objectReferenceValue as Text;

                _clrNormal.colorValue = text.color;
            }
            
            EditorGUILayout.PropertyField(_clrHighlighted);
            EditorGUILayout.PropertyField(_clrPressed);
            EditorGUILayout.PropertyField(_clrDisabled);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
