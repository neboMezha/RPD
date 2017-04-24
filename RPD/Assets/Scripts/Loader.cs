using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Class to load in text files and store lines in a list
/// </summary>
public class Loader : MonoBehaviour {
	List<string> lines = new List<string>();

	public List<string> Lines {  get { return lines; } }

	public void readTextFile(string filepath) {
		StreamReader stream = new StreamReader (filepath);

		while (!stream.EndOfStream) {
			string currentLine = stream.ReadLine ();
			currentLine.Trim ();			// currentLine = currentLine.Trim() ????

			if (currentLine.StartsWith("#"))	// Ignore lines starting with # "comment marker"
				continue;	

			// Add this current line to the string
			lines.Add(currentLine);
		}

		stream.Close ();
	}
}
