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
        public const string FINGERPRINTS = "fingerprints";
        public const string SETTINGS = "settings";
        public const string ___SETTINGS_TEST_ = SETTINGS + "/_test";
    }

    public static class Document
    {
        public static string ID_ZONE(in Vector2 v2) => $"{v2.x}-{v2.y}";
        public const string LAST_FINGERPRINT = Data_Firebase.Collection.SETTINGS + "/lastFingerprint";

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
    public const string DOCUMENT = Data_Firebase.Collection.FINGERPRINTS + "/";
    public static string RANDOM_DOCUMENT => Data_Firebase.Collection.FINGERPRINTS + "/" + MMA.Firebase_Firestore.Service.GetId(Data_Firebase.Collection.FINGERPRINTS);

    [field: SerializeField] public string RefID { get; set; } 
    [field: SerializeField] public double Position_X { get; set; }
    [field: SerializeField] public double Position_Y { get; set; }
    [field: SerializeField] public string Message { get; set; } 
    [field: SerializeField] public string Nick { get; set; }
    [field: SerializeField] public string Color { get; set; }
    public DateTime CreatedAt { get; set; }
    [field: SerializeField] public int Likes { get; set; }
    [field: SerializeField] public int SpriteIndex { get; set; }

    public Fingerprint(Dictionary<string, object> document){
        this.RefID = document.TryGetValue("RefID", out object _ref) ? _ref as string : "";
        this.Position_X = double.TryParse(document["Position_X"].ToString(), out double _Position_X) ? _Position_X : float.MaxValue;
        this.Position_Y = double.TryParse(document["Position_Y"].ToString(), out double _Position_Y) ? _Position_Y : float.MaxValue;
        this.Message = document.TryGetValue("Message", out object msg) ? msg as string : "?";
        this.Nick = document.TryGetValue("Nick", out object _nick) ? _nick as string : "???";
        this.Color = document.TryGetValue("Color", out object _color) ? _color as string : "#ffffff";
        this.CreatedAt = DateTime.TryParse(document["CreatedAt"].ToString(), out DateTime _CreatedAt) ? _CreatedAt : DateTime.Now;
        this.Likes = document.TryGetValue("Likes", out object _Likes) ? int.TryParse(_Likes.ToString(), out int _LikesInt) ? _LikesInt : 0 : 0;
        this.SpriteIndex = document.TryGetValue("SpriteIndex", out object _SpriteIndex) ? int.TryParse(_SpriteIndex.ToString(), out int _spriteIndexInt) ? _spriteIndexInt : 0 : 0;
    }

}
