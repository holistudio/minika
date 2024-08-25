using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public string type;
    public int id;
    public int points;
    public bool inBox;
    public bool touchSameShape;
    public int sameShapeID;

    private CircleCollider2D circleCollider;
    // Start is called before the first frame update
    void Start()
    {
        // id = -1;
        // inBox = false;
        touchSameShape = false;
        circleCollider = GetComponent<CircleCollider2D>();
    }

    bool checkTouchShape()
    {
        // Get the center position and radius of the circle
        Vector2 circlePosition = circleCollider.bounds.center;
        float circleRadius = circleCollider.radius;

        // Find all colliders that overlap with this circle's area
        Collider2D[] colliders = Physics2D.OverlapCircleAll(circlePosition, circleRadius);

        // Iterate through each collider found
        foreach (Collider2D collider in colliders)
        {
            // Check if the collider is not the same as the circle itself (to avoid self-detection)
            if (collider != circleCollider)
            {
                if(collider.gameObject.tag.Equals("Shape") && collider.gameObject.GetComponent<Shape>().type.Equals(gameObject.GetComponent<Shape>().type))
                {
                    sameShapeID = collider.gameObject.GetComponent<Shape>().id;
                    gameObject.transform.parent.GetComponent<Box>().addTouchingShapesPair(id,sameShapeID);
                    return true;
                }
            }
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        if ((id > -1) && inBox)
        {
            touchSameShape = checkTouchShape();
        }
        
    }
}
