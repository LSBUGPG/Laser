using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    public float maxLength = 100.0f;
    public int maxBounces = 4;
    LineRenderer laser;
    Vector3 [] path;

    void Start()
    {
        path = new Vector3[maxBounces + 2];
        laser = GetComponent<LineRenderer>();
    }

    void Update()
    {
        float length = maxLength;
        int points = 2;
        Vector3 direction = transform.up;
        Vector3 source = transform.position + direction * 1.2f;
        path[0] = source;

        RaycastHit hit;
        while (points < path.Length && Physics.Raycast(source, direction, out hit, length))
        {
            source = hit.point;
            direction = Vector3.Reflect(direction, hit.normal);
            path[points - 1] = source;
            points++;
            length -= hit.distance;
        }
        path[points - 1] = source + direction * length;

        laser.positionCount = points;
        for (int i = 0; i < points; ++i)
        {
            laser.SetPosition(i, path[i]);
        }
    }
}
