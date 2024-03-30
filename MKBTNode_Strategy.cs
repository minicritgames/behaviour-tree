using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minikit.BehaviourTree
{
    /// <summary> This interface doesn't need to exist on a class inheriting from the base node class </summary>
    public interface IMKBTNode_Strategy
    {
        public MKBTNode.Result Tick();
    }

    /// <summary> The strategy node will choose strategy during runtime based on the result of the provided function in the constructor. Strategies
    /// are developer-implemented via the strategy interface, and have their own Tick function like nodes do. The strategy node will tick whatever
    /// strategy it is given until the strategy given returns a success or failure.  </summary>
    public class MKBTNode_Strategy<T> : MKBTNode where T : IMKBTNode_Strategy
    {
        private Func<T> getStrategyFunc;


        public MKBTNode_Strategy(MKBehaviourTree _bt, Func<T> _getStrategyFunc) : base(_bt)
        {
            getStrategyFunc = _getStrategyFunc;
        }


        public override Result Tick()
        {
            if (getStrategyFunc == null)
            {
                return Result.Failure;
            }

            T strategy = getStrategyFunc.Invoke();
            if (strategy == null)
            {
                return Result.Failure;
            }

            return getStrategyFunc.Invoke().Tick();
        }
    }
} // Minikit.BehaviourTree namespace
