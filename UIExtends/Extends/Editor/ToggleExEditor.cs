using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(ToggleEx), true)]
[CanEditMultipleObjects]
public class ToggleExEditor : ToggleEditor
{
    private SerializedProperty _text;

    private SerializedProperty _clrNormal;
    private SerializedProperty _clrHighlighted;
    private SerializedProperty _clrPressed;
    private SerializedProperty _clrDisabled;
    private SerializedProperty _clrIsOn;

    protected override void OnEnable()
    {
        base.OnEnable();

        _text = serializedObject.FindProperty("_text");
        _clrNormal = serializedObject.FindProperty("_normalTextColor");
        _clrHighlighted = serializedObject.FindProperty("_highlightedTextColor");
        _clrPressed = serializedObject.FindProperty("_pressedTextColor");
        _clrDisabled = serializedObject.FindProperty("_disabledTextColor");
        _clrIsOn = serializedObject.FindProperty("_isOnTextColor");
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
            EditorGUILayout.PropertyField(_clrIsOn);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
