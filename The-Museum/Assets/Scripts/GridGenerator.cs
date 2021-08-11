using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridGenerator : MonoBehaviour
{
    public int height = 4;
    public int width = 4;
    public Transform squarePrefab;
    public List<Transform> squareList; // target squares
    List<int> squareIndexes = new List<int>(); // target square index
    public List<Material> colors;
    public float clearTime = 3f;

    private List<int> squareSelected = new List<int>();
    private bool showedPattern = false;
    private bool reset = false;
    private float elapsedTime = 0f;
    private int numCorrect = 0;
    int totalSquares;
    int numRandSquares;
    Touch touch;

    // Start is called before the first frame update
    void Start()
    {
        totalSquares = width * height;
        numRandSquares = totalSquares / 3;
        // makes square grid
        for (int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            { 
                var square1 = Instantiate(squarePrefab, transform) as Transform;
                square1.position = new Vector3(i, j, 0);
                square1.GetComponent<Renderer>().material = colors[0];
                squareList.Add(square1);
                // label squares
                square1.name = squareList.Count.ToString();
            }
        }
        // clear random target squares
        squareIndexes.Clear();
        // make a new pattern
        Invoke("makePattern", 2.0f);
        Debug.Log("After pattern.");

    }

    // Update is called once per frame
    void Update()
    {
        if (showedPattern == true)
        {
            elapsedTime += Time.deltaTime;
        }
        

        // get input from User
        userInput();
        // clears squares selected every 5 seconds.
        if(elapsedTime >= 5f)
        {
            checkSquares();
            elapsedTime = elapsedTime % 5f;
            resetColor();
            squareSelected.Clear();
            numCorrect = 0;
        }
        if (reset == false)
        {
            Invoke("resetColor", 3.0f);
            reset = true;
        }

    }

    private void checkSquares()
    {
        
        for(int i = 0; i < squareSelected.Count; i++)
        {
            int squareNumber = squareSelected[i];
            Debug.Log("SquareSelected = " + squareSelected[i]);
            if (squareIndexes.IndexOf(squareNumber) != -1)
            {
                numCorrect++;
                //Debug.Log("numCorrect = " + numCorrect);
            }
        }
        
       
        if(numCorrect == 5)
        {
            Debug.Log("Win. Load Scene.");
            PlayerManager.keyCount++;
            PlayerManager.inGame = false;

            SceneManager.LoadScene("MuseumRoom");
            
        }
        else
        {
            Debug.Log("Try Again");
        }
    }

    private void userInput()
    {
        // TODO: Touchphase began so that the user has to click one square at a time and not click and drag
        //      user should only be able to select numRandSquares amount and afterwards, it checks if the squares are correct.
        #region Computer input
        if (Input.GetMouseButtonDown(0))
        {

            // Raycast from camera to mouse position
            Vector2 raycastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);

            if (hit.collider != null)
            {
               // int indexSelected = squareList.FindIndex(hit.collider.gameObject.transform.);
                //Debug.Log("You've hit " + hit.collider.name);
                
                int squareNumber = int.Parse(hit.collider.name) - 1;
                if (!squareSelected.Contains(squareNumber))
                {
                    squareSelected.Add(squareNumber);
                    Debug.Log("You've hit " + squareNumber);
                    if (squareIndexes.IndexOf(squareNumber) != -1)
                    {
                        hit.collider.gameObject.GetComponent<Renderer>().material = colors[2];
                        Debug.Log("Correct");
                    }
                    else
                    {
                        hit.collider.gameObject.GetComponent<Renderer>().material = colors[1];
                        Debug.Log("Wrong");
                        
                    }
                }
            }
        }
        #endregion

        #region Mobile input
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {

                // Raycast from camera to touch position
                Vector2 raycastPos = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);

                if (hit.collider != null)
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material = colors[1];
                    int squareNumber = int.Parse(hit.collider.name) - 1;
                    if (!squareSelected.Contains(squareNumber))
                    {
                        squareSelected.Add(squareNumber);
                    }
                }
            }
        }
        #endregion


    }

    void makePattern()
    {
        Debug.Log("Make pattern");
        
        
        // pick random idexes of squares that will change color// the user needs to select these indeces
        for (int i = 0; i < numRandSquares; i++)
        {
            int temp = Random.Range(0, totalSquares);
            while (squareIndexes.Contains(temp))
            {
                temp = Random.Range(0, totalSquares);
            }

            if (!squareIndexes.Contains(temp))
            {
                squareIndexes.Add(temp);
            }
        }
        showPattern();
        showedPattern = true;
    }

    void showPattern() { 

        // change color of squares 
        for(int i = 0; i < numRandSquares; i++)
        {
            int index = squareIndexes[i];
            squareList[index].gameObject.GetComponent<Renderer>().material = colors[1];
            //Debug.Log("Change Color of" + index);
        }
           
    }

    void resetColor()
    {
        int total = width * height;
        // change color of squares 
        for (int i = 0; i < total; i++)
        {
            squareList[i].gameObject.GetComponent<Renderer>().material = colors[0];
            //Debug.Log("Reset Color of" + i);
        }
    }

}
