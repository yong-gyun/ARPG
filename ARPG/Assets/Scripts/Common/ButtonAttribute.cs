using System;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class ButtonAttribute : Attribute
{
    public readonly string lable;

    public ButtonAttribute(string lable = "")
    {
        this.lable = lable;
    }
}