using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;

namespace WorkUtils
{
	public class TaskManager : MonoBehaviour
	{
        [LabelText("执行列表"),SceneObjectsOnly]
        public List<RoleBehaviour> runList;

        private async UniTask Start()
        {
            if (runList == null)
            {
                Debug.Log("执行列表为空!");
                return;
            }

            Debug.Log("--- 行为开始执行 ---");

            foreach (RoleBehaviour item in runList)
            {
                Debug.Log(item.transform.name + "开始执行");
                item.OnInit();
                await item.OnEnterAsync(item);
                item.OnLeave();
            }

            Debug.Log("--- 行为执行完毕 ---");
        }
    }
}