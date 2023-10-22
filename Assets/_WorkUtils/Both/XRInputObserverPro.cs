// XRInputObserver.cs
// ���ߣ���Ѻ�
// �������ڣ�2023-10-18
// �ű�������ȫ��ע����������ֱ��¼���ģ���������ֱ��¼�
// ������ϵ����


// ������־
// 2023.10.18 �����ű���ʵ��ģ�����ֱ���trigger��grip�İ���̧�����
// 2023.10.20 ���ʵ���ֱ��ļ����߼�����ӱ༭������ʱ�궨��
// 2023.10.22 �ع������÷�ʽͨ��CallBackContext�ص�ʹ�ã��ɼ������ⰴ�������ú���߼��ɵ�����ʵ��

// ����� ģ����ʵ��ҡ�˽���
// ����ֱ����ã��ֱ��Ƿ���ȡ�����壬

using System;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

/// <summary>
/// XR ��������
/// </summary>
public class XRInputObserverPro : MonoBehaviour
{
    public static XRInputObserverPro Instance;
    [Required("��Ҫ����XRInputActionAsset")]
    public InputActionAsset inputActionAsset_XR;
    [HideInInspector]
    public Transform leftHand;
    [HideInInspector]
    public Transform rightHand;

    private Dictionary<KeyType, InputActionBoth> dic_XRKeyEvent = new Dictionary<KeyType, InputActionBoth>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
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

        dic_XRKeyEvent.Add(KeyType.Primary, new InputActionBoth(inputActionAsset_XR.FindActionMap("XRI LeftHand Interaction").FindAction("Primary"),
            inputActionAsset_XR.FindActionMap("XRI RightHand Interaction").FindAction("Primary")));

        dic_XRKeyEvent.Add(KeyType.Secondary, new InputActionBoth(inputActionAsset_XR.FindActionMap("XRI LeftHand Interaction").FindAction("Primary"),
           inputActionAsset_XR.FindActionMap("XRI RightHand Interaction").FindAction("Secondary")));

        // ע��ҡ��..  �������ܼ��ʹι��ܼ���Ӱ�
    }

    /// <summary>
    /// ע���ֱ������¼�
    /// </summary>
    /// <param name="action">�ص�����</param>
    /// <param name="keyType">��������</param>
    /// <param name="handType">�ֱ�����</param>
    public void RegisterXRkey(Action<CallbackContext> action, KeyType keyType, HandType handType = HandType.Both)
    {
        if (dic_XRKeyEvent.TryGetValue(keyType, out InputActionBoth value))
        {
            value.BindInputAction(handType, action);
        }
    }

    /// <summary>
    /// ȡ��ע���ֱ������¼�
    /// </summary>
    /// <param name="action">�ص�����</param>
    /// <param name="keyType">��������</param>
    /// <param name="handType">�ֱ�����</param>
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
    public InputActionBoth(InputAction left,InputAction right) 
    {
        leftInputAction = left;
        rightInputAction = right;
    }
    private InputAction leftInputAction;
    private InputAction rightInputAction;

    public void BindInputAction(HandType handType,Action<CallbackContext> action)
    {
        if (HandType.Left == handType)
        {
            leftInputAction.started += action;
            leftInputAction.performed += action;
            leftInputAction.canceled += action;
        }
        else if (HandType.Right == handType)
        {
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

/// <summary>
/// �ֱ���������
/// </summary>
public enum KeyType
{
    /// <summary>
    /// �����
    /// </summary>
    Trigger,
    /// <summary>
    /// ץ�ռ�
    /// </summary>
    Grip,
    /// <summary>
    /// �����ܼ�
    /// </summary>
    Primary,
    /// <summary>
    /// �ι��ܼ�
    /// </summary>
    Secondary,
    /// <summary>
    /// ҡ��
    /// </summary>
    Primary2DAxis
}

public enum HandType
{
    Left,
    Right,
    Both
}
