using UnityEngine;
using System.Collections.Generic;
using System;


public class GameController : MonoBehaviour{

    public static GameController Instance{get; protected set;}
    
    public int mapWidth = 100;
    public int mapHeight = 100;
    public int momoCount;

    public int playerTotal = 0;

    public Momo previouslySelected;
    public Momo selectedMomo;

    public List<Momo> chosenMomos;

    Action<Momo> cbMomoChosen;

    void Start(){

        if(Instance != null){

            Debug.Log("There should never be more than one GameController");
        }
        Instance = this;

        chosenMomos = new List<Momo>();
    }

    public void AddSelectedMomo(Momo momo){

        if(chosenMomos.Count <= 3){
            chosenMomos.Add(momo);
            momo.chosen = true;

            if(cbMomoChosen != null){

                cbMomoChosen(momo);
            }
        }else{

            Debug.Log("Cannot choose more than 3 momos");
        }
        
    }

    public void SelectMomo(Momo momo){

        if(selectedMomo != null){
            previouslySelected = selectedMomo;
        }
        selectedMomo = momo;
    }

    public void RegisterCbMomoChosen(Action<Momo> callback){

        cbMomoChosen += callback;
    }

    public void UnRegisterCbMomoChosen(Action<Momo> callback){

        cbMomoChosen -= callback;
    }

}