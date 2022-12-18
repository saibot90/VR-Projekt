using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public bool gamePause = false;
    [SerializeField] private GameObject menuScreen = null;
    [SerializeField] private InputActionReference toggleMenuReference = null;
    private GameObject[] celestials;
    [SerializeField] private Toggle togglePause;
    [SerializeField] private Toggle toggleBig;
    private Vector3[] planetVelocities;
    private Vector3[] planetSize;
    private float sizeBigPlanet = 0.5f;

    PlayerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
    }

    private void Awake()
    {
        toggleMenuReference.action.started += ToggleMenu;
    }

    private void OnDestroy()
    {
        toggleMenuReference.action.started -= ToggleMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleMenu(InputAction.CallbackContext context)
    {
        if (menuScreen)
        {
            bool isActive = !menuScreen.activeInHierarchy;
            menuScreen.SetActive(isActive);
        }
    }

    public void TogglePausePlanets()
    {
        if (togglePause.isOn)
        {
            //Time.timeScale = 1.0f;
            int i = 0;
            celestials = GameObject.FindGameObjectsWithTag("Celestial");
            //Debug.Log(celestials.Length);
            planetVelocities = new Vector3[celestials.Length];
            foreach (GameObject planet in celestials)
            {
                planetVelocities[i] = planet.GetComponent<Rigidbody>().velocity;
                planet.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                //Debug.Log(planetVelocities[i]);
                i++;
            }
        }
        else
        {
            //Time.timeScale = 0.0f;
            int i = 0;
            foreach (GameObject planet in celestials)
            {
                planet.GetComponent<Rigidbody>().velocity = planetVelocities[i];
                i++;
            }
        }
    }

    public void ToggleBigPlanets()
    {
        if (toggleBig.isOn)
        {
            int i = 0;
            celestials = GameObject.FindGameObjectsWithTag("Celestial");
            //Debug.Log(celestials.Length);
            planetSize = new Vector3[celestials.Length - 1];
            GameObject sun = null;
            foreach (GameObject planet in celestials)
            {
                if (planet.name != "Sun")
                {
                    planetSize[i] = planet.transform.localScale;
                    if (planetSize[i].x < sizeBigPlanet)
                    {
                        planet.transform.localScale = new Vector3(sizeBigPlanet, sizeBigPlanet, sizeBigPlanet);
                    }
                    i++;
                } else
                {
                    sun = planet;
                }
            }
        }
        else
        {
            int i = 0;
            foreach (GameObject planet in celestials)
            {
                if (planet.name != "Sun")
                {
                    planet.transform.localScale = planetSize[i];
                    i++;
                }
            }
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
