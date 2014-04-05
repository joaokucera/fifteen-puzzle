using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour 
{
	#region Fields
	private float totalMovement = 0.0f;
	private TextMesh textMesh;
	#endregion

	void Start()
	{
		textMesh = GetComponent<TextMesh> ();
	}

	void Update()
	{
		textMesh.text = string.Format ("{0}", totalMovement);
	}

	void OnMoveBlock()
	{
		this.totalMovement++;
	}
	
	void OnStartShuffle()
	{
		this.totalMovement = 0;
	}
	
	void OnEnable()
	{
		Hole.OnMoveBlock += OnMoveBlock;
		Hole.OnStartShuffle += OnStartShuffle;
		
	}
	
	void OnDisable()
	{
		Hole.OnMoveBlock -= OnMoveBlock;
		Hole.OnStartShuffle -= OnStartShuffle;
	}
}
