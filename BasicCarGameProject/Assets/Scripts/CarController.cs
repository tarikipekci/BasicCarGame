using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CarController : MonoBehaviour
{
    [Header("Components")] public Transform[] wayPointsBlue, wayPointsOrange;
    public Rigidbody2D rb;
    
    [Header("Variables")] public int target;
    public float moveSpeed;
    public float waitToChangeDirection;
    public float counter;
    public float counterCounter;
    public bool isChanged;
    public float start;
    public int crashCounter;

    [Header("Scripts")] public static CarController instance;
    private void Awake()
    {
        instance = this;
        counterCounter = counter;
    }

    private void Start()
    {
        if (gameObject.name == "OrangeCar")
        {
            gameObject.tag = "OrangeCar";
        }
    }

    private void Update()
    {
        start -= Time.deltaTime;
        
        if (gameObject.CompareTag("BlueCar") && gameObject.name == "OrangeCar")
        {
            transform.position =
                    Vector2.MoveTowards(transform.position, wayPointsBlue[target + 2].position,
                        moveSpeed * Time.deltaTime);
        }
        
        if (gameObject.CompareTag("BlueCar") && gameObject.name == "BlueCar")
        {
            transform.position =
                Vector2.MoveTowards(transform.position, wayPointsBlue[target].position, moveSpeed * Time.deltaTime);
        }

        if (gameObject.CompareTag("OrangeCar"))
        {
            transform.position =
                Vector2.MoveTowards(transform.position, wayPointsOrange[target].position, moveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameObject.name == "BlueCar")
            {
               
                if (gameObject.CompareTag("BlueCar"))
                {
                    gameObject.tag = "OrangeCar";
                }
                else if (gameObject.CompareTag("OrangeCar"))
                {
                    gameObject.tag = "BlueCar";
                }
            }
        }
        
        if (transform.position == wayPointsBlue[target].position && gameObject.CompareTag("BlueCar") &&
            gameObject.name == "BlueCar")
        {
            if (target == wayPointsBlue.Length - 1)
            {
                target = 0;
                transform.Rotate(0, 0, -45);
            }
            else
            {
                target++;
                transform.Rotate(0, 0, -45);
            }
        }
        
        if (gameObject.CompareTag("BlueCar") && gameObject.name == "OrangeCar")
        {
            if (transform.position == wayPointsBlue[target + 2].position)
            {
                    if (target + 2 == wayPointsBlue.Length - 1)
                    {
                        target = 0;
                        transform.Rotate(0, 0, 45);
                    }
                    else
                    {
                        target++;
                        transform.Rotate(0, 0, 45);
                    }
            }
        }

        
        if (transform.position == wayPointsOrange[target].position && gameObject.CompareTag("OrangeCar"))
        {
            if (gameObject.name == "OrangeCar")
            {
                if (target == wayPointsOrange.Length - 1)
                {
                    target = 0;
                    transform.Rotate(0, 0, 45);
                }
                else
                {
                    target++;
                    transform.Rotate(0, 0, 45);
                }
            }

            if (gameObject.name == "BlueCar")
            {
                if (target == wayPointsOrange.Length - 1)
                {
                    target = 0;
                    transform.Rotate(0, 0, -45);
                }
                else
                {
                    target++;
                    transform.Rotate(0, 0, -45);
                }
            }
        }

        if (start < 0)
        {
            if (counter > 0 && isChanged == false && gameObject.name == "OrangeCar")
            {
                var chanceOfChangingRotation =
                    Random.Range(0.50f * waitToChangeDirection, 2f * waitToChangeDirection);
                if (chanceOfChangingRotation < 3f  && gameObject.CompareTag("OrangeCar"))
                {
                    gameObject.tag = "BlueCar";
                    isChanged = true;
                }
                else
                {
                    gameObject.tag = "OrangeCar";
                    isChanged = true;
                }
            }
        }

        if (counter > 0)
        {
            counter -= Time.deltaTime;
        }

        if (counter <= 0)
        {
            counter = counterCounter;
            isChanged = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("BlueCar") || other.gameObject.CompareTag("OrangeCar"))
        {
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
            crashCounter++;
        }
    }
}