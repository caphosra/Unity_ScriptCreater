using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "ScriptCreater/ScriptTemplateTable")]
public class ScriptTemplateTable : ScriptableObject
{
    [SerializeField]
    private string defaultPath;
    public string DefaultPath { get { return defaultPath; } }

    [SerializeField]
    private bool canEditDefaultPath;
    public bool CanEditDefaultPath { get { return canEditDefaultPath; } }

    [SerializeField]
    private List<string> replaceWords;
    public List<string> ReplaceWords { get { return replaceWords; } }

    [SerializeField]
    private string text;
    public string Text { get { return text; } }

    [CustomEditor(typeof(ScriptTemplateTable))]
    public class ScriptTemplateTableEx : Editor
    {
        ScriptTemplateTable Target { get { return target as ScriptTemplateTable; } }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("DefaultPath");
            Target.defaultPath = EditorGUILayout.TextField(Target.defaultPath);

            EditorGUILayout.Space();

            Target.canEditDefaultPath = EditorGUILayout.Toggle("CanEditDefaultPath", Target.canEditDefaultPath);

            EditorGUILayout.Space();

            #region ReplaceWords

            if (Target.replaceWords == null)
                Target.replaceWords = new List<string>();

            EditorGUILayout.LabelField("ReplaceWords");

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                for (int i = 0; i < Target.replaceWords.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField("Arg" + i);
                        Target.replaceWords[i] = EditorGUILayout.TextField(Target.replaceWords[i]);
                    }
                    EditorGUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Add"))
                {
                    Target.replaceWords.Add("");
                }
                if (GUILayout.Button("Remove Last Index"))
                {
                    if (Target.replaceWords.Count != 0)
                        Target.replaceWords.RemoveAt(Target.replaceWords.Count - 1);
                }
            }
            EditorGUILayout.EndVertical();

            #endregion

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Text");
            Target.text = EditorGUILayout.TextArea(Target.text, GUILayout.Height(300f));

            EditorUtility.SetDirty(Target);
        }
    }
}
