using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Movement : MonoBehaviour
{

    public int MovementHorizontal = 20;       private float _distanceFromTheGround;
    public int MovementVerticle = 20;         private float _liftHeight = 25f;
    public int MovementRight = 20;            private int _jumpHeight = 350;
    public int CoinAmount;                    private Vector2 _startPos;
    public bool shouldRotate = false;         public Rigidbody _rg;
    public bool _normalControls = true;       private Text _score;
                                              private GameObject _camMove;                                     


    void Start()
    {
        _rg = GetComponent<Rigidbody>();

        Transform trans = GetComponent<Transform>();
        _startPos = trans.position;
        _camMove = GameObject.Find("Main Camera");


    }

    void Update()
    {
        printScore();
        controls();
    }

    void FixedUpdate()
    {
        int TriggerMask = 8;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, TriggerMask))
        {
            _distanceFromTheGround = hit.distance;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            CoinAmount++;
            Destroy(other.gameObject);
        }

        if (other.tag == "Spikes")
        {
            _rg.velocity = Vector3.zero;
            transform.position = new Vector3(_startPos.x, _startPos.y);
        }

        if (other.tag == "BoundryLeft")
        {
            _rg.velocity = Vector3.zero;
            //MovementHorizontal = 0;
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "BoundryRight")
        {
            _rg.velocity = Vector2.zero;
        }

        //---------//

        if (other.tag == "OutOfBounds")
        {
            transform.position = new Vector2(_startPos.x, _startPos.y);
        }

        if (other.tag == "Lift")
        {
            _rg.AddForce(new Vector2(0, _liftHeight), ForceMode.Acceleration);
        }

        MovementHorizontal = 20;
        MovementRight = 20;
    }


    void printScore()
    {
        _score = GameObject.Find("Score").GetComponent<Text>();
        _score.text = "Score is: " + CoinAmount.ToString();
    }

    void controls()
    {
        //---------------------------------------------------------------//

        GameObject _cameraScript;

        _cameraScript = GameObject.Find("Main Camera");

        //---------------------------------------------------------------//

        if (_normalControls == true && _camMove.GetComponent<CameraController>()._invert == false)
        {
            if (Input.GetKey(KeyCode.A))
            {
                _rg.AddForce(new Vector3(-MovementHorizontal, 0), ForceMode.Force);
            }

            if (Input.GetKey(KeyCode.D))
            {
                _rg.AddForce(new Vector3(MovementHorizontal, 0), ForceMode.Force);
            }
        }

        if (_normalControls == false)
        {
            if (Input.GetKey(KeyCode.A))
            {
                _rg.AddForce(new Vector3(0, 0, -MovementVerticle), ForceMode.Force);
            }

            if (Input.GetKey(KeyCode.D))
            {
                _rg.AddForce(new Vector3(0, 0, MovementVerticle), ForceMode.Force);
            }
        }

        if (_camMove.GetComponent<CameraController>()._invert == true )
        {
            if (Input.GetKey(KeyCode.A))
            {
                _rg.AddForce(new Vector3(MovementHorizontal, 0), ForceMode.Force);
                Debug.Log("Invert == A");

            }

            if (Input.GetKey(KeyCode.D))
            {
                _rg.AddForce(new Vector3(-MovementHorizontal, 0), ForceMode.Force);
                Debug.Log("Invert == D");

            }

            Debug.Log("Invert == true");
        }

        if (_distanceFromTheGround <= 0.6f)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _rg.AddForce(new Vector2(1, _jumpHeight), ForceMode.Force);
                _rg.drag = 2;
                _rg.mass = 5;

            }
            else
            {
                _rg.drag = 1;
                _rg.angularDrag = 0;
                _rg.mass = 1;
            }
        }
    }
}

