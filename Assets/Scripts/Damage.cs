using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private float _BaseDamage;
    [SerializeField] private float _Damage;
    [SerializeField] private bool _Ejecte;

    [SerializeField] private Player _PlayerScript;
    [SerializeField] private Player _PlayerTouched;

    [SerializeField] private float _TimerPlayerTouched = 0f;
    [SerializeField] private int _ComboCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _BaseDamage = _Damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (_PlayerTouched != null)
        {
            _TimerPlayerTouched += Time.deltaTime;

            if (_PlayerTouched.GetIsDead())
            {
                _PlayerScript.SetNbVictim(1);
                _PlayerTouched = null;
                _ComboCount = 0;
            }

            if (_TimerPlayerTouched > 3)
            {
                _PlayerTouched = null;
                _ComboCount = 0;
                _TimerPlayerTouched = 0f;
                _Damage = _BaseDamage;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            if (other.gameObject.GetComponent<Player>() != null)
            {
                if (!other.gameObject.GetComponent<Player>().GetIsDead())
                {
                    _PlayerTouched = other.gameObject.GetComponent<Player>();
                    _TimerPlayerTouched = 0f;
                    _ComboCount = _ComboCount + 1;
                    _Damage = _BaseDamage * (1+(_ComboCount / 6));
                    _GameManager.SetCameraShake(2);
                }
            }
        }
    }

    public void PlayerTouchedDamage(Player _PlayerTouched, Vector3 Force)
    {
        //_PlayerTouched.GetCharacterController().Move(new Vector3(Force.x, 0, 0));
        _PlayerTouched.gameObject.GetComponent<Rigidbody>().AddForce(Force);
    }

    #region Get and Set

    public void SetDamage(float value)
    {
        _Damage += value;
    }
    public void Setplayer(Player value)
    {
        _PlayerScript = value;
    }
    public void ComboDamage(int value)
    {
        _Damage *= 1 + 0.5f * value;
    }

    public void SetComboCount(int value)
    {
        _ComboCount += value;
    }

    public float GetDamage()
    {
        return _Damage;
    }

    public Player GetPlayerScript()
    {
        return _PlayerScript;
    }

    public int GetComboCount()
    {
        return _ComboCount;
    }

    public bool GetEject()
    {
        return _Ejecte;
    }

    #endregion
}
