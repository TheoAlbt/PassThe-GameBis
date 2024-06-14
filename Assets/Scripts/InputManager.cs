using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;

    private Vector2 _Direction = Vector2.zero;

    private bool _BasicAttack = false;
    private bool _PowerAttack = false;
    private bool _LowKick = false;
    private bool _IsLittle = false;
    private bool _IsBig = false;

    private bool _OpenShop = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        _Direction = context.ReadValue<Vector2>();
    }

    public void BasicAttack(InputAction.CallbackContext context)
    {
        _BasicAttack = context.ReadValueAsButton();
        _GameManager.BasicAttack();
    }

    public void PowerAttack(InputAction.CallbackContext context) 
    {
        _PowerAttack = context.ReadValueAsButton();
        _GameManager.PowerAttack();
    }

    public void LowKickAttack(InputAction.CallbackContext context) 
    {
        _LowKick = context.ReadValueAsButton();
        _GameManager.LowKickAttack();
    }

    public void TransformToLittle(InputAction.CallbackContext context) 
    {
        _IsLittle = context.ReadValueAsButton();
        _GameManager.TransformToLittle();
    }

    public void TransformToBig(InputAction.CallbackContext context)
    {
        _IsBig = context.ReadValueAsButton();
        _GameManager.TransformToBig();
    }

    public void OpenShop(InputAction.CallbackContext context)
    {
        _OpenShop = context.ReadValueAsButton();
        _GameManager.OpenShop();
    }

    #region Get and Set
    public Vector2 GetDirection()
    {
        return _Direction;
    }
    #endregion
}
