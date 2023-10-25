// XRGrabEnum.cs
// 作者：朱佳豪
// 创建日期：2023.10.24
// 脚本描述：XRGrabbingSystem 相关枚举文件
// 依赖关系：无

// 更新日志：
// 2023.10.24 创建脚本

// 待完成功能：

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRGrabbingSystem
{
    /// <summary>
    /// 抓握物品的类型
    /// </summary>
    public enum XRGrabItemType
    {
        放置到目标点,
        与单目标点交互,
        与多目标点交互
    }

    /// <summary>
    /// 抓握物品的状态
    /// </summary>
    public enum XRGrabItemState
    {
        待抓握,
        已抓握,
        禁止抓握
    }

    /// <summary>
    /// 手柄按键类型
    /// </summary>
    public enum KeyType
    {
        /// <summary>
        /// 扳机键
        /// </summary>
        Trigger,
        /// <summary>
        /// 抓握键
        /// </summary>
        Grip,
        /// <summary>
        /// 主功能键
        /// </summary>
        Primary,
        /// <summary>
        /// 次功能键
        /// </summary>
        Secondary,
        /// <summary>
        /// 摇杆
        /// </summary>
        Primary2DAxis
    }

    /// <summary>
    /// 手柄类型
    /// </summary>
    public enum HandType
    {
        /// <summary>
        /// 左手柄
        /// </summary>
        Left,
        /// <summary>
        /// 右手柄
        /// </summary>
        Right,
        /// <summary>
        /// 双手柄
        /// </summary>
        Both
    }
}


