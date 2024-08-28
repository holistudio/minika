using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public int score = 0;

    public string nextShape;
    private string[] nextPossibleShapes = {"Cherry", "Strawberry", "Grapes", "Tangerine", "Orange"};
    public bool gameOver = false;

    public GameObject cursor;
    public GameObject nextShapeDisplay;
    public GameObject possibleShapes;
    public TextMeshPro scoreText;
    // Start is called before the first frame update
    void Start()
    {
        nextShape = getNextShape();
        updateScoreDisplay();
    }
    GameObject copyShapeToParent(GameObject shape, GameObject parent)
    {
        // Duplicate the found object
        GameObject duplicateObject = Instantiate(shape);

        Vector3 objectScale = duplicateObject.transform.localScale;

        duplicateObject.transform.SetParent(parent.transform);

        // Set local scale
        duplicateObject.transform.localScale = new Vector3(objectScale.x/parent.transform.localScale.x,objectScale.y/parent.transform.localScale.y,1);

        // Set position
        duplicateObject.transform.localPosition = Vector3.zero;

        return duplicateObject;
    }
    void deleteNextShape()
    {
        foreach (Transform item in nextShapeDisplay.transform)
        {
            if(item.gameObject.tag.Equals("Shape"))
            {
                Destroy(item.gameObject);
                break;
            }
        }
    }
    void displayNextShape (string nextShape)
    {
        // find shape with name
        Transform nextShapeTransform = possibleShapes.transform.Find(nextShape);
        if (nextShapeTransform != null)
        {
            GameObject nextShapeObject = nextShapeTransform.gameObject;

            // Duplicate the found object
            GameObject duplicateObject = copyShapeToParent(nextShapeObject, nextShapeDisplay);
        }
        else
        {
            Debug.Log("Child object not found.");
        }
    }

    string getNextShape()
    {
        int randomIndex = Random.Range(0, nextPossibleShapes.Length);
        nextShape = nextPossibleShapes[randomIndex];
        return nextShape;
    }

    void updateCurrentShape(string shapeName)
    {
        // find shape with name
        Transform nextShapeTransform = possibleShapes.transform.Find(nextShape);

        if (nextShapeTransform != null)
        {
            GameObject nextShapeObject = nextShapeTransform.gameObject;

            // Duplicate the found object
            GameObject duplicateObject = copyShapeToParent(nextShapeObject, cursor);

            // Set as cursor's currentShape
            cursor.GetComponent<Cursor>().currentShape = duplicateObject;
        }
        else
        {
            Debug.Log("Child object not found.");
        }
    }
    public void updateScoreDisplay()
    {
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (cursor.GetComponent<Cursor>().dropped)
        {
            deleteNextShape();
            updateCurrentShape(nextShape);
            cursor.GetComponent<Cursor>().dropped = false;
            nextShape = getNextShape();
            displayNextShape(nextShape);
        }
    }
}
