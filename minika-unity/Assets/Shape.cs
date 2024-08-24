using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public int id;
    public int points;
    public bool inBox;
    public bool touchSameShape;
    // Start is called before the first frame update
    void Start()
    {
        id = -1;
        inBox = false;
        touchSameShape = false;
    }

    void checkTouchShape()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (id > -1)
        {
            checkTouchShape();
        }
        
    }
}
