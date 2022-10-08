using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject earth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        calcNewPosition();
    }

    private void calcNewPosition()
    {
        Vector3 newPosition = new Vector3(earth.transform.position.x, earth.transform.position.y, earth.transform.position.z - 10);
        this.transform.position = newPosition;
    }
}
