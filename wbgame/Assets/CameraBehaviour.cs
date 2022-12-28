using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform parent; // controls position of camera. parent will usually be a player controlled gameobject, but could be something else in event of dialogue/cut scene
    public Transform lookTarget; // point that the camera orbits around. might be a better way to accomplish this, but camera parent needs to be the child of this. mouse controls this object's rotation.
    int viewportWidth, viewportHeight;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        viewportWidth = gameObject.GetComponent<Camera>().pixelWidth;
        viewportHeight = gameObject.GetComponent<Camera>().pixelHeight;

        if (parent.gameObject.tag == "Player")
        {
            lookTarget.rotation = Quaternion.Euler((Input.mousePosition.y/viewportHeight) * -180, Input.mousePosition.x/viewportWidth * 180, 0);
        }   
    }
}
