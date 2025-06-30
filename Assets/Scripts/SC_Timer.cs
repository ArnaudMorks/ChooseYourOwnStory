using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Timer : MonoBehaviour
{
	[SerializeField] private float time;	//begin 0
	[SerializeField] private int failOption;
	[SerializeField] private bool failDone = false;
	private SC_TextManager textManager;

	private void Start()
	{
		textManager = FindObjectOfType<SC_TextManager>();
	}

	private void Update()
	{
		if (time > 0)
		{
			time -= 1 * Time.deltaTime;
		}
		else if (failDone == false)
		{
			textManager.MakeChoice(failOption);
			failDone = true;
		}

	}

	public void ActiveTimer(float setTime, int onFail)
	{
		time = setTime;
		failOption = onFail;
		failDone = false;
	}

}
