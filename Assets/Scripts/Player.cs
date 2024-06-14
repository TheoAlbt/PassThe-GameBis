using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager _InputManager;
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private CinemachineVirtualCamera _VirtualCamera;

    [SerializeField] private CharacterController _CharacterController;
    [SerializeField] private CharacterController _GravityController;

    [SerializeField] private Animator _Animator;
    [SerializeField] private SkinnedMeshRenderer _MeshRenderer;

    [SerializeField] private VisualEffect _PowerAttackVFX;

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
    private bool _Ejected = false;

    [SerializeField] private bool _IsLittle;
    [SerializeField] private bool _IsBig;

    private float _TimerBasic = 0f;
    [SerializeField] private float _TimeOfBasic = 0.5f;
    private float _TimerPower = 0f;
    [SerializeField] private float _TimeOfPower = 0.75f;
    private float _TimerLow = 0f;
    [SerializeField] private float _TimeOfLow = 0.5f;

    private float _TimerAtackBot = 0f;

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
            if (!_IA && !_ShopOpen && !_BasicAttack && !_LowKickAttack && !_PowerAttack && !_Ejected)
            {
                _Direction = new Vector3(_InputManager.GetDirection().x, 0, _InputManager.GetDirection().y);
                if (!_Animator.GetBool("Hurt"))
                {
                    _CharacterController.Move(_Direction * _Speed * Time.deltaTime);
                }
                else
                {
                    _CharacterController.Move(_Direction * _Speed/2 * Time.deltaTime);
                }
                if (_Direction == Vector3.zero)
                {
                    SetAnimationFalse(0);
                }
                else
                {
                    SetAnimationTrue(0);
                }
            }
            else if (_IA && !_BasicAttack && !_LowKickAttack && !_PowerAttack && !_Animator.GetBool("Hurt") && !_Ejected)
            {
                float dist = Vector3.Distance(_ControlledPlayer.transform.position, transform.position);
                _Direction = _ControlledPlayer.transform.position - transform.position;
                if (dist < 1.8f && dist > -1.8f)
                {
                    _Direction =Vector3.zero;

                    if (_TimerAtackBot < 0)
                    {
                        _TimerAtackBot = 1.2f;
                        int Rand = Random.Range(0, 2);
                        if (Rand == 0)
                        {
                            SetAnimationTrue(2);
                        }
                        else if (Rand == 1)
                        {
                            SetAnimationTrue(3);
                        }
                        else if (Rand == 2)
                        {
                            SetAnimationTrue(4);
                        }
                    }

                }
                _CharacterController.Move(_Direction.normalized * (_Speed / 2) * Time.deltaTime);
                if (_Direction == Vector3.zero)
                {
                    SetAnimationFalse(0);
                }
                else
                {
                    SetAnimationTrue(0);
                }
            }

            if (_IA)
            {
                _TimerAtackBot -= Time.deltaTime;
            }
            /*if (_BasicAttack)
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
            }*/

            if (!_BasicAttack && !_PowerAttack && !_LowKickAttack && _CurrentStamina < _MaxStamina)
            {
                _TimerStamina += Time.deltaTime;
                if (_TimerStamina > 0.5f)
                {
                    _CurrentStamina += 1;
                    _TimerStamina = 0f;
                }
            }

            /*if (_TimerBasic > _TimeOfBasic)
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
            }*/

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

    public void SetAnimationTrue(int _Anim)
    {
        if (!_IsDead && !_ShopOpen && !_Ejected)
        {
            if (_Anim == 0)
            {
                _Animator.SetBool("Run", true);
            }
            else if (_Anim == 2)
            {
                _Animator.SetBool("Punch", true);
            }
            else if (_Anim == 3)
            {
                _Animator.SetBool("LowKick", true);
            }
            else if (_Anim == 4)
            {
                _Animator.SetBool("Strong", true);
            }
            else if (_Anim == 5)
            {
                _Animator.Play("Idle", 0);
                _Animator.SetBool("Hurt", true);
                _Animator.Play("hurt", 0);
                _Animator.SetBool("Punch", false);
                _Animator.SetBool("LowKick", false);
                _Animator.SetBool("Strong", false);
                BasicAttackStop();
                LowKickAttackStop();
                PowerAttackStop();  
            }
        }
    }

    public void SetAnimationFalse(int _Anim)
    {
        if (!_IsDead && !_ShopOpen && !_Ejected)
        {
            if (_Anim == 0)
            {
                _Animator.SetBool("Run", false);
            }
            else if (_Anim == 2)
            {
                _Animator.SetBool("Punch", false);
            }
            else if (_Anim == 3)
            {
                _Animator.SetBool("LowKick", false);
            }
            else if (_Anim == 4)
            {
                _Animator.SetBool("Strong", false);
            }
            else if (_Anim == 5)
            {
                _Animator.SetBool("Hurt", false);
            }
        }
    }
    public void BasicAttack()
    {
        _BasicAttack = true;
        _BasicAttackCollider.enabled = true;
        _CurrentStamina -= _BasicStamina;
    }

    public void PowerAttack()
    {
        _PowerAttack = true;
        _PowerAttackCollider.enabled = true;
        _CurrentStamina -= _PowerStamina;
    }

    public void LowKickAttack()
    {
        _LowKickAttack = true;
        _LowKickAttackCollider.enabled = true;
        _CurrentStamina -= _LowKickStamina;
    }

    public void BasicAttackStop()
    {
        _BasicAttack = false;
        _BasicAttackCollider.enabled = false;
    }

    public void PowerAttackStop()
    {
        _PowerAttack = false;
        _PowerAttackCollider.enabled = false;
        _PowerAttackVFX.Play();
    }

    public void LowKickAttackStop()
    {
        _LowKickAttack = false;
        _LowKickAttackCollider.enabled = false;
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
                    if ((_IA && !other.gameObject.GetComponent<Damage>().GetPlayerScript().GetIsIA()) || (!_IA && other.gameObject.GetComponent<Damage>().GetPlayerScript().GetIsIA()))
                    {
                        Debug.Log("Touche");
                        if (_IA)
                        {
                            TakeDamage(other.GetComponent<Damage>().GetDamage());
                        }
                        else
                        {
                            TakeDamage(other.GetComponent<Damage>().GetDamage()/4);
                        }
                        Vector3 _Force = other.gameObject.GetComponent<Damage>().GetPlayerScript().transform.position - transform.position * _EjectForce * Time.deltaTime;
                        other.gameObject.GetComponent<Damage>().PlayerTouchedDamage(this, _Force);
                        _Damager = other.gameObject;
                        if (other.gameObject.GetComponent<Damage>().GetEject())
                        {
                            if (other.transform.position.x < transform.position.x)
                            {
                                StartCoroutine(Ejection(new Vector3(1, 0, 0)));
                            }
                            else
                            {
                                StartCoroutine(Ejection(new Vector3(-1, 0, 0)));
                            }
                        }
                    }
                }
            }
            if (other.gameObject.layer == 7)
            {
                _CanShop = true;
            }
        }
    }

    IEnumerator Ejection(Vector3 _DIr)
    {
        _Ejected = true;
        float i = 0;
        while (i < 0.3f)
        {
            _Direction = new Vector3(_DIr.x, 0, 0);
            _CharacterController.Move(_Direction * _Speed*2.5f * Time.deltaTime);
            i += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        _Ejected = false;
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
        SetAnimationTrue(5);
        _MeshRenderer.materials[0].color = Color.red;

        yield return new WaitForSeconds(0.2f);

        _MeshRenderer.materials[0].color = Color.white;
    }
}
