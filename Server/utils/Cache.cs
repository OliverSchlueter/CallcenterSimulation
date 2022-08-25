namespace Server.Utils;

public class Cache<I, E>
{
    private const int TimeInCache = 60;
    private const int MaxElementsInCache = 200;
    
    private readonly Dictionary<I, E> _cache;

    private readonly Dictionary<string, Dictionary<dynamic, List<E>>> _indexes;
    private readonly Dictionary<string, Type> _indexTypes;
    
    private readonly bool _useLFU;
    private readonly LinkedList<E> _leastFrequentlyUsed; // supporting LFU and LRU cache combined, since it's a doubly linked list
    public LinkedList<E> LeastFrequentlyUsed => _leastFrequentlyUsed;

    private readonly bool _autoRemove;
    private readonly Dictionary<I, int> _cacheTimes;
    private readonly Thread _cacheThread;

    public Cache(bool useLFU = false, bool autoRemove = false)
    {
        _autoRemove = autoRemove;
        _cache = new Dictionary<I, E>();
        _cacheTimes = new Dictionary<I, int>();
        _indexes = new Dictionary<string, Dictionary<dynamic, List<E>>>();
        _indexTypes = new Dictionary<string, Type>();
        _useLFU = useLFU;

        _leastFrequentlyUsed = new LinkedList<E>();

        _cacheThread = new Thread(CacheThread);
        _cacheThread.Start();
    }

    ~Cache()
    {
        _cacheThread.Abort();
    }

    private void CacheThread()
    {
        while (true)
        {
            // clear cache if max amount is reached
            bool removeAll = _cacheTimes.Count > MaxElementsInCache;

            foreach (I key in _cacheTimes.Keys)
            {
                int current = --_cacheTimes[key];

                if (removeAll || current == 0)
                    Remove(key);
                else
                    _cacheTimes[key] = current;
            }
            
            Thread.Sleep(1000);
        }
    }

    public void Put(I identifier, E element, bool autoRemove = false)
    {
        if (_cache.ContainsKey(identifier))
            _cache.Remove(identifier);

        if (_cacheTimes.ContainsKey(identifier))
            _cacheTimes.Remove(identifier);
        
        _cache.Add(identifier, element);
        
        if(_autoRemove || autoRemove) 
            _cacheTimes.Add(identifier, TimeInCache);
        
        if(_useLFU) 
            _leastFrequentlyUsed.AddFirst(element);
    }

    public void Remove(I identifier)
    {
        if (!Contains(identifier))
            return;

        if(_useLFU) 
            _leastFrequentlyUsed.Remove(Get(identifier));

        _cache.Remove(identifier);
        _cacheTimes.Remove(identifier);
    }

    public E Get(I identifier)
    {
        if (Contains(identifier))
        {
            E element = _cache[identifier];
            
            // Moving the element to the top
            if (_useLFU)
            {
                _leastFrequentlyUsed.Remove(element);
                _leastFrequentlyUsed.AddFirst(element);
            }
            
            return element;    
        }
        
        throw new NullReferenceException();
    }

    public List<E> GetAll()
    {
        List<E> list = new List<E>();
        foreach (var value in _cache.Values)
            list.Add(value);
        
        return list;
    }

    public bool Contains(I identifier)
    {
        return _cache.ContainsKey(identifier);
    }

    public void AutoRemove(I identifier, int time = TimeInCache)
    {
        if (!_cache.ContainsKey(identifier) || _cacheTimes.ContainsKey(identifier))
            return;
        
        _cacheTimes.Add(identifier, time);
    }

    public void Clear()
    {
        _cache.Clear();
        _cacheTimes.Clear();
        _leastFrequentlyUsed.Clear();
    }

    /// <summary>
    /// Create multiple indexes for faster searching
    /// </summary>
    /// <param name="func">Function to map new index -> element</param>
    /// <param name="indexName">Name of the new index</param>
    /// <typeparam name="S">Index identifier type</typeparam>
    public void AddIndex<T>(string indexName, Func<E, T> mapFunc)
    {
        Dictionary<object, List<E>> index = new Dictionary<object, List<E>>();
        
        foreach (var kvp in _cache)
        {
            object identifier = mapFunc(kvp.Value);
            E element = kvp.Value;
            
            if (index.ContainsKey(identifier))
            {
                index[identifier].Add(element);   
            }
            else
            {
                List<E> list = new List<E>();
                list.Add(element);
                index.Add(identifier, list);
            }
        }
        
        _indexes.Add(indexName, index);
        _indexTypes.Add(indexName, typeof(T));
    }

    public List<E> GetFromIndex(string indexName, object identifier)
    {
        if (!_indexes.ContainsKey(indexName))
            throw new NullReferenceException("Index does not exists");

        Dictionary<object, List<E>> index = _indexes[indexName];
        Type type = _indexTypes[indexName];

        if (identifier.GetType() != type)
            throw new Exception("Type of provided identifier does not match type of identifier from index");

        if (!index.ContainsKey(identifier))
            throw new NullReferenceException("Could not find element in index");

        List<E> elements = index[identifier];
        
        if (_useLFU)
        {
            // Move elements to the top
            foreach (var element in elements)
            {
                _leastFrequentlyUsed.Remove(element);
                _leastFrequentlyUsed.AddFirst(element);
            }
        }

        return elements;
    }

    public E GetFirstFromIndex(string indexName, object identifier)
    {
        List<E> list = GetFromIndex(indexName, identifier);

        if (list.Count == 0)
            throw new NullReferenceException("Could not find element in index");

        return list[0];
    }
    
}