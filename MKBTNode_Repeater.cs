using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minikit.BehaviourTree
{
    /// <summary> The repeater node will simply tick its child and send back a result of running regardless of the child's result </summary>
    public class MKBTNode_Repeater : MKBTNode_Decorator
    {


        public MKBTNode_Repeater(MKBehaviourTree _bt, MKBTNode _child) : base(_bt, _child)
        {

        }


        public override Result Tick()
        {
            bt.OnNodeTicked.Invoke(this);

            child.Tick();

            return Result.Running;
        }

        public override string GetNodeName()
        {
            return "Repeater";
        }
    }
} // Minikit.BehaviourTree namespace
