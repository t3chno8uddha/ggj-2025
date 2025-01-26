using System;
using System.Collections.Generic;

namespace ProjectGZA.StateMachine
{
    public interface IStateMachineHolder
    {
        StateMachine StateMachine { get; set; }
    }

    public static class StateMachineUtilities
    {
        public static T GetState<T>(this StateMachine stateMachine)
        {
            return (T)stateMachine.Nodes[typeof(T)].State;
        }
    }

    public class StateMachine
    {
        private StateNode _currentNode;
        private Dictionary<Type, StateNode> _nodes = new();

        private HashSet<ITransition> _anyTransition = new();

        internal Dictionary<Type, StateNode> Nodes { get => _nodes; set => _nodes = value; }
        internal StateNode CurrentNode { get => _currentNode; set => _currentNode = value; }

        public void Update()
        {
            var transition = GetTransition();

            if (transition != null)
            {
                ChangeState(transition.To);
            }

            _currentNode.State.Update();
        }

        public void SetState(IState state)
        {
            _currentNode = _nodes[state.GetType()];
            _currentNode.State.OnEnter();
        }

        public void ChangeState(IState state)
        {
            if (state == _currentNode.State) return;

            var previousState = _currentNode;
            var nextState = _nodes[state.GetType()];

            previousState.State.OnExit();
            nextState.State.OnEnter();

            _currentNode = _nodes[state.GetType()];

        }

        private ITransition GetTransition()
        {
            foreach (var transition in _anyTransition)
            {
                if (transition.Condition.Evaluate()) return transition;
            }

            foreach (var transition in _currentNode.Transitions)
            {
                if (transition.Condition.Evaluate()) return transition;
            }

            return null;
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        public void AddAnyTransition(IState to, IPredicate condition)
        {
            _anyTransition.Add(new Transition(GetOrAddNode(to).State, condition));
        }

        private StateNode GetOrAddNode(IState state)
        {
            var node = _nodes.GetValueOrDefault(state.GetType());

            if (node == null)
            {
                node = new StateNode(state);

                _nodes.Add(state.GetType(), node);

                //Debug.Log($"New State Created: {state.GetType()}");
            }

            return node;
        }


        internal class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }

            public void AddTransition(IState to, IPredicate condition)
            {
                Transitions.Add(new Transition(to, condition));
            }
        }
    }
}
