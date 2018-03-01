
using UnityEngine;

public class RL_Simulation : MonoBehaviour{

    /*RL_QLerner myQLerner;
    RL_Simulator mySimulator;
    RL_MDP myMdp;

    private RL_State currentState;
    private RL_Action action;
    private RL_State destState;
    private RL_QState currentQState;
    private RL_QState destQState;

    void Start(){

        myQLerner = RL_QLerner.Instance;
        mySimulator = new RL_Simulator();
        myMdp = new RL_MDP();
        StartSimulation();
    }

    void Update(){

    }

    public void StartSimulation(){

        InvokeRepeating("Simulate", 2.0f, 3.0f);
    }

    public void Simulate(){

        //Debug.Log("Im simulating...");
        currentState =  myQLerner.currentState;

        //1) choose some action from your current state
        action = myQLerner.chooseAction(currentState);
        
        //2) simulate action
        if(action != null){
            mySimulator.SimulateAction(action);     //the Simulator sets the reward of the action
        }else{
            Debug.Log("Action was null - Simulation line 44");
        }

        //3) get destination state
        destState = myMdp.GetDestinationState(currentState, action);

        //4) update qValues
        currentQState = myQLerner.qStateFromState[currentState.name];
        destQState = myQLerner.qStateFromState[destState.name];
        currentQState.UpdateQState(action, destQState);
        
        //5) set destination state as new current state
        myQLerner.currentState = destState;

    }*/
}