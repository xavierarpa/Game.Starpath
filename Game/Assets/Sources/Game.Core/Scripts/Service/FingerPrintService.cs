using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;
using X.Common;
using Firebase.Firestore;

public class FingerPrintService : MonoBehaviour
{
    public FingerPrintService _;

    public List<string> list_id_marks = new List<string>();
    public List<Fingerprint> list_fingerprints = new List<Fingerprint>();

    private void Awake()
    {
        this.Singleton(ref _);
    }


    [ContextMenu("Test !")]
    public void GetAllFingerPrints()
    {
        Debug.Log("Initialize Fingerprint GETALL");
        StartCoroutine(Get_FingerPrints());
    }

    public IEnumerator Get_FingerPrints()
    {
        var async_fingerPrints = MMA.Firebase_Firestore.Service.GetAll("fingerprints").ToCoroutine();
        yield return async_fingerPrints;
        var list_fingerPrints = async_fingerPrints.Current;
        Debug.Log(list_fingerPrints);
        
        if (list_fingerPrints is null)
        {
            //NADA
            Debug.LogWarning("Alerta, no logró cargar los FingerPrints");
        }
        else
        {
            this.list_id_marks.Clear();
            this.list_fingerprints.Clear();


            for (int i = 0; i < list_fingerPrints.Count; i++)
            {
                Debug.Log($"[{i}]: {list_fingerPrints[i].id} Document with {list_fingerPrints[i].document.Count} elements");
                list_id_marks.Add(list_fingerPrints[i].id);
                this.list_fingerprints.Add(new Fingerprint(list_fingerPrints[i].document));
                yield return null; // Para no hacer una sobrecarga
            }
        }

    }


}
