using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryDrawer : MonoBehaviour
{
    public int resolution = 30; 
    public float duration = 2f; 

    private LineRenderer lineRenderer;
    private const float G = 9.8f;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawTrajectory(float throwSpeed, float angle, Vector3 startPos)
    {
        Vector3[] points = new Vector3[resolution];
        for (int i = 0; i < resolution; i++)
        {
            float t = (i / (float)(resolution - 1)) * duration;
            float x = throwSpeed * Mathf.Cos(angle) * t;
            float y = throwSpeed * Mathf.Sin(angle) * t - 0.5f * G * t * t;
            points[i] = startPos + new Vector3(x, y, 0);
        }

        lineRenderer.positionCount = resolution;
        lineRenderer.SetPositions(points);
    }
    public void ClearTrajectory()
    {
        lineRenderer.positionCount = 0;
    }

}
