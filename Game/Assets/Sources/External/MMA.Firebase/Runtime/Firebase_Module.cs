#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
#endregion
namespace MMA.Firebase
{
    public static partial class Key
    {
        // public const string _   = KeyData._;
        public static string Initialize   = "Firebase_Initialize";
        public static string OnFirebaseAppIsReady = "Firebase_OnFirebaseAppIsReady";
    }
    public static partial class Import
    {
        //public const string _ = _;
    }
    public sealed partial class Firebase_Module : Module
    {
        #region References
        //[Header("Applications")]
        //[SerializeField] public ApplicationBase interface_Business_Firebase;
        private FirebaseApp app = default;
        private bool isAvailable = default;
        #endregion
        #region Reactions ( On___ )
        // Contenedor de toda las reacciones del Business_Firebase
        protected override void OnSubscription(bool condition)
        {
            //Initialize
            Middleware<bool>.Subscribe_Task(condition, Key.Initialize, Initialize);
        }
        #endregion
        #region Methods
        // Contenedor de toda la logica del Business_Firebase
        #endregion
        #region Request ( Coroutines )
        // Contenedor de toda la Esperas de corutinas del Business_Firebase
        #endregion
        #region Task ( async )
        // Contenedor de toda la Esperas asincronas del Business_Firebase

        private async Task<bool> Initialize()
        {
            isAvailable = await FirebaseApp.CheckAndFixDependenciesAsync() == DependencyStatus.Available;
            app = isAvailable ? FirebaseApp.DefaultInstance : null;
            Middleware.Invoke_Publish(Key.OnFirebaseAppIsReady);
            return isAvailable;
        }
        #endregion
    }
}
