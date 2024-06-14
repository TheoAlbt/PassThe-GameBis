using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Player _Player;

    [SerializeField] private Slider _SliderHP;
    [SerializeField] private Slider _SliderStamina;
    [SerializeField] private Text _TextCurrentHP;
    [SerializeField] private Text _TextMaxHP;
    [SerializeField] private Text _TextCurrentStamina;
    [SerializeField] private Text _TextMaxStamina;

    [SerializeField] private Image _ComboImage;

    [SerializeField] private GameObject _ShopPress;
    [SerializeField] private GameObject _ShopUI;
    [SerializeField] private Button[] _BuyButtons;
    private bool _ShopOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        _ShopUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _SliderHP.maxValue = _Player.GetMaxHP();
        _SliderHP.value = _Player.GetCurrentHP();

        _SliderStamina.maxValue = _Player.GetMaxStamina();
        _SliderStamina.value = _Player.GetCurrentStamina();

        if (_Player.GetCanShop())
        {
            _ShopPress.SetActive(true);
        }
        else
        {
            _ShopPress.SetActive(false);
            if (_ShopUI.activeSelf)
            {
                CloseShop();
            }
        }

        ChangeTextMaxHP(_Player.GetMaxHP());
        ChangeTextCurrentHP(_Player.GetCurrentHP());

        ChangeTextCurrentStamina(_Player.GetCurrentStamina());
        ChangeTextMaxStamina(_Player.GetMaxStamina());
    }

    public void OpenShop()
    {
        _ShopUI.SetActive(true);
        _Player.SetShopOpen(true);
        _ShopOpen = true;
    }

    public void CloseShop()
    {
        _ShopUI.SetActive(false);
        _Player.SetShopOpen(false);
        _ShopOpen = false;
    }

    public void ChangeTextCurrentHP(float value)
    {
        _TextCurrentHP.text = value.ToString();
    }

    public void ChangeTextMaxHP(float value)
    {
        _TextMaxHP.text = value.ToString();
    }

    public void ChangeTextCurrentStamina(float value)
    {
        int result = Mathf.FloorToInt(value);
        _TextCurrentStamina.text = result.ToString();
    }

    public void ChangeTextMaxStamina(float value)
    {
        int result = Mathf.FloorToInt(value);
        _TextMaxStamina.text = result.ToString();
    }

    public void DesactiveButton(int i)
    {
        _BuyButtons[i].interactable = false;
    }



    #region

    public bool GetShopOpen()
    {
        return _ShopOpen;
    }

    #endregion
}
