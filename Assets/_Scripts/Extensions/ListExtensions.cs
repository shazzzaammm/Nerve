using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static T GetRandom<T>(this List<T> list)
    {
        if (list.Count == 0) return default;

        int r = Random.Range(0, list.Count);
        T t = list[r];
        return t;
    }
    public static T Draw<T>(this List<T> list)
    {
        T t = list.GetRandom();
        list.Remove(t);
        return t;
    }
}