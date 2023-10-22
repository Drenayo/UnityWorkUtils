using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

#if ZJH_DEBUG
using WorkUtils;
#endif

public class TestMotion : RoleBehaviour
{
    public string names;

    public override void OnInit()
    {
        Debug.Log("×¢²áÊÂ¼þ");

        XRInputObserverPro.Instance.RegisterXRkey(KeyTrigger, KeyType.Trigger);
        XRInputObserverPro.Instance.RegisterXRkey(KeyPrimary, KeyType.Primary);
    }

    public override async UniTask OnEnterAsync(RoleBehaviour behaviour)
    {
        await UniTask.Yield();
    }

    public void KeyTrigger(CallbackContext contex)
    {
        if(contex.performed)
            Debug.Log("Left Trigger Performed");
        else if (contex.canceled)
            Debug.Log("Left Trigger Up");
        else if(contex.started)
            Debug.Log("Left Trigger Start");
    }

    public void KeyPrimary(CallbackContext contex)
    {
        if (contex.performed)
            Debug.Log("Left Par Performed");
        else if (contex.canceled)
            Debug.Log("Left Par Up");
        else if (contex.started)
            Debug.Log("Left Par Start");
    }

    public override void OnLeave()
    {

    }

}
