
using UnityEngine;

public class RL_TransitionVector{

    public RL_State state;
    public RL_Action action;

    //to simulate the stochastic character of the transitions, which
    //in our case is only relevant for the explore state we keep an
    //array of Destination States and choose them by certain probabilities
    //(at least for the explore state)
    public RL_State[] sDest;

    float prob;

    public RL_TransitionVector(RL_State state, RL_Action action,RL_State[] sDest, float prob = 0.3f){

        this.state = state;
        this.action = action;
        this.sDest = sDest;
        this.prob = prob;
    }

    public RL_State ChooseDestState(){

        if(action.name == "explore"){

            if(Random.Range(0f,1f) > this.prob){

                //make sure sDest[0] = oldState
                return sDest[0];
            }else{

                //make sure sDest[1] = newState
                return sDest[1];
            }
        }else{

            //for all other actions there should only be one state
            return sDest[0];
        }
    }
}