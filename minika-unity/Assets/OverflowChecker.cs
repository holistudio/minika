using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverflowChecker : MonoBehaviour
{
    public bool boxOverflow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log(other.gameObject);
        if (other.gameObject.tag.Equals("Shape") && (other.gameObject.GetComponent<Shape>().inBox == false))
        {
            other.gameObject.GetComponent<Shape>().inBox = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Shape") && other.gameObject.GetComponent<Shape>().inBox)
        {
            Debug.Log("Shape Overflow Detected");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
