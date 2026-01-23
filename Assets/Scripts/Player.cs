using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private void Update() {
        Vector2 inputVector = new(0, 0);
        if (Input.GetKey(KeyCode.W)) {
            inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector.x += 1;
        }
        inputVector.Normalize();

        Vector3 moveDir = new(inputVector.x, 0, inputVector.y);
        this.transform.position += moveDir * Time.deltaTime * moveSpeed;
    }
}
