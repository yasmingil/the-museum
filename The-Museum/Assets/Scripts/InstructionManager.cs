using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    public bool instructionOpen = false;
    public GameObject InstructionsUI;

    // Update is called once per frame
    void Update()
    {
        if (instructionOpen)
        {
            InstructionsUI.SetActive(true);
            if(Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began)
                {
                    instructionOpen = false;
                }
            }

            if (Input.GetMouseButton(0))
            {
                instructionOpen = false;
            }
        }
        else
        {
            InstructionsUI.SetActive(false);
        }
    }

    public void LoadInstructions()
    {
        InstructionsUI.SetActive(true);
        instructionOpen = true;
    }
}
