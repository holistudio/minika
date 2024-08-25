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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((id > -1) && inBox)
        {
            if(collision.gameObject.tag.Equals("Shape") && collision.gameObject.GetComponent<Shape>().type.Equals(gameObject.GetComponent<Shape>().type))
            {
                sameShapeID = collision.gameObject.GetComponent<Shape>().id;
                gameObject.transform.parent.GetComponent<Box>().addTouchingShapesPair(id,sameShapeID);
                touchSameShape = true;
            }
        }
        touchSameShape = false;
    }
    // Update is called once per frame
    void Update()
    {
        
        
    }
}
