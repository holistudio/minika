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
    public bool winTestingMode;
    public GameObject suika;
    private Coroutine suikaCoroutine;
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
    IEnumerator MyCoroutine(float waitTime)
    {
        suika.SetActive(true);
        scoreText.gameObject.SetActive(false);
        // Wait for the given number of seconds
        yield return new WaitForSeconds(waitTime);

        yield return null;
    }

    IEnumerator StopAfterSeconds(float stopTime)
    {
        // Wait for the specified time before stopping the coroutine
        yield return new WaitForSeconds(stopTime);

        // Stop the coroutine
        StopCoroutine(suikaCoroutine);
        suika.SetActive(false);
        scoreText.gameObject.SetActive(true);
    }
    public void displaySuika()
    {
        // Start the coroutine and keep a reference to it
        suikaCoroutine = StartCoroutine(MyCoroutine(10f));
        
        // Stop the coroutine after 2 seconds
        StartCoroutine(StopAfterSeconds(5f));
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
        if(winTestingMode)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("Z button pressed");
                displaySuika();
            }
        }
    }
}
