using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    readonly float G = 100f;
    GameObject[] celestials;


    // Start is called before the first frame update
    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("Celestial");

        InitialVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Gravity();
        PlanetRotation();
    }

    void Gravity()
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if(!a.Equals(b) && a.name != "Sun")
                {
                    float m1 = a.GetComponent<Rigidbody>().mass;
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized * (G * m1 * m2 / (r * r)));
                }
            }
        }
    }

    void InitialVelocity()
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if (!a.Equals(b) && a.name != "Sun")
                {
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);
                    a.transform.LookAt(b.transform);

                    //a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) / r);

                    //a.GetComponent<Rigidbody>().velocity += a.transform.right * 240;

                    a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt(G * m2 * ((2.0f / r) + (1.0f / 149.598023f)));
                }
            }
        }
    }

    void PlanetRotation()
    {
        foreach (GameObject a in celestials)
        {
            if (a.name != "Sun")
            {
                a.transform.Rotate(0, 1.0f, 0);
            }
        }
    }
}
