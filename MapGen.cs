using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapGen : MonoBehaviour
{
    public Tile[] tiles, puddles, roads1, roads2, roads3, roads4, roads5, roads;
    public Tilemap tilemap, tilemap1, tilemap2;
    public Tile tileRock, tileRocks;
    private void Awake()
    {
        tilemap.ClearAllTiles();
        var MapPos = new Vector3Int(0, 0, 0);
        var tile0 = tiles[Random.Range(0, tiles.Length)];
        tilemap.SetTile(MapPos, tile0);
        for (MapPos.x = 0; MapPos.x <= 100; MapPos = MapPos + Vector3Int.right)
            if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[0])
            {
                var tile = tiles[Random.Range(0, 2)];
                tilemap.SetTile(MapPos, tile);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[1])
            {
                var tile = tiles[Random.Range(0, 3)];
                tilemap.SetTile(MapPos, tile);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[2])
            {
                var tile = tiles[Random.Range(1, 4)];
                tilemap.SetTile(MapPos, tile);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[3])
            {
                var tile = tiles[Random.Range(2, 5)];
                tilemap.SetTile(MapPos, tile);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[4])
            {
                var tile = tiles[Random.Range(3, 6)];
                tilemap.SetTile(MapPos, tile);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[5])
            {
                var tile = tiles[Random.Range(4, 7)];
                tilemap.SetTile(MapPos, tile);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[6])
            {
                var tile = tiles[Random.Range(5, 8)];
                tilemap.SetTile(MapPos, tile);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[7])
            {
                var tile = tiles[Random.Range(6, 9)];
                tilemap.SetTile(MapPos, tile);
            }
            else if (tilemap.GetTile(MapPos + Vector3Int.left) == tiles[8])
            {
                var tile = tiles[Random.Range(7, 9)];
                tilemap.SetTile(MapPos, tile);
            }
            else
            {
                var tile = tiles[Random.Range(8, tiles.Length)];
                tilemap.SetTile(MapPos, tile);
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


                int k = Random.Range(0, 100);
                if ((k % 50 == 0))
                {
                    var tile1 = tileRock;
                    tilemap1.SetTile(MapPos, tile1);
                }
            }
        MapPos.x = 0;
        MapPos.y = 0;
        var road0 = roads1[3];
        tilemap2.SetTile(MapPos, road0);
        for (MapPos.x = 0; MapPos.x <= 100; MapPos = MapPos + Vector3Int.right)
        {
            int l = Random.Range(0, 100);
            if ((tilemap1.GetTile(MapPos) != tileRock) && (l % 40 == 0))
            {
                for (int i = 0; i < roads1.Length; i++)
                    if (tilemap2.GetTile(MapPos + Vector3Int.left) == roads1[i])
                    {
                        var road = roads3[Random.Range(0, roads3.Length)];
                        tilemap2.SetTile(MapPos, road);
                    }
                    else
                    {
                        var road = roads[Random.Range(0, roads.Length)];
                        tilemap2.SetTile(MapPos, road);
                    }
            }
        }
        MapPos.x = 0;
        MapPos = MapPos + Vector3Int.up;
        for (MapPos.y = 1; MapPos.y <= 100; MapPos = MapPos + Vector3Int.up)
        {
            int l1 = Random.Range(0, 100);
            if ((tilemap1.GetTile(MapPos) != tileRock) && (l1 % 40 == 0))
            {
                for (int i = 0; i < roads2.Length; i++)
                    if (tilemap2.GetTile(MapPos + Vector3Int.down) == roads2[i])
                    {
                        var road = roads4[Random.Range(0, roads4.Length)];
                        tilemap2.SetTile(MapPos, road);
                    }
                    else
                    {
                        var road = roads[Random.Range(0, roads.Length)];
                        tilemap2.SetTile(MapPos, road);
                    }
            }
        }
        MapPos.y = 0;
        MapPos = MapPos + Vector3Int.right;
        for (MapPos.x = 1; MapPos.x <= 100; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = 1; MapPos.y <= 100; MapPos = MapPos + Vector3Int.up)
            {
                int l2 = Random.Range(0, 100);
                for (int i = 0; i < roads1.Length; i++)
                    for (int j = 0; j < roads2.Length; j++)
                        if ((tilemap1.GetTile(MapPos) != tileRock))
                        if (tilemap2.GetTile(MapPos + Vector3Int.left) == roads1[i])
                        {
                            if (tilemap2.GetTile(MapPos + Vector3Int.down) == roads2[j])
                            {
                                var road = roads5[Random.Range(0, roads5.Length)];
                                tilemap2.SetTile(MapPos, road);
                            }
                            else
                            {
                                var road = roads3[Random.Range(0, roads3.Length)];
                                tilemap2.SetTile(MapPos, road);
                            }
                        }
                        else
                        {
                            if (tilemap2.GetTile(MapPos + Vector3Int.down) == roads2[j])
                            {
                                var road = roads4[Random.Range(0, roads4.Length)];
                                tilemap2.SetTile(MapPos, road);
                            }
                        }
            }
        for (MapPos.x = 0; MapPos.x <= 100; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = 0; MapPos.y <= 100; MapPos = MapPos + Vector3Int.up)
            {
                int g = Random.Range(0, 100);
                for (int i = 0; i < roads.Length; i++)
                if ((g % 7 == 0) && (tilemap1.GetTile(MapPos) != tileRock) && (tilemap2.GetTile(MapPos) != roads[i]) && ((tilemap1.GetTile(MapPos + Vector3Int.down) == tileRock)
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
                int m = Random.Range(0, 100);
                for (int i = 0; i < roads.Length; i++)
                    if ((m % 25 == 0) && (tilemap1.GetTile(MapPos) != tileRock) && (tilemap2.GetTile(MapPos) != roads[i]) && (tilemap2.GetTile(MapPos) != tileRocks))
                    {
                    var tile2 = puddles[Random.Range(0, puddles.Length)];
                    tilemap2.SetTile(MapPos, tile2);
                    }
            }
    }
}