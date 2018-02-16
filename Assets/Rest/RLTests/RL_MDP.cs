using System.Collections;


public class RL_MDP{

    public ArrayList transitions;

    private RL_State destState = null;

    public RL_MDP(){

        transitions = new ArrayList();
        InitMDP();
    }

    //for now we manually initialize our MDP here

    public void InitMDP(){

        //create all states
        RL_State state1 = new RL_State("NoLoadNoTarget", 1);
        RL_State state2 = new RL_State("NoLoadSeeTarget", 2);
        RL_State state3 = new RL_State("LoadTarget", 4);
        RL_State state4 = new RL_State("LoadNoTarget", 3);

        //create all actions
        RL_Action explore = new RL_Action("explore", -0.1f);
        RL_Action collect = new RL_Action("collect", -0.1f);
        RL_Action trade = new RL_Action("trade", 0f);
        RL_Action eat = new RL_Action("eat", 0f);

        //State 1
        RL_State[] s1_explore_dest = {state1, state2};
        RL_TransitionVector s1_explore_s1 = new RL_TransitionVector(state1, explore, s1_explore_dest);
        transitions.Add(s1_explore_s1);

        //State 2
        RL_State[] s2_explore_dest = {state2};
        RL_TransitionVector s2_explore_s2 = new RL_TransitionVector(state2, explore, s2_explore_dest);
        transitions.Add(s2_explore_s2);

        RL_State[] s2_collect_dest = {state3};
        RL_TransitionVector s2_collect_s3 = new RL_TransitionVector(state2, collect, s2_collect_dest);
        transitions.Add(s2_collect_s3);

        //State 3
        RL_State[] s3_collect_dest = {state3};
        RL_TransitionVector s3_collect_s3 = new RL_TransitionVector(state3, collect, s3_collect_dest);
        transitions.Add(s3_collect_s3);

        RL_State[] s3_explore_dest = {state3};
        RL_TransitionVector s3_explore_s3 = new RL_TransitionVector(state3, explore, s3_explore_dest);
        transitions.Add(s3_explore_s3);

        RL_State[] s3_eat_dest = {state2};
        RL_TransitionVector s3_eat_s2 = new RL_TransitionVector(state3, eat, s3_eat_dest);
        transitions.Add(s3_eat_s2);

        RL_State[] s3_trade_dest = {state1};
        RL_TransitionVector s3_trade_s1 = new RL_TransitionVector(state3, trade, s3_trade_dest);
        transitions.Add(s3_trade_s1);

        //state 4
        RL_State[] s4_explore_dest = {state4};
        RL_TransitionVector s4_explore_s4 = new RL_TransitionVector(state4, explore, s4_explore_dest);
        transitions.Add(s4_explore_s4);

        RL_State[] s4_eat_dest = {state2};
        RL_TransitionVector s4_eat_s2 = new RL_TransitionVector(state4, eat, s4_eat_dest);
        transitions.Add(s4_eat_s2);

        RL_State[] s4_trade_dest = {state1};
        RL_TransitionVector s4_trade_s1 = new RL_TransitionVector(state4, trade, s4_trade_dest);
        transitions.Add(s4_trade_s1);
    }

    public RL_State GetDestinationState(RL_State oldState, RL_Action action){

        foreach(RL_TransitionVector transition in transitions){

            if(transition.state.name == oldState.name && transition.action.name == action.name){

                destState = transition.ChooseDestState();
            }
        }

        return destState;
    }
}