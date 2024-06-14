using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAmelioration : MonoBehaviour
{
    [SerializeField] private Player _Player;
    [SerializeField] private int _ID;
    [SerializeField] private UIManager _UIManager;

    private bool _FirstAmelioration = false;
    private bool _SecondAmelioration = false;
    private bool _ThirdAmelioration = false;
    private bool _FourthAmelioration = false;
    private bool _FivethAmelioration = false;
    private bool _SixthAmelioration = false;

    [SerializeField] private float _PriceFirst;
    [SerializeField] private float _PriceSecond;
    [SerializeField] private float _PriceThird;
    [SerializeField] private float _PriceFourth;
    [SerializeField] private float _PriceFiveth;
    [SerializeField] private float _PriceSixth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerFirstAmelioration(float value)
    {
        _FirstAmelioration = true;
        _Player.SetFirstAmeliorationActive(_FirstAmelioration);
        _Player.SetMaxStamina(value);
        _Player.SetMaxHP(_PriceFirst);
        _UIManager.DesactiveButton(_ID);
    }

    public void PlayerSecondAmelioration(float value)
    {
        _SecondAmelioration = true;
        _Player.SetSecondAmeliorationActive(_SecondAmelioration);
        _Player.SetTimerTransformation(value);
        _Player.SetMaxHP(_PriceSecond);
        _UIManager.DesactiveButton(_ID);
    }

    public void PlayerThirdAmelioration(float value) 
    {
        _ThirdAmelioration = true;
        _Player.SetThirdAmeliorationActive(_ThirdAmelioration);
        _Player.SetTimeToTransform(value);
        _Player.SetMaxHP(_PriceThird);
        _UIManager.DesactiveButton(_ID);
    }

    public void PlayerFourthAmelioration(float value)
    {
        _FourthAmelioration = true;
        _Player.SetFourthAmeliorationActive(_FourthAmelioration);
        _Player.SetDamageBasic(value);
        _Player.SetMaxHP(_PriceFourth);
        _UIManager.DesactiveButton(_ID);
    }

    public void PlayerFivethAmelioration(float value)
    {
        _FivethAmelioration = true;
        _Player.SetFivethAmeliorationActive(_FivethAmelioration);
        _Player.SetDamagePower(value);
        _Player.SetMaxHP(_PriceFiveth);
        _UIManager.DesactiveButton(_ID);
    }

    public void PlayerSixthAmelioration(float value) 
    {
        _SixthAmelioration = true;
        _Player.SetSixthAmeliorationActive(_SixthAmelioration);
        _Player.SetDamageLowKick(value);
        _Player.SetMaxHP(_PriceSixth);
        _UIManager.DesactiveButton(_ID);
    }

    #region Get and Set

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
}
