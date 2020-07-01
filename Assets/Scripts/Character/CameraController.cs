using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Camera follow script for bouncy camera :)
 * */
public class CameraController : MonoBehaviour
{
    Vector2 viewPortSize;
    Camera cam;

    public float viewPortFactor;

    Vector3 targetPos;
    private Vector3 currentVel;
    public float followDuration;
    public float maxFollowSpeed;

    public Transform player;

    Vector2 distance;

    void Start()
    {
        cam = Camera.main;  
    }


    void FixedUpdate()
    {
        viewPortSize = (cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) - cam.ScreenToWorldPoint(Vector2.zero)) * viewPortFactor;
        
        // fixes overflow [player going out of camera range while walking]
        distance = player.position - transform.position;
        if(Mathf.Abs(distance.x) > viewPortSize.x / 2)
        {       
            if(cam.orthographicSize < 5)
            {
                targetPos.x = (player.position.x - (viewPortSize.x / 2 * Mathf.Sign(distance.x))) / 2;
            }
            else
            {
                targetPos.x = player.position.x - (viewPortSize.x / 2 * Mathf.Sign(distance.x));
            }
        }
        if (Mathf.Abs(distance.y) > viewPortSize.y / 2)
        {
            if (cam.orthographicSize < 5)
            {
                targetPos.y = (player.position.y - (viewPortSize.y / 2 * Mathf.Sign(distance.y))) / 2;
            }
            else
            {
                targetPos.y = player.position.y - (viewPortSize.y / 2 * Mathf.Sign(distance.y));
            }
        }

        targetPos = player.position - new Vector3(0, 0, 10);

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVel, followDuration, maxFollowSpeed);


    }
}
