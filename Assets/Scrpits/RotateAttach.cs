using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class RotateAttach : MonoBehaviour
{
    [SerializeField] private float scaleSocket = 0.3f;
    [SerializeField] private string planetsytemname = "SonnensytemListe4.json";
    [SerializeField] private TextMeshProUGUI text;
    private Vector3 lastScale = new Vector3(0, 0, 0);
    private float lastScaleFloat = 0.0f;
    private string lastName = "";
    private GameObject[] celestials;

    // Start is called before the first frame update
    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("Celestial");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

    }
    
    private void OnTriggerEnter(Collider other)
    {
        lastName = other.name;
    }

    private void OnTriggerStay(Collider other)
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 45f);
    }

    public void SelectPlanet()
    {
        GameObject planet = GameObject.Find(lastName);
        if (planet)
        {
            lastScaleFloat = planet.transform.localScale.x;
            planet.transform.localScale = new Vector3(scaleSocket, scaleSocket, scaleSocket);
        }
    }

    public void ReturnPlanetSize()
    {
        GameObject planet = GameObject.Find(lastName);
        if (planet)
        {
            planet.transform.localScale = new Vector3(lastScaleFloat, lastScaleFloat, lastScaleFloat);
        }
    }

    public void showInfo()
    {
        string datapath = Application.dataPath + "/Resources/" + planetsytemname;
        Planets planetlist;
        if (File.Exists(datapath))
        {
            string fileContents = File.ReadAllText(datapath);
            planetlist = JsonUtility.FromJson<Planets>(fileContents);

            foreach (Planet p in planetlist.planetlist)
            {
                if (p.name.Equals(lastName))
                {
                    text.text = "Planetenname: " + p.name + "\n";
                    text.text += "Abstand(MKM): " + (p.distance / 10.0f) + "\n";
                    text.text += "Radius(KM): " + (p.size * 1000.0f) + "\n";
                    text.text += "Masse (in Erden): " + (p.mass * 1.0f) + "\n";
                }
            }
        }
        else
        {
            text.text = "Cannot Read PlanetList";
        }
    }
}
