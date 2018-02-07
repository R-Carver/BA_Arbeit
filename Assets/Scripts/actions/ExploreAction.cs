
using UnityEngine;

[CreateAssetMenu (menuName = "TradingGame/Actions/Explore")]
public class ExploreAction: Action 
{   
    public float targetThreshold = 0.4f;

    public override void Act(StateController controller){

        Explore(controller);
    }

    public override void CleanUp(StateController controller){

        controller.myAIPath.target = null;
    }

    private void Explore(StateController controller){

        if(checkTargetReached(controller) == true){

            SetNewDestination(controller);
        }
    }

    private bool checkTargetReached(StateController controller){

        if(controller.myAIPath.target != null){
            //If TargetPosition - CurrentPosition < Threshold
            if((controller.myAIPath.target.position - controller.characterPosition).magnitude < targetThreshold){

                return true;
            }

            return false;
        }
        return true;
        
    }

    private void SetNewDestination(StateController controller){

        //set destination to something in the range of the explorable area
        float targetX = (float)Random.Range(0, WorldController.Instance.world.Width);
        float targetY = (float)Random.Range(0, WorldController.Instance.world.Height);

        GameObject tempGo = new GameObject();
        tempGo.transform.position = new Vector3(targetX, targetY, 0);
        controller.myAIPath.target = tempGo.transform;
    }
}