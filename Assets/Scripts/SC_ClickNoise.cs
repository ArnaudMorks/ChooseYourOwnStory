using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SC_ClickNoise : MonoBehaviour
{
	private AudioSource clickAudioSource;
	[SerializeField] private AudioClip clickSound;

	private void Start()
	{
		clickAudioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			clickAudioSource.PlayOneShot(clickSound);
		}
	}
}
