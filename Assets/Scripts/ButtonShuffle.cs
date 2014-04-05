using UnityEngine;
using System.Collections;

public class ButtonShuffle : MonoBehaviour 
{
	private bool isShuffling = false;
	private Transform hole;

	void Start()
	{
		hole = GameObject.Find("Hole").transform;
		Shuffle ();
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
	}
	
	void OnDisable()
	{
		Hole.OnStartShuffle -= OnStartShuffle;
		Hole.OnEndShuffle -= OnEndShuffle;
	}

	void Shuffle ()
	{
		if (!isShuffling)
			hole.SendMessage("Shuffle");
	}
}
