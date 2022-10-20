using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;

public static class Data_Firebase 
{
    public static class Collection
    {
        public const string ZONES = "Zones";
        public const string SETTINGS = "settings";
        public const string ___SETTINGS_TEST_ = SETTINGS + "/_test";
    }

    public static class Document
    {
        public static string ID_ZONE(in Vector2 v2) => $"{v2.x}-{v2.y}";
        public const string DOCUMENT_LAST_FINGERPRINT = Data_Firebase.Collection.SETTINGS + "/lastFingerprint";

    }
}

[Serializable]
public class Settings_General
{
    public const string DOCUMENT = Data_Firebase.Collection.SETTINGS + "/general";
    [field: SerializeField] public List<string> colors { get; set; }
}



[Serializable]
public struct Fingerprint
{
    [field: SerializeField] public string RefID { get; set; } //TODO
    [field: SerializeField] public double Position_X { get; set; } //TODO
    [field: SerializeField] public double Position_Y { get; set; } //TODO
    [field: SerializeField] public string Message { get; set; } //TODO
    [field: SerializeField] public string Nick { get; set; }
    [field: SerializeField] public string Color { get; set; } 
    public DateTime CreatedAt { get; set; }

    public Fingerprint
    (
        string refId,
        Vector2 position,
        string message,
        string nick,
        string color
    )
    {
        this.RefID = refId;
        this.Position_X = position.x;
        this.Position_Y = position.y;
        this.Message = message;
        this.Nick = nick;
        this.Color = color;
        this.CreatedAt = DateTime.Now;
    }

}
