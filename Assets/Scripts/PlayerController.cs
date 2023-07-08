using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 range = new Vector2(-5f, 5f);

    private Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float xInput = Input.GetAxis("Horizontal");

        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 positionOffset = new Vector3(xInput * -moveSpeed, 0, 0) * Time.deltaTime;

        //limit position
        Vector3 newPosition = transform.position + positionOffset;
        newPosition.x = Mathf.Clamp(newPosition.x, range.x, range.y);

        //transform.position = newPosition;

        worldPoint.x = Mathf.Clamp(worldPoint.x, range.x, range.y);
        worldPoint.y = startingPosition.y;
        worldPoint.z = startingPosition.z;

        transform.position = worldPoint;


    }
}
