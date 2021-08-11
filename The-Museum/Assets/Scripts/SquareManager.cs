using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Material> colors;
    
    public void changeColor()
    {
        gameObject.GetComponent<MeshRenderer>().material = colors[1];
    }

    public void resetColor()
    {
        gameObject.GetComponent<MeshRenderer>().material = colors[0];
    }
}
