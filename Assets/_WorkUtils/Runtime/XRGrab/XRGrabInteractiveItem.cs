// XRGrabInteractiveItem.cs
// 作者：朱佳豪
// 创建日期：2023.10.24
// 脚本描述：可交互物品类，拿取时等待自定义交互逻辑完成，然后生成放置目标点
// 依赖关系：无

// 更新日志：
// 2023.10.24 创建脚本

// 待完成功能：
// 未完成...差得多
// 想法 Public方法来设置一个触发List,然后外接一个事件+碰撞体，一系列触发，完毕后，生成最后的放置点，然后放置

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace XRGrabbingSystem
{
    /// <summary>
    /// 可抓握的XR物品
    /// </summary>
    public class XRGrabInteractiveItem : XRBaseGrabItem
    {
        /// <summary>
        /// 目标点
        /// </summary>
        public Transform targetPos;

        /// <summary>
        /// 放置点
        /// </summary>
        public Vector3 placementPos;
        public Quaternion placementRot;

        /// <summary>
        /// 放置点的Collider
        /// </summary>
        public Collider placementCollider;

        /// <summary>
        /// 当前物品是否已经接触了目标点
        /// </summary>
        public bool isItemHoveringAtTarget;
        /// <summary>
        /// 该物品是否完成交互
        /// </summary>
        public bool isInteracted;

        /// <summary>
        /// 待交互行为集合
        /// </summary>
        public List<InteractiveHandler> interactiveHandleList;


        /// <summary>
        /// 物品被拾取
        /// </summary>
        public override void ItemPickedUp()
        {
            base.ItemPickedUp();
            placementPos = transform.position;
            placementRot = transform.rotation;

            // placementCollider = CloneAndResetPosition(targetPos);
        }

        /// <summary>
        /// 物品被释放
        /// </summary>
        public override void ItemReleased()
        {
            base.ItemReleased();
            transform.SetPositionAndRotation(placementCollider.transform.position, placementCollider.transform.rotation);
            Destroy(placementCollider.gameObject);
        }

        // 设置物品的触发列表，必须在物品被拾取之前设置

        public override bool CanReleaseItem()
        {
            return isItemHoveringAtTarget;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == placementCollider && grabItemState == XRGrabItemState.已抓握)
            {
                if (isInteracted == true)
                {
                    isItemHoveringAtTarget = true;
                }

            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other == placementCollider && grabItemState == XRGrabItemState.已抓握)
            {
                if (isInteracted == true)
                {
                    isItemHoveringAtTarget = false;
                }
            }
        }
    }

    public class InteractiveHandler
    {
        public UnityEvent action;
        public Collider collider;
        public InteractiveHandler(UnityEvent action, Collider collider)
        {
            this.action = action;
            this.collider = collider;
        }
    }
}
