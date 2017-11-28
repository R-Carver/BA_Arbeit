using UnityEngine;
using System.Collections;

public class GizmoDemo : MonoBehaviour
{
    public float ThetaScale = 0.02f;
    public float radius = 4.3f;
    private int Size;
    private LineRenderer lineDrawer;
    private float Theta = 0f;

    void Start ()
    {       
        lineDrawer = gameObject.AddComponent<LineRenderer>();
        lineDrawer.material = new Material(Shader.Find("Particles/Additive"));
        lineDrawer.widthMultiplier = 0.03f;
		lineDrawer.useWorldSpace = false;
    }

    void Update ()
    {      
        Theta = 0f;
        Size = (int)((1f / ThetaScale) + 1f);
        lineDrawer.numPositions = Size; 
        for(int i = 0; i < Size; i++){          
            Theta += (2.0f * Mathf.PI * ThetaScale);         
            float x = radius * Mathf.Cos(Theta);
            float y = radius * Mathf.Sin(Theta);          
            lineDrawer.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}