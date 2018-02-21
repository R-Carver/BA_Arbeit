
using UnityEngine;

public class Game_StateManager : MonoBehaviour{

    private Momo thisMomo;
    private FoodFinder foodFinder;

    public RL_State oldState;

    private RL_State _currentState;
    public RL_State CurrentState{

        get {return _currentState;}
        set {
            oldState = _currentState;
            _currentState = value;
            
            if(oldState != _currentState){

                if(oldState != null){
                Debug.Log("<b><color=teal>StateManager oldState: </color></b>" + oldState.name);
                }
                if(_currentState != null){
                Debug.Log("<b><color=teal>StateManager CurrentState: </color></b>" + CurrentState.name);
                }
                Game_Util.Instance.executor.ResetActions();
                Game_Util.Instance.executor.hasAction = false;
                Debug.Log("GameState changed: " + CurrentState.name);
            }
        }
    }
    private RL_QLerner qLerner;

    void Start(){

        foodFinder = GetComponent<FoodFinder>();
        thisMomo = WorldController.Instance.getMomoFromGo(this.gameObject);
        qLerner = RL_QLerner.Instance;

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