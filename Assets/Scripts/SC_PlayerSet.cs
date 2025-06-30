using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerSet : MonoBehaviour
{
	[SerializeField] private Camera g_mainCamera;

	[Header("Multiplayer")]
	[SerializeField] private CanvasGroup g_playerSwitchOne;
	[SerializeField] private CanvasGroup g_playerSwitchTwo;
	[SerializeField] private bool g_switchScreenOn = false;

	[Header("Inventory")]
	[SerializeField] private SC_Inventory g_inventoryScript;
	[SerializeField] private bool g_inventoryOpen = false;
	[SerializeField] private CanvasGroup g_inventoryUIGroup;


	private void Update()
	{
		if (g_switchScreenOn && Input.GetKeyUp(KeyCode.J))
		{
			if (g_playerSwitchOne == null || g_playerSwitchTwo == null)
				return;

			g_playerSwitchOne.alpha = 0;
			g_playerSwitchTwo.alpha = 0;
			g_playerSwitchOne.blocksRaycasts = false;
			g_playerSwitchTwo.blocksRaycasts = false;

			g_switchScreenOn = false;
		}

		//Open / close Inventory UI
		if (Input.GetKeyUp(KeyCode.I))
		{
			if (g_inventoryScript == null)
				return;

			if (g_inventoryOpen)
			{
				g_inventoryOpen = false;
				g_inventoryUIGroup.alpha = 0;
			}
			else
			{
				g_inventoryOpen = true;
				g_inventoryUIGroup.alpha = 1;
			}
			g_inventoryUIGroup.blocksRaycasts = g_inventoryOpen;
		}

	}



	public void ChangePlayerTurn(int changeToPlayer)
	{
		if (changeToPlayer == 1)
		{
			g_playerSwitchOne.alpha = 1;
			g_playerSwitchOne.blocksRaycasts = true;
			g_mainCamera.backgroundColor = Color.white;
		}
		else if (changeToPlayer == 2)
		{
			g_playerSwitchTwo.alpha = 1;
			g_playerSwitchTwo.blocksRaycasts = true;
			g_mainCamera.backgroundColor = Color.black;
		}
		g_switchScreenOn = true;
	}

}
