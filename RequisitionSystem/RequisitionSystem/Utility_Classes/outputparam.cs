using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[AttributeUsage(AttributeTargets.Property)]
class outputparam : Attribute
{
    public ParamType ParamType { get; set; }
}

public enum ParamType
{
    Input = 0,
    Output = 1,
    InputOutput = 2
}