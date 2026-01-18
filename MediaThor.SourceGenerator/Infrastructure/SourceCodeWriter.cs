using System;
using System.Text;

namespace MediaThor.SourceGenerator.Infrastructure;

public sealed class SourceCodeWriter
{
    private sealed class ScopeDisposable(SourceCodeWriter codeWriter)
        : IDisposable
    {
        public void Dispose()
        {
            codeWriter.IndentationLevel--;
            codeWriter.AppendLine(ClosedBracket);
        }
    }

    private const char IndentationChar = ' ';
    private const char NewLineChar = '\n';
    private const char OpenBracket = '{', ClosedBracket = '}';

    private readonly StringBuilder _sb = new();

    private readonly byte _indentationCharCount;
    private readonly ScopeDisposable _scopeDisposable;

    public SourceCodeWriter(byte indentationCharCount = 4)
    {
        _indentationCharCount = indentationCharCount;
        _scopeDisposable = new ScopeDisposable(this);
    }

    public ushort IndentationLevel { get; set; }

    public SourceCodeWriter AppendIndent()
    {
        _sb.Append(IndentationChar, IndentationLevel * _indentationCharCount);

        return this;
    }

    public SourceCodeWriter Append(string content)
    {
        _sb.Append(content);

        return this;
    }

    public SourceCodeWriter Append(string content, params object[] args) =>
        Append(string.Format(content, args));

    public SourceCodeWriter Append(char c)
    {
        _sb.Append(c);

        return this;
    }

    public SourceCodeWriter AppendLine(string content)
    {
        AppendIndent();
        _sb.AppendLine(content);

        return this;
    }

    public SourceCodeWriter AppendLine(string content, params object[] args) =>
        AppendLine(string.Format(content, args));

    public SourceCodeWriter AppendLine(char c)
    {
        AppendIndent();

        _sb
            .Append(c)
            .Append(NewLineChar);

        return this;
    }

    public SourceCodeWriter AppendLine()
    {
        _sb.AppendLine();

        return this;
    }

    public SourceCodeWriter AppendIndentedLine(char c)
    {
        IndentationLevel++;

        AppendLine(c)
            .IndentationLevel--;

        return this;
    }

    public SourceCodeWriter AppendIndentedLine(string content)
    {
        IndentationLevel++;

        AppendLine(content)
            .IndentationLevel--;

        return this;
    }

    public IDisposable CreateScope()
    {
        AppendIndent();

        _sb
            .Append(OpenBracket)
            .Append(NewLineChar);

        IndentationLevel++;

        return _scopeDisposable;
    }

    public override string ToString() =>
        _sb.ToString();
}