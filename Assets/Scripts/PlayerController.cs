using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.CustomTool;

public class PlayerController : MonoBehaviour
{
    private InputManager inputManager;
    private Rigidbody2D rb;
    private Transform playerTransform;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    void Start()
    {
        inputManager = FindObjectOfType<InputManager>(); // 或通過依賴注入
        rb = GetComponent<Rigidbody2D>();

        playerTransform = UnityTool.FindGameObject("Player").transform;
    }

    void Update()
    {
        HandleMovement();
        HandleLookDir();
    }

    void HandleMovement()
    {
        Vector2 moveDirection = new Vector2(inputManager.MoveInput.x, inputManager.MoveInput.y);
        Vector2 playerPos = transform.position;
        Vector2 playerNewPos = playerPos + moveDirection * moveSpeed * Time.deltaTime;
        playerTransform.position = playerNewPos;
        //rb.MovePosition(playerPos + moveDirection * moveSpeed * Time.deltaTime);
    }

    private void HandleLookDir()
    {
        if (inputManager.LookInput == Vector2.zero) return;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector2(inputManager.LookInput.x, inputManager.LookInput.y));
        Vector2 playerPos = transform.position;
        Vector2 direction = mousePos - playerPos;
        playerTransform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

        //Debug.Log(inputManager.LookInput.x + " " + inputManager.LookInput.y);
        //playerTransform.up = (target - playerPos).normalized;
    }
}
