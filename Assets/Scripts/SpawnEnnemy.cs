using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemy : MonoBehaviour
{
    [SerializeField] private GameObject _PlayerPrefab;
    [SerializeField] private Transform _SpawnZone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject EnnemySpawn(Vector3 Position)
    {
        GameObject Ennemy = Instantiate(_PlayerPrefab, _SpawnZone.position, Quaternion.identity);
        return Ennemy;
    }

    public Transform GetSpawnZone()
    {
        return _SpawnZone;
    }
}
