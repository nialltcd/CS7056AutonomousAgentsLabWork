public class StateMachine <T> {
	
	private T agent;

    private State<T> state = null;
    public State<T> State
    {
        get { return state; }
        set { state = value; }
    }

    private State<T> previousState = null;
    public State<T> PreviousState
    {
        get { return previousState; }
        set { previousState = value; }
    }

    private State<T> globalState = null;
    public State<T> GlobalState
    {
        get { return globalState; }
        set { globalState = value; }
    }
    public void Awake () {
		this.state = null;
	}

	public void Init (T agent, State<T> startState, State<T> globalState) {
		this.agent = agent;
		this.state = startState;
        this.GlobalState = globalState;
	}

    public void Init(T agent, State<T> startState)
    {
        this.agent = agent;
        this.state = startState;
    }

    public void Update () {
        if (this.GlobalState != null) this.GlobalState.Execute(this.agent);
		if (this.state != null) this.state.Execute(this.agent);
	}
	
	public void ChangeState (State<T> nextState) {
		if (this.state != null) this.state.Exit(this.agent);
        this.previousState = this.state;
        this.state = nextState;
         
		if (this.state != null) this.state.Enter(this.agent);
	}

    // Invoked when a state blip is finished
    public void RevertToPreviousState()
    {
        ChangeState(previousState);
    }

}