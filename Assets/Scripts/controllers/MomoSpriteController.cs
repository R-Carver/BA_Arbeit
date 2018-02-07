using System.Collections.Generic;
using UnityEngine;

public class MomoSpriteController : MonoBehaviour {

    public Sprite[] momoSprites = new Sprite[5];

    World world{
        get {return WorldController.Instance.world;}
    }

    void Start(){

        //Register Callbacks for changed Ressources
        foreach(Momo momo in world.theMomos){

            momo.RegisterCbRessourcesChanged(OnRessourceChanged);
        }
    }

    void OnRessourceChanged(Momo momo, int ressourceCount){

        Debug.Log("I Have " + ressourceCount + " ressources");

        //FIXME: Something is wrong with the ressources....way to many are added


        //Change the Sprite of the momo that got the new Ressource
        //The momoGO Map is in the WorldCOntroller
        GameObject momoGo = WorldController.Instance.getGoFromMomo(momo);

        if(momoGo == null){
            Debug.LogError("momoGo was not in the Dictionary... something went wrong");
            return;
        }

        momoGo.transform.GetComponentInChildren<SpriteRenderer>().sprite = momoSprites[ressourceCount];
        
        
    }


}