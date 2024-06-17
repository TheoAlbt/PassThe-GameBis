using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Tornado : MonoBehaviour
{
    [SerializeField] private CharacterController _CharacterController;
    [SerializeField] private BoxCollider _Box;

    private float _Mouve = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void SetMouve(float mouve)
    {
        _Mouve = mouve;
        _Box.enabled = true;
        StartCoroutine(Despawn());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x,0,transform.position.z);
        _CharacterController.Move(new Vector3(_Mouve, 0, 0)* Time.deltaTime);
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
