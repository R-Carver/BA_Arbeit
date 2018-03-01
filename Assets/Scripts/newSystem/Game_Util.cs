
using UnityEngine;

public class Game_Util : MonoBehaviour{

    public AIPath aiPath;
    public FoodFinder foodFinder;
    public Game_Executor executor;
    public Game_StateManager state_manager;
    public Vector3 characterPosition {get; protected set;}
    public Momo myMomo{get; protected set;}

    void Start(){

        aiPath = GetComponent<AIPath>();
        aiPath.target = this.transform;
        foodFinder = GetComponent<FoodFinder>();
        executor = GetComponent<Game_Executor>();
        state_manager = GetComponent<Game_StateManager>();
        myMomo = WorldController.Instance.getMomoFromGo(this.gameObject);
    }

    void Update(){

        characterPosition = this.transform.position;
        //Debug.Log(characterPosition);
    }

    public void destroyGameObject(GameObject go){

        Destroy(go);
    }
}