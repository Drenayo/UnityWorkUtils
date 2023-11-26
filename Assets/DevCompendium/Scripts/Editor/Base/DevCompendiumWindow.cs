using System.Collections;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using System.IO;
using Sirenix.Utilities.Editor;

/// <summary>
/// 全局工具集主窗口
/// </summary>
public class DevCompendiumWindow : OdinMenuEditorWindow
{
    public string soPath = "Assets/DevCompendium/Config/SubTreeSO/";


    [MenuItem("Window/开发工具集",false,0)]
    private static void OpenWindow()
    {
        EditorWindow win = GetWindow<DevCompendiumWindow>();
        win.Show();
        win.titleContent.text = "开发工具集";
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        OdinMenuTree tree = new OdinMenuTree();
        // 设置禁止多选
        tree.Selection.SupportsMultiSelect = false;
        // 绘制搜索框
        tree.Config.DrawSearchToolbar = true;
        // 设置树样式
        tree.DefaultMenuStyle = new OdinMenuStyle()
        {
            Height = 25,
            Offset = 16.00f,
            IndentAmount = 15.00f,
            IconSize = 16.00f,
            IconOffset = 0.00f,
            NotSelectedIconAlpha = 0.85f,
            IconPadding = 3.00f,
            TriangleSize = 17.00f,
            TrianglePadding = 8.00f,
            AlignTriangleLeft = false,
            Borders = true,
            BorderPadding = 15.42f,
            BorderAlpha = 0.53f,
            SelectedColorDarkSkin = new Color(0.367f, 0.466f, 0.585f, 1.000f),
            SelectedColorLightSkin = new Color(0.243f, 0.490f, 0.900f, 1.000f)
        };

        // Test
        tree.Add("AA", new TextureUtilityEditor());
        // 从指定文件夹添加treeItem
        tree.AddAllAssetsAtPath("", soPath, typeof(ScriptableObject), true, false);
        // 按名称排序
        tree.SortMenuItemsByName();
        return tree;
    }

    public class TextureUtilityEditor
    {
       
    }
}

//[DrawerPriority(0, 0, 1)] // 调整绘制的优先级
//public class TitleDrawer : OdinDrawer
//{
//    private const string titleText = "这是大标题";

//    protected override void DrawPropertyLayout(GUIContent label)
//    {
//        SirenixEditorGUI.Title(titleText, "", TextAlignment.Center, false);
//    }
//}