using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _XGO;
    [SerializeField] private bool _isTouched;
    [SerializeField] private Vector2 tilePosition;

    public bool Touched => _isTouched;
    public Vector2 TilePosition => tilePosition;


    void OnMouseEnter()
    {
        if (Touched)
            return;

        _highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        if (Touched)
            return;

        _highlight.SetActive(false);
    }

    void OnMouseDown()
    {
        if (Touched)
            return;

        SetTile(true);
        GridManager.Instance.ControlPoint();
    }

    public void Init(int x, int y)
    {
        tilePosition = new Vector2(x, y);
    }

    public void SetTile(bool value)
    {
        _XGO.SetActive(value);
        _highlight.SetActive(false);
        _isTouched = value;
    }

    public void ClearTile()
    {
        SetTile(false);
    }
}