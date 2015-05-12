using UnityEngine;
using System.Collections;

public class UI_Buttons : MonoBehaviour {

	public void Restart () 
    {
        Application.LoadLevel(1);
        print("Restarting...");
	}

    public void quit()
    {
        Application.Quit();
        print("Quiting...");
    }
}
