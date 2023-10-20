using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonkeyKongPursuit
{
    public class BarrelSpawner : MonoBehaviour
    {
        private BarrelController _barrelCtrl;
        [SerializeField] private GameObject _barrelPrefab;
        [SerializeField] private float _minTime;
        [SerializeField] private float _maxTime;


        private void Start()
        {
            if (_barrelPrefab == null)
                _barrelPrefab = GetComponent<GameObject>();

            SpawnBarrel();
        }

        private void SpawnBarrel()
        {
            Instantiate(_barrelPrefab, transform.position, Quaternion.identity);

            Invoke(nameof(SpawnBarrel), UnityEngine.Random.Range(_minTime, _maxTime));
        }
    }
}