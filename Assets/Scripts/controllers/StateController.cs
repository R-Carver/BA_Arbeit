using UnityEngine;

public class StateController : MonoBehaviour{

    public State currentState;
    public FoodFinder foodFinder {get; protected set;}
    
    [HideInInspector] public AIPath myAIPath {get; protected set;}
    [HideInInspector] public Vector3 characterPosition {get; protected set;}

    public Momo myMomo{get; protected set;}

    void Awake(){

        myAIPath = GetComponent<AIPath>();
        myAIPath.target = this.transform;

        foodFinder = GetComponent<FoodFinder>();
        
    }

    void Start(){
        myMomo = WorldController.Instance.getMomoFromGo(this.gameObject);
    }

    void Update(){
        
        characterPosition = this.transform.position;
        currentState.UpdateState(this);
    }

    public void TransitionToState(State nextState){

        currentState.CleanUpActions(this);
        currentState = nextState;
    }

    public void collectFood(){

        
    }

}