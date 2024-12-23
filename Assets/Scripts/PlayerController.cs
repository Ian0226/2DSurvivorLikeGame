using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.CustomTool;
using System.Threading.Tasks;

public class PlayerController : GameSystemBase
{
    private InputManager inputManager;
    private Transform playerTransform;

    private float moveSpeed = 5f;
    private float jumpForce = 10f;

    /// <summary>
    /// ����CD�A��쬰�@��
    /// </summary>
    private int attackCD = 0;
    private bool canAttack = false;

    public bool CanAttack { get => canAttack; set => canAttack = value; }

    public PlayerController(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        inputManager = SurvivorLikeGame2DFacade.Instance.GetInputManager();

        playerTransform = UnityTool.FindGameObject("Player").transform;

        InitProperties();
    }

    public override void Update()
    {
        HandleMovement();
        HandleLookDir();
    }

    private void InitProperties()
    {
        attackCD = 1000;//��쬰�@��
        canAttack = true;
    }

    private void HandleMovement()
    {
        Vector2 moveDirection = new Vector2(inputManager.MoveInput.x, inputManager.MoveInput.y);
        Vector2 playerPos = playerTransform.position;
        Vector2 playerNewPos = playerPos + moveDirection * moveSpeed * Time.deltaTime;
        playerTransform.position = playerNewPos;
        //Debug.Log(moveDirection);
        //rb.MovePosition(playerPos + moveDirection * moveSpeed * Time.deltaTime);
    }

    private void HandleLookDir()
    {
        if (inputManager.LookInput == Vector2.zero) return;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector2(inputManager.LookInput.x, inputManager.LookInput.y));
        Vector2 playerPos = playerTransform.position;
        Vector2 direction = mousePos - playerPos;
        playerTransform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

        //Debug.Log(inputManager.LookInput.x + " " + inputManager.LookInput.y);
        //playerTransform.up = (target - playerPos).normalized;
    }

    private async void CDCount()
    {
        await Task.Delay(attackCD);
        Debug.Log("CD�����A�i�H����");
        canAttack = true;
    }

    /// <summary>
    /// �����B�z
    /// </summary>
    public void HandleAttack()
    {
        if (!canAttack) return;

        CDCount();
        canAttack = false;
        Debug.Log("Attack");
    }
}
