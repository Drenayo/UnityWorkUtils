using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;

namespace WorkUtils
{
	public abstract class BaseRoleMotion : MonoBehaviour
	{
		public virtual void OnInit(RoleBehaviour behaviour) { }
		public virtual async UniTask OnEnterAsync(RoleBehaviour behaviour) { }
		public virtual async UniTask OnLeaveAsync(RoleBehaviour behaviour) { }
	}

}
