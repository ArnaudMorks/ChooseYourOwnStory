using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Shows the player which items they currently have on them
public class SC_Inventory : MonoBehaviour
{
	[SerializeField] private string[] g_itemsInInventory;
	[SerializeField] private int g_inventoryLenght;

	[SerializeField] private TMP_Text[] g_textArray;
	private const string NO_TEXT = " ";



	private void Start()
	{
		g_inventoryLenght = g_textArray.Length;
		//start with no items (empty text)
		for (int i = 0; i < g_inventoryLenght; i++)
		{
			g_itemsInInventory[i] = NO_TEXT;
			g_textArray[i].text = NO_TEXT;
		}

		//ItemsToUI();
	}



	public void ItemToUI(string itemVisualText)
	{
		for (int i = 0; i < g_inventoryLenght; i++)
		{
			if (g_itemsInInventory[i] == NO_TEXT)
			{
				g_itemsInInventory[i] = itemVisualText;
				g_textArray[i].text = g_itemsInInventory[i];
				break;
			}
		}

	}


}
