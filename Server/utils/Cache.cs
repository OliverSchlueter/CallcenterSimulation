namespace Server.Utils;

public class Cache<I, E>
{
    private const int TimeInCache = 60;
    private const int MaxElementsInCache = 200;
    
    private readonly Dictionary<I, E> _cache;
    private readonly Dictionary<I, int> _cacheTimes;
    private readonly Thread _cacheThread;
    private readonly bool _autoRemove;

    public Cache(bool autoRemove = false)
    {
        _autoRemove = autoRemove;
        _cache = new Dictionary<I, E>();
        _cacheTimes = new Dictionary<I, int>();
        
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
    }

    public void Remove(I identifier)
    {
        if (!Contains(identifier))
            return;

        _cache.Remove(identifier);
        _cacheTimes.Remove(identifier);
    }

    public E Get(I identifier)
    {
        if (Contains(identifier))
            return _cache[identifier];

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
    }
    
}