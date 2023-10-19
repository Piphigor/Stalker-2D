using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class ChatGptGen1 : MonoBehaviour
{
    private int mapWidth = 50;
    private int mapHeight = 50;
    private int newNoise = 0;

    public float noiseScale = 0.1f;
    float heightThreshold = -0.2f;

    public Tilemap tilemap;
    public Tile[] tiles;

    void Start()
    {
        GenerateTileMap();
    }

    private void Update()
    {
        if (Input.GetKeyUp("escape"))
        {
            Application.Quit();
        }
        if (Input.GetKeyUp("r"))
        {
            GenerateTileMap(); ;
        }
    }

    void GenerateTileMap()
    {
        //mapWidth = Random.Range(0, 500);
        //mapHeight = Random.Range(0, 500);
        newNoise = Random.Range(0, 10000);
        tilemap.ClearAllTiles();
        float[,] noiseMap = GenerateNoiseMap(mapWidth, mapHeight, noiseScale);

        for (int x = -50; x <= mapWidth; x++)
        {
            for (int y = -50; y <= mapHeight; y++)
            {
                float currentHeight = noiseMap[x + 50, y + 50];
                Vector3Int MapPos = new Vector3Int(x, y, 0);
                Tile selectedTile = ChooseTile(currentHeight);

                if (currentHeight > heightThreshold)
                {
                    // Устанавливаем тайл на Tilemap
                    tilemap.SetTile(MapPos, selectedTile);
                }
            }
        }
    }

    Tile ChooseTile(float height)
    {
        // Реализуйте свою логику выбора тайла в зависимости от значения высоты
        // Например, можно разбить высоты на диапазоны и возвращать определенные тайлы для каждого диапазона
        // Пример:
        if (height > 0.9f)
        {
            return tiles[5];
        }
        else if (height > 0.8f)
        {
            return tiles[4];
        }
        else if (height > 0.7f)
        {
            return tiles[3];
        }
        else if (height > 0.6f)
        {
            return tiles[2];
        }
        else if (height > 0.5f)
        {
            return tiles[1];
        }
        else if (height > 0.4f)
        {
            return tiles[0];
        }
        // Добавьте другие условия по необходимости

        // Если не соответствует ни одному условию, возвращаем null
        return null;
    }

    float[,] GenerateNoiseMap(int width, int height, float scale)
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
}
