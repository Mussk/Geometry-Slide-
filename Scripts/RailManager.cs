using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using Random = UnityEngine.Random;

public class RailManager : MonoBehaviour
{
    [SerializeField]
    private List<Rail> rails;
    
    [SerializeField] private int railsCount;

    [SerializeField] private List<int> railsXCoord;
    
    private Queue<GameObject> _railsObjectPool;
    [SerializeField] private List<GameObject> intialRailsOnScene;
    [SerializeField] private int maxRailsPoolSize = 21;
    
    private float _tileLength = 72f;
    private float _spawnZ = 32f;
    public float SpawnYOffset { get => _spawnYOffset; set => _spawnYOffset = value; }
    private float _spawnYOffset = 0;
    public float CurrentXCoord { get => _currentXCoord; set => _currentXCoord = value; }
    private float _currentXCoord = 0;

    private void Awake()
    {
        PopulateRailsPool();
        
    }

    private void PopulateRailsPool()
    {
        _railsObjectPool = new Queue<GameObject>();
        
        while (_railsObjectPool.Count <= maxRailsPoolSize - intialRailsOnScene.Count)
        {
            var spawnedRail = Instantiate(PickRandomWeightedRail(), new Vector3(0, 0, _spawnZ), Quaternion.identity);
            _railsObjectPool.Enqueue(spawnedRail);
            spawnedRail.SetActive(false);
        }
        
        foreach (var rail in intialRailsOnScene)
        {
            _railsObjectPool.Enqueue(rail);
        }
    }

    private void Start()
    {   
        _spawnZ += _tileLength;
        
    }

    private void OnEnable()
    {
        RailController.OnRailDestroyed += OnRailDestroyed_ResetNewRail;
    }

    private void OnDisable()
    {
        RailController.OnRailDestroyed -= OnRailDestroyed_ResetNewRail;
    }
    
    private void OnRailDestroyed_ResetNewRail(float xCoord, GameObject inactiveRail)
    {
        
        _railsObjectPool.Enqueue(inactiveRail);
        inactiveRail.SetActive(false);
        var spawnedRail = _railsObjectPool.Dequeue();
        float localSpawnYOffset = (xCoord == 0) ? 0 : _spawnYOffset;

        spawnedRail.transform.position = new Vector3(xCoord, localSpawnYOffset, _spawnZ + _tileLength / 2);
        spawnedRail.transform.rotation = Quaternion.identity;
        spawnedRail.SetActive(true);
        
        SpawnContentOnRail(spawnedRail);
    }
    
    private GameObject PickRandomWeightedRail()
    {
        float totalWeight = 0f;

        
        foreach (var obj in rails)
            totalWeight += obj.spawnWeight;

       
        float randomValue = Random.Range(0, totalWeight);

       
        foreach (var obj in rails)
        {
            if (randomValue < obj.spawnWeight)
                return obj.railObject;

            randomValue -= obj.spawnWeight;
        }

        return null; 
    }

    private void SpawnContentOnRail(GameObject spawnedRail)
    {

        var railControllerPatterns = spawnedRail.GetComponent<RailController>().railContentPatterns;

        for (int i = 0; i < railControllerPatterns.transform.childCount; i++)
        {
            railControllerPatterns.transform.GetChild(i).gameObject.SetActive(false);
        }
        
        if (railControllerPatterns.transform.childCount > 0)
            railControllerPatterns.transform.
                GetChild(Random.Range(0, railControllerPatterns.transform.childCount)).
                gameObject.
                SetActive(true);

    }
    
}
