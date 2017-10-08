using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MicrophoneInput : MonoBehaviour
{
	public static void StartRecord (AudioSource audio)
	{
        Debug.Log("Recording audio");
		audio.clip = Microphone.Start ("Built-in Microphone", true, 60, 44100);
	}

	public static void StopRecord ()
	{
        Debug.Log("Stopping audio");
		if (Microphone.IsRecording (null)) {
            Microphone.End (null);
		}
	}
}
