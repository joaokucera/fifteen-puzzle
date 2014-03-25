using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private Texture background;

    [SerializeField]
    private GUISkin skin;

    [SerializeField]
    private AudioClip audioButtonClick;

    private Rect backgroundArea;
    private Rect buttonArea;
    private Rect aboutArea;
    private Rect windowRect;
    private bool hideWindow = false;
    private bool showingAboutWindow = false;
    private bool showingHighScoreWindow = false;
    #endregion

    #region Start
    void Start()
    {
        backgroundArea = new Rect(0.0f,
            0.0f,
            Screen.width,
            Screen.height);

        buttonArea = new Rect(Screen.width / 2.0f - (Screen.width * 0.5f) / 2.0f,
            Screen.height * 0.55f,
            Screen.width * 0.5f,
            Screen.height * 0.4f);

        aboutArea = new Rect(Screen.width - 50.0f,
            Screen.height - 50.0f,
            40.0f,
            40.0f);

        windowRect = new Rect(Screen.width / 2.0f - (Screen.width * 0.8f) / 2.0f,
            Screen.height * 0.2f,
            Screen.width * 0.8f,
            Screen.height * 0.7f);
    }
    #endregion

    #region Update
    void Update()
    {
        if (hideWindow)
        {
            showingAboutWindow = false;
            showingHighScoreWindow = false;
            hideWindow = false;
        }
    }
    #endregion

    #region OnGUI
    void OnGUI()
    {
        if (skin)
            GUI.skin = skin;

        // Background
        if (background)
            GUI.DrawTexture(backgroundArea, background, ScaleMode.ScaleToFit);

        if (GUI.Button(new Rect(aboutArea), "i"))
        {
            showingAboutWindow = true;
            if (audioButtonClick)
                AudioSource.PlayClipAtPoint(audioButtonClick, Vector3.zero);
        }


        GUILayout.BeginArea(new Rect(buttonArea));

        // New Game
        if (GUILayout.Button("START"))
        {
            if (audioButtonClick)
                AudioSource.PlayClipAtPoint(audioButtonClick, Vector3.zero);
            Application.LoadLevel("Level");
        }

        // High Score
        if (GUILayout.Button("HIGH SCORE"))
        {
            showingHighScoreWindow = true;
            if (audioButtonClick)
                AudioSource.PlayClipAtPoint(audioButtonClick, Vector3.zero);
        }

        // Quit Game
        if (GUILayout.Button("EXIT"))
        {
            if (audioButtonClick)
                AudioSource.PlayClipAtPoint(audioButtonClick, Vector3.zero);
            Application.Quit();
        }

        GUILayout.EndArea();

        if (showingHighScoreWindow)
            windowRect = GUI.ModalWindow(1, windowRect, AboutWindow, "HIGH SCORE");

        if (showingAboutWindow)
            windowRect = GUI.ModalWindow(1, windowRect, HighScoreWindow, "ABOUT");
    }
    #endregion

    #region About Window
    void AboutWindow(int windowID)
    {
        if (GUILayout.Button("BACK"))
        {
            hideWindow = true;
            if (audioButtonClick)
                AudioSource.PlayClipAtPoint(audioButtonClick, Vector3.zero);
        }
    }
    #endregion

    #region High Score Window
    void HighScoreWindow(int windowID)
    {
        if (GUILayout.Button("BACK"))
        {
            hideWindow = true;
            if (audioButtonClick)
                AudioSource.PlayClipAtPoint(audioButtonClick, Vector3.zero);
        }
    }
    #endregion
}