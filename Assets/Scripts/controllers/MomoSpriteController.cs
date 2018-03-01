using System.Collections.Generic;
using UnityEngine;

public class MomoSpriteController : MonoBehaviour {

    public static MomoSpriteController Instance{get; protected set;}

    public Sprite[] momoSprites = new Sprite[5];
    public Transform selectionCircle;
    
    private Color[] colors;
    private string[] colorIds;
    public Dictionary<Momo, Color> momoColorMap;

    private GameController gameController;

    //related to the right button context
    private GameObject momoUnderMouse;
    private GameObject selectButton;

    World world{
        get {return WorldController.Instance.world;}
    }

    void Start(){

        if(Instance != null){

            Debug.Log("There should never be more than one MomoSpriteCOntroller");
        }
        Instance = this;

        //Register Callbacks for changed Ressources
        foreach(Momo momo in world.theMomos){

            momo.RegisterCbRessourcesChanged(OnRessourceChanged);
        }

        gameController = WorldController.Instance.GetGameController();

        InitColors();
        InitColorDict();
        SetInitialColors();
        DeactivateRightMouseContext();
        
    }

    void Update(){

        UpdateSelectionCircle();
        UpdateRightMouseContext();
    }

    private void DeactivateRightMouseContext(){

        foreach(Momo momo in WorldController.Instance.world.theMomos){

            GameObject momoGo = WorldController.Instance.getGoFromMomo(momo);
            GameObject selectButton = momoGo.transform.Find("SelectionButton").gameObject;
            selectButton.SetActive(false);
        }
    }

    private void UpdateRightMouseContext(){

        if(momoUnderMouse != null){

            selectButton = momoUnderMouse.transform.Find("SelectionButton").gameObject;
            selectButton.SetActive(true);
        }
    }

    private void UpdateSelectionCircle(){

        if(gameController.selectedMomo == null){

            return;
        }else{
            //Debug.Log("Selected Momo: " + gameController.selectedMomo);
            selectionCircle = WorldController.Instance.GetCircleFromMomo(gameController.selectedMomo);
            if(selectionCircle.gameObject.active == false){

                //make the circle of the selected momo active
                selectionCircle.gameObject.active = true;

                //deactivate the circle of the previous momo
                if(gameController.previouslySelected != null){

                    Transform oldCircle = WorldController.Instance.GetCircleFromMomo(gameController.previouslySelected);
                    oldCircle.gameObject.active = false;
                }
            }
            
        }
    }

    private void SetInitialColors(){

        foreach(Momo momo in WorldController.Instance.world.theMomos){

            GameObject momoGo = WorldController.Instance.getGoFromMomo(momo);
            SpriteRenderer momoSpriteRenderer = momoGo.transform.GetComponentInChildren<SpriteRenderer>();
            momoSpriteRenderer.color = momoColorMap[momo];

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

        SpriteRenderer momoSpriteRenderer = momoGo.transform.GetComponentInChildren<SpriteRenderer>();
        momoSpriteRenderer.sprite = momoSprites[ressourceCount];
        momoSpriteRenderer.color = momoColorMap[momo];
        
    }

    

    private void InitColors(){

        colors = new Color[8];
        colorIds = new string[8];

        colors[0] = Color.blue;
        colorIds[0] = "blue";

        colors[1] = Color.cyan;
        colorIds[1] = "cyan";

        colors[2] = Color.gray;
        colorIds[2] = "gray";

        colors[3] = Color.green;
        colorIds[3] = "green";

        colors[4] = Color.magenta;
        colorIds[4] = "magenta";
        
        colors[5] = Color.red;
        colorIds[5] = "red";

        colors[6] = Color.white;
        colorIds[6] = "white";

        colors[7] = Color.yellow;
        colorIds[7] = "yellow";
    }

    private void InitColorDict(){

        momoColorMap = new Dictionary<Momo, Color>();
        int colorIndex = 0;

        foreach(Momo momo in WorldController.Instance.world.theMomos){

            momoColorMap.Add(momo, colors[colorIndex]);
            momo.SetId(colorIds[colorIndex]);
            colorIndex++ ;
        }
    }

    public void ActivateRightMouseContext(Momo momo){

        momoUnderMouse = WorldController.Instance.getGoFromMomo(momo);
    }

    public void DeactivateRightMouseContext(Momo momo){

        if(momoUnderMouse != null){

            selectButton = momoUnderMouse.transform.Find("SelectionButton").gameObject;
            selectButton.SetActive(false);
        }
        momoUnderMouse = null;
    }

}