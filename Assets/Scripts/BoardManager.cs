﻿using UnityEngine;
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

    private float startTime = 0.0f;
    private float totalTime = 0.0f;
    private float totalMovement = 0.0f;

    public GUISkin skin;
    private bool isShuffling = false;
    private bool isGameOver = false;

    private Rect windowRect;
    private Texture colorTexture;

    private Texture GetTextureFromColor(Color color)
    {
        Texture2D texture = new Texture2D(1, 1) as Texture2D;
        texture.SetPixel(0, 0, color);
        texture.Apply();
        return (Texture)texture;
    }


    void Start()
    {
        btnWidth = (Screen.width - padding) / 3.0f - space;
        hole = GameObject.Find("Hole").transform;
        hole.SendMessage("Shuffle");

        windowRect = new Rect(Screen.width / 2.0f - (Screen.width * 0.9f) / 2.0f,
            Screen.height * 0.1f,
            Screen.width * 0.9f,
            Screen.height * 0.8f);

        colorTexture = GetTextureFromColor(new Color(0.0f, 0.0f, 0.0f, 0.70f));
    }

    void OnEnable()
    {
        Hole.OnMoveBlock += OnMoveBlock;
        Hole.OnStartShuffle += OnStartShuffle;
        Hole.OnEndShuffle += OnEndShuffle;
        GameOver.OnGameOver += OnGameOver;
        
    }

    void OnDisable()
    {
        Hole.OnMoveBlock -= OnMoveBlock;
        Hole.OnStartShuffle -= OnStartShuffle;
        Hole.OnEndShuffle -= OnEndShuffle;
        GameOver.OnGameOver -= OnGameOver;
    }

    void OnGameOver()
    {
        isGameOver = true;
    }

    void OnMoveBlock()
    {
        this.totalMovement++;
    }

    void OnStartShuffle()
    {
        this.totalMovement = 0;
        this.isShuffling = true;
    }

    void OnEndShuffle()
    {
        this.isShuffling = false;
    }

    void Update()
    {
        if (!isGameOver)
        {
            if (isShuffling)
                startTime = Time.time;

            totalTime = Time.time - startTime;
        }
    }

    void OnGUI()
    {
        if (skin)
            GUI.skin = skin;
        DrawHeader();
        DrawFooter();

        if (isGameOver)
        {
            GUI.depth = 1;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), colorTexture, ScaleMode.StretchToFill, true);
            windowRect = GUI.ModalWindow(1, windowRect, GameOverWindow, "CONGRATULATIONS");
        }
    }

    void GameOverWindow(int windowId)
    {
        GUILayout.Label("<size=16>You have sucessfully solved the puzzle</size>");
        GUILayout.Space(20.0f);
        GUILayout.Label("<size=16>Number of moves</size>");
        GUILayout.Space(-5.0f);
        GUILayout.Label(string.Format("<size=24>{0}</size>", totalMovement));
        GUILayout.Space(10.0f);
        GUILayout.Label("<size=16>Time take to solve</size>");
        GUILayout.Space(-5.0f);
        GUILayout.Label(string.Format("<size=24>{0}:{1}</size>", (totalTime / 60).ToString("00"), (totalTime % 60).ToString("00")));

        GUILayout.Space(40.0f);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<size=20>OK</size>"))
        {
            Application.LoadLevel("Menu");
        }

        if (GUILayout.Button("<size=20>SHARE</size>"))
        {

        }
        GUILayout.EndHorizontal();
    }

    #region Draw Header
    void DrawHeader()
    {
        GUILayout.BeginArea(new Rect(0.0f, 10.0f, Screen.width, 120.0f));
        GUILayout.BeginHorizontal();
        GUILayout.Label(string.Format("Moves\n{0}", totalMovement.ToString("000")),
            GUILayout.Width(Screen.width / 2.0f));
        GUILayout.Label(string.Format("Time\n{0}:{1}", (totalTime / 60).ToString("00"), (totalTime % 60).ToString("00")),
            GUILayout.Width(Screen.width / 2.0f));
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
    #endregion

    #region Draw Buttons
    void DrawFooter()
    {
        GUILayout.BeginArea(new Rect(0.0f, Screen.height - 80.0f, Screen.width, 80.0f));
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
            Application.LoadLevel("Menu");
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
    #endregion

}

