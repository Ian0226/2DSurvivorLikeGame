using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.CustomTool;

public class PlayerEffectHandler
{
    private PlayerController _playerController = null;
    private Transform playerEffectGroup = null;
    private ParticleSystem playerEffectParticle = null;

    private SpriteRenderer playerSprite = null;
    private Color playerTakeDamageColor;
    public PlayerEffectHandler(PlayerController playerController)
    {
        _playerController = playerController;
        Initialize();
    }

    public void Initialize()
    {
        playerEffectGroup = UnityTool.FindGameObject("PlayerParticleEffectGroup").transform;
        playerEffectParticle = UnityTool.FindChildGameObject(playerEffectGroup.gameObject, "PlayerEffect").
            GetComponent<ParticleSystem>();

        playerSprite = _playerController.PlayerTransform.gameObject.GetComponent<SpriteRenderer>();
        playerTakeDamageColor = Color.red;
    }

    public void Update()
    {
        playerEffectGroup.position = _playerController.PlayerPos;
    }

    /// <summary>
    /// ����ɤl��V
    /// </summary>
    /// <param name="direction">���a��L��J</param>
    public void SetParticleVelocity(Vector2 direction)
    {
        var module = playerEffectParticle.velocityOverLifetime;
        module.x = -direction.x;
        module.y = -direction.y;
    }

    /// <summary>
    /// ���a���ˮĪG
    /// </summary>
    public void PlayerTakeDamageEffect()
    {
        playerSprite.color = playerTakeDamageColor;
        CoroutineTool.Instance.DelayExcuteAction(() => { playerSprite.color = Color.white; },0.1f);
    }
}
