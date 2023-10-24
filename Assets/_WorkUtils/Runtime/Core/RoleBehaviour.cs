using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorkUtils
{
    public class RoleBehaviour : MonoBehaviour
    {
        public OwnerFSM_Class OwnerFSM;

        private void Awake()
        {
            OwnerFSM = new OwnerFSM_Class(this);
        }
    }

    public class OwnerFSM_Class
    {
        public OwnerFSM_Class(RoleBehaviour role)
        {
            Owner = role;
        }
        public RoleBehaviour Owner;
    }
}


