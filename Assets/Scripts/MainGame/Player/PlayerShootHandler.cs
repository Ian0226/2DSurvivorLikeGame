using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Unity.CustomTool;

/// <summary>
/// �����O�t�d�B�z���a�g��
/// </summary>
public class PlayerShootHandler
{
    private PlayerController _playerController = null;
    /// <summary>
    /// �l�u�����
    /// </summary>
    private ObjectPool<ProjectileBase> projectilePool = null;

    public PlayerShootHandler(PlayerController playerController)
    {
        this._playerController = playerController;
        Initialize();
    }

    public void Initialize()
    {
        //�������l��
        projectilePool = new ObjectPool<ProjectileBase>(//�Ѽ� : 1.�Ыت���ɭn�����Ʊ� 2.��������ɭn������ 3.�^������ɭn������ 4.����R���ɭn������
            () =>
            {
                ProjectileBase projectile = GameObject.Instantiate(_playerController.PlayerCurrentProjectile.gameObject, _playerController.PlayerTransform.position, Quaternion.identity).GetComponent<ProjectileBase>();
                projectile.Intialize();
                projectile.recycle = (p) =>
                {
                    projectilePool.Release(p);
                };
                return projectile;
            },
            (projectile) =>
            {
                projectile.transform.position = _playerController.PlayerTransform.position;
                projectile.gameObject.SetActive(true);
                //projectile.ExistTime = 1f;//�]�w��g���s�b�ɶ�
            },
            (projectile) =>
            {
                projectile.gameObject.SetActive(false);
            },
            (projectile) =>
            {
                GameObject.Destroy(projectile.gameObject);
            }, true, 100, 10000 //�w�]Pool�e�q100�A�̤j�W��10000
        );

        //��l�ƭ���
        AudioManager.Instance.GetAudioSource(AudioManager.GameAudioSource.PlayerShootAudio).clip = 
            (AudioClip)Resources.Load($"AudioClips/Projectiles/Audio_{_playerController.PlayerCurrentProjectile.name}");
    }

    /// <summary>
    /// �M�Ū����
    /// </summary>
    public void ClearObjectPool()
    {
        projectilePool.Clear();
    }

    public void Shoot()
    {
        AudioManager.Instance.PlayerAudioOneShot(AudioManager.GameAudioSource.PlayerShootAudio,
            AudioManager.Instance.GetAudioSource(AudioManager.GameAudioSource.PlayerShootAudio).clip);

        switch (_playerController.PlayerAttackMode)
        {
            case PlayerController.PlayerAttackModeEnum.projectile:
                Vector2 direction = _playerController.MousePos - _playerController.PlayerPos;
                for (int i = 0; i < _playerController.ProjectilesCount; i++)
                {
                    ProjectileBase projectile = projectilePool.Get();
                    projectile.InitProperties();
                    projectile.InitProjectile(direction);
                }
                break;
            case PlayerController.PlayerAttackModeEnum.ray:
                break;
            case PlayerController.PlayerAttackModeEnum.other:
                break;
        }
    }
}
