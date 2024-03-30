using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minikit.BehaviourTree
{
    public abstract class MKBTNode
    {
        public enum Result
        {
            Running,
            Failure,
            Success
        };


        protected MKBehaviourTree bt { get; private set; }


        public MKBTNode(MKBehaviourTree _bt)
        {
            bt = _bt;
        }


        /// <returns> The result of this node after ticking </returns>
        public abstract Result Tick();

        public virtual string GetNodeName()
        {
            return this.GetType().Name;
        }
    }
} // Minikit.BehaviourTree namespace
