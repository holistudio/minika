using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject possibleShapes;
    private ArrayList possibleShapesList;
    private ArrayList touchingPairs;
    // Start is called before the first frame update
    void Start()
    {
        possibleShapesList = new ArrayList();
        foreach (Transform child in possibleShapes.transform)
        {
            if (child.gameObject.tag.Equals("Shape"))
            {
                possibleShapesList.Add(child.gameObject.name);
            }
        }
        // foreach (string name in possibleShapesList)
        // {
        //     Debug.Log(name);
        // }
        touchingPairs = new ArrayList();
    }

    GameObject getShape(int shapeID)
    {
        // go through the shapes in box
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag.Equals("Shape"))
            {
                if (child.gameObject.GetComponent<Shape>().id == shapeID)
                {
                    return child.gameObject;
                }
            }
        }
        Debug.Log("Shape GameObject with ID not found");
        return null;
    }

    bool pairNotInList(ArrayList pairs, int[] targetPair)
    {
        int[] samePair = new int[2]{targetPair[1], targetPair[0]};

        foreach (int[] pair in pairs)
        {
            if ((pair[0] == samePair[0]) && (pair[1] == samePair[1]))
            {
                return false;
            }
            if ((pair[0] == targetPair[0]) && (pair[1] == targetPair[1]))
            {
                return false;
            }
        }
        return true;
    }

    // ArrayList checkSameShapesTouch()
    // {
    //     // go through the shapes in box
    //     foreach (Transform child in transform)
    //     {
    //         if (child.gameObject.tag.Equals("Shape"))
    //         {
    //             // if shape is touching another shape of same name
    //             if (child.GetComponent<Shape>().touchSameShape)
    //             {
    //                 int shape1ID = child.GetComponent<Shape>().id;
    //                 int shape2ID = child.GetComponent<Shape>().sameShapeID;
    //                 int[] pairIDs = new int[2] {shape1ID,shape2ID};
    //                 //check ArrayList for the opposite pair, avoid double counting
    //                 if(pairNotInList(touchingPairs, pairIDs))
    //                 {
    //                     touchingPairs.Add(pairIDs);
    //                 }
    //             }
                
    //         }
    //     }
    //     // return list of pairs of ids
    //     return touchingPairs;
    // }

    public void addTouchingShapesPair(int shape1ID, int shape2ID)
    {
        int[] pairIDs = new int[2] {shape1ID,shape2ID};
        //check ArrayList for the opposite pair, avoid double counting
        if(pairNotInList(touchingPairs, pairIDs))
        {
            touchingPairs.Add(pairIDs);
        }
    }
    void updateScore(GameObject newShape)
    {
        transform.parent.gameObject.GetComponent<Environment>().score += newShape.GetComponent<Shape>().points;
        transform.parent.gameObject.GetComponent<Environment>().updateScoreDisplay();
        Debug.Log("Score: " + transform.parent.gameObject.GetComponent<Environment>().score);
    }
    void insertShape(string shapeType, Vector3 newPosition, int newID)
    {
        // insert the shape inside the box at a specified position
        // find shape with name
        Transform nextShapeTransform = possibleShapes.transform.Find(shapeType);

        if (nextShapeTransform != null)
        {
            GameObject nextShapeObject = nextShapeTransform.gameObject;
            // Duplicate the found object
            GameObject duplicateObject = Instantiate(nextShapeObject);

            duplicateObject.GetComponent<Shape>().id = newID;
            duplicateObject.GetComponent<Shape>().inBox = true;

            duplicateObject.transform.SetParent(gameObject.transform);

            // Set position
            duplicateObject.transform.position = newPosition;

            // Update score
            updateScore(duplicateObject);

            // Add physics to the shape
            duplicateObject.GetComponent<Rigidbody2D>().simulated = true;
            duplicateObject.GetComponent<CircleCollider2D>().enabled = true;
            // Debug.Log("New Shape ID: " + duplicateObject.GetComponent<Shape>().id);
        }
        else
        {
            Debug.Log(shapeType + "Child object not found.");
        }
        
    }

    void removePair(int[] pairToRemove)
    {
        // Find the index of the pair
        int indexToRemove = -1;
        for (int i = 0; i < touchingPairs.Count; i++)
        {
            int[] currentPair = (int[])touchingPairs[i];
            if (currentPair[0] == pairToRemove[0] && currentPair[1] == pairToRemove[1])
            {
                indexToRemove = i;
                break;
            }
            if (currentPair[0] == pairToRemove[1] && currentPair[1] == pairToRemove[0])
            {
                indexToRemove = i;
                break;
            }
        }

        // If the pair is found, remove it
        if (indexToRemove != -1)
        {
            touchingPairs.RemoveAt(indexToRemove);
            // Debug.Log("Pair removed: (" + pairToRemove[0] + ", " + pairToRemove[1] + ")");
        }
        else
        {
            // Debug.Log("Pair not found: (" + pairToRemove[0] + ", " + pairToRemove[1] + ")");
        }
    }
    
    void mergeShape(GameObject shape1, GameObject shape2)
    {
        string shapeType = shape1.GetComponent<Shape>().type;

        // get shapeIDs
        int shape1ID = shape1.GetComponent<Shape>().id;
        int shape2ID = shape2.GetComponent<Shape>().id;
        int[] pairToRemove = new int[2]{shape1ID,shape2ID};
        // Debug.Log(shape1ID + " " + shape2ID);

        // compute the midpoint between the two shapes' centroids
        float newX = (shape1.transform.position.x + shape2.transform.position.x)/2;
        float newY = (shape1.transform.position.y + shape2.transform.position.y)/2;
        Vector3 newPosition = new Vector3(newX, newY, 1);

        // determine the next largest shape
        int nextIndex = possibleShapesList.IndexOf(shapeType) + 1;

        string newShapeType = (string) possibleShapesList[nextIndex];
        // Debug.Log("Two " + shapeType + " make a " + newShapeType);

        // delete both shape1 and shape2
        Destroy(shape1);
        Destroy(shape2);

        // find and remove the pair from touchingPairs ArrayList
        removePair(pairToRemove);

        // insert new shape
        insertShape(newShapeType,newPosition,shape1ID);
    }

    // Update is called once per frame
    void Update()
    {
        // check if the same shapes are touching
        // and if so, get a list of ID pairs
        // touchingPairs = checkSameShapesTouch();

        // for each list of ID pairs
        // merge shapes into the next largest shape
        while (touchingPairs.Count > 0)
        {
            int[] pair = (int[]) touchingPairs[0];
            GameObject shape1 = getShape(pair[0]);
            GameObject shape2 = getShape(pair[1]);
            mergeShape(shape1, shape2);
        }
    }
}
