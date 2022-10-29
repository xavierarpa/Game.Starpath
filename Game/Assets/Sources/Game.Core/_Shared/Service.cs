using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Service
{
    public static class Scene
    {
        public static IEnumerator Add(in string name) => Middleware<string>.Invoke_IEnumerator(MMA.Scenes.Key.AddScene, name);

    }
}

namespace MMA.Localization
{
    public static class Service
    {
        public static string Translate(in string str) => str;
    }
}