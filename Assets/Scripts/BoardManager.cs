using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private GUISkin skin;
    private Transform hole;
    private bool isPause = false;
    private float btnWidth;
    private float btnHeight = 60.0f;
    private float padding = 20.0f;
    private float space = 3.0f;
    private float startTime = 0.0f;
    private float totalTime = 0.0f;
    private float totalMovement = 0.0f;
    private bool isShuffling = false;
    private bool isGameOver = false;
    private Rect windowRect;
    #endregion

    #region Start
    void Start()
    {
        btnWidth = (Screen.width - padding) / 3.0f - space;
        hole = GameObject.Find("Hole").transform;
        hole.SendMessage("Shuffle");

        windowRect = new Rect(Screen.width / 2.0f - (Screen.width * 0.9f) / 2.0f,
            Screen.height * 0.1f,
            Screen.width * 0.9f,
            Screen.height * 0.8f);

    }
    #endregion

    #region Update
    void Update()
    {
        if (!isGameOver)
        {
            if (isShuffling)
                startTime = Time.time;

            totalTime = Time.time - startTime;
        }
    }
    #endregion

    #region Add and Remove Delegates
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
    #endregion

    #region OnGUI
    void OnGUI()
    {
        if (skin)
            GUI.skin = skin;

        if (isGameOver)
        {
            GUI.depth = 1;
            //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), colorTexture, ScaleMode.StretchToFill, true);
            windowRect = GUI.ModalWindow(1, windowRect, GameOverWindow, "CONGRATULATIONS");
        }
    }
    #endregion
	
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
            //FB.Init();
            //FB.Login();
           // FB.Feed();
        }
        GUILayout.EndHorizontal();
    }

/*
        GUILayout.Label(string.Format("Moves\n{0}", totalMovement.ToString("000")),
            GUILayout.Width(Screen.width / 2.0f));
        GUILayout.Label(string.Format("Time\n{0}:{1}", (totalTime / 60).ToString("00"), (totalTime % 60).ToString("00")),
            GUILayout.Width(Screen.width / 2.0f));

        if (GUILayout.Button("Shuffle", GUILayout.Width(btnWidth), GUILayout.Height(btnHeight)))
        {
            if (!isPause)
                hole.SendMessage("Shuffle");
        }
*/

}

