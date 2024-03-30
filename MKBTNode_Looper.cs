using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minikit.BehaviourTree
{
    /// <summary> The looper node will tick its child to completion multiple times in succession until the child returns a failure. If the child 
    /// returns success for each of the iterations, the looper returns success. If the child fails on any iteration, the looper returns a failure </summary>
    public class MKBTNode_Looper : MKBTNode_Decorator
    {
        private int numberOfLoops;
        private int currentLoopIndex = 0;


        public MKBTNode_Looper(MKBehaviourTree _bt, int _numberOfLoops, MKBTNode _child) : base(_bt, _child)
        {
            numberOfLoops = _numberOfLoops;
        }


        public override Result Tick()
        {
            if (currentLoopIndex < numberOfLoops)
            {
                Result result = child.Tick();
                switch (result)
                {
                    case Result.Running:
                        // If our child is running, return that the looper is still running
                        return child.Tick();

                    case Result.Failure:
                        // If our child returns a failure, then end this looper and return a failure
                        currentLoopIndex = 0;
                        return Result.Failure;

                    case Result.Success:
                        // If our child returns a success, then increment our loop count and start running our child again
                        currentLoopIndex++;
                        if (currentLoopIndex < numberOfLoops)
                        {
                            return Result.Running;
                        }

                        // If we made it to this break, then we are out of loops and can drop out of this switch statement without returning yet
                        break;
                }
            }

            // If we've made it here, we've used all of our loops without returning a failure and can return a success
            currentLoopIndex = 0;
            return Result.Success;
        }
    }
} // Minikit.BehaviourTree namespace
