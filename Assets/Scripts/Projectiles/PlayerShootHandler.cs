using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// 該類別負責處理玩家射擊
/// </summary>
public class PlayerShootHandler
{
    private PlayerController _playerController = null;
    /// <summary>
    /// 物件池
    /// </summary>
    private ObjectPool<ProjectileBase> projectilePool = null;

    public PlayerShootHandler(PlayerController playerController)
    {
        this._playerController = playerController;
        Initialize();
    }

    public void Initialize()
    {
        //物件池初始化
        projectilePool = new ObjectPool<ProjectileBase>(//參數 : 1.創建物件時要做的事情 2.索取物件時要做的事 3.回收物件時要做的事 4.物件刪除時要做的事
            () =>
            {
                ProjectileBase projectile = GameObject.Instantiate(_playerController.PlayerCurrentProjectile, _playerController.PlayerPos, Quaternion.identity).GetComponent<ProjectileBase>();
                //Debug.Log(projectile);
                projectile.recycle = (p) =>
                {
                    projectilePool.Release(p);
                };
                return projectile;
            },
            (projectile) =>
            {
                projectile.gameObject.SetActive(true);
                projectile.transform.position = _playerController.PlayerPos;
                //projectile.ExistTime = 1f;//設定投射物存在時間
                projectile.Intialize();
            },
            (projectile) =>
            {
                projectile.gameObject.SetActive(false);
            },
            (projectile) =>
            {
                GameObject.Destroy(projectile.gameObject);
            }, true, 100, 10000 //預設Pool容量100，最大上限10000
            );
    }

    public void Shoot()
    {
        switch (_playerController.PlayerAttackMode)
        {
            case PlayerController.PlayerAttackModeEnum.projectile:
                Vector2 direction = _playerController.MousePos - _playerController.PlayerPos;
                for (int i = 0; i < _playerController.ProjectilesCount; i++)
                {
                    ProjectileBase projectile = projectilePool.Get();
                    projectile.SetMoveDir(direction);
                }
                break;
            case PlayerController.PlayerAttackModeEnum.ray:
                break;
            case PlayerController.PlayerAttackModeEnum.other:
                break;
        }
    }
}
