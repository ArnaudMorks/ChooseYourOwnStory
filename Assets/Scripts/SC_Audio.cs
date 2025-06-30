using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SC_Audio : MonoBehaviour
{
	[SerializeField] private AudioClip[] allAudioClips;
	[SerializeField] private string audioFileLocation;

	public void LoadAllAudio()
	{
		allAudioClips = Resources.LoadAll<AudioClip>(audioFileLocation);
	}

	public string[] MakeNameStringArray()   //wordt in "SC_TextManager" uitgevoert in "Start"
	{
		string[] allImageNames = new string[allAudioClips.Length];

		for (int i = 0; i < allAudioClips.Length; i++)
		{
			allImageNames[i] = allAudioClips[i].name;
		}

		return allImageNames;
	}

	public void SwitchAudioClip(int currentAudioFile)
	{
		AudioSource thisAudioSource = GetComponent<AudioSource>();
		//thisAudioSource.clip = allAudioClips[currentAudioFile];
		thisAudioSource.PlayOneShot(allAudioClips[currentAudioFile]);
	}

}
