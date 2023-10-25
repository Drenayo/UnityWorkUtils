// XRGrabSimplePlacement.cs
// 作者：朱佳豪
// 创建日期：2023.10.25
// 脚本描述：简单抓握（抓握后不生成目标点，等待所有自定义逻辑完成后再手动生成目标点）
// 依赖关系：无

// 更新日志：
// 2023.10.25 创建脚本，继承自XRGrabPlacementItem

// 待完成功能：
// 拿取后不生成目标点，达成某个条件后再设置目标点

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRGrabbingSystem
{
    /// <summary>
    /// 简单抓握-放置
    /// </summary>
    public class XRGrabSimplePlacement : XRGrabPlacementItem
    {

        public override void ItemPickedUp()
        {
            base.ItemPickedUp();
            //if (!placementPos)
            //    Debug.LogError("未设置物品放置点_XRGrabPlacementItem");
            //placementCollider = CloneAndResetPosition(placementPos);
        }
    }
}
