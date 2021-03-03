using UnityEngine;

public abstract class SingletonSO<T> : ScriptableObject where T : ScriptableObject {
    // private reference to this object's instance
    private static T _instance = null;
    private static int _instID = 0;

    // public reference to this object's reference
    public static T Instance {
        // property to get this object's reference
        get {
            
            // check if current instance is valid
            if (!_instance) {

                // find any/all instances
                T[] allInstances = FindObjectsOfType<T>();

                T thisInstance = null;
                // if more than one instance found ...
                if (allInstances.Length > 0) {
                    // check each one
                    foreach (T inst in allInstances)
                        // see if the instance's ID matches
                        if (inst.GetInstanceID() == _instID)
                            // if so, we found the correct one
                            thisInstance = inst;
                        else
                            // otherwise, destroy duplicate
                            Destroy(inst);
                }

                // if no instance was found...
                if (thisInstance == null) {
                    //  create an instance
                    thisInstance = CreateInstance<T>();

                    // store instance's ID
                    _instID = thisInstance.GetInstanceID();
                }

                // store the found/created instance 
                _instance = thisInstance;
            }

            // set flag to ensure instance persists/survice garbage collection
            _instance.hideFlags = HideFlags.DontUnloadUnusedAsset;

            // return our instance
            return _instance;
        }
    }

    protected virtual void Awake() {
        // this "should" run the code from the Instance property
        // which will create an instance if one doesn't already exist
        if(Instance != null) {
            Destroy(this);
        }
    }
}
