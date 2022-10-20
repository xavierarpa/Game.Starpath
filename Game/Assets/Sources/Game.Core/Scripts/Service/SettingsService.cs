using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettingsService : MonoBehaviour
{
    [SerializeField] private Settings_General general;

    public async Task Initialize()
    {
        //Get General DOcument
        var dic_general = await MMA.Firebase_Firestore.Service.Get(Settings_General.DOCUMENT) as Dictionary<string,object>;

        // SET COLOR
        var list_colors_obj = dic_general["colors"] as List<object>;
        List<String> list_colors = new List<string>();
        list_colors_obj.Convert(ref list_colors);
        general.colors = list_colors;



        // SettingsService_On_GET_Color
        Middleware<List<string>>.Invoke_Publish("SettingsService_On_GET_Color", general.colors);

    }
}

public static class ConvertExtensor
{
    public static void Convert<T>(this List<object> list, ref List<T> tList)
    {
        tList.Clear();
        for (int i = 0; i < list.Count; i++)
        {
            tList.Add((T)list[i]);
        }
    }
    //public static void Convert<T>(this Dictionary<string, object> dic, ref T t)
    //{
    //    var props = typeof(T).GetProperties();
    //    for (int i = 0; i < props.Length; i++)
    //    {
    //        var value = dic[props[i].Name];

    //        props[i].SetValue(t, value);
    //    }
    //}
}