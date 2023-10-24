using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorkUtils;

public class DelayMotion : BaseRoleMotion
{
    public float times;

    public override async UniTask OnEnterAsync(RoleBehaviour behaviour)
    {
        await UniTask.WaitForSeconds(times);
    }
}
