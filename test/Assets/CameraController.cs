using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform target2;
    public GameObject target;
    private GameObject moveScript;
    private Camera MainCam;
    public Ray ray;
    private Rigidbody rigid;
    public GameObject[] CameraTargets;
    private Transform _cameraTargetsStorage;


    private Vector3 RotateTarget;
    public Transform moveToMe;
    public int i = 0;
    private bool isActive = false;
    public bool _invert = false;



	// Use this for initialization
	void Start () 
    {
        moveScript = GameObject.Find("Player");
        MainCam = GetComponent<Camera>();
        rigid = moveScript.GetComponent<Movement>()._rg;
        target2 = GameObject.Find("Player").GetComponent<Transform>();
        _cameraTargetsStorage = GameObject.Find("CameraTargets").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        rotationControls();

        _cameraTargetsStorage.transform.position = new Vector3(_cameraTargetsStorage.transform.position.x, target.transform.position.y + 1, _cameraTargetsStorage.transform.position.z);


        if (moveScript.GetComponent<Movement>().shouldRotate == false && moveScript.GetComponent<Movement>()._normalControls == true && _invert == false)
        {
            transform.position = new Vector3(_cameraTargetsStorage.transform.position.x, _cameraTargetsStorage.transform.position.y, _cameraTargetsStorage.transform.position.z * 1.5f);
        }

        if (moveScript.GetComponent<Movement>().shouldRotate == false && moveScript.GetComponent<Movement>()._normalControls == true && _invert == true && isActive == false)
        {
            transform.position = new Vector3(_cameraTargetsStorage.transform.position.x, _cameraTargetsStorage.transform.position.y, CameraTargets[i].transform.position.z);

        }


        if (moveScript.GetComponent<Movement>().shouldRotate == true)
        {
            StartCoroutine(shouldRotate());
        }

        if (i < 0)
        {
            i = 3;
        }

        if (i > 3)
        {
            i = 0;
        }

        if (i == 2)
        {
            _invert = true;
        }

	}

    IEnumerator shouldRotate()
    {
        isActive = true;
        transform.position = Vector3.MoveTowards(MainCam.transform.position, CameraTargets[i].transform.position, Time.deltaTime * 20);
        transform.LookAt(_cameraTargetsStorage.transform.position);

        Debug.Log("Im Turning");
        

        rigid.velocity = Vector3.zero;

        yield return new WaitForSeconds(2.0f);

        isActive = false;

        moveScript.GetComponent<Movement>().shouldRotate = false;

    }

    public void rotationControls()
    {

        if(isActive == false){
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //i = i + 1;
            i++;
            moveScript.GetComponent<Movement>()._normalControls = !moveScript.GetComponent<Movement>()._normalControls;
            moveScript.GetComponent<Movement>().shouldRotate = true;
            Debug.Log("Right Arrow is Working!");
        }

        /*if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveScript.GetComponent<Movement>().shouldRotate = true;
            i--;
            Debug.Log("Left Arrow is Working!");
        }*/
        }

    }

}
