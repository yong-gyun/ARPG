using System.Linq;
using UnityEngine;

namespace System.Collections.Generic.Serialized
{
    [Serializable]

    public class KeyValueItem<TKey, TValue>
    {
        public TKey key;
        public TValue value;
    }

    [Serializable]
    public class SerializedDictionary<TKey, TValue>
    {
        [SerializeField] 
        private List<KeyValueItem<TKey, TValue>> _keyValueList;

        public List<TKey> Keys { get { return _keyValueList.Select(_ => _.key).ToList(); } }

        public List<TValue> Values { get { return _keyValueList.Select(_ => _.value).ToList(); } }

        public SerializedDictionary()
        {
            _keyValueList = new List<KeyValueItem<TKey, TValue>>();
        }

        public TValue this[TKey key]
        {
            get 
            { 
                var obj = _keyValueList.Find(x => EqualityComparer<TKey>.Default.Equals(x.key, key));
                if (obj == null)
                    return default;

                return obj.value;
            }
            set 
            {
                var obj = _keyValueList.Find(x => EqualityComparer<TKey>.Default.Equals(x.key, key));
                if (obj == null)
                    return;

                obj.value = value;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _keyValueList.GetEnumerator();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            object ret = _keyValueList.Find(x => EqualityComparer<TKey>.Default.Equals(x.key, key));
            if (ret == null)
            {
                value = default;
                return false;
            }

            value = (TValue) ret;
            return true;
        }
    }
}
