using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject target;
    public GameObject moveScript;
    public Camera MainCam;
    public Ray ray;


    private Vector3 RotateTarget;
    public Transform moveToMe;

	// Use this for initialization
	void Start () 
    {
        moveScript = GameObject.Find("Player");
        MainCam = GetComponent<Camera>();


	}
	
	// Update is called once per frame
	void Update () {

        if (moveScript.GetComponent<Movement>().shouldRotate == true)
        {
            tatata();
        }
        else
        {
            transform.position = new Vector3(target.transform.localPosition.x, target.transform.localPosition.y, -10);

        }

        ray = MainCam.ScreenPointToRay(new Vector2(530, 310));
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
	
	}

    /*IEnumerator Rotation()
    {
        transform.position = Vector3.MoveTowards(MainCam.transform.position, moveToMe.transform.position, Time.deltaTime * 5);
        transform.LookAt(target.transform.position);

        yield return new WaitForSeconds(4);

        moveScript.GetComponent<Movement>().shouldRotate = false;

    } */

    void tatata()
    {
        transform.position = Vector3.MoveTowards(MainCam.transform.position, moveToMe.transform.position, Time.deltaTime * 5);
        transform.LookAt(target.transform.position);

    }

}
