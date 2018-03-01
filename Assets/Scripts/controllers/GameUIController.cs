using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameUIController : MonoBehaviour{
    

    public GameObject[] momos;

    //Here is the current player money in the upper right
    public Text playerMoney;
    public Dictionary<Momo, GameObject> momoPanelMap;

    private GameController gameController;

    //for UpdateSelectionPanel
    GameObject momoPanel;
    GameObject selectPanel;

    //MomoDetails panel
    public Dictionary<Momo, GameObject> momoDetailsPanelmap;

    public GameObject[] detailsPanels;
    private int freeDetailsPanelIndex = 0;

    void Start(){

        gameController = GameController.Instance;
        momoPanelMap = new Dictionary<Momo, GameObject>();
        momoDetailsPanelmap = new Dictionary<Momo, GameObject>();

        playerMoney.text = 0.ToString();
        InitMomoPanel();
        InitCallbacks();
        InitDetailsPanel();

        gameController.RegisterCbMomoChosen(AddDetailsPanel);
    }

    void Update(){

        UpdateSelectionPanel();
        UpdatePlayerMoney();
    }

    public void AddDetailsPanel(Momo momo){

        //get the next free datails panel
        GameObject detailsPanel = detailsPanels[freeDetailsPanelIndex];
        freeDetailsPanelIndex ++;

        //add the momo panel pair
        momoDetailsPanelmap.Add(momo, detailsPanel);

        //activate the panel
        detailsPanel.SetActive(true);

        PopulateDetailsPanel(momo, detailsPanel);
    }

    private void PopulateDetailsPanel(Momo momo, GameObject detailsPanel){

        GameObject momoPic = detailsPanel.transform.Find("MomoPic").gameObject;
        momoPic.GetComponent<Image>().color = MomoSpriteController.Instance.momoColorMap[momo];

    }

    private void UpdatePlayerMoney(){

        playerMoney.text = GameController.Instance.playerTotal.ToString();
    }

    private void UpdateSelectionPanel(){

        if(gameController.selectedMomo != null){

            momoPanel = momoPanelMap[gameController.selectedMomo];
            selectPanel = momoPanel.transform.GetChild(0).gameObject;

            if(selectPanel.active == false){

                selectPanel.SetActive(true);

                if(gameController.previouslySelected != null){

                    momoPanel = momoPanelMap[gameController.previouslySelected];
                    selectPanel = momoPanel.transform.GetChild(0).gameObject;
                    selectPanel.SetActive(false);
                }
            }
        }
    }

    private void InitDetailsPanel(){

        foreach(GameObject detailsPanel in detailsPanels){

            detailsPanel.SetActive(false);
        }
    }

    private void InitMomoPanel(){

        //first deactivate all panels and later only activate those for which
        //there is a MOmo in the scene
        foreach(GameObject momoPanel in momos){

            momoPanel.SetActive(false);
        }

        int panelIndex = 0;
        foreach(Momo momo in WorldController.Instance.world.theMomos){

            momos[panelIndex].SetActive(true);
            momoPanelMap.Add(momo, momos[panelIndex]);

            Color momoColor = MomoSpriteController.Instance.momoColorMap[momo];

            //make sure the button is the second child of the panel
            //the first child is the select Panel
            GameObject button = momos[panelIndex].transform.GetChild(1).gameObject;
            button.GetComponent<Image>().color = momoColor;

            //set the text of the button to the same color
            GameObject textGo = button.transform.GetChild(0).gameObject;
            Text text = textGo.GetComponent<Text>();
            text.color = momoColor; 
            text.text = "0";

            //since on start nothing is selected we deactivate all select Panels
            GameObject selectPanel = momos[panelIndex].transform.GetChild(0).gameObject;
            selectPanel.SetActive(false);

            panelIndex++;
        }
    }

    void OnTotalChanged(Momo momo){

        GameObject textGo = momoPanelMap[momo].transform.GetChild(1).GetChild(0).gameObject;
        Text text = textGo.GetComponent<Text>();

        text.text = momo.TotalTradeValue.ToString();
    }

    private void InitCallbacks(){

        foreach(Momo momo in WorldController.Instance.world.theMomos){

            momo.RegisterCbTotalChanged(OnTotalChanged);
        }
    }
} 

