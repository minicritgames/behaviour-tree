using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Minikit.BehaviourTree
{
    public abstract class MKBehaviourTree
    {
        public UnityEvent OnStarted = new();
        public UnityEvent OnStopped = new();
        public UnityEvent<MKBTNode> OnNodeTicked = new();

        protected MKBTNode rootNode;
        
        private MonoBehaviour monoBehaviour;
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

        public MKBTNode.Result TickOnce()
        {
            return rootNode.Tick();
        }

        protected abstract MKBTNode CreateNodeTree();
    }
} // Minikit.BehaviourTree namespace
