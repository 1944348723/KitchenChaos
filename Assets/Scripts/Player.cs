using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 10f;

    private bool isWalking = false;

    private void Update() {
        // 输入
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

        // 移动
        Vector3 moveDir = new(inputVector.x, 0, inputVector.y);
        this.transform.position += moveDir * Time.deltaTime * moveSpeed;
        this.isWalking = !moveDir.Equals(Vector3.zero);

        // 转向
        this.transform.forward = Vector3.Slerp(this.transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking() {
        return this.isWalking;
    }
}
