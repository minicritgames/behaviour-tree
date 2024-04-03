using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minikit.BehaviourTree
{
    /// <summary> The sequencer node will tick each of its children to completion until all have returned a success, or one of them returns a
    /// failure. If all children return success, the sequencer returns success. If any children return failure, the sequencer returns failure </summary>
    public class MKBTNode_Sequencer : MKBTNode_Composite
    {
        private int currentChildIndex = 0;


        public MKBTNode_Sequencer(MKBehaviourTree _bt, MKBTNode[] _children) : base(_bt, _children)
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
                        // If the current child is still running, then the sequencer is still running
                        return Result.Running;

                    case Result.Failure:
                        // If the current child failed, then the sequencer fails
                        currentChildIndex = 0;
                        return Result.Failure;

                    case Result.Success:
                        // If the current child succeeds, then the sequencer moves to the next child
                        currentChildIndex++;
                        if (currentChildIndex < children.Length)
                        {
                            return Result.Running;
                        }

                        // If we made it to this break, then we are out of children to tick and can drop out of this switch statement without returning yet
                        break;
                }
            }

            // If we've made it here, we've ticked all of our children to completion without returning a failure and can return a success
            currentChildIndex = 0;
            return Result.Success;
        }

        public override string GetNodeName()
        {
            return "Sequencer";
        }
    }
} // Minikit.BehaviourTree namespace
