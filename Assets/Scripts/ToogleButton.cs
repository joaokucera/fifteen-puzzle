using UnityEngine;
using System.Collections;

public class ToogleButton : MonoBehaviour 
{
	[SerializeField]
	private Material buttonUp;
	[SerializeField]
	private Material buttonDown;
	[SerializeField]
	private Material buttonHover;
	[SerializeField]
	private AudioClip audioButtonClick;
	[SerializeField]
	private string functionName = string.Empty;

	private bool buttonActive = false;

	void Start()
	{
		if (buttonUp)
			renderer.material = buttonUp;
	}
	
	void OnMouseUp () 
	{
		if (buttonActive) 
		{
			if (buttonDown)
				renderer.material = buttonDown;
		} 
		else 
		{
			if (buttonUp)
				renderer.material = buttonUp;
		}
	}
	
	void OnMouseDown () 
	{
		if (buttonHover)
			renderer.material = buttonHover;
		
		if (audioButtonClick)
			AudioSource.PlayClipAtPoint (audioButtonClick, transform.position);
		
		SendMessage(functionName);

		buttonActive = !buttonActive;
	}
}
