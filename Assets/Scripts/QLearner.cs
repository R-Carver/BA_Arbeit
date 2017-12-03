using System.Collections.Generic;
using UnityEngine;


public class QLearner {

    //TODO: The QLearner must be created and initialized somewhere else
    //Maybe its best to create a new QController

    public enum State {redFood, greenFood};
    public enum Action {eat, dontEat};

    //Those are the reaward for taking thise action
    //Note that the agent is actually not aware of them
    Dictionary<Action, float> qStateRed = new Dictionary<Action, float>();
    Dictionary<Action, float> qStateGreen = new Dictionary<Action, float>();

    //this is the learning rate (between 0-1)
    private float alpha = 0.2f;

    public void InitializeQLearner(){

        //The values at the start can be arbitrary but they should be the same
        //so the agent chooses by 50% chance
        qStateRed.Add(Action.eat, 0.5f);
        qStateRed.Add(Action.dontEat, 0.5f);
        qStateGreen.Add(Action.eat, 0.5f);
        qStateGreen.Add(Action.dontEat, 0.5f);

    }

    //this function also updates the q-values
    public Action getEstimatedAction(Food food){

        State foodState;

        if(food.sort == Food.FoodSort.red){

            foodState = State.redFood;
        }else{

            foodState = State.greenFood;
        }
        //first choose the action
        Action chosenAction = chooseProbAction(foodState);

        UpdateQState(foodState, chosenAction, food);

        return chosenAction;

    }

    //choose the action non-determinitic depending on the values of
    //the Q-states but also taking care that no action has 0 prob
    //FIXME: This is not a resuable sollution. For now this only works
    //with the food-eating decisions
    private Action chooseProbAction(State foodState){

        float threshold = 0;

        if(foodState == State.redFood)
            threshold = qStateRed[Action.eat];

        if(foodState == State.greenFood)
            threshold = qStateGreen[Action.eat];
        
        //the threashold value should be guaranteed to be between 0 and 1
        if(Random.Range(0f,1f) < threshold){

            return Action.eat;
        }else{

            return Action.dontEat;
        }
    }

    private void UpdateQState(State foodState, Action chosenAction, Food food){

        float temp;

        if(foodState == State.redFood){

            temp = qStateRed[chosenAction];

            //this is the Q-Learning Update:
            //(1-alpha) * temp + (alpha) * [sample]
            if(chosenAction == Action.eat){

                //You did eat a red dot, so you suffer
                qStateRed[chosenAction] = (1-alpha) * temp + alpha * food.getReward();
            }else{

                //You found a red dot but you didnt eat it, thats ok, so small reward
                qStateRed[chosenAction] = (1-alpha) * temp + alpha * 0.6f;
            }
            
            
        }else{

            temp = qStateGreen[chosenAction];

            //this is the Q-Learning Update:
            //(1-alpha) * temp + (alpha) * [sample]
            if(chosenAction == Action.eat){

                //You ate a green dot, yammi, biggest possible reward
                qStateGreen[chosenAction] = (1-alpha) * temp + alpha * food.getReward();
            }else{

                //You didnt eat a green dot, idiot
                qStateGreen[chosenAction] = (1-alpha) * temp + alpha * 0f;
            }
            
        }
    }

    //For Debugging  =====================================================================
    public float getQEatRed(){

        return qStateRed[Action.eat];
    }

    public float getQDontEatRed(){

        return qStateRed[Action.dontEat];
    }

    public float getQEatGreen(){

        return qStateGreen[Action.eat];
    }

    public float getQDontEatGreen(){

        return qStateGreen[Action.dontEat];
    }
    //====================================================================================

}