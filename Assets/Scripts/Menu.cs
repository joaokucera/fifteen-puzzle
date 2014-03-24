using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{
    public Texture background;
    public GUISkin skin;
    private Rect backgroundArea;
    private Rect buttonArea;
    private Rect aboutArea;
    private Rect windowRect;

    protected bool hideWindow = false;
    protected bool showingAboutWindow = false;
    protected bool showingHighScoreWindow = false;

    void Start()
    {
        backgroundArea = new Rect(0.0f,
            0.0f,
            Screen.width,
            Screen.height);

        buttonArea = new Rect(Screen.width / 2.0f - (Screen.width * 0.5f) / 2.0f,
            Screen.height * 0.7f,
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

    void Update()
    {
        if (hideWindow)
        {
            HideAboutWindow();
            HideHighScoreWindow();
            hideWindow = false;
        }
    }

    void OnGUI()
    {
        if (skin)
            GUI.skin = skin;

        // Background
        if (background)
            GUI.DrawTexture(backgroundArea, background, ScaleMode.ScaleToFit);

        if (GUI.Button(new Rect(aboutArea), "i"))
            ShowAboutWindow();


        GUILayout.BeginArea(new Rect(buttonArea));

        // New Game
        if (GUILayout.Button("START"))
            Application.LoadLevel("Level");

        // High Score
        if (GUILayout.Button("HIGH SCORE"))
            ShowHighScoreWindow();

        // Quit Game
        if (GUILayout.Button("EXIT"))
            Application.Quit();

        GUILayout.EndArea();

        if (showingHighScoreWindow)
            windowRect = GUI.ModalWindow(1, windowRect, AboutWindow, "HIGH SCORE");

        if (showingAboutWindow)
            windowRect = GUI.ModalWindow(1, windowRect, HighScoreWindow, "ABOUT");
    }

    void AboutWindow(int windowID)
    {
        if (GUILayout.Button("BACK"))
            hideWindow = true;
    }

    void HighScoreWindow(int windowID)
    {
        if (GUILayout.Button("BACK"))
            hideWindow = true;
    }


    void ShowAboutWindow()
    {
        showingAboutWindow = true;
    }

    void ShowHighScoreWindow()
    {
        showingHighScoreWindow = true;
    }

    void HideAboutWindow()
    {
        showingAboutWindow = false;
    }

    void HideHighScoreWindow()
    {
        showingHighScoreWindow = false;
    }
}