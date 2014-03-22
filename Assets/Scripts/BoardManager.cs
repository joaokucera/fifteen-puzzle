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

    private float startTime = 0.0f;
    private float totalTime = 0.0f;
    private float totalMovement = 0.0f;

    public GUISkin skin;
    private bool isShuffling = false;


    void Start()
    {
        btnWidth = (Screen.width - padding) / 3.0f - space;
        hole = GameObject.Find("Hole").transform;
        hole.SendMessage("Shuffle");
    }

    void OnEnable()
    {
        Hole.OnMoveBlock += OnMoveBlock;
        Hole.OnStartShuffle += OnStartShuffle;
        Hole.OnEndShuffle += OnEndShuffle;
        
    }

    void OnDisable()
    {
        Hole.OnMoveBlock -= OnMoveBlock;
        Hole.OnStartShuffle -= OnStartShuffle;
        Hole.OnEndShuffle -= OnEndShuffle;
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
        if (isShuffling)
            startTime = Time.time;

         totalTime = Time.time - startTime;
    }

    void OnGUI()
    {
        if (skin)
            GUI.skin = skin;
        DrawHeader();
        DrawFooter();
    }

    #region Draw Header
    void DrawHeader()
    {
        GUILayout.BeginArea(new Rect(padding / 2.0f, padding, Screen.width - padding, 100.0f));
        GUILayout.BeginHorizontal();

        GUILayout.Label(string.Format("Moves: {0}", totalMovement.ToString("000")), 
            GUILayout.Width((Screen.width - padding) / 2.0f));
        GUILayout.Label(string.Format("Time: {0}:{1}", (totalTime / 60).ToString("00"), (totalTime % 60).ToString("00")), 
            GUILayout.Width((Screen.width - padding) / 2.0f));

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
    #endregion

    #region Draw Buttons
    void DrawFooter()
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
    #endregion

}

