//// XRInputObserver.cs
//// ���ߣ���Ѻ�
//// �������ڣ�2023-10-18
//// �ű�������ȫ��ע����������ֱ��¼���ģ���������ֱ��¼�
//// ������ϵ����

//// ʹ��ʾ����
//// 1. XRInputObserver.Instnace.RegXRTriggerButtonPressed(..) ���ɣ���Ϊ��ȫ�ֵ���

//// ������־
//// 2023.10.18 �����ű���ʵ��ģ�����ֱ���trigger��grip�İ���̧�����
//// 2023.10.20 ���ʵ���ֱ��ļ����߼�����ӱ༭������ʱ�궨��

//// ����ɹ���
//// �����ع����򻯵����߼�������ҡ�ˣ��˵����ȼ��������Է��أ����½��ȣ����Է���ҡ��V2ֵ�������¼���̧���¼���һֱ�����¼�

//using System;
//using UnityEngine;
//using Sirenix.OdinInspector;
//using UnityEngine.XR;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.InputSystem;
//using UnityEngine.Events;
//using UnityEngine.EventSystems;
//using UnityEngine.XR.Interaction.Toolkit.Inputs;
//using static UnityEngine.InputSystem.InputAction;
//#if UNITY_EDITOR
//using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
//#endif

///// <summary>
///// XR ��������
///// </summary>
//public class XRInputObserver : MonoBehaviour
//{
//    public static XRInputObserver Instance;
//    [Required("��Ҫ����XRInputActionAsset")]
//    public InputActionAsset inputActionAsset_XR;
//    [Required("��Ҫ����XRDeviceInputActionAsset")]
//    public InputActionAsset inputActionAsset_Device;
//    // �Զ����ң�Editor����ʱ
//#if UNITY_EDITOR
//    private XRDeviceSimulator simulator = null;
//#endif
//    private InputAction inputAction_XRLeft_triggerButton;
//    private InputAction inputAction_XRLeft_gripButton;

//    private InputAction inputAction_XRRight_triggerButton;
//    private InputAction inputAction_XRRight_gripButton;

//    private InputAction inputAction_Device_triggerButton;
//    private InputAction inputAction_Device_gripButton;

//    private UnityEvent triggerPressedEvent_Left = new UnityEvent();
//    private UnityEvent gripPressedEvent_Left = new UnityEvent();
//    private UnityEvent triggerReleasedEvent_Left = new UnityEvent();
//    private UnityEvent gripReleasedEvent_Left = new UnityEvent();

//    private UnityEvent triggerPressedEvent_Right = new UnityEvent();
//    private UnityEvent gripPressedEvent_Right = new UnityEvent();
//    private UnityEvent triggerReleasedEvent_Right = new UnityEvent();
//    private UnityEvent gripReleasedEvent_Right = new UnityEvent();

//    private void Awake()
//    {
//        Instance = this;
//#if UNITY_EDITOR
//        simulator = transform.GetComponentInChildren<XRDeviceSimulator>();
//#endif
//    }

//    private void Start()
//    {
//        OnInit_XR();
//#if UNITY_EDITOR
//       // OnInit_Device();
//#endif
//    }

//    private void OnInit_XR()
//    {
//        inputAction_XRLeft_triggerButton = inputActionAsset_XR.FindActionMap("XRI LeftHand Interaction").FindAction("Activate");
//        inputAction_XRLeft_triggerButton.started += ctx => XR_TriggerButtonPressed(ctx, HandType.Left);
//        inputAction_XRLeft_triggerButton.canceled += ctx => XR_TriggerButtonReleased(ctx, HandType.Left);

//        inputAction_XRLeft_gripButton = inputActionAsset_XR.FindActionMap("XRI LeftHand Interaction").FindAction("Select");
//        inputAction_XRLeft_gripButton.started += ctx => XR_GripButtonPressed(ctx, HandType.Left);
//        inputAction_XRLeft_gripButton.canceled += ctx => XR_GripButtonReleased(ctx, HandType.Left);

//        inputAction_XRRight_triggerButton = inputActionAsset_XR.FindActionMap("XRI RightHand Interaction").FindAction("Activate");
//        inputAction_XRRight_triggerButton.started += ctx => XR_TriggerButtonPressed(ctx, HandType.Right);
//        inputAction_XRRight_triggerButton.canceled += ctx => XR_TriggerButtonReleased(ctx, HandType.Right);

//        inputAction_XRRight_gripButton = inputActionAsset_XR.FindActionMap("XRI RightHand Interaction").FindAction("Select");
//        inputAction_XRRight_gripButton.started += ctx => XR_GripButtonPressed(ctx, HandType.Right);
//        inputAction_XRRight_gripButton.canceled += ctx => XR_GripButtonReleased(ctx, HandType.Right);
//    }

//#if UNITY_EDITOR
//    private void OnInit_Device()
//    {
//        inputAction_Device_triggerButton = inputActionAsset_Device.FindActionMap("Controller").FindAction("Trigger");
//        inputAction_Device_triggerButton.started += ctx => InputDevice_TriggerButtonPressed(ctx);
//        inputAction_Device_triggerButton.canceled += ctx => InputDevice_TriggerButtonReleased(ctx);


//        inputAction_Device_gripButton = inputActionAsset_Device.FindActionMap("Controller").FindAction("Grip");
//        inputAction_Device_gripButton.started += ctx => InputDevice_GripButtonPressed(ctx);
//        inputAction_Device_gripButton.canceled += ctx => InputDevice_GripButtonReleased(ctx);
//    }
//#endif

//    #region �ⲿע�᷽��
//    /// <summary>
//    /// ע�� ���������
//    /// </summary>
//    public void RegXRTriggerButtonPressed(UnityAction action, HandType handType = HandType.Both)
//    {
//        if (handType == HandType.Both)
//        {
//            triggerPressedEvent_Left.AddListener(action);
//            triggerPressedEvent_Right.AddListener(action);
//        }
//        else
//        {
//            if (handType == HandType.Left)
//                triggerPressedEvent_Left.AddListener(action);
//            else if (handType == HandType.Right)
//                triggerPressedEvent_Right.AddListener(action);
//        }
//    }

//    /// <summary>
//    /// ע�� �����̧��
//    /// </summary>
//    public void RegXRTriggerButtonReleased(UnityAction action, HandType handType = HandType.Both)
//    {
//        if (handType == HandType.Both)
//        {
//            triggerReleasedEvent_Left.AddListener(action);
//            triggerReleasedEvent_Right.AddListener(action);
//        }
//        else
//        {
//            if (handType == HandType.Left)
//                triggerReleasedEvent_Left.AddListener(action);
//            else if (handType == HandType.Right)
//                triggerReleasedEvent_Right.AddListener(action);
//        }
//    }

//    /// <summary>
//    /// ע�� ץ�ռ�����
//    /// </summary>
//    public void RegXRGripButtonPressed(UnityAction action, HandType handType = HandType.Both)
//    {
//        if (handType == HandType.Both)
//        {
//            gripPressedEvent_Left.AddListener(action);
//            gripPressedEvent_Right.AddListener(action);
//        }
//        else
//        {
//            if (handType == HandType.Left)
//                gripPressedEvent_Left.AddListener(action);
//            else if (handType == HandType.Right)
//                gripPressedEvent_Right.AddListener(action);
//        }
//    }

//    /// <summary>
//    /// ע�� ץ�ռ�̧��
//    /// </summary>
//    public void RegXRGripButtonReleased(UnityAction action, HandType handType = HandType.Both)
//    {
//        if (handType == HandType.Both)
//        {
//            gripReleasedEvent_Left.AddListener(action);
//            gripReleasedEvent_Right.AddListener(action);
//        }
//        else
//        {
//            if (handType == HandType.Left)
//                gripReleasedEvent_Left.AddListener(action);
//            else if (handType == HandType.Right)
//                gripReleasedEvent_Right.AddListener(action);
//        }
//    }
//    #endregion

//    #region �ⲿȡ��ע�᷽��
//    /// <summary>
//    /// ע�� ���������
//    /// </summary>
//    public void UnRegXRTriggerButtonPressed(HandType handType = HandType.Both)
//    {
//        if (handType == HandType.Both)
//        {
//            triggerPressedEvent_Left.RemoveAllListeners();
//            triggerPressedEvent_Right.RemoveAllListeners();
//        }
//        else
//        {
//            if (handType == HandType.Left)
//                triggerPressedEvent_Left.RemoveAllListeners();
//            else if (handType == HandType.Right)
//                triggerPressedEvent_Right.RemoveAllListeners();
//        }
//    }

//    /// <summary>
//    /// ע�� �����̧��
//    /// </summary>
//    public void UnRegXRTriggerButtonReleased(HandType handType = HandType.Both)
//    {
//        if (handType == HandType.Both)
//        {
//            triggerReleasedEvent_Left.RemoveAllListeners();
//            triggerReleasedEvent_Right.RemoveAllListeners();
//        }
//        else
//        {
//            if (handType == HandType.Left)
//                triggerReleasedEvent_Left.RemoveAllListeners();
//            else if (handType == HandType.Right)
//                triggerReleasedEvent_Right.RemoveAllListeners();
//        }
//    }

//    /// <summary>
//    /// ע�� ץ�ռ�����
//    /// </summary>
//    public void UnRegXRGripButtonPressed(HandType handType = HandType.Both)
//    {
//        if (handType == HandType.Both)
//        {
//            gripPressedEvent_Left.RemoveAllListeners();
//            gripPressedEvent_Right.RemoveAllListeners();
//        }
//        else
//        {
//            if (handType == HandType.Left)
//                gripPressedEvent_Left.RemoveAllListeners();
//            else if (handType == HandType.Right)
//                gripPressedEvent_Right.RemoveAllListeners();
//        }
//    }

//    /// <summary>
//    /// ע�� ץ�ռ�̧��
//    /// </summary>
//    public void UnRegXRGripButtonReleased(HandType handType = HandType.Both)
//    {
//        if (handType == HandType.Both)
//        {
//            gripReleasedEvent_Left.RemoveAllListeners();
//            gripReleasedEvent_Right.RemoveAllListeners();
//        }
//        else
//        {
//            if (handType == HandType.Left)
//                gripReleasedEvent_Left.RemoveAllListeners();
//            else if (handType == HandType.Right)
//                gripReleasedEvent_Right.RemoveAllListeners();
//        }
//    }
//    #endregion

//#if UNITY_EDITOR
//    #region XRģ���� �ڲ�ע�᷽��
//    private void InputDevice_TriggerButtonPressed(CallbackContext context)
//    {
//        if (simulator)
//        {
//            if (simulator.manipulatingLeftController)
//            {
//                triggerPressedEvent_Left?.Invoke();
//            }
//            else if (simulator.manipulatingRightController)
//            {
//                triggerPressedEvent_Right?.Invoke();
//            }
//        }
//    }
//    private void InputDevice_GripButtonPressed(CallbackContext context)
//    {
//        if (simulator)
//        {
//            if (simulator.manipulatingLeftController)
//            {
//                gripPressedEvent_Left?.Invoke();
//            }
//            else if (simulator.manipulatingRightController)
//            {
//                gripPressedEvent_Right?.Invoke();
//            }
//        }
//    }

//    private void InputDevice_TriggerButtonReleased(CallbackContext context)
//    {
//        if (simulator)
//        {
//            if (simulator.manipulatingLeftController)
//            {
//                triggerReleasedEvent_Left?.Invoke();
//            }
//            else if (simulator.manipulatingRightController)
//            {
//                triggerReleasedEvent_Right?.Invoke();
//            }
//        }
//    }
//    private void InputDevice_GripButtonReleased(CallbackContext context)
//    {
//        if (simulator)
//        {
//            if (simulator.manipulatingLeftController)
//            {
//                gripReleasedEvent_Left?.Invoke();
//            }
//            else if (simulator.manipulatingRightController)
//            {
//                gripReleasedEvent_Right?.Invoke();
//            }
//        }
//    }
//    #endregion
//#endif

//    #region ʵ�� �ڲ�ע�᷽��
//    private void XR_TriggerButtonPressed(CallbackContext context, HandType handType)
//    {
//        if (HandType.Left == handType)
//        {
//            triggerPressedEvent_Left?.Invoke();
//        }
//        else if (HandType.Right == handType)
//        {
//            triggerPressedEvent_Right?.Invoke();
//        }
//    }
//    private void XR_GripButtonPressed(CallbackContext context, HandType handType)
//    {
//        if (HandType.Left == handType)
//        {
//            gripPressedEvent_Left?.Invoke();
//        }
//        else if (HandType.Right == handType)
//        {
//            gripPressedEvent_Right?.Invoke();
//        }
//    }
//    private void XR_TriggerButtonReleased(CallbackContext context, HandType handType)
//    {
//        if (HandType.Left == handType)
//        {
//            triggerReleasedEvent_Left?.Invoke();
//        }
//        else if (HandType.Right == handType)
//        {
//            triggerReleasedEvent_Right?.Invoke();
//        }
//    }
//    private void XR_GripButtonReleased(CallbackContext context, HandType handType)
//    {
//        if (HandType.Left == handType)
//        {
//            gripReleasedEvent_Left?.Invoke();
//        }
//        else if (HandType.Right == handType)
//        {
//            gripReleasedEvent_Right?.Invoke();
//        }
//    }
//    #endregion

//    public void OnDestroy()
//    {
//        triggerPressedEvent_Left.RemoveAllListeners();
//        gripPressedEvent_Left.RemoveAllListeners();
//        triggerReleasedEvent_Left.RemoveAllListeners();
//        gripReleasedEvent_Left.RemoveAllListeners();

//        triggerPressedEvent_Right.RemoveAllListeners();
//        gripPressedEvent_Right.RemoveAllListeners();
//        triggerReleasedEvent_Right.RemoveAllListeners();
//        gripReleasedEvent_Right.RemoveAllListeners();
//    }
//}

//public enum HandType
//{
//    Left,
//    Right,
//    Both
//}
