using UnityEngine;
using System.IO;
using System.Collections.Generic;

// Generates "random" names from a list. Call RandomName() to get a new random name.
public static class RandomNamer
{
	static List<string> names;
	static int namesIndex = 0;
    static string startingList = "Names.txt";
	static bool initialized = false;

	static void Init()
	{
		initialized = true;
		// Pick "random" seed between 0 and 60
		Random.InitState (System.DateTime.Now.Second);
		SwapNameList(startingList);
	}

	// Loads a new list of names.
	public static void SwapNameList(string nameList)
	{
        Debug.Log("test2");

		namesIndex = 0;

		// Load initial name list from text file
		StreamReader sr = File.OpenText("Assets/Data/" + nameList);
		names = new List<System.String>(sr.ReadToEnd().Split("\n"[0]));
		sr.Close();

		for(int i = 0; i < names.Count; i++)
		{
			// remove whitespace
			names[i] = names[i].Trim();
		}

		// Make a name list that is shuffled so the list is a different order every time
		for (int i = 0; i < names.Count; i++)
		{
			string temp = names[i];
			int randomIndex = Random.Range(i, names.Count);
			names[i] = names[randomIndex];
			names[randomIndex] = temp;
		}
	}

	// Returns the next element from the shuffled list
    public static string RandomName()
    {
        Debug.Log("test1");
		if (!initialized)
		{
			Init();
		}

		string name = names[namesIndex++];
		// Make sure we don't go off the end of the list
		namesIndex = namesIndex % names.Count;
        return name;
    }
}