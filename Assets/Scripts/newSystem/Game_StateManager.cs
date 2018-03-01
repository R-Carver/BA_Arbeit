
using UnityEngine;

public class Game_StateManager : MonoBehaviour{

    private Momo thisMomo;
    private FoodFinder foodFinder;
    private Game_Util util;

    public RL_State oldState;

    private RL_State _currentState;
    public RL_State CurrentState{

        get {return _currentState;}
        set {
            oldState = _currentState;
            _currentState = value;

            if(oldState != _currentState){

                if(oldState != null){
                //Debug.Log("<b><color=teal>StateManager oldState: </color></b>" + oldState.name);
                }
                if(_currentState != null){
                //TODO: Remove this
                    string message = CurrentState.name;
                    LogController.Instance.AddLogMessage(this.transform.gameObject,"StateManager CurrentState: " + message);
                    
                    message = util.foodFinder.colliders.Length.ToString();
                    LogController.Instance.AddLogMessage(this.transform.gameObject, "***See " + message + " Food after change of state");
                }

                util.executor.ResetActions();
                util.executor.hasAction = false;
                //Debug.Log("GameState changed: " + CurrentState.name);
            }
        }
    }
    private RL_QLerner qLerner;

    void Start(){

        foodFinder = GetComponent<FoodFinder>();
        thisMomo = WorldController.Instance.getMomoFromGo(this.gameObject);
        qLerner = GetComponent<RL_QLerner>();
        util = GetComponent<Game_Util>();
    }

    void Update(){

        CheckTransitions();
    }

    private void CheckTransitions(){

        if(thisMomo.GetRessourceCount() == 0 && foodFinder.colliders.Length == 0){

            CurrentState = qLerner.states[0];
        }

        if(thisMomo.GetRessourceCount() == 0 && foodFinder.colliders.Length > 0){

            CurrentState = qLerner.states[1];
        }

        if(thisMomo.GetRessourceCount() > 0 && foodFinder.colliders.Length > 0){

            CurrentState = qLerner.states[2];
        }

        if(thisMomo.GetRessourceCount() > 0 && foodFinder.colliders.Length == 0){

            CurrentState = qLerner.states[3];
        }

        qLerner.currentState = CurrentState;
    }

    //this function gives other functions to check the state transitions additionaly to the
    //normal StateManager Update Loop, so we can be sure those checks happen before something else
    public void CheckTransitionsExtra(){

        CheckTransitions();
    }
}