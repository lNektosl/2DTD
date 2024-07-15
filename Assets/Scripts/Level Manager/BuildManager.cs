using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager main;

    [SerializeField] private GameObject[] _buildings;

    private int _curSelectedBuilding = 0;

   
    private void Awake () {
        main = this;
    }

    public GameObject GetSelectedBuilding () {
        return _buildings[_curSelectedBuilding];
    }
}
