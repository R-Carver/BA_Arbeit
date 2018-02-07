using System;
using UnityEngine;

[CreateAssetMenu(menuName = "TradingGame/Actions/CollectRessource")]
public class CollectRessourceAction : Action
{   
    public float targetThreshold = 0.4f; 

    public override void Act(StateController controller)
    {   
        if(controller.foodFinder.foodTarget == null && controller.foodFinder.colliders.Length > 0){
            controller.foodFinder.foodTarget = ChooseFood(controller);
            controller.myAIPath.target = controller.foodFinder.foodTarget.transform;
        }
        
        
        if(controller.myAIPath.target != null){

            //check if you allready reached the ressource
            if((controller.myAIPath.target.position - controller.characterPosition).magnitude < targetThreshold){

                //The food finder knows about the current food
                Food currFood = WorldController.Instance.getFoodfromGo(controller.foodFinder.foodTarget);

                //now add the food to the ressources of this Momo
                controller.myMomo.addNewRessource(currFood);

                //TODO: Remove the food Go from the world since it is in the player cart
                Destroy(controller.foodFinder.foodTarget);

                //DO we have to set the target to null in order to get to the next ressource?
                controller.myAIPath.target = null;
            }
        }
        
    }

    public override void CleanUp(StateController controller)
    {
        
    }

    private GameObject ChooseFood(StateController ctr){

        //Randomly choose one of the food objects that is currently visible
        //FIXME: For now we assume the colliders are only food objects
        if(ctr.foodFinder.colliders.Length > 0){

            int foodIndex = UnityEngine.Random.Range(0,ctr.foodFinder.colliders.Length);
            if(ctr.foodFinder.colliders[foodIndex] != null)
                return ctr.foodFinder.colliders[foodIndex].gameObject;
        }
        return null;
    }
}