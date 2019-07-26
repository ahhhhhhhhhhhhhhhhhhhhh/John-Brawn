using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Camera camera;
    private float minsize;
    private float maxsize;

    //public float maxsize;
    //public float minsize;

	// Use this for initialization
	void Start () {

        camera = GetComponent<Camera>();

	}
	
	// Update is called once per frame
	void Update () {

        //camera size adjustment
        float cameraSize = camera.orthographicSize;
        cameraSize -= Input.mouseScrollDelta.y;

        if (cameraSize > maxsize) {
            cameraSize = maxsize;
        }
        if (cameraSize < minsize) {
            cameraSize = minsize;
        }

        camera.orthographicSize = cameraSize;

        //camera location adjustment
        Vector3 cameraPosition = camera.transform.position;

        cameraPosition.x += 5f * Time.deltaTime * Input.GetAxis("Horizontal");
        cameraPosition.y += 5f * Time.deltaTime * Input.GetAxis("Vertical");

        camera.transform.position = cameraPosition;

    }

    //adjusts boundaries for the camera's size, set by the gridcontroller
    public void setMaxSize(float size) {
        maxsize = size;
        minsize = size / 5;
    }

}
