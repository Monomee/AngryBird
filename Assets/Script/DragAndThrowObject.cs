using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragAndThrowObject : MonoBehaviour
{
    private TrajectoryDrawer drawer;

    private Transform startPositionSlingShot;
    private Vector3 startPosition;
    private bool isDragging = false;
    private Camera cam;
    private bool hasLaunched = false;
    //public float maxDistance;

    float x0;
    float y0;

    private float time;
    private float x;
    private float y;
    private float angle; //goc nem

    public float forceMultiplier = 10f;
    private float throwSpeed; 

    const float G = 9.8f;

    BoxCollider2D safeZone;
    Bounds bounds;
    float right;
    float bottom;
    bool isInSafeZone = true;
    void Awake()
    {
        cam = Camera.main;
        startPositionSlingShot = GameObject.FindGameObjectWithTag("Slingshot").transform;
        drawer = GameObject.FindGameObjectWithTag("Slingshot").GetComponent<TrajectoryDrawer>();
        safeZone = GameObject.FindGameObjectWithTag("SafeZone").GetComponent<BoxCollider2D>();
        bounds = safeZone.bounds;
        right = bounds.max.x;
        bottom = bounds.min.y;
    }

    void OnMouseDown()
    {
        isDragging = true;
        startPosition = startPositionSlingShot.position;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            transform.position = mouseWorldPos;

            x0 = transform.position.x;
            y0 = transform.position.y;
            angle = Vector3.Angle(transform.position - startPosition, Vector2.left) * Mathf.Deg2Rad;
            throwSpeed = Vector3.Distance(startPosition, transform.position) * forceMultiplier;

            drawer.DrawTrajectory(throwSpeed, angle, new Vector3(x0, y0, 0)); 
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        time = 0;
        hasLaunched = true;
    }
    void ResetBall()
    {
        time = 0;
        x0 = 0;
        y0 = 0;
        angle = 0;
        throwSpeed = 0;
        hasLaunched = false;
        isInSafeZone = true;
        isDragging = false; 
        transform.position = new Vector3(startPositionSlingShot.position.x, startPositionSlingShot.position.y);
        drawer.ClearTrajectory();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInSafeZone && hasLaunched)
        {
            time += Time.deltaTime;

            x = throwSpeed * Mathf.Cos(angle) * time + x0;
            y = throwSpeed * Mathf.Sin(angle) * time - 0.5f * G * time * time + y0;

            this.transform.position = new Vector3(x, y, transform.position.z);
        }
        if(transform.position.x > right || transform.position.y < bottom)
        {
            isInSafeZone = false;
            ResetBall();
        }
    }
}
