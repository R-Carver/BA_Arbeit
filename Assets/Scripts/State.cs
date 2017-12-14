
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    public Action[] actions;
    //public Transition[] transitions;
    
    public void UpdateState(StateController controller){

        DoActions(controller);
        CheckTransitions(controller);
    }

    public void CleanUpActions(StateController ctrl){

        for (int i = 0; i < actions.Length; i++) {
             
             actions[i].CleanUp(ctrl);
        }
    }

    private void DoActions(StateController ctrl)
    {
        for (int i = 0; i < actions.Length; i++) {
             
             actions[i].Act(ctrl);
        }
    }

    private void CheckTransitions(StateController ctrl)
    {

    }

    
}