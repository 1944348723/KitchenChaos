using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 10f;

    private bool isWalking = false;

    private void Update() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

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
