using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _Player;
    [SerializeField] private UIManager _UIManager;

    [SerializeField] private SpawnEnnemy _SpawnEnnemy;

    [SerializeField] private CinemachineVirtualCamera _VirtualCamera;

    [SerializeField] private float _TransformTimer = 0;
    [SerializeField] private float _TimeToTransform = 10;

    [SerializeField] private float _TimeTransformed = 0f;

    private float _TimerSpawn = 0f;

    // Start is called before the first frame update
    void Start()
    {
        SetCameraShake(0);
    }

    // Update is called once per frame
    void Update()
    {
        _TimerSpawn += Time.deltaTime;

        if (_Player.GetIsLittle() || _Player.GetIsBig()) 
        {
            _TimeTransformed += Time.deltaTime;
            if (_TimeTransformed > 3)
            {
                BackToNormal();
                _TimeTransformed = 0f;
                _TransformTimer = 0f;
            }
        }

        if (!_Player.GetIsLittle() && !_Player.GetIsBig()) 
        {
            _TransformTimer += Time.deltaTime;
        }

        Transform[] _SpawnZone = _SpawnEnnemy.GetSpawnZone();

        if (_TimerSpawn > 5)
        {
            GameObject Ennemy = _SpawnEnnemy.EnnemySpawn();
            Ennemy.GetComponent<Player>().SetIsIA(true);
            _TimerSpawn = 0f;
        }
    }

    public void BasicAttack()
    {
        //_Player.BasicAttack();
        _Player.SetAnimationTrue(2);
    }

    public void PowerAttack()
    {
        //_Player.PowerAttack();
        _Player.SetAnimationTrue(4);
    }

    public void LowKickAttack()
    {
        //_Player.LowKickAttack();
        _Player.SetAnimationTrue(3);
    }

    public void TransformToLittle()
    {
        if (_TransformTimer > _TimeToTransform)
        {
            _Player.SetIsLittle(true);
            StartCoroutine(TransformToLittleCoroutine());
        }
    }

    public void TransformToBig()
    {
        if (_TransformTimer > _TimeToTransform)
        {
            _Player.SetIsBig(true);
            StartCoroutine(TransformToBigCoroutine());
        }
    }

    public void BackToNormal()
    {
        _Player.SetIsBig(false);
        _Player.SetIsLittle(false);
        StartCoroutine(BackToNormalCoroutine());
    }

    public void OpenShop()
    {
        _UIManager.OpenShop();
    }

    public void Respawn(Vector3 SpawnPosition)
    {
        _Player.Respawn(SpawnPosition);
    }

    public void SetCameraShake(float value)
    {
        StartCoroutine(CameraShakeCoroutine(value));
    }

    public IEnumerator TransformToLittleCoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        if (_Player.gameObject.transform.localScale.x > 0.3f)
        {
            _Player.gameObject.transform.localScale -= new Vector3(0.3f, 0.3f, 0.3f);

            _Player.SetCurrentHP(-5);
            _Player.SetMaxHP(-5);
            _UIManager.ChangeTextCurrentHP(_Player.GetCurrentHP());
            _UIManager.ChangeTextMaxHP(_Player.GetMaxHP());

            _Player.SetCurrentStamina(5);
            _Player.SetMaxStamina(5);
            _UIManager.ChangeTextCurrentStamina(_Player.GetCurrentStamina());
            _UIManager.ChangeTextMaxStamina(_Player.GetMaxStamina());
        }

        yield return new WaitForSeconds(0.2f);
    }

    public IEnumerator TransformToBigCoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        if (_Player.gameObject.transform.localScale.x < 1.7f)
        {
            _Player.gameObject.transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);

            _Player.SetCurrentHP(5);
            _Player.SetMaxHP(5);
            _UIManager.ChangeTextCurrentHP(_Player.GetCurrentHP());
            _UIManager.ChangeTextMaxHP(_Player.GetMaxHP());

            _Player.SetCurrentStamina(-5);
            _Player.SetMaxStamina(-5);
            _UIManager.ChangeTextCurrentStamina(_Player.GetCurrentStamina());
            _UIManager.ChangeTextMaxStamina(_Player.GetMaxStamina());
        }

        yield return new WaitForSeconds(0.2f);
    }

    public IEnumerator BackToNormalCoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        if (_Player.gameObject.transform.localScale.x != 1)
        {
            if (_Player.gameObject.transform.localScale.x > 1)
            {
                _Player.gameObject.transform.localScale -= new Vector3(0.3f, 0.3f, 0.3f);

                _Player.SetCurrentHP(-5);
                _Player.SetMaxHP(-5);
                _UIManager.ChangeTextCurrentHP(_Player.GetCurrentHP());
                _UIManager.ChangeTextMaxHP(_Player.GetMaxHP());

                _Player.SetCurrentStamina(5);
                _Player.SetMaxStamina(5);
                _UIManager.ChangeTextCurrentStamina(_Player.GetCurrentStamina());
                _UIManager.ChangeTextMaxStamina(_Player.GetMaxStamina());
            }
            else if (_Player.gameObject.transform.localScale.x < 1) 
            {
                _Player.gameObject.transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);

                _Player.SetCurrentHP(5);
                _Player.SetMaxHP(5);
                _UIManager.ChangeTextCurrentHP(_Player.GetCurrentHP());
                _UIManager.ChangeTextMaxHP(_Player.GetMaxHP());

                _Player.SetCurrentStamina(-5);
                _Player.SetMaxStamina(-5);
                _UIManager.ChangeTextCurrentStamina(_Player.GetCurrentStamina());
                _UIManager.ChangeTextMaxStamina(_Player.GetMaxStamina());
            }
        }

        yield return new WaitForSeconds(0.2f);
    }

    #region Get and Set

    public void SetTimerTransformation(float value)
    {
        _TimeTransformed += value; 
    }

    public void SetTimeToTransform(float value)
    {
        _TimeToTransform += value;
    }

    public Player GetPlayer()
    {
        return _Player;
    }

    #endregion

    IEnumerator CameraShakeCoroutine(float value)
    {
        _VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = value;

        yield return new WaitForSeconds(0.25f);

        _VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
}
