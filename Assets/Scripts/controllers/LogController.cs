using UnityEngine;
using System.IO;


public class LogController : MonoBehaviour{

    public static LogController Instance{get; protected set;}

    string basePath = "Assets/Resources/";

    StreamWriter writer;

    void Start(){

        if(Instance != null){

            Debug.Log("There should never be more than one LogController");
        }
        Instance = this;

        InitLogFiles();
    }

    private void InitLogFiles(){

        string fileName;
        string fullPath;
        //Init Momo Lof Files
        foreach(Momo momo in WorldController.Instance.world.theMomos){

            fileName = momo.GetId() + ".txt";
            fullPath = basePath + fileName;

            writer = new StreamWriter(fullPath, false);
            writer.WriteLine(momo.GetId() + " - Momo LogFile");
            writer.Close();

            //Init Food Finder LogFile
            //This doenst help
            /*fileName = momo.GetId() + "FoodFinderLog.txt";
            fullPath = basePath + fileName;

            writer = new StreamWriter(fullPath, false);
            writer.WriteLine(momo.GetId() + "FoodFinder Logfile");
            writer.Close();*/
        }

        
    }

    //this takes a Momo and not a MonoBehaviour
    private void AddLogMessageLocal(Momo momo, string message){

        string fileName = momo.GetId() + ".txt";
        string fullPath = basePath + fileName;

        writer = new StreamWriter(fullPath, true);
        writer.WriteLine(message);
        writer.Close();
    }

    //this function is only there to make it more convenient for the other classes to call
    //the loggind function with a MonoBehaviour
    public void AddLogMessage(GameObject momoGo, string message){

        Momo momo = WorldController.Instance.getMomoFromGo(momoGo);
        AddLogMessageLocal(momo, message);
    }

    /*public void AddLogFoodFinderLocal(Momo momo, string message){

        string fileName = momo.GetId() + "FoodFinderLog.txt";
        string fullPath = basePath + fileName;

        writer = new StreamWriter(fullPath, true);
        writer.WriteLine(message);
        writer.Close();
    }

    //this function is only there to make it more convenient for the other classes to call
    //the loggind function with a MonoBehaviour
    public void AddLogFoodFinder(GameObject momoGo, string message){

        Momo momo = WorldController.Instance.getMomoFromGo(momoGo);
        AddLogFoodFinderLocal(momo, message);
    }*/
}