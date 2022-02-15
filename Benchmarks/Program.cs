using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Olive.Autocomplete.Core.TrieImpl;
using System.Reflection;

var results = BenchmarkRunner.Run<DictionaryLoadTest>();


[MemoryDiagnoser]
public class DictionaryLoadTest
{
    private string[] _data;

    [GlobalSetup]
    public void Init()
    {
        _data = Task.Run( async () => await File.ReadAllLinesAsync($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/words.txt")).Result;
    }

    [Benchmark]
    public void LoadData()
    {
        var trie = new Trie();

        foreach (var line in _data)
            trie.AddWord(line);
    }
}

[MemoryDiagnoser]
public class DictionarySearchTest
{
    private string[] _data;
    private Trie _trie;

    [GlobalSetup]
    public void Init()
    {
        _trie = new Trie();
        _data = File.ReadAllLines($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/words.txt");

        foreach (var line in _data)
            _trie.AddWord(line);

    }

    [Benchmark]
    public void SearchRandomWord()
    {
        var randomWordIndex = Random.Shared.Next(0, _data.Length - 1);
        _trie.GetSuggestionsFor(_data[randomWordIndex].Substring(0, _data[randomWordIndex].Length /2));
    }
}