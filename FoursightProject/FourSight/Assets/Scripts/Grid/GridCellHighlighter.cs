using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GridCellHighlighter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color highlightColor = Color.cyan;
    public Color posColor = Color.green;
    public Color negColor = Color.red;
    private Color originalColor;
    public GridCell gridCell;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        gridCell = GetComponent<GridCell>();
    }

    // When the mouse enters the collider area
    void OnMouseEnter()
    {
        Debug.Log("e");
        if (!GameManager.Instance.PlayingCard)
        {
            spriteRenderer.color = highlightColor;
        }
        else if(gridCell.cellFull || gridCell.gridIndex.x > 1)
        {
            spriteRenderer.color = negColor;
        }
        else
        {
            spriteRenderer.color = posColor;
        }
    }

    // When the mouse exits the collider area
    void OnMouseExit()
    {
        spriteRenderer.color = originalColor;
    }
}
