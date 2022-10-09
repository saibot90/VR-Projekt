using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.Json;
using System.IO;


[System.Serializable]
public class Planet
{
    public string name;
    public float distance;
    public float size;
    public string material;
    //public Planet(string name, int distance, float scale1, float scale2, float scale3, string material)
    //{
    //    name = name;
    //    distance = distance;
    //    size = new Vector3 (scale1, scale2, scale3);
    //    Material = material;
    //}
}

public class Planets
{
    public Planet[] planetlist;
}

public class SolarSystem : MonoBehaviour
{
    public float G = 1f;
    //readonly float G = 100000f;
    GameObject[] celestials;
    //Planet[] planet =  new Planet[1];
    public string planetsytemname = "SonnensytemListe3.json";




    // Start is called before the first frame update
    void Start()
    {

        string datapath = Application.dataPath + "/Resources/" + planetsytemname;
        Planets planetlist;
        if (File.Exists(datapath))
        {
            string fileContents = File.ReadAllText(datapath);
            planetlist = JsonUtility.FromJson<Planets>(fileContents);

            foreach (Planet p in planetlist.planetlist)
            {
                CreatePlanet(p);
            }
        }

        celestials = GameObject.FindGameObjectsWithTag("Celestial");
        InitialVelocity();
    }

    void CreatePlanet(Planet planet)
    {
        Material newMat = Resources.Load(planet.material, typeof(Material)) as Material;

        GameObject jupiter = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        jupiter.gameObject.GetComponent<Renderer>().material = newMat;
        jupiter.transform.position = new Vector3(planet.distance, 0, 0);
        jupiter.transform.localScale = new Vector3(planet.size, planet.size, planet.size);
        jupiter.name = planet.name;
        jupiter.tag = "Celestial";
        jupiter.AddComponent<Rigidbody>();
        jupiter.GetComponent<Rigidbody>().useGravity = false;
        jupiter.AddComponent<TrailRenderer>();
        jupiter.GetComponent<TrailRenderer>().time = 100;
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
                if (!a.Equals(b) && a.name != "Sun")
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

                    a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) / r);

                    //a.GetComponent<Rigidbody>().velocity += a.transform.right * 240;

                    //a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt(G * m2 * ((2.0f / r) + (1.0f / 149.598023f)));
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
