using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public GameObject currentShape;
    public float xPosition;
    private float testPosition;
    private float cursorSize;
    public float minPosition;
    public float maxPosition;
    public bool dropped;
    public float sensitivity;

    public GameObject box;

    // Start is called before the first frame update
    void Start()
    {
        xPosition = transform.position.x;
        testPosition = transform.position.x;
        cursorSize = transform.localScale.x;
    }

    bool checkInBounds(float testPosition)
    {
        if (((testPosition - (cursorSize*currentShape.transform.localScale.x/2)) > minPosition) & ((testPosition + (cursorSize*currentShape.transform.localScale.x/2)) < maxPosition))
        {
            return true;
        }
        return false;
    }
    void moveCursor(string direction)
    {

        if (direction.Equals("left"))
        {
            testPosition = xPosition - sensitivity / 5f;
        }
        if (direction.Equals("right"))
        {
            testPosition = xPosition + sensitivity / 5f;
        }

        // check if next cursor position is within bounds
        if(checkInBounds(testPosition))
        {
            // move cursor to next position
            xPosition = testPosition;
            transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
        }
        
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            if (e.keyCode == KeyCode.LeftArrow)
            {
                moveCursor("left");
            }
            if (e.keyCode == KeyCode.RightArrow)
            {
                moveCursor("right");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Detect only key press down for dropping shapes
        // with space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Add physics to the shape
            currentShape.GetComponent<Rigidbody2D>().simulated = true;
            currentShape.GetComponent<CircleCollider2D>().enabled = true;

            // set currentShape as child of Box
            currentShape.transform.SetParent(box.transform);

            // set dropped to true
            dropped = true;
        }
    }
}
