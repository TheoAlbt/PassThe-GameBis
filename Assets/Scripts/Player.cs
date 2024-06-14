using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager _InputManager;
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private CinemachineVirtualCamera _VirtualCamera;

    [SerializeField] private CharacterController _CharacterController;
    [SerializeField] private CharacterController _GravityController;

    [SerializeField] private List<GameObject> _RotateObject;

    [SerializeField] private bool _IA;

    [SerializeField] private float _Speed;
    private Vector3 _Direction = Vector3.zero;

    [SerializeField] private Vector3 _GroundForce = Vector3.zero;
    [SerializeField] private float _Gravity;

    [SerializeField] private float _CurrentHp;
    [SerializeField] private float _MaxHp;

    [SerializeField] private float _CurrentStamina;
    [SerializeField] private float _MaxStamina;
    private float _TimerStamina = 0f;

    [SerializeField] private float _BasicStamina;
    [SerializeField] private float _PowerStamina;
    [SerializeField] private float _LowKickStamina;

    private bool _BasicAttack = false;
    private bool _PowerAttack = false;
    private bool _LowKickAttack = false;

    [SerializeField] private bool _IsLittle;
    [SerializeField] private bool _IsBig;

    private float _TimerBasic = 0f;
    [SerializeField] private float _TimeOfBasic = 0.5f;
    private float _TimerPower = 0f;
    [SerializeField] private float _TimeOfPower = 0.75f;
    private float _TimerLow = 0f;
    [SerializeField] private float _TimeOfLow = 0.5f;

    [SerializeField] private Collider _BasicAttackCollider;
    [SerializeField] private Collider _PowerAttackCollider;
    [SerializeField] private Collider _LowKickAttackCollider;

    [SerializeField] private bool _IsDead = false;

    private bool _CanShop = false;
    private bool _ShopOpen = false;

    private bool _FirstAmelioration = false;
    private bool _SecondAmelioration = false;
    private bool _ThirdAmelioration = false;
    private bool _FourthAmelioration = false;
    private bool _FivethAmelioration = false;
    private bool _SixthAmelioration = false;

    private GameObject _Damager;
    private float _TimerHeal = 0f;
    private float _TimeOffFight = 0f;

    [SerializeField] private int _NbVictims = 0;
    [SerializeField] private float _EjectForce = 3f;

    private float _TimerShop = 0f;

    private Player _ControlledPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _CurrentHp = _MaxHp;
        _CurrentStamina = _MaxStamina;

        _BasicAttackCollider.enabled = false;
        _PowerAttackCollider.enabled = false;
        _LowKickAttackCollider.enabled = false;

        if (!GameObject.Find("Player").GetComponent<Player>().GetIsIA())
        {
            _ControlledPlayer = GameObject.Find("Player").GetComponent<Player>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_IsDead)
        {
            if (!_IA && !_ShopOpen)
            {
                _Direction = new Vector3(_InputManager.GetDirection().x, 0, _InputManager.GetDirection().y);
                _CharacterController.Move(_Direction * _Speed * Time.deltaTime);
            }
            else if (_IA)
            {
                _Direction = _ControlledPlayer.transform.position - transform.position;
                _CharacterController.Move(_Direction * (_Speed / 2) * Time.deltaTime);
            }

            if (_BasicAttack)
            {
                _TimerBasic += Time.deltaTime;
            }
            if (_PowerAttack)
            {
                _TimerPower += Time.deltaTime;
            }
            if (_LowKickAttack) 
            {
                _TimerLow += Time.deltaTime;
            }

            if (!_BasicAttack && !_PowerAttack && !_LowKickAttack && _CurrentStamina < _MaxStamina)
            {
                _TimerStamina += Time.deltaTime;
                if (_TimerStamina > 0.5f)
                {
                    _CurrentStamina += 1;
                    _TimerStamina = 0f;
                }
            }

            if (_TimerBasic > _TimeOfBasic)
            {
                _BasicAttackCollider.enabled = false;
                _BasicAttack = false;
                _TimerBasic = 0f;
            }
            if (_TimerPower > _TimeOfPower) 
            {
                _PowerAttackCollider.enabled = false;
                _PowerAttack = false;
                _TimerPower = 0f;
            }
            if (_TimerLow > _TimeOfLow)
            {
                _LowKickAttackCollider.enabled = false;
                _LowKickAttack = false;
                _TimerLow = 0f;
            }

            if (_CurrentHp <= 0)
            {
                _IsDead = true;
            }

            if (_CurrentHp > _MaxHp)
            {
                _CurrentHp = _MaxHp;
            }

            if (_CurrentStamina > _MaxStamina)
            {
                _CurrentStamina = _MaxStamina;
            }

            if (_Damager == null)
            {
                _TimerHeal += Time.deltaTime;
                if (_CurrentHp < _MaxHp)
                {
                    Heal(2);
                }
            }
            else
            {
                _TimeOffFight += Time.deltaTime;
                if (_TimeOffFight > 3)
                {
                    _TimeOffFight = 0f;
                }
            }

            int _Modulo = _NbVictims % 20;
            if (_NbVictims != 0 && _Modulo == 0)
            {
                _MaxHp += 10;
                _NbVictims = 0;
            }

            Rotation(_RotateObject, _Direction);

            if (!_CanShop)
            {
                _TimerShop += Time.deltaTime;
                if (_TimerShop > 30)
                {
                    _CanShop = true;
                }
            }
        }
        else
        {
            _BasicAttack = false;
            _PowerAttack = false;
            _LowKickAttack = false;

            _CanShop = false;

            if (_IA)
            {
                Death();
            }
            else
            {
                //Respawn();
            }
        }

        if (!_CharacterController.isGrounded && _GroundForce.y > -1)
        {
            _GroundForce.y -= _Gravity * Time.deltaTime;
        }
        _GravityController.Move(_GroundForce);
    }

    public void BasicAttack()
    {
        if (!_IsDead && !_ShopOpen)
        {
            if (!_BasicAttack && !_PowerAttack && !_LowKickAttack)
            {
                Debug.Log("Basic attack");
                _BasicAttack = true;
                _BasicAttackCollider.enabled = true;
                _CurrentStamina -= _BasicStamina;
            }
        }
    }

    public void PowerAttack()
    {
        if (!_IsDead && !_ShopOpen)
        {
            if (!_BasicAttack && !_PowerAttack && !_LowKickAttack)
            {
                _PowerAttack = true;
                _PowerAttackCollider.enabled = true;
                _CurrentStamina -= _PowerStamina;
            }
        }
    }

    public void LowKickAttack()
    {
        if (!_IsDead && !_ShopOpen)
        {
            if (!_BasicAttack && !_PowerAttack && !_LowKickAttack)
            {
                _LowKickAttack = true;
                _LowKickAttackCollider.enabled = true;
                _CurrentStamina -= _LowKickStamina;
            }
        }
    }

    public void Respawn(Vector3 Position)
    {

    }

    public void Death()
    {
        Destroy(this.gameObject);
    }

    public void TakeDamage(float value)
    {
        if (!_IsDead)
        {
            if (_CurrentHp > 0)
            {
                _CurrentHp -= value;
                _TimeOffFight = 0f;
                StartCoroutine(TakeDamageFeedback());
            }
        }
    }

    public void Heal(float value)
    {
        if (!_IsDead)
        {
            if (_CurrentHp < _MaxHp)
            {
                if (_TimerHeal > 1)
                {
                    _CurrentHp += value;
                    _TimerHeal = 0f;
                }
            }
        }
    }

    public void Rotation(List<GameObject> RotateObjects, Vector3 Direction)
    {
        foreach (var ObjectToRotate in RotateObjects)
        {
            if (Direction.x > 0)
            {
                ObjectToRotate.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (Direction.x < 0)
            {
                ObjectToRotate.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8 && collision.gameObject != this.gameObject)
        {
            if (collision.gameObject.GetComponent<Damage>())
            {
                
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!_IsDead)
        {
            if (other.gameObject.layer == 8 && other.gameObject != this.gameObject)
            {
                if (other.gameObject.GetComponent<Damage>() != null && other.gameObject.GetComponent<Damage>().GetPlayerScript() != this)
                {
                    Debug.Log("Touche");
                    TakeDamage(other.GetComponent<Damage>().GetDamage());
                    Vector3 _Force = other.gameObject.GetComponent<Damage>().GetPlayerScript().transform.position - transform.position * _EjectForce * Time.deltaTime;
                    other.gameObject.GetComponent<Damage>().PlayerTouchedDamage(this, _Force);
                    _Damager = other.gameObject;
                }
            }
            if (other.gameObject.layer == 7)
            {
                _CanShop = true;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            _CanShop = false;
        }
    }

    #region Get and Set

    public void SetCurrentHP(float value)
    {
        _CurrentHp += value;
    }

    public void SetMaxHP(float value)
    {
        _MaxHp += value;
    }

    public void SetCurrentStamina(float value)
    {
        _CurrentStamina += value;
    }

    public void SetMaxStamina(float value)
    {
        _MaxStamina += value;
    }

    public void SetShopOpen(bool value)
    {
        _ShopOpen = value;
    }

    public void SetIsLittle(bool state)
    {
        _IsLittle = state;
    }

    public void SetIsBig(bool state)
    {
        _IsBig = state;
    }

    public void SetTimerTransformation(float value)
    {
        _GameManager.SetTimerTransformation(value);
    }

    public void SetTimeToTransform(float value)
    {
        _GameManager.SetTimeToTransform(value);
    }

    public void SetDamageBasic(float value)
    {
        _BasicAttackCollider.gameObject.GetComponent<Damage>().SetDamage(value);
    }

    public void SetDamagePower(float value)
    {
        _PowerAttackCollider.gameObject.GetComponent<Damage>().SetDamage(value);
    }

    public void SetDamageLowKick(float value)
    {
        _LowKickAttackCollider.gameObject.GetComponent<Damage>().SetDamage(value);
    }
    
    public void SetFirstAmeliorationActive(bool state)
    {
        _FirstAmelioration = state;
        _CanShop = false;
    }

    public void SetSecondAmeliorationActive(bool state)
    {
        _SecondAmelioration = state;
        _CanShop = false;
    }

    public void SetThirdAmeliorationActive(bool state)
    {
        _ThirdAmelioration = state;
        _CanShop = false;
    }

    public void SetFourthAmeliorationActive(bool state)
    {
        _FourthAmelioration = state;
        _CanShop = false;
    }

    public void SetFivethAmeliorationActive(bool state)
    {
        _FivethAmelioration = state;
        _CanShop = false;
    }

    public void SetSixthAmeliorationActive(bool state)
    {
        _SixthAmelioration = state;
        _CanShop = false;
    }

    public void SetNbVictim(int value)
    {
        _NbVictims += value;
    }

    public void SetIsIA(bool state)
    {
        _IA = state;
    }

    public float GetCurrentHP()
    {
        return _CurrentHp;
    }

    public float GetMaxHP()
    {
        return _MaxHp;
    }

    public bool GetIsDead()
    {
        return _IsDead;
    }

    public float GetCurrentStamina()
    {
        return _CurrentStamina;
    }

    public float GetMaxStamina()
    {
        return _MaxStamina;
    }

    public bool GetCanShop()
    {
        return _CanShop;
    }

    public bool GetIsIA()
    {
        return _IA;
    }

    public CharacterController GetCharacterController()
    {
        return _CharacterController;
    }

    public bool GetIsLittle()
    {
        return _IsLittle;
    }

    public bool GetIsBig()
    {
        return _IsBig;
    }

    public bool GetFirstAmeliorationActive()
    {
        return _FirstAmelioration;
    }

    public bool GetSecondAmeliorationActive()
    {
        return _SecondAmelioration;
    }

    public bool GetThirdAmeliorationActive()
    {
        return _ThirdAmelioration;
    }

    public bool GetFourthAmeliorationActive()
    {
        return _FourthAmelioration;
    }

    public bool GetFivethAmeliorationActive()
    {
        return _FivethAmelioration;
    }

    public bool GetSixthAmeliorationActive()
    {
        return _SixthAmelioration;
    }

    #endregion

    IEnumerator TakeDamageFeedback()
    {
        foreach (var material in this.gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<MeshRenderer>().materials)
        {
            material.SetColor(name ,Color.red);
        }

        yield return new WaitForSeconds(0.2f);
    }
}
