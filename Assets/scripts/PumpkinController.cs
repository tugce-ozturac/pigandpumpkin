using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Vector2 moveInput;
    [SerializeField] float moveSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(
            moveInput.x, 0f, moveInput.y);

        this.transform.Translate(
            direction * moveSpeed * Time.deltaTime,
            Space.World);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}