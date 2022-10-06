using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private int _width;
    [SerializeField] private int _height;

    [SerializeField] private Tile _tile;

    [SerializeField] private Camera _cam;
    [SerializeField] private GamePanel gamePanel;

    private Dictionary<Vector2, Tile> _tiles;
    private int _point = 0;

    public int Point => _point;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GenerateGrid();
    }

    public void SetSize(int value)
    {
        _width = value;
        _height = value;
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        DestroyGrid();
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";


                spawnedTile.Init(x, y);


                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        _cam.transform.position = new Vector3((float) _width / 2 - 0.5f, (float) _height / 2 - 0.5f, -10);
        _cam.orthographicSize = (_width > _height ? _width : _height) * 1.1f;
    }


    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }

    private void DestroyGrid()
    {
        if (_tiles == null)
            return;

        foreach (var tile in _tiles)
        {
            Destroy(tile.Value.gameObject);
        }

        _tiles.Clear();
        // _tiles = null;
    }

    public void ControlPoint()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var tile = GetTileAtPosition(new Vector2(x, y));

                if (tile.Touched)
                {
                    #region Horizontal Control

                    print("Tile : " + tile.name);
                    var _XCount = 0;
                    var tempTile = GetTileAtPosition(new Vector2(x, y));
                    for (int i = x + 1; tempTile.Touched; i++)
                    {
                        _XCount++;
                        tempTile = GetTileAtPosition(new Vector2(i, y));
                        if (tempTile == null)
                            break;
                    }

                    if (_XCount > 2)
                    {
                        print("Aynen Kanka : " + _XCount);
                        tempTile = GetTileAtPosition(new Vector2(x, y));
                        for (int i = x; i <= x + _XCount; i++)
                        {
                            tempTile = GetTileAtPosition(new Vector2(i, y));
                            if (tempTile == null)
                                break;
                            tempTile.ClearTile();
                        }

                        _point += _XCount;
                        gamePanel.SetPointText(_point);
                    }

                    #endregion

                    #region Vertical Control

                    print("Tile : " + tile.name);
                    _XCount = 0;
                    tempTile = GetTileAtPosition(new Vector2(x, y));
                    for (int i = y + 1; tempTile.Touched; i++)
                    {
                        _XCount++;
                        tempTile = GetTileAtPosition(new Vector2(x, i));
                        if (tempTile == null)
                            break;
                    }

                    if (_XCount > 2)
                    {
                        tempTile = GetTileAtPosition(new Vector2(x, y));
                        for (int i = y; i <= y + _XCount; i++)
                        {
                            tempTile = GetTileAtPosition(new Vector2(x, i));
                            if (tempTile == null)
                                break;
                            tempTile.ClearTile();
                        }

                        _point += _XCount;
                        gamePanel.SetPointText(_point);
                    }

                    #endregion
                }
            }
        }
    }
}