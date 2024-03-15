using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayableCharacter
{
    private readonly Character _class;
    private readonly List<Stat> _currentStats;

    public PlayableCharacter(Character newClass)
    {
        _class = newClass;
        _currentStats = DeepCopy(newClass.InitialStats);
    }
    
    public Character Class() => _class;
    public float GetStat(Stats stat) => _currentStats.Find(x => x.Name == stat).Value;
    public void AddStat(Stats stat, float amount) => _currentStats.Find(x => x.Name == stat).Value += amount;

    private static T DeepCopy<T>(T item)
    {
        BinaryFormatter formatter = new();
        MemoryStream stream = new();
        
        formatter.Serialize(stream, item);
        stream.Seek(0, SeekOrigin.Begin);
        T result = (T)formatter.Deserialize(stream);
        stream.Close();

        return result;
    }
}
