using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StarsInteraction : MonoBehaviour
{
    //[SerializeField] Transform raycastOrigin;
    [SerializeField] LayerMask targetLayer;
    LineController line;

    public void Trigger()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, targetLayer))
        {
            if(hit.transform.tag == "Star")
            {
                line.SetLineOnStar(hit.point);
            }
            if(hit.transform.tag == "StarBackground")
            {
                line.SetUpLine(hit.point);
            }
        }
    }

    public void TriggerRelease()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, targetLayer))
        {
            if (hit.transform.tag == "Star")
            {
                line.SetLineOnStar(hit.point);
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        line = FindObjectOfType<LineController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FireRayCastAtStars()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, targetLayer))
        {
            line.SetUpLine(hit.point);
            Debug.Log(hit.rigidbody.tag);
        }

    }
}
