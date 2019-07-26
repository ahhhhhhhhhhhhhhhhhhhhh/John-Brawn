using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private new Camera camera;

    private float maxSize; //max size is set automatically by gridcontroller
    public float minSize;
    public float movementSpeed;
    public float scrollSpeed;

    [Header("Unity Setup")]
    public GridController grid;

	// Use this for initialization
	void Start ()
    {
        camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //camera size adjustment
        float cameraSize = camera.orthographicSize;
        cameraSize -= Input.mouseScrollDelta.y * scrollSpeed;

        cameraSize = Mathf.Min(maxSize, cameraSize);
        cameraSize = Mathf.Max(minSize, cameraSize);

        camera.orthographicSize = cameraSize;

        //camera movement
        Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        movementVector = movementVector * movementSpeed * Time.deltaTime * (cameraSize / 5f);
        Vector3 futurePosition = camera.transform.position + movementVector;

        if (futurePosition.y < cameraSize - 0.5f || futurePosition.y > (grid.getHeight() - 0.5f) - cameraSize)
        {
            futurePosition.y = camera.transform.position.y;
        }
        float cameraWidth = cameraSize * camera.aspect;
        if (futurePosition.x < cameraWidth - 0.5f || futurePosition.x > (grid.getWidth() - 0.5f) - cameraWidth)
        {
            futurePosition.x = camera.transform.position.x;
        }

        camera.transform.position = futurePosition;
    }

    //adjusts boundaries for the camera's size, set by the gridcontroller
    public void setMaxSize(float size) {
        maxSize = size;
    }
}
