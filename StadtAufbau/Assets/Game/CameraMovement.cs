using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    public float maxY;
    public float minY;

    // Update is called once per frame
    void Update()
    {
        float yMovement = -Input.mouseScrollDelta.y * speed * Time.deltaTime;
        Vector3 position = transform.position;
        position.y = Mathf.Clamp(position.y + yMovement, minY, maxY);
        transform.position = position;
        float percentageY = (position.y - minY) / (maxY - minY);
        percentageY = Mathf.Clamp(percentageY, 0.1f, 1);
        float xMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime * percentageY;
        float zMovement = -Input.GetAxis("Horizontal") * speed * Time.deltaTime * percentageY;

        Vector3 movement = new Vector3(xMovement, 0, zMovement);

        transform.position += movement;
    }
}
