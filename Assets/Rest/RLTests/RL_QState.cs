
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RL_QState{

    public RL_State state;
    public Dictionary<string, float> qValues;

    public RL_QState(RL_State state){

        this.state = state;
        qValues = new Dictionary<string, float>();
    }

    public void addQValue(RL_Action action, float value){

        qValues.Add(action.name, value);
    }

    public void UpdateQState(RL_Action action, RL_QState destinationState){

        //FIXME: Dest State and Current State are the same

        //Debug.Log("Max of Destinationstate: " + destinationState.qValues.Values.Max());
        Debug.Log("Current State: " + state.name);
        Debug.Log("Destination State: " + destinationState.state.name);
        float sample = action.reward + destinationState.qValues.Values.Max();
        float alpha = RL_QLerner.alpha;
        qValues[action.name] = (1-alpha) * qValues[action.name] + alpha * sample;  
    }

    public RL_Action getMaxAction(){

        float maxSoFar = -1000f;
        RL_Action maxAction = null;
        foreach(string action in qValues.Keys){

            if(qValues[action] >= maxSoFar){
                maxAction = state.GetActionFromName(action);
            }
        }
        if(maxAction == null){
            Debug.Log("maxAction was null - RL_QState getMaxAction line 40");
        }
        return maxAction;
    }

    public RL_Action getRandomAction(){

        int range = state.actionCount;

        RL_Action randomAction = (RL_Action)state.actions[Random.Range(0,range)];
        if(randomAction == null){
            Debug.Log("randomAction was null - RL_QState getRandomAction line 51");
        }
        return randomAction;
    }
}