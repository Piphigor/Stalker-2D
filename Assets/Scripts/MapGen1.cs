using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapGen1 : MonoBehaviour
{
    public Tile[] tiles, puddles;
    public Tile NE_SW, NE_SE, NE_SW_SE, NW_NE, NW_NE_SE, NW_NE_SW, NW_NE_SW_SE, NW_SE, NW_SW, NW_SW_SE, SW_SE;
    public Tilemap tilemap, tilemap1, tilemap2;
    public Tile tileRock, tileRocks;
    private void Awake()
    {
        Cursor.visible = false;
        Gen();
    }

    private void Update()
    {
        if (Input.GetKeyUp("escape"))
        {
            Application.Quit();
        }
        if (Input.GetKeyUp("r"))
        {
            Gen();
        }
    }

    private void TileEq(int n1,int n2, Vector3Int mp)
    {
        var tile = tiles[Random.Range(n1, n2)];
        tilemap.SetTile(mp, tile);
    }

    private void Gen()
    {
        tilemap.ClearAllTiles();
        tilemap1.ClearAllTiles();
        tilemap2.ClearAllTiles();
        var MapPos = new Vector3Int(0, 0, 0);
        var tile0 = tiles[Random.Range(0, tiles.Length)];
        tilemap.SetTile(MapPos, tile0);
        for (MapPos.x = 0; MapPos.x <= 100; MapPos = MapPos + Vector3Int.right)
            //for (int i == 0; i < 9; i++)
            if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[0])
            {
                TileEq(0, 2, MapPos);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[1])
            {
                TileEq(0, 3, MapPos);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[2])
            {
                TileEq(1, 4, MapPos);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[3])
            {
                TileEq(2, 5, MapPos);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[4])
            {
                TileEq(3, 6, MapPos);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[5])
            {
                TileEq(4, 7, MapPos);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[6])
            {
                TileEq(5, 8, MapPos);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[7])
            {
                TileEq(6, 9, MapPos);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[8])
            {
                TileEq(7, 9, MapPos);
            }
            else
            {
                TileEq(8, tiles.Length, MapPos);
            }
        MapPos.x = 0;
        MapPos = MapPos + Vector3Int.up;
        for (MapPos.y = 1; MapPos.y <= 100; MapPos = MapPos + Vector3Int.up)
            if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[0])
            {
                var tile = tiles[Random.Range(0, 2)];
                tilemap.SetTile(MapPos, tile);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[1])
            {
                var tile = tiles[Random.Range(0, 3)];
                tilemap.SetTile(MapPos, tile);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[2])
            {
                var tile = tiles[Random.Range(1, 4)];
                tilemap.SetTile(MapPos, tile);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[3])
            {
                var tile = tiles[Random.Range(2, 5)];
                tilemap.SetTile(MapPos, tile);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[4])
            {
                var tile = tiles[Random.Range(3, 6)];
                tilemap.SetTile(MapPos, tile);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[5])
            {
                var tile = tiles[Random.Range(4, 7)];
                tilemap.SetTile(MapPos, tile);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[6])
            {
                var tile = tiles[Random.Range(5, 8)];
                tilemap.SetTile(MapPos, tile);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[7])
            {
                var tile = tiles[Random.Range(6, 9)];
                tilemap.SetTile(MapPos, tile);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[8])
            {
                var tile = tiles[Random.Range(7, 9)];
                tilemap.SetTile(MapPos, tile);
            }
            else
            {
                var tile = tiles[Random.Range(8, tiles.Length)];
                tilemap.SetTile(MapPos, tile);
            }
        MapPos.y = 0;
        MapPos = MapPos + Vector3Int.right;
        for (MapPos.x = 1; MapPos.x <= 100; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = 1; MapPos.y <= 100; MapPos = MapPos + Vector3Int.up)
            {
                if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[0])
                {
                    if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[0])
                    {
                        var tile = tiles[Random.Range(0, 1)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[1])
                    {
                        var tile = tiles[Random.Range(0, 2)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[2])
                    {
                        var tile = tiles[Random.Range(0, 3)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[3])
                    {
                        var tile = tiles[Random.Range(0, 4)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[4])
                    {
                        var tile = tiles[Random.Range(0, 5)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[5])
                    {
                        var tile = tiles[Random.Range(0, 6)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[6])
                    {
                        var tile = tiles[Random.Range(0, 7)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[7])
                    {
                        var tile = tiles[Random.Range(0, 8)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[8])
                    {
                        var tile = tiles[Random.Range(0, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else
                    {
                        var tile = tiles[Random.Range(0, tiles.Length)];
                        tilemap.SetTile(MapPos, tile);
                    }
                }
                else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[1])
                {
                    if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[0])
                    {
                        var tile = tiles[Random.Range(0, 2)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[1])
                    {
                        var tile = tiles[Random.Range(1, 2)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[2])
                    {
                        var tile = tiles[Random.Range(1, 3)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[3])
                    {
                        var tile = tiles[Random.Range(1, 4)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[4])
                    {
                        var tile = tiles[Random.Range(1, 5)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[5])
                    {
                        var tile = tiles[Random.Range(1, 6)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[6])
                    {
                        var tile = tiles[Random.Range(1, 7)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[7])
                    {
                        var tile = tiles[Random.Range(1, 8)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[8])
                    {
                        var tile = tiles[Random.Range(1, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else
                    {
                        var tile = tiles[Random.Range(1, tiles.Length)];
                        tilemap.SetTile(MapPos, tile);
                    }
                }
                else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[2])
                {
                    if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[0])
                    {
                        var tile = tiles[Random.Range(0, 2)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[1])
                    {
                        var tile = tiles[Random.Range(1, 3)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[2])
                    {
                        var tile = tiles[Random.Range(2, 3)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[3])
                    {
                        var tile = tiles[Random.Range(2, 4)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[4])
                    {
                        var tile = tiles[Random.Range(2, 5)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[5])
                    {
                        var tile = tiles[Random.Range(2, 6)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[6])
                    {
                        var tile = tiles[Random.Range(2, 7)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[7])
                    {
                        var tile = tiles[Random.Range(2, 8)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[8])
                    {
                        var tile = tiles[Random.Range(2, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else
                    {
                        var tile = tiles[Random.Range(2, tiles.Length)];
                        tilemap.SetTile(MapPos, tile);
                    }
                }
                else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[3])
                {
                    if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[0])
                    {
                        var tile = tiles[Random.Range(0, 3)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[1])
                    {
                        var tile = tiles[Random.Range(1, 3)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[2])
                    {
                        var tile = tiles[Random.Range(2, 4)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[3])
                    {
                        var tile = tiles[Random.Range(3, 4)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[4])
                    {
                        var tile = tiles[Random.Range(3, 5)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[5])
                    {
                        var tile = tiles[Random.Range(3, 6)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[6])
                    {
                        var tile = tiles[Random.Range(3, 7)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[7])
                    {
                        var tile = tiles[Random.Range(3, 8)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[8])
                    {
                        var tile = tiles[Random.Range(3, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else
                    {
                        var tile = tiles[Random.Range(3, tiles.Length)];
                        tilemap.SetTile(MapPos, tile);
                    }
                }
                else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[4])
                {
                    if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[0])
                    {
                        var tile = tiles[Random.Range(0, 4)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[1])
                    {
                        var tile = tiles[Random.Range(1, 4)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[2])
                    {
                        var tile = tiles[Random.Range(2, 4)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[3])
                    {
                        var tile = tiles[Random.Range(3, 5)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[4])
                    {
                        var tile = tiles[Random.Range(4, 5)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[5])
                    {
                        var tile = tiles[Random.Range(4, 6)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[6])
                    {
                        var tile = tiles[Random.Range(4, 7)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[7])
                    {
                        var tile = tiles[Random.Range(4, 8)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[8])
                    {
                        var tile = tiles[Random.Range(4, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else
                    {
                        var tile = tiles[Random.Range(4, tiles.Length)];
                        tilemap.SetTile(MapPos, tile);
                    }
                }
                else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[5])
                {
                    if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[0])
                    {
                        var tile = tiles[Random.Range(0, 5)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[1])
                    {
                        var tile = tiles[Random.Range(1, 5)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[2])
                    {
                        var tile = tiles[Random.Range(2, 5)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[3])
                    {
                        var tile = tiles[Random.Range(3, 5)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[4])
                    {
                        var tile = tiles[Random.Range(4, 6)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[5])
                    {
                        var tile = tiles[Random.Range(5, 6)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[6])
                    {
                        var tile = tiles[Random.Range(5, 7)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[7])
                    {
                        var tile = tiles[Random.Range(5, 8)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[8])
                    {
                        var tile = tiles[Random.Range(5, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else
                    {
                        var tile = tiles[Random.Range(5, tiles.Length)];
                        tilemap.SetTile(MapPos, tile);
                    }
                }
                else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[6])
                {
                    if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[0])
                    {
                        var tile = tiles[Random.Range(0, 6)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[1])
                    {
                        var tile = tiles[Random.Range(1, 6)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[2])
                    {
                        var tile = tiles[Random.Range(2, 6)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[3])
                    {
                        var tile = tiles[Random.Range(3, 6)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[4])
                    {
                        var tile = tiles[Random.Range(4, 6)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[5])
                    {
                        var tile = tiles[Random.Range(5, 7)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[6])
                    {
                        var tile = tiles[Random.Range(6, 7)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[7])
                    {
                        var tile = tiles[Random.Range(6, 8)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[8])
                    {
                        var tile = tiles[Random.Range(6, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else
                    {
                        var tile = tiles[Random.Range(6, tiles.Length)];
                        tilemap.SetTile(MapPos, tile);
                    }
                }
                else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[7])
                {
                    if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[0])
                    {
                        var tile = tiles[Random.Range(0, 7)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[1])
                    {
                        var tile = tiles[Random.Range(1, 7)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[2])
                    {
                        var tile = tiles[Random.Range(2, 7)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[3])
                    {
                        var tile = tiles[Random.Range(3, 7)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[4])
                    {
                        var tile = tiles[Random.Range(4, 7)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[5])
                    {
                        var tile = tiles[Random.Range(5, 7)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[6])
                    {
                        var tile = tiles[Random.Range(6, 8)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[7])
                    {
                        var tile = tiles[Random.Range(7, 8)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[8])
                    {
                        var tile = tiles[Random.Range(7, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else
                    {
                        var tile = tiles[Random.Range(7, tiles.Length)];
                        tilemap.SetTile(MapPos, tile);
                    }
                }
                else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[8])
                {
                    if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[0])
                    {
                        var tile = tiles[Random.Range(0, 8)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[1])
                    {
                        var tile = tiles[Random.Range(1, 8)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[2])
                    {
                        var tile = tiles[Random.Range(2, 8)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[3])
                    {
                        var tile = tiles[Random.Range(3, 8)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[4])
                    {
                        var tile = tiles[Random.Range(4, 8)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[5])
                    {
                        var tile = tiles[Random.Range(5, 8)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[6])
                    {
                        var tile = tiles[Random.Range(6, 8)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[7])
                    {
                        var tile = tiles[Random.Range(7, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[8])
                    {
                        var tile = tiles[Random.Range(8, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else
                    {
                        var tile = tiles[Random.Range(8, tiles.Length)];
                        tilemap.SetTile(MapPos, tile);
                    }
                }
                else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[9])
                {
                    if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[0])
                    {
                        var tile = tiles[Random.Range(0, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[1])
                    {
                        var tile = tiles[Random.Range(1, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[2])
                    {
                        var tile = tiles[Random.Range(2, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[3])
                    {
                        var tile = tiles[Random.Range(3, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[4])
                    {
                        var tile = tiles[Random.Range(4, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[5])
                    {
                        var tile = tiles[Random.Range(5, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[6])
                    {
                        var tile = tiles[Random.Range(6, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[7])
                    {
                        var tile = tiles[Random.Range(7, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[8])
                    {
                        var tile = tiles[Random.Range(8, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                    else
                    {
                        var tile = tiles[Random.Range(8, 9)];
                        tilemap.SetTile(MapPos, tile);
                    }
                }
                else
                {
                    var tile = tiles[Random.Range(0, tiles.Length)];
                    tilemap.SetTile(MapPos, tile);
                }
            }
        int rx = Random.Range(30, 70);
        int ry = Random.Range(30, 70);
        MapPos.x = rx;
        MapPos.y = ry;
        var road = NW_NE_SW_SE;
        tilemap2.SetTile(MapPos, road);
        int r;
        MapPos.x = rx;
        for (MapPos.y = ry - 1; MapPos.y >= 0; MapPos = MapPos + Vector3Int.down)
        {
            if ((tilemap2.GetTile(MapPos + Vector3Int.up) == NE_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.up) == NE_SW_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_NE_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_NE_SW_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_SW_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.up) == SW_SE))
                if (Random.Range(0f, 100f) <= 97f)
                {
                    road = NW_SE;
                    tilemap2.SetTile(MapPos, road);
                }
                else
                {
                    r = Random.Range(0, 6);
                    if (r == 0)
                        road = NW_NE;
                    else if (r == 1)
                        road = NW_NE_SE;
                    else if (r == 2)
                        road = NW_NE_SW;
                    else if (r == 3)
                        road = NW_NE_SW_SE;
                    else if (r == 4)
                        road = NW_SW;
                    else if (r == 5)
                        road = NW_SW_SE;
                    tilemap2.SetTile(MapPos, road);
                }
            else if (Random.Range(0f, 100f) >= 99f)
            {
                r = Random.Range(0, 4);
                if (r == 0)
                    road = NE_SE;
                else if (r == 1)
                    road = NE_SW;
                else if (r == 2)
                    road = SW_SE;
                else if (r == 3)
                    road = NE_SW_SE;
                tilemap2.SetTile(MapPos, road);
            }
        }
        MapPos.y = ry;
        for (MapPos.x = rx - 1; MapPos.x >= 0; MapPos = MapPos + Vector3Int.left)
        {
            if ((tilemap2.GetTile(MapPos + Vector3Int.right) == SW_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NE_SW) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NE_SW_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_NE_SW) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_NE_SW_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_SW) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_SW_SE))
                if (Random.Range(0f, 100f) <= 97f)
                {
                    road = NE_SW;
                    tilemap2.SetTile(MapPos, road);
                }
                else
                {
                    r = Random.Range(0, 6);
                    if (r == 0)
                        road = NE_SE;
                    else if (r == 1)
                        road = NE_SW_SE;
                    else if (r == 2)
                        road = NW_NE;
                    else if (r == 3)
                        road = NW_NE_SW;
                    else if (r == 4)
                        road = NW_NE_SW_SE;
                    else if (r == 5)
                        road = NW_NE_SE;
                    tilemap2.SetTile(MapPos, road);
                }
            else if (Random.Range(0f, 100f) >= 99f)
            {
                r = Random.Range(0, 4);
                if (r == 0)
                    road = NW_SE;
                else if (r == 1)
                    road = NW_SW;
                else if (r == 2)
                    road = SW_SE;
                else if (r == 3)
                    road = NW_SW_SE;
                tilemap2.SetTile(MapPos, road);
            }
        }
        MapPos.y = ry;
        for (MapPos.x = rx + 1; MapPos.x <= 100; MapPos = MapPos + Vector3Int.right)
        {
            if ((tilemap2.GetTile(MapPos + Vector3Int.left) == NE_SE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NE_SW) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NE_SW_SE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE_SW) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE_SW_SE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE_SE))
                if (Random.Range(0f, 100f) <= 97f)
                {
                    road = NE_SW;
                    tilemap2.SetTile(MapPos, road);
                }
                else
                {
                    r = Random.Range(0, 6);
                    if (r == 0)
                        road = SW_SE;
                    else if (r == 1)
                        road = NE_SW_SE;
                    else if (r == 2)
                        road = NW_NE_SW;
                    else if (r == 3)
                        road = NW_NE_SW_SE;
                    else if (r == 4)
                        road = NW_SW;
                    else if (r == 5)
                        road = NW_SW_SE;
                    tilemap2.SetTile(MapPos, road);
                }
            else if (Random.Range(0f, 100f) >= 99f)
            {
                r = Random.Range(0, 4);
                if (r == 0)
                    road = NE_SE;
                else if (r == 1)
                    road = NW_NE;
                else if (r == 2)
                    road = NW_SE;
                else if (r == 3)
                    road = NW_NE_SE;
                tilemap2.SetTile(MapPos, road);
            }
        }
        MapPos.x = rx;
        for (MapPos.y = ry + 1; MapPos.y <= 100; MapPos = MapPos + Vector3Int.up)
        {
            if ((tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE_SW) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE_SW_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_SW_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_SW))
                if (Random.Range(0f, 100f) <= 97f)
                {
                    road = NW_SE;
                    tilemap2.SetTile(MapPos, road);
                }
                else
                {
                    r = Random.Range(0, 6);
                    if (r == 0)
                        road = NE_SE;
                    else if (r == 1)
                        road = NW_NE_SE;
                    else if (r == 2)
                        road = NE_SW_SE;
                    else if (r == 3)
                        road = NW_NE_SW_SE;
                    else if (r == 4)
                        road = NW_NE_SE;
                    else if (r == 5)
                        road = NW_SW_SE;
                    tilemap2.SetTile(MapPos, road);
                }
            else if (Random.Range(0f, 100f) >= 99f)
            {
                r = Random.Range(0, 4);
                if (r == 0)
                    road = NE_SW;
                else if (r == 1)
                    road = NW_NE;
                else if (r == 2)
                    road = NW_SW;
                else if (r == 3)
                    road = NW_NE_SW;
                tilemap2.SetTile(MapPos, road);
            }
        }
        float rank = 0f;
        for (MapPos.x = rx - 1; MapPos.x >= 0; MapPos = MapPos + Vector3Int.left)
            for (MapPos.y = ry - 1; MapPos.y >= 0; MapPos = MapPos + Vector3Int.down)
            {
                float ran = Random.Range(rank, 100f);
                if (((tilemap2.GetTile(MapPos + Vector3Int.up) == NE_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NE_SW_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_NE_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_NE_SW_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_SW_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == SW_SE)) &&
                    ((tilemap2.GetTile(MapPos + Vector3Int.right) == SW_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NE_SW) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NE_SW_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_NE_SW) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_NE_SW_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_SW) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_SW_SE)))
                {
                    r = Random.Range(0, 4);
                    if (r == 0)
                        road = NW_NE;
                    else if (r == 1)
                        road = NW_NE_SE;
                    else if (r == 2)
                        road = NW_NE_SW;
                    else if (r == 3)
                        road = NW_NE_SW_SE;
                    tilemap2.SetTile(MapPos, road);
                }
                else if (((tilemap2.GetTile(MapPos + Vector3Int.up) == NE_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NE_SW_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_NE_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_NE_SW_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_SW_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == SW_SE)))
                {
                    if (tilemap2.GetTile(MapPos + Vector3Int.up) != NW_SE)
                    {
                        road = NW_SE;
                        tilemap2.SetTile(MapPos, road);
                        rank += 5f;
                    }
                    else if (ran <= 97f)
                    {
                        road = NW_SE;
                        tilemap2.SetTile(MapPos, road);
                        rank += 5f;
                    }
                    else if (ran >= 97f)
                    {
                        r = Random.Range(0, 2);
                        if (r == 0)
                            road = NW_SW;
                        else if (r == 1)
                            road = NW_SW_SE;
                        tilemap2.SetTile(MapPos, road);
                        rank = 0f;
                    }
                }
                else if (((tilemap2.GetTile(MapPos + Vector3Int.right) == SW_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NE_SW) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NE_SW_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_NE_SW) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_NE_SW_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_SW) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_SW_SE)))
                {
                    if (tilemap2.GetTile(MapPos + Vector3Int.right) != NE_SW)
                    {
                        road = NE_SW;
                        tilemap2.SetTile(MapPos, road);
                        rank += 5f;
                    }
                    else if (ran <= 97f)
                    {
                        road = NE_SW;
                        tilemap2.SetTile(MapPos, road);
                        rank += 5f;
                    }
                    else if (ran >= 97f)
                    {
                        r = Random.Range(0, 2);
                        if (r == 0)
                            road = NE_SE;
                        else if (r == 1)
                            road = NE_SW_SE;
                        tilemap2.SetTile(MapPos, road);
                        rank = 0f;
                    }
                }
                else if (Random.Range(0f, 400f) >= 399f)
                {
                    road = SW_SE;
                    tilemap2.SetTile(MapPos, road);
                }
            }
        rank = 0f;
        for (MapPos.x = rx + 1; MapPos.x <= 100; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = ry - 1; MapPos.y >= 0; MapPos = MapPos + Vector3Int.down)
            {
                float ran = Random.Range(rank, 100f);
                if (((tilemap2.GetTile(MapPos + Vector3Int.up) == NE_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NE_SW_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_NE_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_NE_SW_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_SW_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == SW_SE)) &&
                    ((tilemap2.GetTile(MapPos + Vector3Int.left) == NE_SE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NE_SW) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NE_SW_SE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE_SW) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE_SW_SE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE_SE)))
                {
                    r = Random.Range(0, 4);
                    if (r == 0)
                        road = NW_SW;
                    else if (r == 1)
                        road = NW_NE_SW;
                    else if (r == 2)
                        road = NW_SW_SE;
                    else if (r == 3)
                        road = NW_NE_SW_SE;
                    tilemap2.SetTile(MapPos, road);
                }
                else if (((tilemap2.GetTile(MapPos + Vector3Int.up) == NE_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NE_SW_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_NE_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_NE_SW_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == NW_SW_SE) ||
                    (tilemap2.GetTile(MapPos + Vector3Int.up) == SW_SE)))
                {
                    if (tilemap2.GetTile(MapPos + Vector3Int.up) != NW_SE)
                    {
                        road = NW_SE;
                        tilemap2.SetTile(MapPos, road);
                        rank += 5f;
                    }
                    else if (ran <= 97f)
                    {
                        road = NW_SE;
                        tilemap2.SetTile(MapPos, road);
                        rank += 5f;
                    }
                    else if (ran >= 97f)
                    {
                        r = Random.Range(0, 2);
                        if (r == 0)
                            road = NW_NE;
                        else if (r == 1)
                            road = NW_NE_SE;
                        tilemap2.SetTile(MapPos, road);
                        rank = 0f;
                    }
                }
                else if (((tilemap2.GetTile(MapPos + Vector3Int.left) == NE_SE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NE_SW) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NE_SW_SE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE_SW) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE_SW_SE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE_SE)))
                {
                    if (tilemap2.GetTile(MapPos + Vector3Int.left) != NE_SW)
                    {
                        road = NE_SW;
                        tilemap2.SetTile(MapPos, road);
                        rank += 5f;
                    }
                    else if (ran <= 97f)
                    {
                        road = NE_SW;
                        tilemap2.SetTile(MapPos, road);
                        rank += 5f;
                    }
                    else if (ran >= 97f)
                    {
                        r = Random.Range(0, 2);
                        if (r == 0)
                            road = SW_SE;
                        else if (r == 1)
                            road = NE_SW_SE;
                        tilemap2.SetTile(MapPos, road);
                        rank = 0f;
                    }
                }
                else if (Random.Range(0f, 400f) >= 399f)
                {
                    road = NE_SE;
                    tilemap2.SetTile(MapPos, road);
                }
            }
        rank = 0f;
        for (MapPos.x = rx - 1; MapPos.x >= 0; MapPos = MapPos + Vector3Int.left)
            for (MapPos.y = ry + 1; MapPos.y <= 100; MapPos = MapPos + Vector3Int.up)
            {
                float ran = Random.Range(rank, 100f);
                if (((tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE_SW) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE_SW_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_SW_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_SW)) &&
                    ((tilemap2.GetTile(MapPos + Vector3Int.right) == SW_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NE_SW) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NE_SW_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_NE_SW) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_NE_SW_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_SW) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_SW_SE)))
                {
                    r = Random.Range(0, 4);
                    if (r == 0)
                        road = NE_SE;
                    else if (r == 1)
                        road = NE_SW_SE;
                    else if (r == 2)
                        road = NW_NE_SE;
                    else if (r == 3)
                        road = NW_NE_SW_SE;
                    tilemap2.SetTile(MapPos, road);
                }
                else if (((tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE_SW) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE_SW_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_SW_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_SW)))
                {
                    if (tilemap2.GetTile(MapPos + Vector3Int.down) != NW_SE)
                    {
                        road = NW_SE;
                        tilemap2.SetTile(MapPos, road);
                        rank += 5f;
                    }
                    else if (ran <= 97f)
                    {
                        road = NW_SE;
                        tilemap2.SetTile(MapPos, road);
                        rank += 5f;
                    }
                    else if (ran >= 97f)
                    {
                        r = Random.Range(0, 2);
                        if (r == 0)
                            road = SW_SE;
                        else if (r == 1)
                            road = NW_SW_SE;
                        tilemap2.SetTile(MapPos, road);
                        rank = 0f;
                    }
                }
                else if (((tilemap2.GetTile(MapPos + Vector3Int.right) == SW_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NE_SW) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NE_SW_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_NE_SW) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_NE_SW_SE) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_SW) ||
                (tilemap2.GetTile(MapPos + Vector3Int.right) == NW_SW_SE)))
                {
                    if (tilemap2.GetTile(MapPos + Vector3Int.right) != NE_SW)
                    {
                        road = NE_SW;
                        tilemap2.SetTile(MapPos, road);
                        rank += 5f;
                    }
                    else if (ran <= 97f)
                    {
                        road = NE_SW;
                        tilemap2.SetTile(MapPos, road);
                        rank += 5f;
                    }
                    else if (ran >= 97f)
                    {
                        r = Random.Range(0, 2);
                        if (r == 0)
                            road = NW_NE;
                        else if (r == 1)
                            road = NW_NE_SW;
                        tilemap2.SetTile(MapPos, road);
                        rank = 0f;
                    }
                }
                else if (Random.Range(0f, 400f) >= 399f)
                {
                    road = NW_SW;
                    tilemap2.SetTile(MapPos, road);
                }
            }
        rank = 0f;
        for (MapPos.x = rx + 1; MapPos.x <= 100; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = ry + 1; MapPos.y <= 100; MapPos = MapPos + Vector3Int.up)
            {
                float ran = Random.Range(rank, 100f);
                if (((tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE_SW) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE_SW_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_SW_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_SW)) &&
                    ((tilemap2.GetTile(MapPos + Vector3Int.left) == NE_SE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NE_SW) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NE_SW_SE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE_SW) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE_SW_SE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE_SE)))
                {
                    r = Random.Range(0, 4);
                    if (r == 0)
                        road = SW_SE;
                    else if (r == 1)
                        road = NE_SW_SE;
                    else if (r == 2)
                        road = NW_SW_SE;
                    else if (r == 3)
                        road = NW_NE_SW_SE;
                    tilemap2.SetTile(MapPos, road);
                }
                else if (((tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE_SW) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_NE_SW_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_SW_SE) ||
              (tilemap2.GetTile(MapPos + Vector3Int.down) == NW_SW)))
                {
                    if (tilemap2.GetTile(MapPos + Vector3Int.down) != NW_SE)
                    {
                        road = NW_SE;
                        tilemap2.SetTile(MapPos, road);
                        rank += 5f;
                    }
                    else if (ran <= 97f)
                    {
                        road = NW_SE;
                        tilemap2.SetTile(MapPos, road);
                        rank += 5f;
                    }
                    else if (ran >= 97f)
                    {
                        r = Random.Range(0, 2);
                        if (r == 0)
                            road = NE_SE;
                        else if (r == 1)
                            road = NW_NE_SE;
                        tilemap2.SetTile(MapPos, road);
                        rank = 0f;
                    }
                }
                else if (((tilemap2.GetTile(MapPos + Vector3Int.left) == NE_SE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NE_SW) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NE_SW_SE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE_SW) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE_SW_SE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE) ||
               (tilemap2.GetTile(MapPos + Vector3Int.left) == NW_NE_SE)))
                {
                    if (tilemap2.GetTile(MapPos + Vector3Int.left) != NE_SW)
                    {
                        road = NE_SW;
                        tilemap2.SetTile(MapPos, road);
                        rank += 5f;
                    }
                    else if (ran <= 97f)
                    {
                        road = NE_SW;
                        tilemap2.SetTile(MapPos, road);
                        rank += 5f;
                    }
                    else if (ran >= 97f)
                    {
                        r = Random.Range(0, 2);
                        if (r == 0)
                            road = NW_SW;
                        else if (r == 1)
                            road = NW_NE_SW;
                        tilemap2.SetTile(MapPos, road);
                        rank = 0f;
                    }
                }
                else if (Random.Range(0f, 400f) >= 399f)
                {
                    road = NW_NE;
                    tilemap2.SetTile(MapPos, road);
                }
            }
        for (MapPos.x = 0; MapPos.x <= 100; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = 0; MapPos.y <= 100; MapPos = MapPos + Vector3Int.up)
            {
                if ((Random.Range(0f, 100f) >= 99f) && (tilemap2.HasTile(MapPos) == false))
                {
                    var tile1 = tileRock;
                    tilemap1.SetTile(MapPos, tile1);
                }
            }
        for (MapPos.x = 0; MapPos.x <= 100; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = 0; MapPos.y <= 100; MapPos = MapPos + Vector3Int.up)
            {
                if ((Random.Range(0f, 100f) >= 80f) && (tilemap1.GetTile(MapPos) != tileRock) && (tilemap2.HasTile(MapPos) == false) && ((tilemap1.GetTile(MapPos + Vector3Int.down) == tileRock)
            || (tilemap1.GetTile(MapPos + Vector3Int.up) == tileRock)
            || (tilemap1.GetTile(MapPos + Vector3Int.left) == tileRock)
            || (tilemap1.GetTile(MapPos + Vector3Int.right) == tileRock)
            || (tilemap1.GetTile(MapPos + Vector3Int.down + Vector3Int.right) == tileRock)
            || (tilemap1.GetTile(MapPos + Vector3Int.down + Vector3Int.left) == tileRock)
            || (tilemap1.GetTile(MapPos + Vector3Int.up + Vector3Int.right) == tileRock)
            || (tilemap1.GetTile(MapPos + Vector3Int.up + Vector3Int.left) == tileRock)))
                {
                    var tile2 = tileRocks;
                    tilemap2.SetTile(MapPos, tile2);
                }
            }
        for (MapPos.x = 0; MapPos.x <= 100; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = 0; MapPos.y <= 100; MapPos = MapPos + Vector3Int.up)
            {
                if ((Random.Range(0f, 100f) >= 99f) && (tilemap1.GetTile(MapPos) != tileRock) && (tilemap2.HasTile(MapPos) == false))
                {
                    var tile2 = puddles[Random.Range(0, puddles.Length)];
                    tilemap2.SetTile(MapPos, tile2);
                }
            }
    }
}