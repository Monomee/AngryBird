using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;

public class SetBullseye : MonoBehaviour
{
    public BoxCollider2D spawnArea;
    Bounds bounds;
    float x;
    float y;

    GameObject bullseye;
    private void Awake()
    {
        bounds = spawnArea.bounds;
        
    }
    private void Start()
    {
        SetNewBullseye();
    }
    public void SetNewBullseye()
    {
        bullseye = ObjectPoolingTarget.SharedInstance.GetPooledObject();
        if (bullseye != null)
        {
            x = Random.Range(bounds.min.x, bounds.max.x);
            y = Random.Range(bounds.min.y, bounds.max.y);
            bullseye.transform.position = new Vector3(x, y);
            bullseye.SetActive(true);
        }
    }
}
