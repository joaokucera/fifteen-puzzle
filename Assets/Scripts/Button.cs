using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
	[SerializeField]
	private Material buttonUp;
	[SerializeField]
	private Material buttonDown;
	[SerializeField]
	private AudioClip audioButtonClick;
	[SerializeField]
	private bool exit = false;
	[SerializeField]
	private bool changeScene = false;
	[SerializeField]
	private string nextSceneToLoad = string.Empty;
	[SerializeField]
	private string functionName = string.Empty;

	void Start()
	{
		if (buttonUp)
			renderer.material = buttonUp;
	}
	
	void OnMouseUp () 
	{
		if (buttonUp)
			renderer.material = buttonUp;
	}

	void OnMouseDown () 
	{
		if (buttonDown)
			renderer.material = buttonDown;

		if (audioButtonClick)
			AudioSource.PlayClipAtPoint (audioButtonClick, transform.position);

		if (exit) 
			Application.Quit ();
		else if (changeScene) 
			Application.LoadLevel(nextSceneToLoad);
		else 
			SendMessage(functionName);
	}
}