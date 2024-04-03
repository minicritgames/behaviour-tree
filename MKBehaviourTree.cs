using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Minikit.BehaviourTree
{
    public abstract class MKBehaviourTree
    {
        public UnityEvent OnStarted = new();
        public UnityEvent OnStopped = new();
        public UnityEvent<MKBTNode> OnNodeTicked = new();

        private MonoBehaviour monoBehaviour;
        private MKBTNode rootNode;
        private Coroutine tickCoroutine;


        public MKBehaviourTree(MonoBehaviour _monoBehaviour)
        {
            monoBehaviour = _monoBehaviour;
            rootNode = CreateNodeTree();
        }


        public void Start()
        {
            if (tickCoroutine == null)
            {
                OnStarted.Invoke();

                tickCoroutine = monoBehaviour.StartCoroutine(Tick());
            }
        }

        public void Stop()
        {
            if (tickCoroutine != null)
            {
                monoBehaviour.StopCoroutine(tickCoroutine);
                tickCoroutine = null;

                OnStopped.Invoke();
            }
        }

        public void Restart()
        {
            Stop();
            Start();
        }

        protected virtual IEnumerator Tick()
        {
            MKBTNode.Result result = rootNode.Tick();
            while (result == MKBTNode.Result.Running)
            {
                yield return null;

                result = rootNode.Tick();
            }

            Stop();
        }

        protected abstract MKBTNode CreateNodeTree();
    }
} // Minikit.BehaviourTree namespace
