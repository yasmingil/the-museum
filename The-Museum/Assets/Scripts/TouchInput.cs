using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    
    public float moveSpeed = 1f;
    Rigidbody2D rb;

    Touch touch;
    Vector3 touchPos;
    Vector3 targetPos;
    bool isMoving = false;

    float initialDistanceToTarget;
    float currentDistanceToTarget;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            currentDistanceToTarget = (touchPos - transform.position).magnitude;
        }
        // rb.angularVelocity = 0;

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            
            initialDistanceToTarget = 0;
            currentDistanceToTarget = 0;
            isMoving = true;
            touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchPos.z = 0;
            targetPos = (touchPos - transform.position).normalized;

            rb.velocity = new Vector2(targetPos.x, targetPos.y).normalized * moveSpeed * Time.deltaTime;
            // transform.eulerAngles = Vector3.zero;

            

        }
        if (Input.GetMouseButton(0))
        {
            initialDistanceToTarget = 0;
            currentDistanceToTarget = 0;
            isMoving = true;
            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPos.z = 0;
            targetPos = (touchPos - transform.position).normalized;

            rb.velocity = new Vector2(targetPos.x, targetPos.y).normalized * moveSpeed * Time.deltaTime;
            
        }

        if(currentDistanceToTarget > initialDistanceToTarget)
        {
            isMoving = false;
            rb.velocity = Vector2.zero;
        }
        if (isMoving)
        {
            initialDistanceToTarget = (touchPos - transform.position).magnitude;
        }

    }


}
