
using UnityEngine;

public class MouseController : MonoBehaviour{

    GameController gameController;
    Momo currentMomo;

    //For the rightlick context
    Momo momo = null;

    void Start(){

        gameController = GetComponent<GameController>();
    }

    void Update(){

        CheckClick();
        CheckRightMouseContext();
    }

    private void CheckClick(){

        if(Input.GetMouseButtonDown(0)){

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(hit.collider != null){

                //Debug.Log(hit.transform.name);
                currentMomo = WorldController.Instance.getMomoFromGo(hit.transform.gameObject);
                gameController.SelectMomo(currentMomo);
            }
        }
    }

    private void CheckRightMouseContext(){

        if(Input.GetMouseButtonDown(1)){

            //set the selected context
            momo = getMomoUnderMouse();
            if(momo != null){
                MomoSpriteController.Instance.ActivateRightMouseContext(momo);
            }else{
                Debug.Log("No Momo under the mouse - MouseController CheckRightMouseClick");
            }

        }else if(Input.GetMouseButtonUp(1)){

            //unset the context
            if(momo != null){
                MomoSpriteController.Instance.DeactivateRightMouseContext(momo);
            }else{
                Debug.Log("No Momo under the mouse - MouseController CheckRightMouseClick");
            }
        }
    }

    private Momo getMomoUnderMouse(){

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if(hit.collider != null){

            //Debug.Log(hit.transform.name);
            return WorldController.Instance.getMomoFromGo(hit.transform.gameObject);
        }
        return null;
    }

    private GameObject getGOUnderMouse(){

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if(hit.collider != null){

            //Debug.Log(hit.transform.name);
            return hit.transform.gameObject;
        }
        return null;
    }
}