// XRBaseGrabItem.cs
// 作者：朱佳豪
// 创建日期：2023.10.24
// 脚本描述：XR抓握物品的基类，所有实现抓握功能的物品继承此类
// 依赖关系：无

// 更新日志：
// 2023.10.24 创建脚本

// 待完成功能：

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace XRGrabbingSystem
{
    /// <summary>
    /// XR抓握物品基类
    /// </summary>
    public class XRBaseGrabItem : MonoBehaviour
    {
        /// <summary>
        /// 抓握物品的类型
        /// </summary>
        [ReadOnly]
        public XRGrabItemType grabItemType;

        /// <summary>
        /// 抓握物品的状态
        /// </summary>
        public XRGrabItemState grabItemState = XRGrabItemState.禁止抓握;

        /// <summary>
        /// 拾取限制手柄的类型，限制只能左或右手拾取，或双手都可
        /// </summary>
	    public HandType pickLimitHandType = HandType.Both;

        /// <summary>
        /// 物品被拾取
        /// </summary>
        public virtual void ItemPickedUp()
        {
            grabItemState = XRGrabItemState.已抓握;
        }

        /// <summary>
        /// 物品被释放
        /// </summary>
        public virtual void ItemReleased()
        {
            grabItemState = XRGrabItemState.禁止抓握;
        }

        /// <summary>
        /// 物品是否可以被释放
        /// </summary>
        /// <returns></returns>
        public virtual bool CanReleaseItem() { return false; }

        /// <summary>
        /// 根据目标位置生成一个目标点
        /// </summary>
        /// <returns>返回生成的Collider</returns>
        protected Collider CloneAndResetPosition(Transform targetTransform)
        {
            // 检查是否有MeshRenderer和Collider组件
            MeshRenderer renderer = transform.GetComponentInChildren<MeshRenderer>();
            Collider collider = transform.GetComponentInChildren<Collider>();

            if (renderer != null && collider != null)
            {
                // 创建一个新GameObject
                GameObject newObject = new GameObject();

                // 复制Mesh Filter和Mesh Renderer组件
                MeshFilter meshFilter = newObject.AddComponent<MeshFilter>();
                meshFilter.sharedMesh = transform.GetComponentInChildren<MeshFilter>().sharedMesh;

                MeshRenderer newRenderer = newObject.AddComponent<MeshRenderer>();
                newRenderer.sharedMaterial = XRGrabber.Instance.transparentMat; // 指定半透明材质

                //  重新生成一个Collider  //并设置大小和缩放
                Collider newCollider = newObject.AddComponent<BoxCollider>();
                newCollider.isTrigger = true;

                //if (collider is BoxCollider)
                //{
                //    BoxCollider boxCollider = (BoxCollider)collider;
                //    newCollider = newObject.AddComponent<BoxCollider>();
                //    BoxCollider newBoxCollider = (BoxCollider)newCollider;
                //    newBoxCollider.size = boxCollider.size;
                //    newBoxCollider.isTrigger = true;
                //}
                // 如果有其他类型的Collider，可以在这里继续添加对应的处理

                // 将新对象的位置重置为传入的目标位置
                newObject.transform.SetPositionAndRotation(targetTransform.position, targetTransform.rotation);
                // 找到当前抓握物体的缩放，并且必须是拥有MeshFilter的缩放
                newObject.transform.localScale = transform.GetComponentInChildren<MeshFilter>().transform.localScale;
                return newCollider;
            }
            return null;
        }

        /// <summary>
        /// 设置物品的状态（拿取后需要再次设置拿取）
        /// </summary>
        /// <param name="state"></param>
        public void SetGrabItemState(XRGrabItemState state)
        {
            grabItemState = state;
        }


    }
}

