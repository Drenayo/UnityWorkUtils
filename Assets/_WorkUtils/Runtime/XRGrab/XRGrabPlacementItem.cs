// XRGrabPlacementItem.cs
// 作者：朱佳豪
// 创建日期：2023.10.24
// 脚本描述：抓握生成目标点，并放置
// 依赖关系：无

// 更新日志：
// 2023.10.24 创建脚本

// 待完成功能：

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;

namespace XRGrabbingSystem
{
    /// <summary>
    /// 可抓握的XR物品
    /// </summary>
    public class XRGrabPlacementItem : XRBaseGrabItem
    {
        /// <summary>
        /// 放置点
        /// </summary>
        // [ReadOnly]
        public Transform placementPos;
        /// <summary>
        /// 放置点的Collider
        /// </summary>
        [ReadOnly]
        public Collider placementCollider;
        /// <summary>
        /// 当前物品是否已经接触了目标点
        /// </summary>
        [ReadOnly]
        public bool isItemHoveringAtPlacementPos;


        /// <summary>
        /// 物品被拾取
        /// </summary>
        public override void ItemPickedUp()
        {
            base.ItemPickedUp();
            if (!placementPos)
                Debug.LogError("未设置物品放置点_XRGrabPlacementItem");
            placementCollider = CloneAndResetPosition(placementPos);
        }

        /// <summary>
        /// 物品被释放
        /// </summary>
        public override void ItemReleased()
        {
            base.ItemReleased();
            transform.SetPositionAndRotation(placementCollider.transform.position, placementCollider.transform.rotation);
            Destroy(placementCollider.gameObject);
            isItemHoveringAtPlacementPos = false;
            Destroy(placementPos.gameObject);
        }


        public override bool CanReleaseItem()
        {
            return isItemHoveringAtPlacementPos;
        }

        /// <summary>
        /// 异步等待 等待物品被拾取
        /// </summary>
        /// <returns></returns>
        public async UniTask WaitItemPickedUpAsync()
        {
            while (grabItemState == XRGrabItemState.待抓握)
            {
                await UniTask.Yield();
            }
        }

        /// <summary>
        /// 异步等待 等待物品被释放
        /// </summary>
        /// <returns></returns>
        public async UniTask WaitItemReleasedAsync()
        {
            while (grabItemState != XRGrabItemState.禁止抓握)
            {
                await UniTask.Yield();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == placementCollider && grabItemState == XRGrabItemState.已抓握)
            {
                isItemHoveringAtPlacementPos = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other == placementCollider && grabItemState == XRGrabItemState.已抓握)
            {
                isItemHoveringAtPlacementPos = false;
            }
        }
    }
}
