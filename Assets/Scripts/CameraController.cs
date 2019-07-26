using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private new Camera camera;

    private float maxSize; //max size is set automatically by gridcontroller
    public float minSize;
    public float movementSpeed;

	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

        //camera size adjustment
        float cameraSize = camera.orthographicSize;
        cameraSize -= Input.mouseScrollDelta.y;

        cameraSize = Mathf.Min(maxSize, cameraSize);
        cameraSize = Mathf.Max(minSize, cameraSize);

        camera.orthographicSize = cameraSize;

        //camera movement
        Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        movementVector = movementVector * movementSpeed * Time.deltaTime * (cameraSize / 2f);

        if (camera.transform.position.y + movementVector.y < cameraSize - 0.5f)
        {
            movementVector.y = 0;
        }
        if (camera.transform.position.y + movementVector.y > (maxSize * 2 - 0.5f) - cameraSize)
        {
            movementVector.y = 0;
        }

        camera.transform.position = camera.transform.position + movementVector;
    }

    //adjusts boundaries for the camera's size, set by the gridcontroller
    public void setMaxSize(float size) {
        maxSize = size;
    }
}
