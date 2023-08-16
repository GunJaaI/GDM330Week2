using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public int numIterations = 4;
    public float lengthLeft = 1f; //0.1
    public float lengthRight = 2f; //0.5
    public float thicknessLeft = 0.1f; //0.01
    public float thicknessRight = 0.13f; //0.05
    public Color color = Color.white;
    public float xLeft = -1f;
    public float xRight = 1f;
    public float yUp = -1f;
    public float yDown = -5f;
    public float rotationSpeedXLeft = 30f;
    public float rotationSpeedXRight = 45f;
    public float rotationSpeedZLeft = 15f;
    public float rotationSpeedZRight = 30f;
    private float currentRotation = 0f;

    private LineRenderer lineRenderer;
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<LineRenderer>().enabled = true;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(Random.Range(xLeft, xRight), yUp);

        lineRenderer = GetComponent<LineRenderer>();

        float length = Random.Range(lengthLeft, lengthRight); // Random Thickness value snowflake
        float thickness = Random.Range(thicknessLeft, thicknessRight); // Random Thickness value snowflake
        Color color = new Color(92f, 226f, 255f, Random.Range(0.25f, 0.75f)); // Random Opacity for Linerenderer
        
        Vector3[] points = GenerateSnowflakePoints(numIterations, length);

        lineRenderer.useWorldSpace = false;
        lineRenderer.loop = true;

        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
        lineRenderer.startWidth = thickness;
        lineRenderer.endWidth = thickness;
        lineRenderer.material.color = color;

    }

    void Update() {
        if (transform.position.y < yDown) {
            Destroy(gameObject);
        }

        float rotationSpeedX = Random.Range(rotationSpeedXLeft, rotationSpeedXRight); // Random RotationXAxis value snowflake
        float rotationSpeedZ = Random.Range(rotationSpeedZLeft, rotationSpeedZRight);
        currentRotation += rotationSpeedX * Time.deltaTime;
        currentRotation += rotationSpeedZ * Time.deltaTime;
        if (currentRotation >= 360f) {
            currentRotation -= 360f;
        }
        lineRenderer.transform.rotation = Quaternion.Euler(currentRotation, 0f, currentRotation);
    }

    private Vector3[] GenerateSnowflakePoints(int iterations, float length) {
        Vector3[] points = new Vector3[6];

        // Initialize points for first line segment
        points[0] = new Vector3(0f, 0f, 0f);
        points[1] = new Vector3(length/2, 0f, 0f);
        points[2] = new Vector3(-length/2, 0f, 0f);

        // Recursively generate the rest of the snowflake points
        for (int i = 0; i < iterations; i++) {
            int numPoints = points.Length;
            Vector3[] newPoints = new Vector3[numPoints * 4 - 3];
            int index = 0;

            for (int j = 0; j < numPoints - 1; j++) {
                // Calculate the two points that will form the new line segment
                Vector3 p1 = points[j];
                Vector3 p2 = points[j + 1];
                Vector3 delta = (p2 - p1) / 3f;

                // Calculate the three additional points that form the snowflake shape
                Vector3 a = p1 + delta;
                Vector3 c = p2 - delta;
                Vector3 b = a + Quaternion.Euler(0f, 0f, 60f) * delta;

                // Add the new points to the list
                newPoints[index++] = p1;
                newPoints[index++] = a;
                newPoints[index++] = b;
                newPoints[index++] = c;
            }
            newPoints[index] = points[numPoints - 1];

            // Update the points list for the next iteration
            points = newPoints;
        }

        return points;
    }
}
