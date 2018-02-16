using UnityEngine;

public class CameraController : MonoBehaviour {

    float xAxis;
    float yAxis;

    Vector3 cameraPosition;
    float camXBorder;
    float camYBorder;

    void Start(){

    }

    void Update(){

        UpdateCameraMovement();
    }

    void CheckCameraRange(){

        Vector3 cameraPosition = Camera.main.transform.position;

        if(cameraPosition.x >= 50){
            camXBorder = cameraPosition.x + Camera.main.orthographicSize;
        }else{
            camXBorder = cameraPosition.x - Camera.main.orthographicSize;
        }

        if(cameraPosition.y >= 50){
            camYBorder = cameraPosition.y + (Camera.main.orthographicSize * 1/Camera.main.aspect);
        }else{
            camYBorder = cameraPosition.y - (Camera.main.orthographicSize * 1/Camera.main.aspect);
        }
        
        
        

        if(WorldController.Instance.world.Width < camXBorder ){

            //camera leaving to the right
            Camera.main.transform.position = 
                new Vector3(WorldController.Instance.world.Width - Camera.main.orthographicSize,
                            Camera.main.transform.position.y,
                            Camera.main.transform.position.z);

        }else if(camXBorder < 0f)
        {
            //camera leaving to the left
            Camera.main.transform.position = 
                new Vector3(Camera.main.orthographicSize,
                            Camera.main.transform.position.y,
                            Camera.main.transform.position.z);
        }

        if(WorldController.Instance.world.Height < camYBorder ){

            //camera leaving the top
            Camera.main.transform.position = 
                new Vector3(Camera.main.transform.position.x,
                            WorldController.Instance.world.Height - (Camera.main.orthographicSize * 1/Camera.main.aspect),
                            Camera.main.transform.position.z);

        }else if(camYBorder < 0f)
        {
            //camera leaving the bottom
            Camera.main.transform.position = 
                new Vector3(Camera.main.transform.position.x,
                            (Camera.main.orthographicSize * 1/Camera.main.aspect),
                            Camera.main.transform.position.z);
        }

    }

    void UpdateCameraMovement(){

        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        if(Camera.current != null){
            Camera.main.transform.Translate(new Vector3(xAxis, yAxis, 0.0f));
            CheckCameraRange();
        }

        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 5f, 35f);
    }
}