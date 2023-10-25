// XRInputObserver.cs
// 作者：朱佳豪
// 创建日期：2023-10-18
// 脚本描述：全局注册监听左右手柄事件，模拟器左右手柄事件
// 依赖关系：无

// 更新日志：
// 2023.10.18 创建脚本、实现模拟器手柄的trigger、grip的按下抬起监听
// 2023.10.20 添加实机手柄的监听逻辑、添加编辑器运行时宏定义
// 2023.10.22 重构，调用方式通过CallBackContext回调使用，可监听任意按键，调用后的逻辑由调用者实现
// 2023.10.23 可监听摇杆Button的事件

// 待完成功能：
// 模拟器实现摇杆进度 
// 添加手柄引用，手柄是否拿取了物体 

using System;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace XRGrabbingSystem
{
    /// <summary>
    /// XR 监听输入
    /// </summary>
    public class XRInputObserver : MonoBehaviour
    {
        public static XRInputObserver Instance;
        [Required("需要赋予XRInputActionAsset")]
        public InputActionAsset inputActionAsset_XR;
        [HideInInspector]
        public Transform leftHand;
        [HideInInspector]
        public Transform rightHand;

        private Dictionary<KeyType, InputActionBoth> dic_XRKeyEvent = new Dictionary<KeyType, InputActionBoth>();

        private void Awake()
        {
            Instance = this;
            OnInit_XR();
            leftHand = transform.GetComponentInChildren<ActionBasedController>().transform;
            rightHand = leftHand.parent.GetChild(leftHand.GetSiblingIndex() + 1);
        }

        private void OnInit_XR()
        {
            dic_XRKeyEvent.Add(KeyType.Trigger, new InputActionBoth(inputActionAsset_XR.FindActionMap("XRI LeftHand Interaction").FindAction("Activate"),
                inputActionAsset_XR.FindActionMap("XRI RightHand Interaction").FindAction("Activate")));

            dic_XRKeyEvent.Add(KeyType.Grip, new InputActionBoth(inputActionAsset_XR.FindActionMap("XRI LeftHand Interaction").FindAction("Select"),
                inputActionAsset_XR.FindActionMap("XRI RightHand Interaction").FindAction("Select")));

            dic_XRKeyEvent.Add(KeyType.Primary, new InputActionBoth(inputActionAsset_XR.FindActionMap("XRI LeftHand Interaction").FindAction("PrimaryButton"),
                inputActionAsset_XR.FindActionMap("XRI RightHand Interaction").FindAction("PrimaryButton")));

            dic_XRKeyEvent.Add(KeyType.Secondary, new InputActionBoth(inputActionAsset_XR.FindActionMap("XRI LeftHand Interaction").FindAction("SecondaryButton"),
               inputActionAsset_XR.FindActionMap("XRI RightHand Interaction").FindAction("SecondaryButton")));

            dic_XRKeyEvent.Add(KeyType.Primary2DAxis, new InputActionBoth(inputActionAsset_XR.FindActionMap("XRI LeftHand Interaction").FindAction("Rotate Anchor"),
               inputActionAsset_XR.FindActionMap("XRI RightHand Interaction").FindAction("Rotate Anchor")));
        }


        /// <summary>
        /// 注册手柄按键事件
        /// </summary>
        /// <param name="action">回调函数</param>
        /// <param name="keyType">按键类型</param>
        /// <param name="handType">手柄类型</param>
        public void RegisterXRkey(Action<CallbackContext> action, KeyType keyType, HandType handType = HandType.Both)
        {
            if (dic_XRKeyEvent.TryGetValue(keyType, out InputActionBoth value))
            {
                value.BindInputAction(handType, action);
            }
        }

        /// <summary>
        /// 取消注册手柄按键事件
        /// </summary>
        /// <param name="action">回调函数</param>
        /// <param name="keyType">按键类型</param>
        /// <param name="handType">手柄类型</param>
        public void UnRegisterXRkey(Action<CallbackContext> action, KeyType keyType, HandType handType = HandType.Both)
        {
            if (dic_XRKeyEvent.TryGetValue(keyType, out InputActionBoth value))
            {
                value.UnBindInputAction(handType, action);
            }
        }
    }

    public class InputActionBoth
    {
        public InputActionBoth(InputAction left, InputAction right)
        {
            leftInputAction = left;
            rightInputAction = right;
        }
        private InputAction leftInputAction;
        private InputAction rightInputAction;

        public void BindInputAction(HandType handType, Action<CallbackContext> action)
        {
            if (HandType.Left == handType)
            {
                //Debug.Log("左手被绑定");
                leftInputAction.started += action;
                leftInputAction.performed += action;
                leftInputAction.canceled += action;
            }
            else if (HandType.Right == handType)
            {
                //Debug.Log("右手被绑定");
                rightInputAction.started += action;
                rightInputAction.performed += action;
                rightInputAction.canceled += action;
            }
            else if (HandType.Both == handType)
            {
                leftInputAction.started += action;
                leftInputAction.performed += action;
                leftInputAction.canceled += action;

                rightInputAction.started += action;
                rightInputAction.performed += action;
                rightInputAction.canceled += action;
            }
        }

        public void UnBindInputAction(HandType handType, Action<CallbackContext> action)
        {
            if (HandType.Left == handType)
            {
                leftInputAction.started -= action;
                leftInputAction.performed -= action;
                leftInputAction.canceled -= action;
            }
            else if (HandType.Right == handType)
            {
                rightInputAction.started -= action;
                rightInputAction.performed -= action;
                rightInputAction.canceled -= action;
            }
            else if (HandType.Both == handType)
            {
                leftInputAction.started -= action;
                leftInputAction.performed -= action;
                leftInputAction.canceled -= action;

                rightInputAction.started -= action;
                rightInputAction.performed -= action;
                rightInputAction.canceled -= action;
            }
        }
    }
}
