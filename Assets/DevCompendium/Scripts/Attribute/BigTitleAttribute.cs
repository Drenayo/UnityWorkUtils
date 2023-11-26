using UnityEditor;
using UnityEngine;

namespace DevCompendium
{
    // 创建一个特性类，带有字体大小参数
    public class BigTitleAttribute : PropertyAttribute
    {
        public int fontSize = 15; // 字体大小
    }

    // 创建一个自定义属性绘制器类
    [CustomPropertyDrawer(typeof(BigTitleAttribute))]
    public class BigTitleDrawer : PropertyDrawer
    {
        // 重写OnGUI方法，实现自定义的绘制逻辑
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 获取特性参数
            BigTitleAttribute bigTitleAttribute = (BigTitleAttribute)attribute;

            // 获取属性的字符串值
            string stringValue = property.stringValue;

            // 创建GUIStyle，并设置字体大小
            GUIStyle style = new GUIStyle(EditorStyles.boldLabel);
            style.fontSize = bigTitleAttribute.fontSize;

            // 将字符串值绘制成标题，使用自定义的字体大小
            EditorGUI.LabelField(position, stringValue, style);
        }
    }
}
