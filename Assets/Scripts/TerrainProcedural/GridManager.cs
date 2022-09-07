using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

//[ExecuteInEditMode]
public class GridManager : MonoBehaviour
{
    [SerializeField] private int rows = 32;
    [SerializeField] private int columns = 24;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform cam;

    private Tile[,] grid;
    private PixelPerfectCamera pixelPerfectCam;

    private void Awake()
    {
        pixelPerfectCam = cam.GetComponent<PixelPerfectCamera>();    
    }

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new Tile[rows, columns];
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                Tile spawnedTile = Instantiate(tilePrefab, new Vector3(x + gameObject.transform.position.x, y + gameObject.transform.position.y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                // Checkers effect
                bool isOffset =
                    (x % 2 == 0 && y % 2 != 0) ||
                    (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);

                spawnedTile.gameObject.transform.parent = gameObject.transform;
                grid[x, y] = spawnedTile;
            }
        }

        // Center Grid to cam
        // 0.5f = half of asset size
        const float halfAssetUnit = 0.5f;
        transform.position = new Vector3(transform.position.x - pixelPerfectCam.refResolutionX / pixelPerfectCam.assetsPPU / 2 + halfAssetUnit, transform.position.y - columns / 2 + halfAssetUnit);
    }

    public Tile GetTileAtPosition(Vector2 pos /*int tileId*/)
    {
        return grid[(int)pos.x, (int)pos.y];
    }
}