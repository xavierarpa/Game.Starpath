#region Access
using System;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Runtime.CompilerServices;
#endregion
namespace MMA.Firebase_Firestore
{
    [FirestoreData]
    public struct Test
    {
        [FirestoreProperty]
        public string _Test { get; set; }
    }
    public static class Key
    {
        // public const string _   = KeyData._;
        public static string Initialize = "Firebase_Firestore_Initialize";
        public static string Set = "Firebase_Firestore_Set";
        public static string Get = "Firebase_Firestore_Get";
        public static string GetAll = "Firebase_Firestore_GetAll";
        public static string GetId = "Firebase_Firestore_Get";
        
    }
    public static class Service
    {
        public static void Initialize() => Middleware.Invoke_Publish(Key.Initialize);

        public static Task Set(in (string path, object value) data) => Middleware<(string path, object value)>.Invoke_Task(Key.Set, data);
        public static Task Set(in (string path, IDictionary<string, object> value) data) => Middleware<(string path, IDictionary<string, object> value)>.Invoke_Task(Key.Set, data);
        public static Task Set(in (string path, ITuple value) data) => Middleware<(string path, ITuple value)>.Invoke_Task(Key.Set, data);

        public static Task<object> Get(in string path) => Middleware<string, object>.Invoke_Task(Key.Get, path);
        public static Task<object> Get(in (string path, object defaultValue) data) => Middleware<(string path, object defaultValue), object>.Invoke_Task(Key.Get, data);
        public static Task<List<(string id, Dictionary<string, object> document)>> GetAll(in string path) => Middleware<string, List<(string id, Dictionary<string, object> document)>>.Invoke_Task(Key.GetAll, path);

        public static string GetId(string pathCollection) => Middleware<string, string>.Invoke_Publish(Key.GetId, pathCollection);
    }
    public sealed partial class Firebase_Firestore_Module : Module
    {
        private FirebaseFirestore db = default;

        protected override void OnSubscription(bool condition)
        {
            //Initialize
            Middleware.Subscribe_Publish(condition, Key.Initialize, Initialize);

            //Set
            Middleware<(string path, object value)>.Subscribe_Task(condition, Key.Set, Set);
            Middleware<(string path, IDictionary<string, object> value)>.Subscribe_Task(condition, Key.Set, Set);
            Middleware<(string path, ITuple value)>.Subscribe_Task(condition, Key.Set, Set);

            ////Get
            Middleware<string, object>.Subscribe_Task(condition, Key.Get, Get);
            Middleware<(string path, object defaultValue), object>.Subscribe_Task(condition, Key.Get, Get);

            //GetAll
            Middleware<string, List<(string id, Dictionary<string, object> document)>>.Subscribe_Task(condition, Key.GetAll, GetAll);

            //GetId
            Middleware<string, string>.Subscribe_Publish(condition, Key.GetId, GetId);

            ////Subscribe
            //Middleware<(string path, bool condition, Action<object> callback)>.Subscribe_Publish(condition, Key.Subscribe, Subscribe);
        }

        private void Initialize() => db = FirebaseFirestore.DefaultInstance;

        private string GetId(string path) => this.db.Collection(path).Document().Id;

        private async Task<object> Get(string path)
        {
            try
            {
                Dictionary<string, object> data = (await db.Document(path).GetSnapshotAsync()).ConvertTo<Dictionary<string, object>>();
                return data;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Error GET: '{path}' => {ex}");
                return null;
            }
            
        }
        private async Task<object> Get((string path, object defaultValue) data) => (await Get(data.path)) ?? data.defaultValue;


        private async Task<List<(string id, Dictionary<string, object> document)>> GetAll(string path) => (await this.db.Collection(path).GetSnapshotAsync()).ToList().ConvertAll(_item => (_item.Id, _item.ToDictionary()));


        private async Task Set((string path, IDictionary<string, object> value) data) => await db.Document(data.path).SetAsync(data.value);
        private async Task Set((string path, object value) data)
        {
            Dictionary<string, object> dic_value = new Dictionary<string, object>();
            var _properties = data.value.GetType().GetProperties();
            for (int i = 0; i < _properties.Length; i++) dic_value.Add(_properties[i].Name, _properties[i].GetValue(data.value));
            await Set((data.path, dic_value));
        }
        private async Task Set((string path, ITuple value) data)
        {
            Dictionary<string, object> dic_value = new Dictionary<string, object>();
            for (int i = 0; i < data.value.Length; i++) dic_value.Add(i.ToString(), data.value[i]);
            await Set((data.path, dic_value));
        }



        //public ListenerRegistration Sync(Action<T> callback, string _id = default)
        //{
        //    string _idToUse = _id ?? id ?? default; // si no encuentra ningún ID entonces toca añadir
        //    return DocRef(_idToUse).Listen(snap => {
        //        if (snap.Exists)
        //        {
        //            T data = snap.ConvertTo<T>();
        //            data.id = _idToUse;
        //            callback?.Invoke(data);
        //        }
        //    });
        //}


        //protected static ListenerRegistration SyncCollection(string colRef, Action<T[]> callback)
        //{
        //    return db.Collection(PrefixDevelop + colRef).Listen(snap => {
        //        T[] data = _ConvertCollection(snap);
        //        callback.Invoke(data);
        //    });
        //}

    }
}

/* de BD
 * 
 * 
 * /// <summary>
    /// Sync the information
    /// </summary>
    public ListenerRegistration Sync(Action<T> callback, string _id = default)
    {
        string _idToUse = _id ?? id ?? default; // si no encuentra ningún ID entonces toca añadir
        return DocRef(_idToUse).Listen(snap =>{
            if (snap.Exists)
            {
                T data = snap.ConvertTo<T>();
                data.id = _idToUse;
                callback?.Invoke(data);
            }
        });
    }

    #region CollectionsCalling (static)
    /// <summary>
    /// Returns an array of Docs
    /// </summary>
    /// <param name="callBack"></param>
    public void GetAll(Action<T[]> callBack)
    {
        ColRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Assert.IsNull(task.Exception);
            T[] data = _ConvertCollection(task.Result);
            callBack.Invoke(data);
        });
    }



    /// <summary>
    /// Apply the transformation of the collection
    /// </summary>
    private static T[] _ConvertCollection(QuerySnapshot snap)
    {
        DocumentSnapshot[] Ddata = snap.ToArray();
        T[] data = new T[Ddata.Length];
        for (int i = 0; i < data.Length; i++){
            data[i] = Ddata[i].ConvertTo<T>();
            data[i].id = Ddata[i].Id;
        }
        return data;
    }


 */


//private static T[] _ConvertCollection(QuerySnapshot snap)
//{
//    DocumentSnapshot[] Ddata = snap.ToArray();
//    T[] data = new T[Ddata.Length];
//    for (int i = 0; i < data.Length; i++)
//    {
//        data[i] = Ddata[i].ConvertTo<T>();
//        data[i].id = Ddata[i].Id;
//    }
//    return data;
//}
