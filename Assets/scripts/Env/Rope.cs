using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Rope : MonoBehaviour
{
    [Header("Rope Settings")]
    public int numberOfJoints = 15;
    public float distanceBetween = 0.5f;
    public float gravityScale = 2f;

    [Header("Collision")]
    public float colliderRadius = 0.1f;

    public List<Rigidbody2D> joints = new List<Rigidbody2D>();
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        CreateRope();
    }

    void CreateRope()
    {
        Rigidbody2D previousBody = null;

        for (int i = 0; i < numberOfJoints; i++)
        {
            GameObject jointObj = new GameObject("Joint_" + i);
            jointObj.transform.parent = transform;
            jointObj.transform.position = transform.position + Vector3.down * distanceBetween * i;

            Rigidbody2D rb = jointObj.AddComponent<Rigidbody2D>();
            rb.gravityScale = gravityScale;
            rb.mass = 0.2f;

            if (i == 0)
            {
                rb.isKinematic = true; // top anchor
            }
            else
            {
                DistanceJoint2D joint = jointObj.AddComponent<DistanceJoint2D>();
                joint.connectedBody = previousBody;
                joint.autoConfigureDistance = false;
                joint.distance = distanceBetween;

                CircleCollider2D col = jointObj.AddComponent<CircleCollider2D>();
                col.radius = colliderRadius;
            }

            joints.Add(rb);
            previousBody = rb;
        }

        lineRenderer.positionCount = numberOfJoints;
    }

    void Update()
    {
        UpdateLine();
    }

    void UpdateLine()
    {
        for (int i = 0; i < joints.Count; i++)
        {
            lineRenderer.SetPosition(i, joints[i].transform.position);
        }
    }

    // 🔥 IMPORTANT → Player will attach here
    public Rigidbody2D GetEndPoint()
    {
        return joints[joints.Count - 1];
    }
}