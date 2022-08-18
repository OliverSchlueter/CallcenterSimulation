namespace Server.Utils;

public class Cache<I, E>
{
    private const int TimeInCache = 60;
    private readonly Dictionary<I, E> _cache;
    private readonly Dictionary<I, int> _cacheTimes;
    private readonly Thread _cacheThread;

    public Cache()
    {
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
            foreach (I key in _cacheTimes.Keys)
            {
                int current = _cacheTimes[key]--;

                if (current == 0)
                {
                    _cache.Remove(key);
                    _cacheTimes.Remove(key);
                }
                else
                    _cacheTimes[key] = current;
            }
            
            Thread.Sleep(1000);
        }
    }

    public void Put(I identifier, E element)
    {
        if (_cache.ContainsKey(identifier))
            _cache.Remove(identifier);

        if (_cacheTimes.ContainsKey(identifier))
            _cacheTimes.Remove(identifier);
        
        _cache.Add(identifier, element);
        _cacheTimes.Add(identifier, TimeInCache);
    }

    public E Get(I identifier)
    {
        if (Contains(identifier))
            return _cache[identifier];

        throw new NullReferenceException();
    }

    public bool Contains(I identifier)
    {
        return _cache.ContainsKey(identifier);
    }
    
}