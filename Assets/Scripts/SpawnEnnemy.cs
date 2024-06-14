using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnnemy : MonoBehaviour
{
    [SerializeField] private GameObject _PlayerPrefab;
    [SerializeField] private Transform[] _SpawnZone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject EnnemySpawn()
    {
        GameObject Ennemy = Instantiate(_PlayerPrefab, _SpawnZone[Random.Range(0, _SpawnZone.Count())].position, Quaternion.identity);
        return Ennemy;
    }

    public Transform[] GetSpawnZone()
    {
        return _SpawnZone;
    }
}
