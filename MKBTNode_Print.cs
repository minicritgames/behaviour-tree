using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minikit.BehaviourTree
{
    public class MKBTNode_Print : MKBTNode
    {
        public enum MKBTNode_PrintLogType
        {
            Log,
            Warning,
            Error
        }

        private string text;
        private MKBTNode_PrintLogType logType;


        public MKBTNode_Print(MKBehaviourTree _bt, string _text, MKBTNode_PrintLogType _logType) : base(_bt)
        {
            text = _text;
            logType = _logType;
        }


        public override Result Tick()
        {
            switch (logType)
            {
                case MKBTNode_PrintLogType.Log:
                    Debug.Log(text);
                    break;
                case MKBTNode_PrintLogType.Warning:
                    Debug.LogWarning(text);
                    break;
                case MKBTNode_PrintLogType.Error:
                    Debug.LogError(text);
                    break;
            }

            return Result.Success;
        }
    }
} // Minikit.BehaviourTree namespace
