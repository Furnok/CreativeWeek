using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_DynamicGrid : MonoBehaviour
{
    [SerializeField] GridLayoutGroup _gridLayout;
    [SerializeField] RectTransform _content;
    [SerializeField] int _rowCount = 2;

    void Start()
    {
        AdjustCellSize();
    }

    void Update()
    {
        AdjustCellSize();
    }

    void AdjustCellSize()
    {
        if (_gridLayout == null || _content == null) return;

        int totalElements = _content.childCount;

        int columnCount = Mathf.CeilToInt((float)totalElements / _rowCount);
        columnCount = Mathf.Max(columnCount, 1);

        float contentWidth = _content.rect.width;
        float contentHeight = _content.rect.height;

        float spacingX = _gridLayout.spacing.x * (columnCount - 1);
        float spacingY = _gridLayout.spacing.y * (_rowCount - 1);

        float cellWidth = (contentWidth - _gridLayout.padding.left - _gridLayout.padding.right - spacingX) / columnCount;
        float cellHeight = (contentHeight - _gridLayout.padding.top - _gridLayout.padding.bottom - spacingY) / _rowCount;

        _gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
    }
}
