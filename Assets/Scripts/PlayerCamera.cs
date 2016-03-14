using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{

    public float dampTime = 0.15f;
    public float rotateSpeed = 20.0f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public Vector3 newDirection;

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
            Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
        if (Input.GetButton("Rotateleft"))
        {

            newDirection = Camera.main.transform.TransformDirection(Vector3.forward);

            transform.RotateAround(target.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);


        }
        if (Input.GetButton("Rotateright"))
        {

            newDirection = Camera.main.transform.TransformDirection(Vector3.forward);
          
            transform.RotateAround(target.transform.position, Vector3.down, rotateSpeed * Time.deltaTime);

        }

    }
}
