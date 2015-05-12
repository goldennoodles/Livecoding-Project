using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Movement : MonoBehaviour
{

    public int MovementLeft = 20;
    public int MovementRight = 20;

    [HideInInspector]
    public int CoinAmount;

    private int _jumpHeight = 300;
    private float _distanceFromTheGround;

    private Rigidbody _rg;
    private Vector2 _startPos;

    private float _liftHeight = 25f;

    public bool shouldRotate = false;

    private Text _score;

    void Start()
    {

        _rg = GetComponent<Rigidbody>();

        Transform trans = GetComponent<Transform>();
        _startPos = trans.position;


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

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "BoundryRight")
        {
            _rg.velocity = Vector2.zero;
            MovementRight = 0;

        }

        if (other.tag == "BoundryLeft")
        {
            _rg.velocity = Vector3.zero;
            MovementLeft = 0;
            shouldRotate = true;

        }
        else
        {
            shouldRotate = false;
        }

        if (other.tag == "OutOfBounds")
        {
            transform.position = new Vector2(_startPos.x, _startPos.y);
        }

        if (other.tag == "Lift")
        {
            _rg.AddForce(new Vector2(0, _liftHeight), ForceMode.Acceleration);
        }

        MovementLeft = 20;
        MovementRight = 20;
    }


    void printScore()
    {
        _score = GameObject.Find("Score").GetComponent<Text>();
        _score.text = "Score is: " + CoinAmount.ToString();

    }

    void controls()
    {

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _rg.AddForce(new Vector3(-MovementLeft, 0), ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _rg.AddForce(new Vector3(MovementRight, 0), ForceMode.Force);
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

