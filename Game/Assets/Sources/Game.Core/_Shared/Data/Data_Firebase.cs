using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data_Firebase 
{
    public static class Collection
    {
        public const string ZONES = "Zones";
        public const string SETTINGS = "settings";
    }

    public static class Document
    {
        public static string ID_ZONE(in Vector2 v2) => $"{v2.x}-{v2.y}";

    }


}
