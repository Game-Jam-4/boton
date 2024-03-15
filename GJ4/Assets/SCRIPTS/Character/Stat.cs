[System.Serializable]
public class Stat
{
    public Stat(Stats stat)
    {
        Name = stat;
    }
    
    public Stats Name;
    public float Value;
}
