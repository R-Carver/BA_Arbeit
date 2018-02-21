using System.Collections.Generic;
using UnityEngine;

public class RL_QLerner{

    private static RL_QLerner _instance;

    public static RL_QLerner Instance{ 
        get{
            if(_instance == null){
                _instance = new RL_QLerner();
            }
                
            return _instance;
        } 
    }

    public static float alpha = 0.5f;

    public RL_State[] states;
    public RL_QState[] qStates;
    public int numOfQStates;

    public RL_State currentState;

    //we use a dictionary to find the qState for a given state
    //There are problems with the hashing when storing objects as keys
    //so now we store the names of the states 
    public Dictionary<string, RL_QState> qStateFromState;

    //the current model assumes 4 QStates
    public RL_QLerner(int numOfQStates = 4){

        this.numOfQStates = numOfQStates;
        states = new RL_State[numOfQStates];
        qStates = new RL_QState[numOfQStates];

        qStateFromState = new Dictionary<string, RL_QState>();

        InitQStates();
        InitDictionary();

        //Start with NoLoadNoTarget state which is at index 0 in the state array
        currentState = states[0];
    }


    public RL_Action chooseAction(RL_State s){

        /*we get a qState from which we look at the qValues,
          Now we have to make sure that we choose actions according to the q values,
          but sometime also choose randomly to make sure we explore everything
        */
        RL_QState qState = null;

        //get the q state
        if(qStateFromState.ContainsKey(s.name)){
            qState = qStateFromState[s.name];
        }else{
            Debug.Log("something went wrong, key not in dictionary - RL_Qlerner Line 43");
            return null;
        }
        
        RL_Action action;

        //for testing just use 80% times we follow the policy and 20% we choose a random action
        if(Random.Range(0f,1f) < 0.8f){
    
            //choose the action with the highest value
            action = qState.getMaxAction();
        }else{

            //choose a random action from this state
            action = qState.getRandomAction();
        }
        Debug.Log("Action chosen: " + action.name);
        return action;
    }

    private void InitQStates(){

        //create all possible actions
        RL_Action explore = new RL_Action("explore", -0.1f);
        RL_Action collect = new RL_Action("collect", -0.1f);
        RL_Action trade = new RL_Action("trade", 0f);
        RL_Action eat = new RL_Action("eat", 0f);

        //State1 ------------------------------------------------------------

        RL_State state1 = new RL_State("NoLoadNoTarget", 1);
        state1.addAction(explore);

        RL_QState qState1 = new RL_QState(state1);
        qState1.state = state1;

        //Initial qValue is 0 unless you want to start with some predetermined values
        qState1.addQValue(explore,0f);

        states[0] = state1;
        qStates[0] = qState1;

        //-------------------------------------------------------------------

        //State2 ------------------------------------------------------------

        RL_State state2 = new RL_State("NoLoadSeeTarget", 2);
        state2.addAction(explore);
        state2.addAction(collect);

        RL_QState qState2 = new RL_QState(state2);
        qState2.state = state2;

        //Initial qValue is 0 unless you want to start with some predetermined values
        qState2.addQValue(explore, 0f);
        qState2.addQValue(collect,0f);

        states[1] = state2;
        qStates[1] = qState2;

        //-------------------------------------------------------------------

        //State3 ------------------------------------------------------------

        RL_State state3 = new RL_State("LoadTarget", 4);
        state3.addAction(explore);
        state3.addAction(collect);
        state3.addAction(eat);
        state3.addAction(trade);

        RL_QState qState3 = new RL_QState(state3);
        qState3.state = state3;

        //Initial qValue is 0 unless you want to start with some predetermined values
        qState3.addQValue(explore, 0f);
        qState3.addQValue(collect,0f);
        qState3.addQValue(eat,0f);
        qState3.addQValue(trade,0f);

        states[2] = state3;
        qStates[2] = qState3; 

        //-------------------------------------------------------------------

        //State4 ------------------------------------------------------------

        RL_State state4 = new RL_State("LoadNoTarget", 3);
        state4.addAction(explore);
        state4.addAction(eat);
        state4.addAction(trade);

        RL_QState qState4 = new RL_QState(state4);
        qState4.state = state4;

        //Initial qValue is 0 unless you want to start with some predetermined values
        qState4.addQValue(explore, 0f);
        qState4.addQValue(eat,0f);
        qState4.addQValue(trade,0f);

        states[3] = state4;
        qStates[3] = qState4;

        //-------------------------------------------------------------------
    }


    private void InitDictionary(){

        for(int i=0; i < numOfQStates; i++){

            qStateFromState.Add(states[i].name,qStates[i]); 
        }
    }

}