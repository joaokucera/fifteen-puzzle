using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour 
{
	[SerializeField]
	private Material rightBlock;
	[SerializeField]
	private Material wrongBlock;

	void Start () 
	{
		renderer.material = rightBlock;
	}

	void RightPosition()
	{
		renderer.material = rightBlock;
	}

	void WrongPosition()
	{
		renderer.material = wrongBlock;
	}
}
