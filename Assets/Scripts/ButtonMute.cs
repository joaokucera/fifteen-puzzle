using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ToogleButton))]
public class ButtonMute : MonoBehaviour 
{
	void Mute()
	{
		AudioListener.volume = 1 - AudioListener.volume;		
	}
}
