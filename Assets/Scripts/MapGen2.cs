using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using NavMeshPlus.Components;
using System.Text.RegularExpressions;

public class MapGen2 : MonoBehaviour
{
    public Grid grid;
    public Tile[] tiles, puddles;
    public Tile NE_SW, NE_SE, NE_SW_SE, NW_NE, NW_NE_SE, NW_NE_SW, NW_NE_SW_SE, NW_SE, NW_SW, NW_SW_SE, SW_SE;
    public Tilemap tilemap, tilemap1, tilemap2, tilemap3;
    public Tile tileRock, tileRocks;
    public Tile tileTree;
    public Tile[] Grass;
    public Transform tree;
    public Transform treeTriggerCollider;
    public GameObject mc;
    public GameObject boar;
    public GameObject layout;
    public GameObject navmesh;
    private int mapWidth = 50;
    private int mapHeight = 50;
    private int newNoise = 0;
    public float noiseScale = 0.1f;
    float heightThreshold = 0.0f;

    private void Awake()
    {
        //Cursor.visible = false;
        Gen();
    }

    void Start()
    {
        navmesh.GetComponent<NavMeshSurface>().BuildNavMeshAsync();
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
        navmesh.GetComponent<NavMeshSurface>().UpdateNavMesh(navmesh.GetComponent<NavMeshSurface>().navMeshData);
        navmesh.GetComponent<NavMeshSurface>().RemoveData();
    }

    private Vector3 IsoToN(Vector3Int mps)
    {
        float row = mps.x + mps.y + 1;
        float column = mps.x - mps.y;

        row /= 4;
        column /= 2;

        return new Vector3(column, row, 0);
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
                        if (i <= 4) r1 = 0;
                        else r1 = i - 5;
                        if (i >= tiles.Length - 4) r2 = tiles.Length;
                        else r2 = i + 5;
                        var tile = tiles[Random.Range(r1, r2)];
                        tilemap.SetTile(MP, tile);
                    }
                    else if (i > j)
                    {
                        if (j <= 4) r1 = 0;
                        else r1 = j - 5;
                        if (i >= tiles.Length - 4) r2 = tiles.Length;
                        else r2 = i + 5;
                        var tile = tiles[Random.Range(r1, r2)];
                        tilemap.SetTile(MP, tile);
                    }
                    else if (i < j)
                    {
                        if (i <= 4) r1 = 0;
                        else r1 = i - 5;
                        if (j >= tiles.Length - 4) r2 = tiles.Length;
                        else r2 = j + 5;
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

    private Tile ChooseTile(float height)
    {
        // Реализуйте свою логику выбора тайла в зависимости от значения высоты
        // Например, можно разбить высоты на диапазоны и возвращать определенные тайлы для каждого диапазона
        // Пример:
        if (height > 0.9f)
        {
            return Grass[5];
        }
        else if (height > 0.8f)
        {
            return Grass[4];
        }
        else if (height > 0.7f)
        {
            return Grass[3];
        }
        else if (height > 0.6f)
        {
            return Grass[2];
        }
        else if (height > 0.5f)
        {
            return Grass[1];
        }
        else if (height > 0.4f)
        {
            return Grass[0];
        }
        // Добавьте другие условия по необходимости

        // Если не соответствует ни одному условию, возвращаем null
        return null;
    }

    private float[,] GenerateNoiseMap(int width, int height, float scale)
    {
        int Width = 2 * width + 1;
        int Height = 2 * height + 1;

        float[,] noiseMap = new float[Width, Height];

        // Генерация шума Перлина
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                int X = x - 50;
                int Y = y - 50;
                float xCoord = X * scale;
                float yCoord = Y * scale;
                float dnewNoise = newNoise * scale;
                float sample = Mathf.PerlinNoise(xCoord + dnewNoise, yCoord + dnewNoise);
                noiseMap[x, y] = sample;
            }
        }

        return noiseMap;
    }


    private void Gen()
    {
        navmesh.GetComponent<NavMeshSurface>().RemoveData();
        tilemap.ClearAllTiles();
        tilemap1.ClearAllTiles();
        tilemap2.ClearAllTiles();
        tilemap3.ClearAllTiles();
        Destroy(GameObject.Find("Tilemap (3)(Clone)"));
        while (GameObject.FindWithTag("Tree") != null)
            DestroyImmediate(GameObject.FindWithTag("Tree"));
        while (GameObject.FindWithTag("Boar") != null)
            DestroyImmediate(GameObject.FindWithTag("Boar"));
        newNoise = Random.Range(0, 10000);
        float[,] noiseMap = GenerateNoiseMap(mapWidth, mapHeight, noiseScale);
        var MapPos = new Vector3Int(0, 0, 0);
        var tile0 = tiles[Random.Range(0, tiles.Length)];
        tilemap.SetTile(MapPos, tile0);
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
        MapPos.x = rx;
        MapPos.y = ry;
        var road = NW_NE_SW_SE;
        tilemap2.SetTile(MapPos, road);
        int r;
        MapPos.x = rx;
        for (MapPos.y = ry - 1; MapPos.y >= -50; MapPos = MapPos + Vector3Int.down)
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
        for (MapPos.x = rx - 1; MapPos.x >= -50; MapPos = MapPos + Vector3Int.left)
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
        for (MapPos.x = rx + 1; MapPos.x <= 50; MapPos = MapPos + Vector3Int.right)
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
        for (MapPos.y = ry + 1; MapPos.y <= 50; MapPos = MapPos + Vector3Int.up)
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
        for (MapPos.x = rx - 1; MapPos.x >= -50; MapPos = MapPos + Vector3Int.left)
            for (MapPos.y = ry - 1; MapPos.y >= -50; MapPos = MapPos + Vector3Int.down)
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
        for (MapPos.x = rx + 1; MapPos.x <= 50; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = ry - 1; MapPos.y >= -50; MapPos = MapPos + Vector3Int.down)
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
        for (MapPos.x = rx - 1; MapPos.x >= -50; MapPos = MapPos + Vector3Int.left)
            for (MapPos.y = ry + 1; MapPos.y <= 50; MapPos = MapPos + Vector3Int.up)
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
        for (MapPos.x = rx + 1; MapPos.x <= 50; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = ry + 1; MapPos.y <= 50; MapPos = MapPos + Vector3Int.up)
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
        for (MapPos.x = -50; MapPos.x <= 50; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = -50; MapPos.y <= 50; MapPos = MapPos + Vector3Int.up)
            {
                if ((Random.Range(0f, 100f) >= 99f) && (tilemap2.HasTile(MapPos) == false))
                {
                    var tile1 = tileRock;
                    tilemap1.SetTile(MapPos, tile1);
                }
            }
        for (MapPos.x = -50; MapPos.x <= 50; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = -50; MapPos.y <= 50; MapPos = MapPos + Vector3Int.up)
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
        for (MapPos.x = -50; MapPos.x <= 50; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = -50; MapPos.y <= 50; MapPos = MapPos + Vector3Int.up)
            {
                if ((Random.Range(0f, 100f) >= 99f) && (tilemap1.GetTile(MapPos) != tileRock) && (tilemap2.HasTile(MapPos) == false))
                {
                    var tile2 = puddles[Random.Range(0, puddles.Length)];
                    tilemap2.SetTile(MapPos, tile2);
                }

            }
        for (MapPos.x = -50; MapPos.x <= 50; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = -50; MapPos.y <= 50; MapPos = MapPos + Vector3Int.up)
            {
                if (/*(Random.Range(0f, 100f) >= 85f) && */(tilemap1.HasTile(MapPos) == false) && (tilemap2.HasTile(MapPos) == false) && (tilemap3.HasTile(MapPos) == false))
                {
                    float currentHeight = noiseMap[MapPos.x + 50, MapPos.y + 50];
                    Color clr = Color.white;
                    Texture2D txtr;
                    Sprite sprt;
                    Tile tile3 = ChooseTile(currentHeight);
                    if (tile3 != null)
                    {
                        sprt = tilemap.GetSprite(MapPos);
                        txtr = sprt.texture;
                        clr = txtr.GetPixel(100, 50);
                        clr.g = clr.g + 0.2f;
                        clr.r = clr.r + 0.1f;
                        tile3.color = clr;
                        if (currentHeight > heightThreshold)
                        {
                            tilemap3.SetTile(MapPos, tile3);
                        }
                        tile3.color = Color.white;
                    }
                }
            }
        for (MapPos.x = -50; MapPos.x <= 50; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = -50; MapPos.y <= 50; MapPos = MapPos + Vector3Int.up)
            {
                /*if ((Random.Range(0f, 100f) >= 98f) && (tilemap1.HasTile(MapPos) == false) && (tilemap2.HasTile(MapPos) == false))
                {
                    var tile3 = tileTree;
                    tilemap3.SetTile(MapPos, tile3);
                    Instantiate(treeTriggerCollider, IsoToN(MapPos), Quaternion.identity);
                }*/
                if ((Random.Range(0f, 100f) >= 98f) && (tilemap1.HasTile(MapPos) == false) && (tilemap2.HasTile(MapPos) == false) && (tilemap3.HasTile(MapPos) == false))
                {
                    Instantiate(tree, IsoToN(MapPos), Quaternion.identity, layout.transform);
                }
            }
        for (MapPos.x = -50; MapPos.x <= 50; MapPos = MapPos + Vector3Int.right)
            for (MapPos.y = -50; MapPos.y <= 50; MapPos = MapPos + Vector3Int.up)
            {
                if (Random.Range(0f, 100f) >= 99.9f)
                    Instantiate(boar, IsoToN(MapPos), Quaternion.identity);
            }
        /*Tilemap tilemap4 = Instantiate(tilemap3, tilemap3.transform.localPosition, Quaternion.identity);
        tilemap4.transform.SetParent(grid.transform);
        tilemap4.transform.position = tilemap4.transform.position + new Vector3(0, 1, 0);
        TilemapCollider2D tcollider = tilemap4.GetComponent<TilemapCollider2D>();
        tcollider.isTrigger = true;
        TilemapRenderer trenderer3 = tilemap3.GetComponent<TilemapRenderer>();
        TilemapRenderer trenderer4 = tilemap4.GetComponent<TilemapRenderer>();
        trenderer3.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        trenderer4.enabled = false;*/
    }
}