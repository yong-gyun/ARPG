using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
public partial class EventAction
{
    public bool foldout;

    public void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        switch (Type)
        {
            case Define.EventActionType.SetAnimation:
                OnGUISetAnimation();
                break;
            case Define.EventActionType.Bound: 
                OnGUIBound(); 
                break;
            case Define.EventActionType.BoundHeal: 
                OnGUIBoundHeal(); 
                break;
            case Define.EventActionType.BoundDamage: 
                OnGUIBoundDamage(); 
                break;
            case Define.EventActionType.BoundBuff: 
                OnGUIBoundBuff(); 
                break;
            case Define.EventActionType.BoundDebuff: 
                OnGUIBoundDebuff(); 
                break;
            case Define.EventActionType.Position: 
                OnGUIPosition(); 
                break;
            case Define.EventActionType.InputAction: 
                OnGUIInputAction(); 
                break;
            case Define.EventActionType.Effect: 
                OnGUIEffect(); 
                break;
        }

        EditorGUILayout.EndVertical();
    }


    private void OnGUISetAnimation()
    {

    }

    private void OnGUIBound() 
    {
        
    }

    private void OnGUIBoundHeal() 
    { 
        
    }

    private void OnGUIBoundDamage() 
    { 
        
    }

    private void OnGUIBoundBuff() 
    {
        
    }

    private void OnGUIBoundDebuff() 
    { 
        
    }

    private void OnGUIPosition() 
    { 
        
    }

    private void OnGUIInputAction() 
    { 
        
    }

    private void OnGUIEffect() 
    { 
        
    }
}
#endif
