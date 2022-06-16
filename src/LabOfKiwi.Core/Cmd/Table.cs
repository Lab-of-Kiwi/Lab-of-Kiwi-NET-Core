using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LabOfKiwi.Cmd;

public class Table : ViewElement
{
    #region Fields
    private ICell[][] _cells;
    private SizeOption[] _columnSizes;
    private SizeOption[] _rowSizes;
    private TableBorder _tableBorder;
    #endregion

    public Table(int rows = 0, int columns = 0)
    {
        ThrowHelper.RangeCheck(nameof(rows), rows, minValue: 0);
        ThrowHelper.RangeCheck(nameof(columns), columns, minValue: 0);

        _cells = new ICell[rows][];

        for (int r = 0; r < rows; r++)
        {
            _cells[r] = new ICell[columns];

            for (int c = 0; c < columns; c++)
            {
                _cells[r][c] = new Cell(this, r, c);
            }
        }

        _rowSizes = new SizeOption[rows];
        _columnSizes = new SizeOption[columns];

        RowCount = rows;
        ColumnCount = columns;
    }

    #region Properties
    public Cell this[int row, int column]
    {
        get
        {
            if ((uint)row >= (uint)RowCount || (uint)column >= (uint)ColumnCount)
            {
                throw new IndexOutOfRangeException();
            }

            ICell cell = _cells[row][column];

            if (cell is CellExtension ecell)
            {
                return ecell.Parent;
            }

            Debug.Assert(cell is Cell);
            return (Cell)cell;
        }
    }

    public int ColumnCount { get; private set; }

    public int RowCount { get; private set; }
    #endregion

    #region Public Methods
    public Table AddColumn()
    {
        Array.Resize(ref _columnSizes, ColumnCount + 1);

        for (int r = 0; r < RowCount; r++)
        {
            ICell[] row = _cells[r];
            Array.Resize(ref row, ColumnCount + 1);
            row[ColumnCount] = new Cell(this, r, ColumnCount);
            _cells[r] = row;
        }

        ColumnCount++;
        return this;
    }

    public Table AddColumns(int count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        for (int i = 0; i < count; i++)
        {
            AddColumn();
        }

        return this;
    }

    public Table AddRow()
    {
        Array.Resize(ref _cells, RowCount + 1);
        Array.Resize(ref _rowSizes, RowCount + 1);
        _cells[RowCount] = new ICell[ColumnCount];

        for (int c = 0; c < ColumnCount; c++)
        {
            _cells[RowCount][c] = new Cell(this, RowCount, c);
        }

        RowCount++;
        return this;
    }

    public Table AddRows(int count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        for (int i = 0; i < count; i++)
        {
            AddRow();
        }

        return this;
    }

    public Table SetColumnWidth(int column, string value)
    {
        if ((uint)column >= (uint)ColumnCount)
        {
            throw new IndexOutOfRangeException();
        }

        ThrowHelper.NullCheck(nameof(value), value);

        if (!SizeOption.TryParse(value, out SizeOption result))
        {
            throw new ArgumentException("Invalid width value: " + value, nameof(value));
        }

        _columnSizes[column] = result;
        return this;
    }

    public Table SetRowHeight(int row, string value)
    {
        if ((uint)row >= (uint)RowCount)
        {
            throw new IndexOutOfRangeException();
        }

        ThrowHelper.NullCheck(nameof(value), value);

        if (!SizeOption.TryParse(value, out SizeOption result))
        {
            throw new ArgumentException("Invalid height value: " + value, nameof(value));
        }

        _rowSizes[row] = result;
        return this;
    }

    public Table SetTableBorder(BorderType outerBorder, BorderType innerBorder)
    {
        _tableBorder = new TableBorder(outerBorder, innerBorder);
        return this;
    }
    #endregion

    #region Internal Methods
#if DEBUG
    public void DebugDump()
    {
        for (int i = 0 ; i < _cells.Length ; i++)
        {
            var row = _cells[i];

            Debug.Assert(row != null);

            for (int j = 0; j < row.Length; j++)
            {
                char c = row[j] is CellExtension ? '.' : 'x';
                Console.Write(c);
            }

            Console.WriteLine();
        }
    }
#endif

    protected override List<string> InternalContentRows(int allocatedWidth, int? allocatedHeight, TextAlignOption textAlignment)
    {
        throw new NotImplementedException();
    }

    private void UpdateColumnSpans(Cell cell, int newColSpan)
    {
        int origColSpan = cell.ColumnSpan;

        if (origColSpan == newColSpan) return;

        int cellRow = cell.Row;
        int cellColumn = cell.Column;
        int rowSpan = cell.RowSpan;
        bool isAdding = newColSpan > origColSpan;

        if (isAdding)
        {
            for (int c = origColSpan; c < newColSpan; c++)
            {
                int thisCol = cellColumn + c;

                // Verify first before doing anything
                for (int r = 0; r < rowSpan; r++)
                {
                    int thisRow = cellRow + r;
                    ICell thisCell = _cells[thisRow][thisCol];

                    if (thisCell is CellExtension cellExt && cellExt.Parent != cell)
                    {
                        throw new InvalidOperationException("Cell space already claimed by another cell.");
                    }
                }

                for (int r = 0; r < rowSpan; r++)
                {
                    int thisRow = cellRow + r;
                    ICell thisCell = _cells[thisRow][thisCol];

                    if (thisCell is Cell actualCell)
                    {
                        actualCell.Delete();
                    }

                    _cells[thisRow][thisCol] = new CellExtension(cell, thisRow, thisCol);
                }
            }
        }
        else
        {
            for (int c = newColSpan; c < origColSpan; c++)
            {
                int thisCol = cellColumn + c;

                for (int r = 0; r < rowSpan; r++)
                {
                    int thisRow = cellRow + r;
                    _cells[thisRow][thisCol] = new Cell(this, thisRow, thisCol);
                }
            }
        }
    }

    private void UpdateRowSpans(Cell cell, int newRowSpan)
    {
        int origRowSpan = cell.RowSpan;

        if (origRowSpan == newRowSpan) return;

        int cellRow = cell.Row;
        int cellColumn = cell.Column;
        int colSpan = cell.ColumnSpan;
        bool isAdding = newRowSpan > origRowSpan;

        if (isAdding)
        {
            for (int r = origRowSpan; r < newRowSpan; r++)
            {
                int thisRow = cellRow + r;

                // Verify first before doing anything
                for (int c = 0; c < colSpan; c++)
                {
                    int thisCol = cellColumn + c;
                    ICell thisCell = _cells[thisRow][thisCol];

                    if (thisCell is CellExtension cellExt && cellExt.Parent != cell)
                    {
                        throw new InvalidOperationException("Cell space already claimed by another cell.");
                    }
                }

                for (int c = 0; c < colSpan; c++)
                {
                    int thisCol = cellColumn + c;
                    ICell thisCell = _cells[thisRow][thisCol];

                    if (thisCell is Cell actualCell)
                    {
                        actualCell.Delete();
                    }

                    _cells[thisRow][thisCol] = new CellExtension(cell, thisRow, thisCol);
                }
            }
        }
        else
        {
            for (int r = newRowSpan; r < origRowSpan; r++)
            {
                int thisRow = cellRow + r;

                for (int c = 0; c < colSpan; c++)
                {
                    int thisCol = cellColumn + c;
                    _cells[thisRow][thisCol] = new Cell(this, thisRow, thisCol);
                }
            }
        }
    }
    #endregion

    #region Types
    internal interface ICell
    {
        int Row { get; }

        int Column { get; }
    }

    public sealed class Cell : ICell
    {
        private readonly int _row;
        private readonly int _column;

        private Table _table;
        private int _colSpan = 1;
        private int _rowSpan = 1;
        private ViewElement? _content;

        internal Cell(Table table, int row, int column)
        {
            _table = table;
            _row = row;
            _column = column;
        }

        internal ViewElement? Content => _content;

        public Table Table
        {
            get
            {
                ThrowIfDeleted();
                return _table;
            }
        }

        public int Row
        {
            get
            {
                ThrowIfDeleted();
                return _row;
            }
        }

        public int Column
        {
            get
            {
                ThrowIfDeleted();
                return _column;
            }
        }

        public int RowSpan
        {
            get
            {
                ThrowIfDeleted();
                return _rowSpan;
            }

            set
            {
                ThrowIfDeleted();

                if (_rowSpan == value)
                {
                    return;
                }

                if (value < 1 || _row + value > _table.RowCount)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _table.UpdateRowSpans(this, value);
                _rowSpan = value;
            }
        }

        public int ColumnSpan
        {
            get
            {
                ThrowIfDeleted();
                return _colSpan;
            }

            set
            {
                ThrowIfDeleted();

                if (_colSpan == value)
                {
                    return;
                }

                if (value < 1 || _column + value > _table.ColumnCount)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _table.UpdateColumnSpans(this, value);
                _colSpan = value;
            }
        }

        public Cell SetContent(ViewElement? content)
        {
            _content = content;
            return this;
        }

        public Cell SetContent(string? content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                _content = new TextBlock().Add(content);
            }

            return this;
        }

        internal void Delete()
        {
            _table = null!;
        }

        private void ThrowIfDeleted()
        {
            if (_table == null)
            {
                throw new InvalidOperationException("Table cell has been deleted.");
            }
        }
    }

    internal sealed class CellExtension : ICell
    {
        public CellExtension(Cell parent, int row, int column)
        {
            Parent = parent;
            Row = row;
            Column = column;
        }

        public Cell Parent { get; }

        public int Row { get; }

        public int Column { get; }
    }
    #endregion
}
