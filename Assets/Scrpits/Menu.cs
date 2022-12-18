using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Menu : MonoBehaviour
{

    public bool gamePause = false;
    [SerializeField] private GameObject menuScreen = null;
    public InputActionReference toggleMenuReference = null;

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
        if (Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0.0f;
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
