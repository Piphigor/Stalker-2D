using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapGen2 : MonoBehaviour
{
    public Tile[] tiles, puddles;
    public Tile NE_SW, NE_SE, NE_SW_SE, NW_NE, NW_NE_SE, NW_NE_SW, NW_NE_SW_SE, NW_SE, NW_SW, NW_SW_SE, SW_SE;
    public Tilemap tilemap, tilemap1, tilemap2;
    public Tile tileRock, tileRocks;
<<<<<<< HEAD
    public Transform tree;
    private Transform[] trees;
=======
>>>>>>> c9e8c67ec5caf7ed670db7e45f02c15e6550aa76
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

<<<<<<< HEAD
    private Vector3Int IsoToN(Vector3Int mps)
    {
        int row = mps.x + mps.y;
        int column = mps.x - mps.y;

        // Shift alternating rows by one.
        if ((Mathf.Abs(row) & 1) == 1)
            column++;

        // All column numbers are now even numbers.
        // Divide by 2 to get them into consecutive integers.
        column /= 2;
        row /= 4;

        return new Vector3Int(column, row, 0);
    }

    private void SetTilePro(Vector3Int MP, Vector3Int MPL1, Vector3Int MPL2)
    {
        for (int i = 0; i < tiles.Length; i++)
            for (int j = 0; j < tiles.Length; j++)
                if ((tilemap.GetTile(MPL1) == tiles[i]) && (tilemap.GetTile(MPL2) == tiles[j]))
                {
                    int r1, r2;
                    if (i == j)
                    {
                        if ((i == 0) || (i == 1)) r1 = 0;
                        else r1 = i - 1;
                        if ((i == tiles.Length) || (i == tiles.Length - 1)) r2 = tiles.Length;
                        else r2 = i + 2;
                        var tile = tiles[Random.Range(r1, r2)];
                        tilemap.SetTile(MP, tile);
                    }
                    else if (i > j)
                    {
                        if ((j == 0) || (j == 1)) r1 = 0;
                        else r1 = j - 1;
                        if ((i == tiles.Length) || (i == tiles.Length - 1)) r2 = tiles.Length;
                        else r2 = i + 2;
                        var tile = tiles[Random.Range(r1, r2)];
                        tilemap.SetTile(MP, tile);
                    }
                    else if (i < j)
                    {
                        if ((i == 0) || (i == 1)) r1 = 0;
                        else r1 = i - 1;
                        if ((j == tiles.Length) || (j == tiles.Length - 1)) r2 = tiles.Length;
                        else r2 = j + 2;
                        var tile = tiles[Random.Range(r1, r2)];
                        tilemap.SetTile(MP, tile);
                    }
                }
    }

    private void SpiralGen(Vector3Int MP)
    {
        var MPL1 = MP + Vector3Int.left;
        var MPL2 = MP + Vector3Int.left + Vector3Int.down;
        while (((MP.x <= 50) && (MP.x >= -50)) && ((MP.y <= 50) && (MP.y >= -50)))
        {
            if ((MP.x == MP.y) && (MP.x > 0) && (tilemap.HasTile(MP + Vector3Int.down) == false) && (tilemap.HasTile(MP + Vector3Int.left) == true))
            {
                SetTilePro(MP, MPL1, MPL2);
                MPL1 = MP;
                MP.y--;
            }
            else if ((Math.Abs(MP.x) == MP.y) && (MP.x < 0) && (MP.y > 0) && (tilemap.HasTile(MP + Vector3Int.down) == true) && (tilemap.HasTile(MP + Vector3Int.up) == false))
            {
                SetTilePro(MP, MPL1, MPL2);
                MPL1 = MP;
                MP.y++;
                if (tilemap.HasTile(MPL2 + Vector3Int.up) == true) MPL2.y++;
            }
            else if ((MP.x == Math.Abs(MP.y)) && (MP.x > 0) && (MP.y < 0) && (tilemap.HasTile(MP + Vector3Int.up) == true) && (tilemap.HasTile(MP + Vector3Int.left) == false))
            {
                SetTilePro(MP, MPL1, MPL2);
                MPL1 = MP;
                MP.x--;
            }
            else if ((MP.x == MP.y) && (MP.x < 0) && (MP.y < 0) && (tilemap.HasTile(MP + Vector3Int.right) == true) && (tilemap.HasTile(MP + Vector3Int.up) == false))
            {
                SetTilePro(MP, MPL1, MPL2);
                MPL1 = MP;
                MP.y++;
            }
            else if ((tilemap.HasTile(MP + Vector3Int.left) == true) && (tilemap.HasTile(MP + Vector3Int.down) == false) && (tilemap.HasTile(MP + Vector3Int.up) == true) && (MP.x > 0))
            {
                SetTilePro(MP, MPL1, MPL2);
                if ((Math.Abs(MP.y - 1) != MP.x) && (MP.x > 1))
                {
                    MPL2.y--;
                    //if ((MPL1.y == MPL1.x) && (MP.x > 1)) MPL2.y++;
                }
                MPL1 = MP;
                MP.y--;
            }
            else if ((tilemap.HasTile(MP + Vector3Int.up) == true) && (tilemap.HasTile(MP + Vector3Int.left) == false) && (tilemap.HasTile(MP + Vector3Int.right) == true) && (MP.y < 0))
            {
                SetTilePro(MP, MPL1, MPL2);
                if (((MP.x - 1) != MP.y) && (MP.y < -1))
                {
                    MPL2.x--;
                    //if ((Math.Abs(MPL1.y) == MPL1.x) && (MP.y < -1)) MPL2.x++;
                }
                MPL1 = MP;
                MP.x--;
            }
            else if ((tilemap.HasTile(MP + Vector3Int.right) == true) && (tilemap.HasTile(MP + Vector3Int.up) == false) && (tilemap.HasTile(MP + Vector3Int.down) == true) && (MP.x < 0))
            {
                SetTilePro(MP, MPL1, MPL2);
                if (MP.x < -1)
                {
                    if (tilemap.HasTile(MPL2 + Vector3Int.up) == true) MPL2.y++;
                    //if ((Math.Abs(MP.x) == MP.y + 1) && (MP.y > 0)) 
                        //MPL2.y--;
                    //if ((MPL1.y == MPL1.x) && (MP.x < -1)) MPL2.y--;
                }
                MPL1 = MP;
                MP.y++;
            }
            else if ((tilemap.HasTile(MP + Vector3Int.down) == true) && (tilemap.HasTile(MP + Vector3Int.right) == false) && (tilemap.HasTile(MP + Vector3Int.left) == true) && (MP.y > 0))
            {
                SetTilePro(MP, MPL1, MPL2);
                if ((MP.y != (MP.x + 1)) && (MP.y > 1))
                {
                    MPL2.x++;
                    //if ((MPL1.y == Math.Abs(MPL1.x)) && (MP.y > 1)) MPL2.x--;
                }
                MPL1 = MP;
                MP.x++;
            }
            else if ((MP.y - (Math.Abs(MP.x)) == 1) && (MP.x < 0) && (MP.y > 0) && (tilemap.HasTile(MP + Vector3Int.down) == true) && (tilemap.HasTile(MP + Vector3Int.right) == false))
            {
                SetTilePro(MP, MPL1, MPL2);
                MPL1 = MP;
                MP.x++;
            }
            else break;
            /*Debug.Log("MP:");
            Debug.Log(MP);
            Debug.Log("MPL1:");
            Debug.Log(MPL1);
            Debug.Log("MPL2:");
            Debug.Log(MPL2);*/
        }
    }

=======
>>>>>>> c9e8c67ec5caf7ed670db7e45f02c15e6550aa76
    private void Gen()
    {
        tilemap.ClearAllTiles();
        tilemap1.ClearAllTiles();
        tilemap2.ClearAllTiles();
        var MapPos = new Vector3Int(0, 0, 0);
        var tile0 = tiles[Random.Range(0, tiles.Length)];
        tilemap.SetTile(MapPos, tile0);
<<<<<<< HEAD
        MapPos = MapPos + Vector3Int.up;
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tilemap.GetTile(MapPos + Vector3Int.down) == tiles[i])
            {
                int r1, r2;
                if ((i == 0) || (i == 1)) r1 = 0;
                else r1 = i - 2;
                if ((i == tiles.Length) || (i == tiles.Length - 1)) r2 = tiles.Length;
                else r2 = i + 2;
                var tile = tiles[Random.Range(r1, r2)];
                tilemap.SetTile(MapPos, tile);
            }
        }
        MapPos = MapPos + Vector3Int.right;
        SpiralGen(MapPos);
        int rx = Random.Range(-20, 20);
        int ry = Random.Range(-20, 20);
=======
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
            }
        int rx = Random.Range(30, 70);
        int ry = Random.Range(30, 70);
>>>>>>> c9e8c67ec5caf7ed670db7e45f02c15e6550aa76
        MapPos.x = rx;
        MapPos.y = ry;
        var road = NW_NE_SW_SE;
        tilemap2.SetTile(MapPos, road);
        int r;
        MapPos.x = rx;
<<<<<<< HEAD
        for (MapPos.y = ry - 1; MapPos.y >= -50; MapPos = MapPos + Vector3Int.down)
=======
        for (MapPos.y = ry - 1; MapPos.y >= 0; MapPos = MapPos + Vector3Int.down)
>>>>>>> c9e8c67ec5caf7ed670db7e45f02c15e6550aa76
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
<<<<<<< HEAD
        for (MapPos.x = rx - 1; MapPos.x >= -50; MapPos = MapPos + Vector3Int.left)
=======
        for (MapPos.x = rx - 1; MapPos.x >= 0; MapPos = MapPos + Vector3Int.left)
>>>>>>> c9e8c67ec5caf7ed670db7e45f02c15e6550aa76
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
<<<<<<< HEAD
        for (MapPos.x = rx + 1; MapPos.x <= 50; MapPos = MapPos + Vector3Int.right)
=======
        for (MapPos.x = rx + 1; MapPos.x <= 100; MapPos = MapPos + Vector3Int.right)
>>>>>>> c9e8c67ec5caf7ed670db7e45f02c15e6550aa76
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
<<<<<<< HEAD
        for (MapPos.y = ry + 1; MapPos.y <= 50; MapPos = MapPos + Vector3Int.up)
=======
        for (MapPos.y = ry + 1; MapPos.y <= 100; MapPos = MapPos + Vector3Int.up)
>>>>>>> c9e8c67ec5caf7ed670db7e45f02c15e6550aa76
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
<<<<<<< HEAD
        for (MapPos.x = rx - 1; MapPos.x >= -50; MapPos = MapPos + Vector3Int.left)
            for (MapPos.y = ry - 1; MapPos.y >= -50; MapPos = MapPos + Vector3Int.down)
=======
        for (MapPos.x = rx - 1; MapPos.x >= 0; MapPos = MapPos + Vector3Int.left)
            for (MapPos.y = ry - 1; MapPos.y >= 0; MapPos = MapPos + Vector3Int.down)
>>>>>>> c9e8c67ec5caf7ed670db7e45f02c15e6550aa76
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
<<<<<<< HEAD
        for (MapPos.x = rx + 1; MapPos.x <= 50; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = ry - 1; MapPos.y >= -50; MapPos = MapPos + Vector3Int.down)
=======
        for (MapPos.x = rx + 1; MapPos.x <= 100; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = ry - 1; MapPos.y >= 0; MapPos = MapPos + Vector3Int.down)
>>>>>>> c9e8c67ec5caf7ed670db7e45f02c15e6550aa76
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
<<<<<<< HEAD
        for (MapPos.x = rx - 1; MapPos.x >= -50; MapPos = MapPos + Vector3Int.left)
            for (MapPos.y = ry + 1; MapPos.y <= 50; MapPos = MapPos + Vector3Int.up)
=======
        for (MapPos.x = rx - 1; MapPos.x >= 0; MapPos = MapPos + Vector3Int.left)
            for (MapPos.y = ry + 1; MapPos.y <= 100; MapPos = MapPos + Vector3Int.up)
>>>>>>> c9e8c67ec5caf7ed670db7e45f02c15e6550aa76
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
<<<<<<< HEAD
        for (MapPos.x = rx + 1; MapPos.x <= 50; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = ry + 1; MapPos.y <= 50; MapPos = MapPos + Vector3Int.up)
=======
        for (MapPos.x = rx + 1; MapPos.x <= 100; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = ry + 1; MapPos.y <= 100; MapPos = MapPos + Vector3Int.up)
>>>>>>> c9e8c67ec5caf7ed670db7e45f02c15e6550aa76
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
<<<<<<< HEAD
        for (MapPos.x = -50; MapPos.x <= 50; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = -50; MapPos.y <= 50; MapPos = MapPos + Vector3Int.up)
=======
        for (MapPos.x = 0; MapPos.x <= 100; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = 0; MapPos.y <= 100; MapPos = MapPos + Vector3Int.up)
>>>>>>> c9e8c67ec5caf7ed670db7e45f02c15e6550aa76
            {
                if ((Random.Range(0f, 100f) >= 99f) && (tilemap2.HasTile(MapPos) == false))
                {
                    var tile1 = tileRock;
                    tilemap1.SetTile(MapPos, tile1);
                }
            }
<<<<<<< HEAD
        for (MapPos.x = -50; MapPos.x <= 50; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = -50; MapPos.y <= 50; MapPos = MapPos + Vector3Int.up)
=======
        for (MapPos.x = 0; MapPos.x <= 100; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = 0; MapPos.y <= 100; MapPos = MapPos + Vector3Int.up)
>>>>>>> c9e8c67ec5caf7ed670db7e45f02c15e6550aa76
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
<<<<<<< HEAD
        for (MapPos.x = -50; MapPos.x <= 50; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = -50; MapPos.y <= 50; MapPos = MapPos + Vector3Int.up)
=======
        for (MapPos.x = 0; MapPos.x <= 100; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = 0; MapPos.y <= 100; MapPos = MapPos + Vector3Int.up)
>>>>>>> c9e8c67ec5caf7ed670db7e45f02c15e6550aa76
            {
                if ((Random.Range(0f, 100f) >= 99f) && (tilemap1.GetTile(MapPos) != tileRock) && (tilemap2.HasTile(MapPos) == false))
                {
                    var tile2 = puddles[Random.Range(0, puddles.Length)];
                    tilemap2.SetTile(MapPos, tile2);
                }
<<<<<<< HEAD
                if ((Random.Range(0f, 100f) >= 97f) && (tilemap1.HasTile(MapPos) == false) && (tilemap2.HasTile(MapPos) == false))
                { 
                    Instantiate(tree, IsoToN(MapPos), Quaternion.identity);
                }
            }

=======
            }
>>>>>>> c9e8c67ec5caf7ed670db7e45f02c15e6550aa76
    }
}