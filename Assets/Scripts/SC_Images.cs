using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SC_Images : MonoBehaviour
{
	[SerializeField] private Sprite[] allImages;
	[SerializeField] private string imageFileLocation;		//vanaf "Resources"; bijvoorbeeld "Images"
	//[SerializeField] public string[] AllImageNames;		//wordt in "Start" aangemaakt
	[SerializeField] private GameObject contiuneButton;		//LATER WEG; TIJDELIJKE OPLOSSING
	/*[SerializeField] private Sprite image;		VOORBEELD DIT
	[SerializeField] private string thisName;*/


	public void LoadAllImages()				//wordt in "SC_TextManager" uitgevoert, zodat het voor de andere "Start" functies gebeurt
	{
		allImages = Resources.LoadAll<Sprite>(imageFileLocation);
	}


	public string[] MakeNameStringArray()	//wordt in "SC_TextManager" uitgevoert in "Start"
	{
		string[] allImageNames = new string[allImages.Length];

		for (int i = 0; i < allImages.Length; i++)
		{
			allImageNames[i] = allImages[i].name;
		}

		return allImageNames;
	}


	public void SwitchImage(int currentImage)		//wordt uitgevoert in "SC_TextManager"
	{
		Image thisImage = GetComponent<Image>();
		thisImage.sprite = allImages[currentImage];

		if (thisImage.color.a == 0)
			thisImage.color = Color.white;		//normale kleuren

		if (allImages[currentImage].name == "explosion_white_greyscale"
			|| allImages[currentImage].name == "explosion"
			|| allImages[currentImage].name == "sad_drag_out")			//TIJDELIJK; LATER WEG
			contiuneButton.SetActive(false);
	}

	public void NoImage()
	{
		Image thisImage = GetComponent<Image>();
		thisImage.color = new Color(1, 1, 1, 0);
	}

}
