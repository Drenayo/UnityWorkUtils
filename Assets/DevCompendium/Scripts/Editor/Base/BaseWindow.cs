using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace DevCompendium
{
	/// <summary>
	/// 子树窗口基类
	/// </summary>
	public class BaseWindow : ScriptableObject
	{
		[BigTitle(fontSize = 30)]
		public string title = "标题";
		[BigTitle(fontSize = 13)]
		public string introduce = "详细信息详细信息详细信息详细信息详细信息详细信息详细信息详细信息详细信息详细信息详细信息详细信息详细信息详细信息详细信息详细信息详细信息";
	}
}