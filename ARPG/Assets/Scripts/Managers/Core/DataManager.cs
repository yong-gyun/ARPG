//using Cysharp.Threading.Tasks;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using UnityEngine;

//public interface ILoader<T>
//{
//    public List<T> result { get; set; }
//    public List<T> Read();
//}

//public interface ILoader<TKey, TValue>
//{
//    public List<TValue> result { get; set; }
//    public Dictionary<TKey, TValue> MakeDict();
//}

//public partial class DataManager
//{
//    public async UniTask<List<TItem>> Load<TLoader, TItem>(string dir, string key) where TLoader : ILoader<TItem>
//    {
//        List<TItem> result = null;
//#if UNITY_EDITOR == false || TEST_DOWNLOAD == true
//        try
//        {
//            TextAsset textAsset = await Managers.Resource.LoadByte(key);
//            using (MemoryStream stream = new MemoryStream(textAsset.bytes))
//            {
                
//            }
//        }
//        catch (Exception e)
//        {
//            Debug.LogError($"Failed read {key}.byte");
//        }

//        result = null;
//#endif
//        try
//        {
//            if (result == null)
//            {
//                TextAsset textAsset = await Managers.Resource.LoadJson(dir, key);
//                result = JsonConvert.DeserializeObject<TLoader>("{ \"result\" : " + textAsset.text + "}").Read();
//                Debug.Log($"Load {key}.json");
//            }
//        }
//        catch (Exception e)
//        {
//            Debug.LogError($"Failed read {key}.json\n {e}");
//        }
//        finally
//        {
//            Managers.Resource.Release(Managers.Resource.CheckDir(dir, "Data"), key + ".json", 1, true);
//        }

//        return result;
//    }

//    public async UniTask<Dictionary<TKey, TValue>> Load<TLoader, TKey, TValue>(string dir, string key) where TLoader : ILoader<TKey, TValue>
//    {
//        try
//        {
//            TextAsset textAsset = await Managers.Resource.LoadJson(dir, key);
//            Dictionary<TKey, TValue> result = JsonConvert.DeserializeObject<TLoader>("{ \"result\" : " + textAsset.text + "}").MakeDict();

//            if (result != null)
//            {
//                Managers.Resource.Release(Managers.Resource.CheckDir(dir, "Data"), key, 1, true);
//                return result;
//            }

//        }
//        catch (Exception e)
//        {
//            Debug.LogError($"Failed read {key}.json");
//        }

//        return null;
//    }
//}