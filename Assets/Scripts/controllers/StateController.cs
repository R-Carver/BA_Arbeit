using UnityEngine;

public class StateController : MonoBehaviour{

    public State currentState;
    
    [HideInInspector] public AIPath myAIPath {get; protected set;}
    [HideInInspector] public Vector3 characterPosition {get; protected set;}


    void Awake(){

        myAIPath = GetComponent<AIPath>();
        myAIPath.target = this.transform;
    }

    void Update(){
        
        characterPosition = this.transform.position;
        currentState.UpdateState(this);
    }

    public void TransitionToState(State nextState){

        //currentState.CleanUpActions(this);
    }

}