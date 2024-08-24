using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public int score = 0;

    public string nextShape;
    private string[] nextPossibleShapes = {"Cherry", "Strawberry", "Grapes", "Tangerine"};
    public bool gameOver = false;

    public GameObject cursor;
    public GameObject possibleShapes;
    // Start is called before the first frame update
    void Start()
    {
        nextShape = updateNextShape();
        Debug.Log(nextShape);
    }

    string updateNextShape()
    {
        int randomIndex = Random.Range(0, nextPossibleShapes.Length);
        nextShape = nextPossibleShapes[randomIndex];
        // find shape with name
        Transform nextShapeTransform = possibleShapes.transform.Find(nextShape);

        if (nextShapeTransform != null)
        {
            GameObject nextShapeObject = nextShapeTransform.gameObject;
            // Duplicate the found object
            GameObject duplicateObject = Instantiate(nextShapeObject);

            Vector3 objectScale = duplicateObject.transform.localScale;

            duplicateObject.transform.SetParent(cursor.transform);

            // Set local scale
            duplicateObject.transform.localScale = new Vector3(objectScale.x/cursor.transform.localScale.x,objectScale.y/cursor.transform.localScale.y,1);

            // Set position
            duplicateObject.transform.localPosition = Vector3.zero;

            // Set as cursor's currentShape
            cursor.GetComponent<Cursor>().currentShape = duplicateObject;
        }
        else
        {
            Debug.Log("Child object not found.");
        }

        return nextShape;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
