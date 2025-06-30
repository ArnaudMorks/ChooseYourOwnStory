using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SC_TextManager : MonoBehaviour
{
	[Header("Text UI")]
	[SerializeField] private TextMeshProUGUI g_storyText;

	[SerializeField] private Story g_currentStory;

	[SerializeField] private TextAsset g_inkJSON;

	private static SC_TextManager g_instance;

	[SerializeField] private GameObject g_continueButton;

	[Header("Choices UI")]
	[SerializeField] private GameObject[] g_choices;
	private TextMeshProUGUI[] g_choicesText;

	[Header("Image")]
	private const string IMAGE_TAG = "image";
	private const string NO_IMAGE_VALUE = "non";		//gets rid of the image (maybe make it not a const because of story editor)
	private SC_Images g_imageObject;
	[SerializeField] private string[] g_allImageNames;
	[SerializeField] private int g_totalNumberOfImages;

	[Header("Audio")]
	private const string AUDIO_TAG = "audio";
	private SC_Audio g_audioObject;
	[SerializeField] private string[] g_allAudioNames;
	[SerializeField] private int g_totalNumberOfAudioFiles;

	[Header("PlayerTurn")]
	private const string PLAYER_TAG = "player";
	private SC_PlayerSet g_playerSetObject;
	[SerializeField] private int g_player = 0;		//"0" = no player, "1" = player 1, "2" = player 2

	[Header("SceneManager")]
	private const string SCENE_TAG = "scene";
	private SC_SceneManager g_sceneManager;



	private void Awake()
	{
		if (g_instance != null)
			Debug.LogWarning("Found more than one Text Manager in the scene");
		g_instance = this;
	}


	private void Start()
	{
		ImagesLoadAndSortOnStart();
		AudioLoadAndSortOnStart();

		g_sceneManager = FindObjectOfType<SC_SceneManager>();

		g_playerSetObject = FindObjectOfType<SC_PlayerSet>();

		g_currentStory = new Story(g_inkJSON.text);
		ContinueStory();
		Debug.Log(g_currentStory);

		//get all of the choices text
		g_choicesText = new TextMeshProUGUI[g_choices.Length];
		int i = 0;
		foreach (GameObject choice in g_choices)
		{
			g_choicesText[i] = choice.GetComponentInChildren<TextMeshProUGUI>();
			i++;
		}
	}



	private void ImagesLoadAndSortOnStart()
	{
		g_imageObject = FindObjectOfType<SC_Images>();
		g_imageObject.LoadAllImages();
		g_allImageNames = g_imageObject.MakeNameStringArray();
		g_totalNumberOfImages = g_allImageNames.Length - 1;			//"-1" because position "0" in a ".Lenght" is "1"
	}


	private void AudioLoadAndSortOnStart()
	{
		g_audioObject = FindObjectOfType<SC_Audio>();
		if (g_audioObject != null)
		{
			g_audioObject.LoadAllAudio();
			g_allAudioNames = g_audioObject.MakeNameStringArray();
		}

		g_totalNumberOfAudioFiles = g_allAudioNames.Length - 1;
	}


	private void HandleTags(List<string> currentTags)
	{
		foreach (string tag in currentTags)
		{
			// parse the tag
			string[] splitTag = tag.Split(':');
			if (splitTag.Length != 2)
				Debug.LogError("Tag could not be appropriately parsed: " + tag);
			string tagKey = splitTag[0].Trim();		//"Trim" gets rid of whitespace
			string tagValue = splitTag[1].Trim();

			// check the tag
			int i = 0;
			if (tagKey == IMAGE_TAG)
			{
				while (g_allImageNames[i] != tagValue && i < g_totalNumberOfImages)
				{
					i++;
				}

				if (tagValue == g_allImageNames[i])
				{
					Debug.Log(tagValue + " is same as " + g_allImageNames[i]);
				}
				else if (tagValue != NO_IMAGE_VALUE)
				{
					Debug.LogError("Image does not exist in the allImageNames Array, nor is the value the same as "
						+ NO_IMAGE_VALUE);
					return;
				}
			}
			else if (tagKey == AUDIO_TAG)
			{
				while (g_allAudioNames[i] != tagValue && i < g_totalNumberOfAudioFiles)
				{
					i++;
				}

				if (tagValue == g_allAudioNames[i])
				{
					Debug.Log(tagValue + " is same as " + g_allAudioNames[i]);
				}
				else
				{
					Debug.LogError("Audio does not exist in the allAudioFileNames Array");
					return;
				}
			}
			else if (tagKey == PLAYER_TAG)
			{
				if (tagValue == "1")
					i = 1;
				else if (tagValue == "2")
					i = 2;
				else
				{
					Debug.LogError("Player number non existent");
					return;
				}

			}
			else if (tagKey == SCENE_TAG)
			{
				if (tagValue != "restart")	//later more tags to check
				{
					Debug.LogError(tagValue + " does not exist in scene");
					return;
				}
			}

			// handle the tag
			switch (tagKey)
			{
				case IMAGE_TAG:
					Debug.Log("image=" + tagValue);

					//checks first if a new image should be shown, or no image should be shown
					//sends "i" wich takes an image from the array in "SC_Images"-
					//-based on the array location of the name of the "sprite"
					if (tagValue == NO_IMAGE_VALUE)
						g_imageObject.NoImage();
					else
						g_imageObject.SwitchImage(i);

					break;
				case AUDIO_TAG:
					Debug.Log("audio=" + tagValue);

					g_audioObject.SwitchAudioClip(i);
					break;
				case PLAYER_TAG:
					Debug.Log("current player=" + tagValue);

					PlayerTurnSwitch(i);
					break;
				case SCENE_TAG:
					Debug.Log("Restart scene");

					g_sceneManager.RestartStory();
					break;
				default:
					Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
					break;
			}

		}

	}


	private void PlayerTurnSwitch(int changeToPlayer)
	{
		g_player = changeToPlayer;		//for buttons

		if (changeToPlayer == 1)
		{
			g_storyText.color = Color.black;
		}
		else if (changeToPlayer == 2)
		{
			g_storyText.color = Color.white;
		}
		if (g_continueButton == isActiveAndEnabled)
		{
			ButtonColorChange(g_continueButton);
		}

		int i = 0;
		foreach (GameObject choice in g_choices)
		{
			ButtonColorChange(g_choices[i]);
			i++;
		}


		g_playerSetObject.ChangePlayerTurn(changeToPlayer);
	}


	private void DisplayChoises()
	{
		List<Choice> currentChoicesList = g_currentStory.currentChoices;

		if (currentChoicesList.Count == 0)
		{
			for (int j = 0; j < g_choices.Length; j++)
			{
				g_choices[j].gameObject.SetActive(false);
			}
			return;
		}
		g_continueButton.SetActive(false);

		// defensive check to make sure UI can support the number of choices coming in.
			if (currentChoicesList.Count > g_choices.Length)
		{
			Debug.Log("More choices were given than the UI can support. Number of choices given: " + currentChoicesList.Count);
		}


		// enable and initialize the choices up to the amount of choices for this line of dialogue
		int i = 0;
		foreach (Choice choice in currentChoicesList)
		{
			g_choices[i].gameObject.SetActive(true);
			ButtonColorChange(g_choices[i]);			//TEMPORARY SOLUTION
			g_choicesText[i].text = choice.text;			//Choice can't be made on the first line of text now; I DON'T KNOW WHY
			i++;
		}
		DisableChoices(i);

	}


	private void DisableChoices(int i)
	{

		// go through the remaining choices the UI supports and make sure they're hidden
		for (int j = i; j < g_choices.Length; j++)
		{
			g_choices[i].gameObject.SetActive(false);
			Debug.Log("Choice display disabled " + i);
		}
	}


	//Special function for a "2 player mode"
	private void ButtonColorChange(GameObject thisButton)
	{
		TMP_Text buttonText = thisButton.GetComponentInChildren<TMP_Text>();

		if (g_player == 1)
		{
			buttonText.color = Color.black;
			thisButton.GetComponent<Image>().color = Color.white;
		}
		else if (g_player == 2)
		{
			buttonText.color = Color.white;
			thisButton.GetComponent<Image>().color = Color.black;
		}
	}



	//Public functions
	//Executed from the "Continue" button
	public void ContinueStory()
	{
		if (g_currentStory.canContinue)
		{
			// set text for the current dialogue line
			g_storyText.text = g_currentStory.Continue();
			// display choices, if any, for this dialogue line
			g_continueButton.SetActive(true);

			ButtonColorChange(g_continueButton);

			DisplayChoises();
			// handle tags
			HandleTags(g_currentStory.currentTags);
		}
	}

	//Executed from "Multiple choice" buttons, "choice" MUST be coupled with "Ink" text file choice index
	public void MakeChoice(int choice)
	{
		g_currentStory.ChooseChoiceIndex(choice);
		ContinueStory();
	}


}
