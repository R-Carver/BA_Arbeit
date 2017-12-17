
[System.Serializable]
public class Transition
{
    public Decision decision;

    //Go to this state if the decision evaluates to true
    public State newState;

    //stay in this state when the decision is false
    public State oldState;
}