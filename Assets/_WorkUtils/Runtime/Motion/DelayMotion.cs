using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorkUtils;

public class DelayMotion : RoleBehaviour
{
    public float times;

    public override void OnInit()
    {

    }

    public override async UniTask OnEnterAsync(RoleBehaviour behaviour)
    {
        await UniTask.WaitForSeconds(times);
    }

    public override void OnLeave()
    {

    }

}
