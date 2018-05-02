using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AddTemplate : Editor
{
    const string FOLDER_PATH = "Assets/ScriptCreater/Editor/Templates/";
    const string MENU_PATH = "Assets/Create/Template/";

    [MenuItem(MENU_PATH + "C# Class")]
    static void CreateClass()
    {
        ScriptCreater.Show(FOLDER_PATH + "CSharpClass.asset");
    }

    [MenuItem(MENU_PATH + "C# Struct")]
    static void CreateStruct()
    {
        ScriptCreater.Show(FOLDER_PATH + "CSharpStruct.asset");
    }

    [MenuItem(MENU_PATH + "MITLicense")]
    static void CreateMitLicense()
    {
        ScriptCreater.Show(FOLDER_PATH + "MITLicense.asset");
    }

    [MenuItem(MENU_PATH + "README")]
    static void CreateREADME()
    {
        ScriptCreater.Show(FOLDER_PATH + "README.asset");
    }

    [MenuItem(MENU_PATH + "AddTemplate")]
    static void CreateAddTemplate()
    {
        ScriptCreater.Show(FOLDER_PATH + "AddTemplate.asset");
    }
}


