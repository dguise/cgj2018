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

public static class Tags
{

}