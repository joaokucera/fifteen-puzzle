using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour 
{
    private Transform hole;
    private bool isPause = false;
    private float btnWidth;
    private float btnHeight = 60.0f;
    private float padding = 20.0f;
    private float space = 3.0f;

    void Start()
    {
        hole = GameObject.Find("Hole").transform;
        btnWidth = (Screen.width - padding) / 3.0f - space;
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(padding / 2.0f, Screen.height - 100.0f, Screen.width - padding, 100.0f));
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Shuffle", GUILayout.Width(btnWidth), GUILayout.Height(btnHeight)))
        {
            hole.SendMessage("Shuffle");
        }

        if (GUILayout.Button((isPause ? "Resume" : "Pause"), GUILayout.Width(btnWidth), GUILayout.Height(btnHeight)))
        {
            isPause = !isPause;
            if (isPause)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }

        if (GUILayout.Button("Quit", GUILayout.Width(btnWidth), GUILayout.Height(btnHeight)))
        {
            Application.Quit();
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

}
