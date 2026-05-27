using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Animator animator;

    public float speed;
    public float rotationSpeed = 360f;
    public float gravity = -9.8f;
    public float groundCheckDistance = 0.4f;

    private Vector2 lastInput;
    private Quaternion targetRotaion;
    private bool isGround;

    private void Start()
    {
        speed = PlayerStats.Instance.Speed;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        isGround = controller.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDir = Vector3.zero;
        moveDir.x = input.x;
        moveDir.z = input.y;

        controller.Move(moveDir * speed * Time.deltaTime);

        //중력적용 (characterController 쓰면 rigidabody 대신 이방법 사용)
        //점프가 있으면 계속 누르는데 없으니까 걍 고정만 시킴(사실 걍 써보고싶었음
        if (isGround && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f; // 지면에 닿았을 때 y 속도를 최소값으로 설정
        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime; // 중력 가속도 적용
        }

        controller.Move(playerVelocity * Time.deltaTime);
        animator.SetFloat("Speed", input.magnitude);

        if (input != lastInput)
        {
            if (input.x < 0) //좌{
            {
                targetRotaion = Quaternion.Euler(0, -90, 0);
            }
            else if (input.x > 0) //우
                targetRotaion = Quaternion.Euler(0, 90, 0);
            else if (input.y < 0) //하
                targetRotaion = Quaternion.Euler(0, 180, 0);
            else if (input.y > 0) //상
                targetRotaion = Quaternion.Euler(0, 0, 0);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotaion, rotationSpeed * Time.deltaTime);

        lastInput = input;
    }
}
