using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] private Canvas _CanvasEnnemyHP;
    [SerializeField] private Slider _SliderHP;
    [SerializeField] private Player _Player;

    // Start is called before the first frame update
    void Start()
    {
        //_Player = this.gameObject.transform.parent.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_Player.GetIsIA())
        {
            if (!_Player.GetIsDead()) 
            {
                _CanvasEnnemyHP.gameObject.SetActive(true);
            }
            else
            {
                _CanvasEnnemyHP.gameObject.SetActive(false);
            }
        }
        else
        {
            _CanvasEnnemyHP.gameObject.SetActive(false);
        }

        _SliderHP.maxValue = _Player.GetMaxHP();
        _SliderHP.value = _Player.GetCurrentHP();
    }
}
