using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour 
{
	public Canvas instructionMenu;
	public Button StartText;
	public Button InstructionText;

	void Start()
	{
		instructionMenu = instructionMenu.GetComponent<Canvas> ();
		StartText = StartText.GetComponent<Button> ();
		InstructionText = InstructionText.GetComponent<Button> ();
		instructionMenu.enabled = false;
	}

	public void InstructionPress()
	{
		instructionMenu.enabled = true;
		StartText.enabled = false;
		InstructionText.enabled = false;
	}

	public void BackPress()
	{
		instructionMenu.enabled = false;
		StartText.enabled = true;
		InstructionText.enabled = true;
	}

	public void StartLevel()
	{
		SceneManager.LoadScene (1);
	}
}
