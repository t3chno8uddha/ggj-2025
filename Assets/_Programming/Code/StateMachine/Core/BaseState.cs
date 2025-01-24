using ProjectGZA.StateMachine;

namespace ProjectGZA
{
    public abstract class BaseState : IState
    {
        public string StateName => GetType().Name;

        public abstract void OnEnter();
        public abstract void Update();
        public abstract void OnExit();
    }
}
