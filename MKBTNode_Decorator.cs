using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minikit.BehaviourTree
{
    public abstract class MKBTNode_Decorator : MKBTNode
    {
        protected MKBTNode child;


        public MKBTNode_Decorator(MKBehaviourTree _bt, MKBTNode _child) : base(_bt)
        {
            child = _child;
        }
    }
} // Minikit.BehaviourTree namespace
