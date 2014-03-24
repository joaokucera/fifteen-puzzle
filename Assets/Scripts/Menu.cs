using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{
    public Texture background;
    private Rect backgroundArea;
    private Rect buttonArea;

    void Start()
    {
        backgroundArea = new Rect(0.0f, 0.0f, Screen.width, Screen.height);
        buttonArea = new Rect(Screen.width / 2.0f - (Screen.width * 0.6f) / 2.0f, Screen.height * 0.7f, Screen.width * 0.6f, Screen.height * 0.4f);
    }

    void OnGUI()
    {
        GUI.depth = (int)transform.position.z;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;

        // Background
        if (background)
            GUI.DrawTexture(backgroundArea, background, ScaleMode.ScaleToFit);

        if (GUI.Button(new Rect(), "About"))
            Application.Quit();


        GUILayout.BeginArea(new Rect(buttonArea));

        // New Game
        if (GUILayout.Button("Start"))
            Application.LoadLevel("Level");

        // High Score
        if (GUILayout.Button("High Score"))
            Application.Quit();

        // Quit Game
        if (GUILayout.Button("Exit"))
            Application.Quit();


        GUILayout.EndArea();

    }

}