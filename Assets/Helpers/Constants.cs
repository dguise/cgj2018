using UnityEngine;

public static class Inputs
{
    private static string _horizontal = "Horizontal";
    private static string _vertical = "Vertical";
    private static string _fireHorizontal = "HorizontalFire";
    private static string _fireVertical = "VerticalFire";

    public static string Horizontal(int id) 
    {
        return _horizontal + id;
    }

    public static string Vertical(int id) 
    {
        return _vertical + id;
    }

    public static string FireHorizontal(int id) 
    {
        return _fireHorizontal + id;
    }

    public static string FireVertical(int id) 
    {
        return _fireVertical + id;
    }

}

public static class AnimatorConstants
{
    public static string Speed = "Speed";
}

public static class Tags
{
    public static string Player = "Player";
}

public static class LayerConstants
{
    public static int Enemies = 8;
    public static int Players = 9;
    public static string EnemyProjectiles = "EnemyProjectiles";
    public static string PlayerProjectiles = "PlayerProjectiles";

    public static LayerMask GetLayer(string name)
    {
        return LayerMask.NameToLayer(name);
    }

    public static int GetAllExceptLayers(params string[] names)
    {
        int i = 0;
        foreach (var name in names)
        {
            i += GetLayer(name);
        }
        return ~(1 << i);
    }

    public static int GetOnlyLayer(params string[] names)
    {
        int i = 0;
        foreach (var name in names)
        {
            i += GetLayer(name);
        }
        return (1 << i);
    }
}