using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void getShape (int shapeID)
    {

    }

    ArrayList checkSameShapesTouch(ArrayList shapesInBox)
    {
        
        ArrayList pairs = new ArrayList();
        // go through the shapes in box

        // return list of pairs of ids
        return pairs;
    }

    void insertShape(string shapeName, Vector3 newPosition, int newID)
    {
        // insert the shape inside the box at a specified position
    }

    void mergeShape(GameObject shape1, GameObject shape2)
    {
        // get shape1's ID
        
        // compute the midpoint between the two shapes' centroids

        // determine the next largest shape

        // delete both shape1 and shape2

        // insert new shape

    }

    // Update is called once per frame
    void Update()
    {
        // check if the same shapes are touching
        // and if so, get a list of ID pairs

        // for each list of ID pairs
        // merge shapes into the next largest shape
    }
}
