using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour {

    public int coinsss;

	void Start () 
    {

	}
	
	void OnTriggerEnter (Collider other) 
    {

        if (other.tag == "User")
        {
            coinsss++;
            Destroy(this.gameObject);
        }
	}
}
