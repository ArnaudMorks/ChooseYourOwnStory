using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_DisableScreen : MonoBehaviour
{
	public void DisableThisScreen()
	{
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0;
		canvasGroup.blocksRaycasts = false;
		Debug.Log("Disablethis");
	}

}
