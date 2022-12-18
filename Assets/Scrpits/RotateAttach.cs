using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class RotateAttach : MonoBehaviour
{
    [SerializeField]
    private float scaleSocket = 0.5f;
    private Vector3 lastScale = new Vector3(0, 0, 0);
    private float time = 0.0f;
    private float lastScaleFloat = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //transform.RotateAround(transform.position, transform.up, Time.deltaTime * 45f);
        time += Time.deltaTime;
        if (time >= 1.0f)
        {
            Debug.Log("loop last Scale" + lastScale);
            Debug.Log("loop last Scale Float" + lastScaleFloat);
            time -= 1.0f;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        float scaleFactor = scaleSocket * 0.01f;
        lastScaleFloat = other.transform.localScale.x;
        lastScale = new Vector3(other.transform.localScale.x, other.transform.localScale.y, other.transform.localScale.z);
        Debug.Log(other.name);
        //Debug.Log(other.transform.localScale);
        //other.transform.localScale = new Vector3(other.transform.localScale.x / scaleFactor, other.transform.localScale.y / scaleFactor, other.transform.localScale.z / scaleFactor);
        other.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        Debug.Log("loop last Scale Float" + lastScaleFloat);
        Debug.Log("Last Scale" + lastScale);
        Debug.Log("local Scale" + other.transform.localScale);
    }

    private void OnTriggerStay(Collider other)
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 45f);
    }
}
