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
        public const string SETTINGS_TEST_ = SETTINGS + "/_test_";
    }

    public static class Document
    {
        public static string ID_ZONE(in Vector2 v2) => $"{v2.x}-{v2.y}";


    }



    [Serializable]
    [FirestoreData]
    public struct Fingerprint
    {
        [FirestoreProperty] public string Id { get; set; }
        [FirestoreProperty] public string RefID { get; set; }
        [FirestoreProperty] public Vector2 Position { get; set; }
        [FirestoreProperty] public string Message { get; set; }
        [FirestoreProperty] public string Nick { get; set; }
        [FirestoreProperty] public string Color { get; set; }
        [FirestoreProperty] public DateTime CreatedAt { get; set; }

        public Fingerprint(
            string refId,
            Vector2 position,
            string message,
            string nick,
            string color
        )
        {
            this.Id = default;
            this.RefID = refId;
            this.Position = position;
            this.Message = message;
            this.Nick = nick;
            this.Color = color;
            this.CreatedAt = DateTime.Now;
        }

    }
}
