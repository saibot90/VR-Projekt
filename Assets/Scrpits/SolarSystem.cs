using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

[System.Serializable]
public class Planet
{
    public string name;
    public float distance;
    public float size;
    public float mass;
    public string ring;
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
    [SerializeField]
    private float G = 1f;
    //readonly float G = 100000f;
    private GameObject[] celestials;
    //Planet[] planet =  new Planet[1];
    [SerializeField]
    private string planetsytemname = "SonnensytemListe4.json";
    [SerializeField]
    private TextMeshProUGUI text;
    public static float scaleSS = 0.01f;




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
        } else
        {
            text.text = "Cannot Read PlanetList";
        }

        celestials = GameObject.FindGameObjectsWithTag("Celestial");
        //this.transform.localScale = new Vector3(scaleSS, scaleSS, scaleSS);
        foreach (GameObject planet in celestials)
        {
            planet.transform.localScale = new Vector3(planet.transform.localScale.x * scaleSS, planet.transform.localScale.y * scaleSS, planet.transform.localScale.z * scaleSS);
            planet.transform.position = new Vector3(planet.transform.position.x * scaleSS, planet.transform.position.y * scaleSS, planet.transform.position.z * scaleSS);
        }
        this.transform.position = new Vector3(0, 0, 30);
        InitialVelocity();
    }

    void CreatePlanet(Planet planet)
    {
        string materialpath = "Planet_Materials/" + planet.name + "_Material";
        Material newMat = Resources.Load(materialpath, typeof(Material)) as Material;
        string trailMaterial = "TrailRenderer";
        Material newTrailMat = Resources.Load(trailMaterial, typeof(Material)) as Material;

        GameObject currentplanet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        currentplanet.gameObject.GetComponent<Renderer>().material = newMat;

        currentplanet.transform.position = new Vector3(planet.distance, 0, 0);
        currentplanet.transform.localScale = new Vector3(planet.size, planet.size, planet.size);
        currentplanet.transform.parent = this.gameObject.transform;

        currentplanet.name = planet.name;
        currentplanet.tag = "Celestial";

        currentplanet.AddComponent<Rigidbody>();
        currentplanet.GetComponent<Rigidbody>().useGravity = false;
        currentplanet.GetComponent<Rigidbody>().mass = planet.mass;
        currentplanet.AddComponent<TrailRenderer>();
        currentplanet.GetComponent<TrailRenderer>().time = 1;
        currentplanet.GetComponent<TrailRenderer>().material = newTrailMat;
        currentplanet.GetComponent<TrailRenderer>().startWidth = scaleSS;
        //currentplanet.GetComponent<TrailRenderer>().endWidth = scaleSS;

        if (planet.ring == "y")
        {
            materialpath = "Planet_Materials/" + planet.name + "_Ring_Material";
            Material newMatRing = Resources.Load(materialpath, typeof(Material)) as Material;
            GameObject planetRing = Resources.Load("PlanetRingPrefab") as GameObject;

            planetRing.gameObject.GetComponent<Renderer>().material = newMatRing;
            planetRing.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            planetRing.name = planet.name + "_Ring";
            planetRing.GetComponent<Rigidbody>().mass = planet.mass;

            Instantiate(planetRing, new Vector3(planet.distance, 0f, 0f), Quaternion.Euler(90, 0, 0), currentplanet.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Gravity();
        //PlanetRotation();
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
                    if (a.transform.childCount != 0)
                    {
                        Transform ring = a.transform.GetChild(0);
                        ring.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized * (G * m1 * m2 / (r * r)));
                        //ring.transform.position = a.transform.position;
                    }
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
                    if (a.transform.childCount != 0)
                    {
                        Transform ring = a.transform.GetChild(0);
                        ring.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) / r);
                    }

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
