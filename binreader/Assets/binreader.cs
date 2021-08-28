using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleFileBrowser;

public class binreader : MonoBehaviour
{
    public Text reference;
    public Text value;

	private int currentbyte;
	private string path;

    // Update is called once per frame
    public void OpenFile()
    {
        StartCoroutine(ShowLoadDialogCoroutine());
    }

	public void Left()
	{
		if (currentbyte <= 1)
			currentbyte = FileBrowserHelpers.ReadBytesFromFile(path).Length;
		else
			currentbyte -= 1;

		ShowValues();
	}

	public void Right()
	{
		if (currentbyte >= FileBrowserHelpers.ReadBytesFromFile(path).Length)
			currentbyte = 1;
		else
			currentbyte += 1;

		ShowValues();
	}

	IEnumerator ShowLoadDialogCoroutine()
	{
		// Show a load file dialog and wait for a response from user
		// Load file/folder: both, Allow multiple selection: true
		// Initial path: default (Documents), Initial filename: empty
		// Title: "Load File", Submit button text: "Load"
		yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Open File", "Open");

		if (FileBrowser.Success)
		{
			// Read the bytes of the first file via FileBrowserHelpers
			// Contrary to File.ReadAllBytes, this function works on Android 10+, as well
			currentbyte = 1;
			path = FileBrowser.Result[0];
			ShowValues();
		}
	}

	private void ShowValues()
	{
		byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(path);
		reference.text = "Byte " + currentbyte;
		value.text = bytes[currentbyte - 1].ToString();
	}
}