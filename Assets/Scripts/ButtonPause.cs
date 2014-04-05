using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ToogleButton))]
public class ButtonPause : MonoBehaviour 
{
	private bool isPause;

	void Pause()
	{
		isPause = !isPause;
		if (isPause)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}
}
