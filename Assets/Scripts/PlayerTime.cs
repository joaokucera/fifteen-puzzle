using UnityEngine;
using System.Collections;

public class PlayerTime : MonoBehaviour 
{
	#region Fields
	private float startTime = 0.0f;
	private float totalTime = 0.0f;
	private bool isShuffling = false;
	private bool isGameOver = false;
	private TextMesh textMesh;
	#endregion

	void Start()
	{
		textMesh = GetComponent<TextMesh> ();
	}
	
	#region Update
	void Update()
	{
		if (!isGameOver)
		{
			if (isShuffling)
				startTime = Time.time;
			
			totalTime = Time.time - startTime;
		}

		textMesh.text = string.Format ("{0}:{1}", (totalTime / 60).ToString ("00"), (totalTime % 60).ToString ("00"));
	}
	#endregion
	
	#region Add and Remove Delegates
	void OnGameOver()
	{
		isGameOver = true;
	}

	void OnStartShuffle()
	{
		this.isShuffling = true;
	}
	
	void OnEndShuffle()
	{
		this.isShuffling = false;
	}
	
	void OnEnable()
	{
		Hole.OnStartShuffle += OnStartShuffle;
		Hole.OnEndShuffle += OnEndShuffle;
		GameOver.OnGameOver += OnGameOver;
		
	}
	
	void OnDisable()
	{
		Hole.OnStartShuffle -= OnStartShuffle;
		Hole.OnEndShuffle -= OnEndShuffle;
		GameOver.OnGameOver -= OnGameOver;
	}
	#endregion
}
