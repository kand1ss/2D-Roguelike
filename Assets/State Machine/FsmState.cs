public class FsmState
{
    protected readonly Fsm Fsm;

    public FsmState(Fsm fsm)
    {
        Fsm = fsm;
    }
    
    public virtual void Enter() {}
    public virtual void Update() {}
    public virtual void Exit() {}
}