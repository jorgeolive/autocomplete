using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Olive.Autocomplete.Core.TernaryTreeImpl;
using Olive.Autocomplete.Core.TrieImpl;
using System.Reflection;

var results = BenchmarkRunner.Run<DictionarySearchTest>();


[MemoryDiagnoser]
public class DictionaryLoadTest
{
    private string[] _data;

    [GlobalSetup]
    public async Task Init()
    {
        _data = await File.ReadAllLinesAsync($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/words.txt");
    }

    [Benchmark]
    public void LoadDataTernary()
    {
        var trie = new TernaryTree();

        foreach (var line in _data)
        {
            trie.AddWord(line);
        }
    }
}

[MemoryDiagnoser]
public class DictionarySearchTest
{
    private string[] _data;
    private Trie _trie;
    private TernaryTree _trieTree;

    [Params("cl", "clo", "clou", "cloud")]
    public string SearchText { get; set; }

    [GlobalSetup]
    public void Init()
    {
        _trie = new Trie();
        _trieTree = new TernaryTree();
        _data = File.ReadAllLines($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/words.txt");

        foreach (var line in _data)
        {
            _trie.AddWord(line);
            _trieTree.AddWord(line);
        }
    }

    [Benchmark]
    public void SearchRandomWordTrie()
    {
        _trie.GetSuggestionsFor(SearchText);
    }

    [Benchmark]
    public void SearchRandomWordTernaryTree()
    {
        _trieTree.GetSuggestionsFor(SearchText);
    }
}