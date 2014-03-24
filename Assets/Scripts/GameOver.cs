using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameOver : MonoBehaviour
{
    #region Events
    public delegate void GameOverAction();
    public static event GameOverAction OnGameOver;
    #endregion

    private List<BlockCollider> colliders;
    private bool isShuffling = true;
    private bool isGameOver = false;

	void Start () 
    {
        colliders = new List<BlockCollider>();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("BlockCollider");
        foreach (GameObject go in gos)
            colliders.Add(go.GetComponent<BlockCollider>());
	}

    void OnEnable()
    {
        Hole.OnStartShuffle += OnStartShuffle;
        Hole.OnEndShuffle += OnEndShuffle;
    }

    void OnDisable()
    {
        Hole.OnStartShuffle -= OnStartShuffle;
        Hole.OnEndShuffle -= OnEndShuffle;
    }

    void Update()
    {
        if (!isShuffling && !isGameOver)
        {
            bool over = true;
            foreach (BlockCollider col in colliders)
            {
                if (!col.BlockIsRightPlace)
                {
                    over = false;
                    break;
                }
            }

            if (over && OnGameOver != null)
            {
                isGameOver = true;
                Debug.Log("Game over");
                OnGameOver();
            }
        }
    }

    void OnStartShuffle()
    {
        isShuffling = true;
    }

    void OnEndShuffle()
    {
        isShuffling = false;
    }
}
