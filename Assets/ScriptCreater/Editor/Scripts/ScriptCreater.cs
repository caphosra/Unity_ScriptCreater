#pragma warning disable 0168

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScriptCreater : EditorWindow
{
    private static string path;
    
    public static void Show(string path)
    {
        ScriptCreater.path = path;
        GetWindow<ScriptCreater>();
    }

    private string saveFilePath;
    private List<string> argNames;
    private List<string> args;

    void OnGUI()
    {
        //
        // Load the resource
        //
        var table = AssetDatabase.LoadAssetAtPath<ScriptTemplateTable>(path);

        #region NullCheck

        if (saveFilePath == null) saveFilePath = table.DefaultPath;
        if (argNames == null) argNames = table.ReplaceWords;
        if (args == null) args = new List<string>(new string[argNames.Count]);

        #endregion

        #region EditorGUILayout Path

        EditorGUILayout.LabelField("Path");
        EditorGUI.BeginDisabledGroup(!table.CanEditDefaultPath);
        {
            saveFilePath = EditorGUILayout.TextField(saveFilePath);
        }
        EditorGUI.EndDisabledGroup();

        #endregion

        EditorGUILayout.Space();

        #region EditorGUILayout Arguments

        if (argNames.Count != 0)
        {
            EditorGUILayout.LabelField("Arguments");

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                for (int i = 0; i < argNames.Count; i++)
                {
                    if (args[i] == null) args[i] = "";
                    args[i] = EditorGUILayout.TextField(argNames[i], args[i]);
                }
            }
            EditorGUILayout.EndVertical();
        }

        #endregion

        EditorGUILayout.Space();

        if (GUILayout.Button("Create"))
        {
            #region CreateFile

            string text = table.Text;

            for(int i = 0; i < argNames.Count; i++)
            {
                text = text.Replace(argNames[i], args[i]);
            }

            //
            // Received words
            //
            text = text.Replace("[FileName]", Path.GetFileNameWithoutExtension(saveFilePath));
            text = text.Replace("[FileFullName]", Path.GetFileName(saveFilePath));

            try
            {
                var str = saveFilePath;

                if(File.Exists(str))
                {
                    for(int i = 1; ; i++)
                    {
                        str += "(" + i + ")";
                        if(File.Exists(str))
                        {
                            str = saveFilePath;
                        }
                        else
                        {
                            saveFilePath = str;
                            break;
                        }
                    }
                }

                File.WriteAllText(saveFilePath, text);
                AssetDatabase.ImportAsset(saveFilePath);
            }
            catch(DirectoryNotFoundException e)
            {
                EditorUtility.DisplayDialog("System.IO.DirectoryNotFoundException", "Can't find the floder !", "OK");
            }
            catch (System.Exception e)
            {
                EditorUtility.DisplayDialog("System.Exception", "Throwed Exception !", "OK");
            }

            #endregion

            Init();
            Close();
        }

        if (GUILayout.Button("Cancel"))
        {
            Init();
            Close();
        }
    }

    void Init()
    {
        saveFilePath = null;
        argNames = null;
        args = null;
    }
}

