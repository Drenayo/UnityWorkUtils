using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;

namespace WorkUtils
{
    public abstract class RoleBehaviour : MonoBehaviour
	{
		public abstract void OnInit();
		public abstract UniTask OnEnterAsync(RoleBehaviour behaviour);
		public abstract void OnLeave();
    }
}