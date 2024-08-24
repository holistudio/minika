using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public int score = 0;

    public string nextShape;
    private string[] nextPossibleShapes = {"Cherry", "Strawberry", "Grapes", "Tangerine"};
    public bool gameOver = false;

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
        return nextPossibleShapes[randomIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
