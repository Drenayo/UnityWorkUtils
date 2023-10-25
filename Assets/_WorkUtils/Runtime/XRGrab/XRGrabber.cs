// XRGrabber.cs
// 作者：朱佳豪
// 创建日期：2023.10.24
// 脚本描述：所有和玩家相关的抓握逻辑写在此处
// 依赖关系：无

// 更新日志：
// 2023.10.24 创建脚本
// 2023.10.25 增加对于双手柄控制器的引用

// 待完成功能：
// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRGrabbingSystem
{
    /// <summary>
    /// 抓握逻辑的执行者，玩家 XROrigin
    /// </summary>
	public class XRGrabber : MonoBehaviour
    {
        public static XRGrabber Instance;
        public XRGrabController leftController;
        public XRGrabController rightController;
        public Material transparentMat;
        private void Awake()
        {
            Instance = this;
        }

    }
}


