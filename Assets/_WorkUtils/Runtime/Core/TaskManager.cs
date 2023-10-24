using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using WorkUtils;

namespace WorkUtils
{
    public class TaskManager : MonoBehaviour
    {
        [LabelText("执行列表"), SceneObjectsOnly]
        public List<BaseRoleMotion> runList = new List<BaseRoleMotion>();

        private async UniTask Start()
        {
            if (runList == null)
            {
                Debug.Log("执行列表为空!");
                return;
            }

            Debug.Log("<color=yellow>----- 行为列表开始执行 -----</color>");

            foreach (Transform item in transform)
            {
                runList.Add(item.GetComponent<BaseRoleMotion>());
                item.gameObject.AddComponent<RoleBehaviour>();

                // 初始化执行
                item.GetComponent<BaseRoleMotion>().OnInit(item.GetComponent<RoleBehaviour>());
            }

            foreach (BaseRoleMotion item in runList)
            {
                Debug.Log($"<color=yellow>[{item.transform.name} ({item.transform.GetComponent<BaseRoleMotion>().GetType().Name})] 开始执行！</color>");
                await item.OnEnterAsync(item.GetComponent<RoleBehaviour>());
                await item.OnLeaveAsync(item.GetComponent<RoleBehaviour>());
                Debug.Log($"<color=yellow>[{item.transform.name} ({item.transform.GetComponent<BaseRoleMotion>().GetType().Name})] 执行完毕！</color>");
            }

            Debug.Log("<color=yellow>----- 行为列表执行完毕 -----</color>");
        }
    }
}


