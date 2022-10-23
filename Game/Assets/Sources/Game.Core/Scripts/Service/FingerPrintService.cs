using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;
using X.Common;
using Firebase.Firestore;

public class FingerPrintService : MonoBehaviour
{
    public static FingerPrintService _;

    [Header("Mark settings")]
    [Space]
    public Transform marks_parent;
    public Mark pref_mark = default;

    [Header("Data")]
    [Space]
    public List<string> list_id_marks = new List<string>();
    public List<Fingerprint> list_fingerprints = new List<Fingerprint>();
    public List<Mark> list_marks = new List<Mark>();

    private void Awake()
    {
        this.Singleton(ref _);
    }


    [ContextMenu("Test !")]
    public void __GetAllFingerPrints()
    {
        Debug.Log("Initialize Fingerprint GETALL");
        StartCoroutine(Get_FingerPrints());
    }

    public IEnumerator Get_FingerPrints()
    {
        var async_fingerPrints = MMA.Firebase_Firestore.Service.GetAll("fingerprints").ToCoroutine();
        yield return async_fingerPrints;
        var list_fingerPrints = async_fingerPrints.Current;
        //Debug.Log(list_fingerPrints);
        
        if (list_fingerPrints is null)
        {
            //NADA
            Debug.LogWarning("Alerta, no logró cargar los FingerPrints");
        }
        else
        {
            this.list_id_marks.Clear();
            this.list_fingerprints.Clear();
            this.list_marks.Clear();
            marks_parent.ClearChilds();

            for (int i = 0; i < list_fingerPrints.Count; i++)
            {
                //Debug.Log($"[{i}]: {list_fingerPrints[i].id} Document with {list_fingerPrints[i].document.Count} elements");
                list_id_marks.Add(list_fingerPrints[i].id);
                var finger = new Fingerprint(list_fingerPrints[i].document);
                this.list_fingerprints.Add(finger);
                yield return null; // Para no hacer una sobrecarga

                //Crea la marca
                var mark = Instantiate(pref_mark, marks_parent);
                list_marks.Add(mark);
            }


            //Coloca cada Mark los datos
            for (int i = 0; i < list_marks.Count; i++)
            {
                list_marks[i].SetFingerprint(this.list_fingerprints[i]);
                yield return null;
            }



            yield return new WaitForSeconds(1);
        }

    }
}
