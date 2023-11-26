using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

namespace DevCompendium
{
	[CreateAssetMenu(fileName = "Data", menuName = "Crearte", order = 0)]
	public class CreateSO : ToolWindow
	{

		[LabelText("创建的名字"),BoxGroup("config"),HorizontalGroup("config/1")]
		public string soName;
		[LabelText("创建的类型"),BoxGroup("config"),HorizontalGroup("config/1")]
		public string soType;
		[LabelText("保存的路径"),BoxGroup("config"),FolderPath()]
		public string soPath;


		[Button("创建节点")]
		public void Create()
		{
			ScriptableObject tempSO = ScriptableObject.CreateInstance(soType);
			
			AssetDatabase.CreateAsset(tempSO as BaseWindow, soPath + "/" + soName + ".asset");
			Debug.Log($"创建{soName}.asset成功，保存在{soPath}处");
			//保存创建的资源
			AssetDatabase.SaveAssets();
			//刷新界面
			AssetDatabase.Refresh();
		}

	}
}