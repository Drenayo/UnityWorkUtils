using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorkUtils;

public class ActiveMotion : BaseRoleMotion
{
    public GameObject activeObj;
    public bool isActive;


    public override async UniTask OnEnterAsync(RoleBehaviour behaviour)
    {
        activeObj.SetActive(isActive);
        await UniTask.Yield();
    }

}
