using UnityEngine;

public class FoodFinder : MonoBehaviour{

    public float visionRadius = 2f;
    public Collider2D[] colliders;

    public GameObject foodTarget;

    void FixedUpdate(){

        colliders = Physics2D.OverlapCircleAll(this.transform.position, visionRadius, LayerMask.GetMask("BadFood"));
    }
}