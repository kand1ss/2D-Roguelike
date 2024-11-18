public abstract class FsmState
{
    protected readonly Fsm StateMachine;

    protected FsmState(Fsm stateMachine)
    {
        StateMachine = stateMachine;
    }
    
    public virtual void Enter() {}
    public virtual void Update() {}
    public virtual void Exit() {}
}