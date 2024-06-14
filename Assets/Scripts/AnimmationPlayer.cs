using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AnimmationPlayer : MonoBehaviour
{
    [SerializeField] private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetAnimationFalse(int _Anim)
    {
        _player.SetAnimationFalse(_Anim);
    }

    public void SetAttack(int _Anim)
    {
        if (_Anim == 2)
        {
            _player.BasicAttack();
        }
        else if (_Anim == 3)
        {
            _player.LowKickAttack();
        }
        else if (_Anim == 4)
        {
            _player.PowerAttack();
        }
    }

    public void StopAttack(int _Anim)
    {
        if (_Anim == 2)
        {
            _player.BasicAttackStop();
        }
        else if (_Anim == 3)
        {
            _player.LowKickAttackStop();
        }
        else if (_Anim == 4)
        {
            _player.PowerAttackStop();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
