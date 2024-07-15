using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour {

    private SpriteRenderer _sr;

    private GameObject building;
    private Color _defaultColor;
    [SerializeField] private Color _hoverColor;

    private void Awake () {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Start () {
        _defaultColor = _sr.color;   
    }

    private void OnMouseEnter () {
        _sr.color = _hoverColor;
    }

    private void OnMouseExit () {
        _sr.color = _defaultColor;
    }

    private void OnMouseDown () {
        if(building != null) return;

        GameObject buildingHolder = BuildManager.main.GetSelectedBuilding();
        if (buildingHolder != null) {
            building = Instantiate(buildingHolder, transform.position, Quaternion.identity);
        }
    }

}
