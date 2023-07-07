using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 range = new Vector2(-5f, 5f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float xInput = Input.GetAxis("Horizontal");

        Vector3 positionOffset = new Vector3(xInput * -moveSpeed, 0, 0) * Time.deltaTime;

        //limit position
        Vector3 newPosition = transform.position + positionOffset;
        newPosition.x = Mathf.Clamp(newPosition.x, range.x, range.y);

        transform.position = newPosition;

    }
}
