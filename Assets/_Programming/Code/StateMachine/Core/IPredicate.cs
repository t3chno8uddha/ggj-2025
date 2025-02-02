﻿namespace ProjectGZA.StateMachine
{
    public interface IPredicate
    {
        bool Evaluate();
    }

    public interface ITransition
    {
        IState To { get; }
        IPredicate Condition { get; }
    }
}