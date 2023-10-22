using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;

namespace WorkUtils
{
	public class TaskManager : MonoBehaviour
	{
        [LabelText("ִ���б�"),SceneObjectsOnly]
        public List<RoleBehaviour> runList;

        private async UniTask Start()
        {
            if (runList == null)
            {
                Debug.Log("ִ���б�Ϊ��!");
                return;
            }

            Debug.Log("--- ��Ϊ��ʼִ�� ---");

            foreach (RoleBehaviour item in runList)
            {
                Debug.Log(item.transform.name + "��ʼִ��");
                item.OnInit();
                await item.OnEnterAsync(item);
                item.OnLeave();
            }

            Debug.Log("--- ��Ϊִ����� ---");
        }
    }
}