// SetGrabItemMotion.cs
// 作者：朱佳豪
// 创建日期：2023.10.24
// 脚本描述：抓握物品设置的行为脚本
// 依赖关系：无

// 更新日志：
// 2023.10.24 创建脚本
// 2023.10.25 

// 待完成功能：

using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using XRGrabbingSystem;
using Sirenix.OdinInspector;
using WorkUtils;
// using TaskCore;

public class SetGrabItemMotion : BaseRoleMotion
{
    [LabelText("是否在抓握时生成放置点")]
    public bool isCreatePlacementPos;
    [LabelText("设置抓握物品放置点"), ShowIf("isCreatePlacementPos")]
    public Transform placementPos;
    [LabelText("设置抓握物品类型")]
    public XRGrabItemState grabState = XRGrabItemState.待抓握;
    [LabelText("设置抓握等待行为")]
    public WaitType waitType = WaitType.不设置等待;
    [ReadOnly]
    public XRGrabPlacementItem grab;
    public override async UniTask OnEnterAsync(RoleBehaviour behaviour)
    {
        // 获取组件，设置物品为可抓握
        grab = behaviour.OwnerFSM.Owner.GetComponent<XRGrabPlacementItem>();
        // 设置目标的状态
        grab.SetGrabItemState(grabState);
        // 设置目标的Pos(如果不设置，则抓握时不生成目标点，则一直无法释放，直到某些逻辑达成后，再手动调用生成目标点)
        if (isCreatePlacementPos)
        {
            grab.placementPos = placementPos;
        }

        if (waitType != WaitType.不设置等待)
        {
            if (waitType == WaitType.等待被拾取)
            {
                await grab.WaitItemPickedUpAsync();
                Debug.Log("物品已经被拾取---Motion等待结束");
            }
            else if (waitType == WaitType.等待被释放)
            {
                await grab.WaitItemReleasedAsync();
                Debug.Log("物品已经被释放---Motion等待结束");
            }
        }

        await UniTask.Yield();
    }

    public enum WaitType
    {
        不设置等待,
        等待被拾取,
        等待被释放
    }
}
