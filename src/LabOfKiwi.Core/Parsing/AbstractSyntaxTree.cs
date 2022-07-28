using LabOfKiwi.Collections;
using System.Collections.Generic;

namespace LabOfKiwi.Parsing;

public sealed class AbstractSyntaxTree
{
    private readonly List<Result> _results;

    internal AbstractSyntaxTree()
    {
        _results = new List<Result>();
    }

    public int Count
    {
        get => _results.Count;
        internal set => _results.SetLength(value);
    }

    internal void Add(Result result)
    {
        _results.Add(result);
    }

    public readonly struct Result
    {
        private readonly ParserParentNode _node;
        private readonly string? _value;

        internal Result(ParserParentNode node, string? value)
        {
            _node = node;
            _value = value;
        }

        public ParserNode Node => _node;

        public string? Value => _value;
    }
}