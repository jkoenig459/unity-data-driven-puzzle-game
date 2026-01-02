using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridTriggerSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Tilemap triggersTilemap;
    [SerializeField] private Transform player;

    [Header("Trigger Tiles")]
    [SerializeField] private TileBase plateTile;

    [Header("Actions")]
    [SerializeField] private List<Door2D> doorsToToggle = new();

    private Vector3Int lastPlayerCell;
    private bool hasLastCell;

    private void Start()
    {
        if (triggersTilemap == null || player == null)
            return;

        lastPlayerCell = triggersTilemap.WorldToCell(player.position);
        hasLastCell = true;
    }

    private void Update()
    {
        if (triggersTilemap == null || player == null)
            return;

        Vector3Int cell = triggersTilemap.WorldToCell(player.position);

        if (hasLastCell && cell == lastPlayerCell)
            return;

        lastPlayerCell = cell;
        hasLastCell = true;

        HandleCellEntered(cell);
    }

    private void HandleCellEntered(Vector3Int cell)
    {
        TileBase tile = triggersTilemap.GetTile(cell);
        if (tile == null)
            return;

        if (plateTile != null && tile == plateTile)
        {
            foreach (var door in doorsToToggle)
            {
                if (door != null)
                    door.Toggle();
            }
        }
    }
}
