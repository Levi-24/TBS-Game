using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterMovement : MonoBehaviour
{
    public Tilemap tilemap;
    [SerializeField] GameObject character;
    [SerializeField] Sprite boat;
    [SerializeField] Sprite knight;

    public float moveSpeed = 5f;
    public float constantZ = 3f;
    private Vector3Int currentTilePosition;
    private bool isMoving = false;

    private void Start()
    {
        currentTilePosition = tilemap.WorldToCell(transform.position);
        // Set initial z position
        transform.position = new Vector3(transform.position.x, transform.position.y, constantZ);
    }

    private void Update()
    {
        if (!isMoving && Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int targetTilePosition = tilemap.WorldToCell(mouseWorldPosition);

            if (IsWithinMoveRange(targetTilePosition))
            {
                TileBase tile = tilemap.GetTile(targetTilePosition);
                bool water = tilemap.GetTile(targetTilePosition).name.Contains("watertile");

                if (tile == null || water)
                {
                    Debug.Log("Beszoptad");
                }
                else
                {
                    MoveToTile(targetTilePosition);
                }
            }
        }
    }

    private bool IsWithinMoveRange(Vector3Int targetTilePosition)
    {
        return Vector3Int.Distance(currentTilePosition, targetTilePosition) <= 6;
    }

    private void MoveToTile(Vector3Int targetTilePosition)
    {
        Vector3 targetWorldPosition = tilemap.GetCellCenterWorld(targetTilePosition);
        targetWorldPosition.z = constantZ;
        StartCoroutine(MoveCoroutine(targetWorldPosition));
        currentTilePosition = targetTilePosition;
    }

    private IEnumerator MoveCoroutine(Vector3 targetPosition)
    {
        isMoving = true;
        while (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, constantZ), targetPosition) > 0.01f)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            newPosition.z = constantZ;
            transform.position = newPosition;


            Vector3 worldPosition = transform.position;

            // Convert the world position to grid (cell) position
            Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

            // Retrieve the tile at the grid position
            TileBase tile = tilemap.GetTile(gridPosition);

            if(tile.name == "watertile")
            {
                character.GetComponent<SpriteRenderer>().sprite = boat;
            }
            else
            {
                character.GetComponent<SpriteRenderer>().sprite = knight;
            }


            yield return null;
        }
        transform.position = new Vector3(targetPosition.x, targetPosition.y, constantZ);
        isMoving = false;
    }
}