using UnityEngine;

public class PigAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeDirectionInterval = 3f;
    public float detectionRange = 5f;

    public Transform pumpkinTransform;

    private Vector3 movementDirection;
    private float timer;
    private enum State { Wandering, Chasing }
    private State currentState = State.Wandering;

    void Start()
    {
        if (pumpkinTransform == null)
        {
            GameObject pumpkinObj = GameObject.Find("Pumpkin");
            if (pumpkinObj != null)
            {
                pumpkinTransform = pumpkinObj.transform;
            }
        }

        ChooseNewDirection();
    }

    void Update()
    {
        if (pumpkinTransform != null)
        {
            float distanceToPumpkin = Vector3.Distance(transform.position, pumpkinTransform.position);

            
            Vector3 directionToPumpkin = (pumpkinTransform.position - transform.position).normalized;
            directionToPumpkin.y = 0f; 

           
            float dotProd = Vector3.Dot(transform.forward, directionToPumpkin.normalized);

            
            if (distanceToPumpkin <= detectionRange && dotProd > 0f)
            {
                currentState = State.Chasing;
            }
            else
            {
 
                if (currentState == State.Chasing)
                {
                    currentState = State.Wandering;
                    ChooseNewDirection();
                }
            }
        }

        
        if (currentState == State.Chasing)
        {
        
            Vector3 directionToPumpkin = (pumpkinTransform.position - transform.position).normalized;
            directionToPumpkin.y = 0f;

            if (directionToPumpkin != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(directionToPumpkin);
            }

            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
        
            transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);

            timer += Time.deltaTime;
            if (timer >= changeDirectionInterval)
            {
                ChooseNewDirection();
                timer = 0f;
            }
        }
    }

    void ChooseNewDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        Quaternion rotation = Quaternion.Euler(0f, randomAngle, 0f);
        movementDirection = rotation * Vector3.forward;

        if (movementDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movementDirection);
        }
    }
}