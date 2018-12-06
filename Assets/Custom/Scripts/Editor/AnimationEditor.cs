using UnityEditor;

[CustomEditor(typeof(EnumeratedAnimation.Animation), true)]
public class AnimationEditor : Editor
{
    private SerializedObject anim;

    private SerializedProperty playTime;
    private SerializedProperty targetObject;
    private SerializedProperty animationMode;
    private SerializedProperty animationName;
    private SerializedProperty events;
    private SerializedProperty autoPause;

    private static bool useEvents;

    public void OnEnable()
    {
        anim = new SerializedObject(target);

        playTime = anim.FindProperty("playTime");
        targetObject = anim.FindProperty("target");
        animationMode = anim.FindProperty("animationmode");
        animationName = anim.FindProperty("animationName");
        events = anim.FindProperty("events");
        autoPause = anim.FindProperty("autoPause");
    }

    public override void OnInspectorGUI()
    {
        anim.Update();

        EditorGUILayout.PropertyField(playTime);
        EditorGUILayout.PropertyField(targetObject);
        EditorGUILayout.PropertyField(animationMode);
        if (animationMode.enumValueIndex == (int)EnumeratedAnimation.Animation.AnimationMode.PlayAnimation)
        {
            EditorGUILayout.PropertyField(animationName);
        }
        useEvents = EditorGUILayout.Foldout(useEvents, "Events");
        if (useEvents)
        {
            EditorGUILayout.PropertyField(events);
        }
        EditorGUILayout.PropertyField(autoPause);

        anim.ApplyModifiedProperties();
    }
}