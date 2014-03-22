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
    private bool isShuffling = false;

	void Start () 
    {
        colliders = new List<BlockCollider>();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("BlockCollider");
        foreach (GameObject go in gos)
            colliders.Add(go.GetComponent<BlockCollider>());
	}
	
	void LateUpdate () 
    {
        if (!isShuffling)
        {
            foreach (BlockCollider col in colliders)
            {
                if (!col.BlockIsRightPlace)
                    return;

                if (OnGameOver != null)
                {
                    Debug.Log("Game over");
                    OnGameOver();
                }
            }
        }
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

    void OnStartShuffle()
    {
        isShuffling = true;
    }

    void OnEndShuffle()
    {
        isShuffling = false;
    }
}
