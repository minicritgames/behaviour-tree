using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minikit.BehaviourTree
{
    public abstract class MKBTNode_Composite : MKBTNode
    {
        protected MKBTNode[] children;


        public MKBTNode_Composite(MKBehaviourTree _bt, MKBTNode[] _children) : base(_bt)
        {
            children = _children;
        }
    }
} // Minikit.BehaviourTree namespace
