using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool debug;

    [Header("References")]
    public static GameController instance;
    private InputAction debugAction;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        debugAction = InputSystem.actions.FindAction("Debug");
    }

    private void Update()
    {
        Application.targetFrameRate = 60;

        if (debugAction.WasPressedThisFrame()) debug = !debug;

        //if (Input.GetKeyDown(KeyCode.Space)) InstantiateQTE().Set(0.20f, 0.4f, new Action[] { Test });
    }

    private void Test() => print("QTE Complete");

    public QTE InstantiateQTE()
    {
        return Instantiate((GameObject)Resources.Load("QTE"), CanvasController.instance.transform).GetComponent<QTE>();
    }

    private void OnGUI()
    {
        if (!debug) return;

        // Text
        GUI.Label(new Rect(10, 10, 100, 20), $"ms per frame: {System.Decimal.Round((decimal)(Time.deltaTime * 1000), 2)}");

        // Buttons
        if (GUI.Button(new Rect(10, 40, 100, 20), "Reload")) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (GUI.Button(new Rect(10, 70, 100, 20), "Exit")) Application.Quit(); ;
    }

    private void Reset()
    {
        transform.position = Vector3.zero;
        gameObject.tag = "GameController";
        gameObject.name = "--- GameController ---";
    }
}
