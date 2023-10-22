using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

public class ToDoWindow : OdinEditorWindow
{
    [MenuItem("Tools/ToDo Window")]
	private static void OpenWindow()
    {
        GetWindow<ToDoWindow>().Show();
    }



    [LabelText("待办事项列表")]
    [HideReferenceObjectPicker]
    [ListDrawerSettings(Expanded = true, DraggableItems = true)]
    [SerializeField]
    private List<TodoItem> items = new List<TodoItem>();

    [CustomValueDrawer("DrawCustomTodoList")]
    public List<TodoItem> Items
    {
        get { return items; }
        set { items = value; }
    }

    private List<TodoItem> DrawCustomTodoList(GUIContent label, List<TodoItem> value, GUIContent context)
    {
        // 在这里你可以自定义绘制待办事项列表的UI
        if (value == null)
            value = new List<TodoItem>();

        foreach (var item in value)
        {
            GUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("标题: " + item.Title);
            EditorGUILayout.LabelField("描述: " + item.Description);
            EditorGUILayout.LabelField("标签: " + string.Join(", ", item.Tags));
            item.IsCompleted = EditorGUILayout.Toggle("是否完成", item.IsCompleted);
            GUILayout.EndVertical();
        }

        return value;
    }
}
public class TodoItem
{
    [LabelText("标题")]
    public string Title;

    [LabelText("描述")]
    public string Description;

    [LabelText("标签")]
    public string[] Tags;

    [LabelText("是否完成")]
    public bool IsCompleted;

    // 这里你可以添加其他需要的字段
}