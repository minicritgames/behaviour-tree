using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minikit.BehaviourTree
{
    /// <summary> The selector node will tick each of its children until completion until one of the children returns a success. If one of the
    /// children returns a success, the selector returns a success. If all children return a failure, the selector returns a failure </summary>
    public class MKBTNode_Selector : MKBTNode_Composite
    {
        private int currentChildIndex = 0;


        public MKBTNode_Selector(MKBehaviourTree _bt, MKBTNode[] _children) : base(_bt, _children)
        {

        }


        public override Result Tick()
        {
            bt.OnNodeTicked.Invoke(this);

            if (currentChildIndex < children.Length)
            {
                Result result = children[currentChildIndex].Tick();
                switch (result)
                {
                    case Result.Running:
                        // If the current child is still running, then the selector is still running
                        return Result.Running;

                    case Result.Failure:
                        // If the current child failed, then the selector moves to the next child
                        currentChildIndex++;
                        if (currentChildIndex < children.Length)
                        {
                            return Result.Running;
                        }

                        // If we made it to this break, then we are out of children to tick and can drop out of this switch statement without returning yet
                        break;

                    case Result.Success:
                        // If the current child succeeds, then the selector succeeds
                        currentChildIndex = 0;
                        return Result.Success;
                }
            }

            // If we've made it here, we've ticked all of our children to completion without returning success and can return a failure
            currentChildIndex = 0;
            return Result.Failure;
        }

        public override string GetNodeName()
        {
            return "Selector";
        }
    }
} // Minikit.BehaviourTree namespace
