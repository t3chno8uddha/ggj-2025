namespace ProjectGZA.StateMachine
{
    public interface IState
    {
        string StateName { get; }
        void OnEnter();
        void Update();
        void OnExit();
    }

}