using UnityEngine;

public class ButtonGrid
{
    public int CellX => _cellX;
    public int CellY => _cellY;
    public Vector3 Pos => _view.transform.position;

    public bool IsOpen
    {
        get => _isOpen;
        set
        {
            _isOpen = value;
            _view.Active(value);
        }
    }
    
    public bool IsKey
    {
        get => _isKey;
        set => _isKey = value;
    }
    
    private int _cellX, _cellY;
    private bool _isOpen, _isKey;
    
    private ButtonGridView _view;
    
    public ButtonGrid(int cellX, int cellY, ButtonGridView view)
    {
        _cellX = cellX;
        _cellY = cellY;
        _view = view;
        IsOpen = false;
        _isKey = false;
    }
}
