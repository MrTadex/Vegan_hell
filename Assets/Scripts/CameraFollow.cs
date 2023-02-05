using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    public GameObject target;

    public float smoothSpeed = 0.125f;
    public Vector3 locationOffset;
    public Vector3 rotationOffset;

    float mapX = 200.3f;
    float mapY = 200.3f;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private void Start()
    {
        var vertExtent = Camera.main.orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;

        // Calculations assume map is position at the origin
        minX = horzExtent - mapX / 2;
        maxX = mapX / 2 - horzExtent;
        minY = vertExtent - mapY / 2;
        maxY = mapY / 2 - vertExtent;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.transform.position + target.transform.rotation * locationOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private void LateUpdate()
    {
        var v3 = transform.position;
        v3.x = Mathf.Clamp(v3.x, minX, maxX);
        v3.y = Mathf.Clamp(v3.y, minY, maxY);
        transform.position = v3;
    }
}
