using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using System;

public static class SaveManager
{
	public static string storedSessionPath = Application.persistentDataPath + "/speechBubbleInfo.dat";

	public static void Save (List<BubbleObject> session, string destinationPath)
	{
		BinaryFormatter formatter = new BinaryFormatter ();
		            
		FileStream saveFile = File.Create (storedSessionPath);

		formatter.Serialize (saveFile, session);

		saveFile.Close ();
	}


	public static List<BubbleObject> Load ()
	{
		if (File.Exists (storedSessionPath)) {
			BinaryFormatter formatter = new BinaryFormatter ();
			FileStream saveFile = File.Open (storedSessionPath, FileMode.Open);
			List<BubbleObject> savedSession = (List<BubbleObject>)formatter.Deserialize (saveFile);
			saveFile.Close ();
			return savedSession;
		}
		return null;
	}
		
}