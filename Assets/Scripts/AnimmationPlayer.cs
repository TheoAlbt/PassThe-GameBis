using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AnimmationPlayer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject[] _SPeK2;
    [SerializeField] private GameObject[] _SPeK3;

    [SerializeField] private GameObject _Tornado;
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

    public void SpeAttackK2()
    {
        StartCoroutine(SpeAttack2K());
        _player.PowerAttackSpe();
    }

    IEnumerator SpeAttack3K()
    {
        _SPeK3[0].SetActive(true);
        _SPeK3[1].SetActive(true);
        yield return new WaitForSeconds(0.4f);

        _SPeK3[2].SetActive(true);
        _player.PowerAttackSpeStop();
        yield return new WaitForSeconds(0.6f);
        _SPeK3[0].SetActive(false);
        _SPeK3[1].SetActive(false);
        _SPeK3[2].SetActive(false);
    }

    public void SpeAttackK3()
    {
        StartCoroutine(SpeAttack3K());
        _player.PowerAttackSpe();
    }

    IEnumerator SpeAttack2K()
    {
        _SPeK2[0].SetActive(true);
        yield return new WaitForSeconds(0.4f);
        _SPeK2[1].SetActive(true);
        yield return new WaitForSeconds(0.4f);
        _player.PowerAttackSpeStop();

        _SPeK2[2].SetActive(true);
        yield return new WaitForSeconds(0.4f);
        _SPeK2[0].SetActive(false);
        _SPeK2[1].SetActive(false);
        _SPeK2[2].SetActive(false);

    }

    public void SpawnTornado()
    {
        GameObject Spawn = Instantiate(_Tornado, new Vector3(_player.transform.position.x,0, _player.transform.position.z), Quaternion.identity);
        Spawn.GetComponent<Damage>().Setplayer(_player);

        if (_player.transform.GetChild(1).rotation.y == 0)
        {
            Spawn.GetComponent<Tornado>().SetMouve(4);
        }
        else
        {
            Spawn.GetComponent<Tornado>().SetMouve(-4);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
