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
		[LabelText("标题")]
		public string title;
		[LabelText("介绍"),TextArea]
		public string introduce;
		// 大标题
		// 小标题

		// 方法
	}
}