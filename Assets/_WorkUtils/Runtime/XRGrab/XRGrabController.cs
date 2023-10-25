// XRGrabController.cs
// 作者：朱佳豪
// 创建日期：2023.10.24
// 脚本描述：手柄类
// 依赖关系：无

// 更新日志：
// 2023.10.24 创建脚本
// 2023.10.25 增加根据手柄类型判断是否拾取物品的逻辑；修复当多个物品重叠，无法正确抓取的逻辑；

// 待完成功能：

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.InputSystem.InputAction;

namespace XRGrabbingSystem
{
	/// <summary>
	/// XRGrab 控制 挂载到手柄
	/// </summary>
	public class XRGrabController : MonoBehaviour
	{
		/// <summary>
		/// 手柄类型
		/// </summary>
		public HandType handType;
		/// <summary>
		/// 抓握物品时是否隐藏手柄模型
		/// </summary>
		public bool hideHandModel;
		/// <summary>
		/// 射线发射方向
		/// </summary>
		public Transform rayOrigin;
		/// <summary>
		/// 手部的物品抓握位置
		/// </summary>
		public Transform attach;
		/// <summary>
		/// 手柄模型
		/// </summary>
		[ReadOnly]
		public Transform handModelTran;
		/// <summary>
		/// 该控制器是否抓握了物品
		/// </summary>
		public bool isGrabbing;
		/// <summary>
		/// 已经被抓握的物品
		/// </summary>
		[ReadOnly]
		public XRBaseGrabItem grabbedItem;
		/// <summary>
		/// 手柄悬浮的物品
		/// </summary>
		[ReadOnly]
		public XRBaseGrabItem hoveringOverItem;



		private void Start()
		{
			XRInputObserver.Instance.RegisterXRkey(OnGripButton, KeyType.Grip, handType);
			//Debug.Log("注册成功" + handType.ToString());
			handModelTran = transform.GetComponent<ActionBasedController>().modelParent.transform;
		}


		public void OnGripButton(CallbackContext context)
		{
			//Debug.Log("GripButton" + handType.ToString());
			if (context.started)
			{
				// 如果手里没东西，则是拿取
				if (!isGrabbing && hoveringOverItem && hoveringOverItem.grabItemState == XRGrabItemState.待抓握)
				{
					SetGrabItemAttach(hoveringOverItem);
					hoveringOverItem.ItemPickedUp();
					isGrabbing = true;
					grabbedItem = hoveringOverItem;
					SetShowOrHideHandModel();
				}
				// 如果手里有东西，则是放置
				else if (isGrabbing && grabbedItem && grabbedItem.grabItemState == XRGrabItemState.已抓握)
				{
					if (grabbedItem.CanReleaseItem())
					{
						isGrabbing = false;
						grabbedItem.transform.parent = null;
						grabbedItem.ItemReleased();
						grabbedItem = null;
						handModelTran.gameObject.SetActive(true);
					}
					// else
					//  Debug.Log("物品释放条件不足，未抵达目标点");
				}
				else
				{
					//Debug.LogError("手中物品拾取或释放异常，grabItem为NULL");
				}
			}
		}

		// 设置待抓握物品的位置
		private void SetGrabItemAttach(XRBaseGrabItem item)
		{
			item.transform.SetParent(attach, true);
		}



		/// <summary>
		/// 设置手柄模型的隐藏
		/// </summary>
		private void SetShowOrHideHandModel()
		{
			if (hideHandModel)
			{
				handModelTran.gameObject.SetActive(false);
			}
		}

		private void OnTriggerStay(Collider other)
		{
			if (other.TryGetComponent<XRBaseGrabItem>(out XRBaseGrabItem item))
			{
				if (CanPickUpItem(item))
					hoveringOverItem = item;
			}
		}

		//     private void OnTriggerEnter(Collider other)
		//     {
		//         if (other.TryGetComponent<XRBaseGrabItem>(out XRBaseGrabItem item))
		//         {
		//	if(CanPickUpItem(item))
		//		hoveringOverItem = item;
		//         }
		//     }
		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent<XRBaseGrabItem>(out XRBaseGrabItem item))
			{
				if (CanPickUpItem(item))
					hoveringOverItem = null;
			}
		}

		/// <summary>
		/// 判定是否可以拾取该物品
		/// </summary>
		private bool CanPickUpItem(XRBaseGrabItem xrbaseItem)
		{

			if (xrbaseItem.grabItemState == XRGrabItemState.待抓握)
			{
				if (xrbaseItem.pickLimitHandType == handType || xrbaseItem.pickLimitHandType == HandType.Both)
				{
					return true;
				}
			}
			return false;
		}

	}
}
