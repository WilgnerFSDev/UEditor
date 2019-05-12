using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using UnityEditor;

public class SideMenuWindow : EditorWindow {

    // General
    static string[] menuItems = new string[] { "Welcome", "Menu Item 1", "Menu Item 2", "Menu Item 3" };
    string selectedMenu = menuItems[0];
    string version = "1.0";  
    bool stylesNotLoaded = true;

    // Colors
    Color menuBackgroundColor = new Color32(67, 66, 67, 255);
    Color selectedMenuColor = new Color32(60, 95, 154, 255);

    // Fonts
    int headerFontSize = 24;
    int menuItemFontSize = 16;

    // Style
    static GUIStyle titleStyle, menuStyle, menuBackgroundStyle, menuSelectedStyle, publisherStyle, justRichStyleText, horizontalLine;

    // Size Control
    float width;
    int menuWidth;
    int widthContent;

    [MenuItem("Wilgner's Studio/Side Menu Window")]

    // Start Window
    static void ShowWindow() {
        SideMenuWindow window = GetWindow<SideMenuWindow>(true, "WilgnerStudio - Side Menu", true);
        window.minSize = new Vector2(1020, 640);
        window.maxSize = window.minSize;
        //myWindow.titleContent = new GUIContent("Title");
        window.Show();
    }

    private void OnEnable() {
        menuWidth = (int)(position.width * 0.2f);
        widthContent = (int)(position.width * 0.8f);
    }

    private void OnDisable() {
        stylesNotLoaded = true;
    }

    private void LoadConfig() {
        //logo = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Wilgner Studio/Easy Inventory/Images/Editor/easyinventory_logo.png", typeof(Texture));

        menuWidth = (int)(position.width * 0.2f); // 20% width of side menu
        widthContent = (int)(position.width * 0.8f); // 80% width of content

        titleStyle = new GUIStyle(EditorStyles.label)
        {
            alignment = TextAnchor.MiddleLeft,
            richText = true
        };

        menuStyle = new GUIStyle(EditorStyles.label)
        {
            alignment = TextAnchor.MiddleRight,
            richText = true
        };

        publisherStyle = new GUIStyle(EditorStyles.label)
        {
            alignment = TextAnchor.MiddleCenter,
            richText = true
        };

        justRichStyleText = new GUIStyle(EditorStyles.label)
        {
            richText = true
        };


        menuBackgroundStyle = new GUIStyle();
        menuBackgroundStyle.normal.background = MakeTex(menuWidth, 420, menuBackgroundColor);

        menuSelectedStyle = new GUIStyle();
        menuSelectedStyle.normal.background = MakeTex(menuWidth, 20, selectedMenuColor);

        horizontalLine = new GUIStyle();
        horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
        horizontalLine.margin = new RectOffset(0, 0, 4, 4);
        horizontalLine.fixedHeight = 1;

        stylesNotLoaded = false;
    }

    public void OnGUI() {
        if (stylesNotLoaded) LoadConfig();
        EditorGUILayout.BeginHorizontal();
        DrawMenu();

        EditorGUILayout.BeginVertical(GUILayout.Width(widthContent));

        #region Header
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(string.Format("<size={0}>{1}</size>", headerFontSize, selectedMenu), titleStyle);
        EditorGUILayout.EndHorizontal();
        #endregion

        #region Content
        DrawContent();
        #endregion

        #region Footer
        GUILayout.FlexibleSpace();
        HorizontalLine(new Color(33, 33, 33, 255));
        EditorGUILayout.BeginHorizontal(GUILayout.Width(widthContent - 7));
        GUILayout.FlexibleSpace();
        GUILayout.Label(string.Format("<size=12><b>{0}</b></size>", version), justRichStyleText);
        EditorGUILayout.EndHorizontal();
        #endregion

        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

    }

    void DrawMenu() {
        EditorGUILayout.BeginVertical(menuBackgroundStyle, GUILayout.Width(menuWidth));
        //GUILayout.Label(logo, GUILayout.Width(menuWidth));
        GUILayout.Label("", GUILayout.Height(20));
        for (int i = 0; i < menuItems.Length; i++)
        {
            if (selectedMenu == menuItems[i])
                EditorGUILayout.BeginHorizontal(menuSelectedStyle, GUILayout.Width(menuWidth), GUILayout.Height(20));
            else
                EditorGUILayout.BeginHorizontal(GUILayout.Width(menuWidth), GUILayout.Height(20));
            if (GUILayout.Button(string.Format("<size={0}><color=#ffffff>{1}</color></size>", menuItemFontSize, menuItems[i]), menuStyle))
            {
                selectedMenu = menuItems[i];
            }
            EditorGUILayout.EndHorizontal();
        }
        GUILayout.FlexibleSpace();
        #region Footer
        GUILayout.FlexibleSpace();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(string.Format("<size=16><b>{0}</b></size>", "Wilgner's Studio"), publisherStyle);
        EditorGUILayout.EndHorizontal();
        #endregion
        EditorGUILayout.EndVertical();
    }

    void DrawContent() {
        switch (selectedMenu)
        {
            case "Welcome":
                DrawWelcome();
                break;
        }
    }

    void DrawWelcome() {

    }

    Texture2D MakeTex(int width, int height, Color col) {
        var pix = new Color[width * height];

        for (int i = 0; i < pix.Length; i++)
        {
            pix[i] = col;
        }

        var result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    static void HorizontalLine(Color color) {
        var c = GUI.color;
        GUI.color = color;
        GUILayout.Box(GUIContent.none, horizontalLine);
        GUI.color = c;
    }

}