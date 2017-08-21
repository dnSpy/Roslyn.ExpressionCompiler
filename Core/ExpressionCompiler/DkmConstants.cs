using System;

namespace Microsoft.VisualStudio.Debugger.Clr
{
    internal enum DkmClrAliasKind
    {
        Exception = 0,
        StowedException = 1,
        ReturnValue = 2,
        Variable = 3,
        ObjectId = 4
    }
}
namespace Microsoft.VisualStudio.Debugger.Evaluation
{
    [Flags]
    internal enum DkmEvaluationFlags
    {
        None = 0,
        TreatAsExpression = 1,
        TreatFunctionAsAddress = 2,
        NoSideEffects = 4,
        NoFuncEval = 8,
        DesignTime = 16,
        AllowImplicitVariables = 32,
        ForceEvaluationNow = 64,
        ShowValueRaw = 128,
        ForceRealFuncEval = 256,
        HideNonPublicMembers = 512,
        NoToString = 1024,
        NoFormatting = 2048,
        NoRawView = 4096,
        NoQuotes = 8192,
        DynamicView = 16384,
        ResultsOnly = 32768,
        NoExpansion = 65536,
        EnableExtendedSideEffects = 131072
    }
    public enum DkmEvaluationResultCategory
    {
        Other = 0,
        Data = 1,
        Method = 2,
        Event = 3,
        Property = 4,
        Class = 5,
        Interface = 6,
        BaseClass = 7,
        InnerClass = 8,
        MostDerivedClass = 9
    }
    public enum DkmEvaluationResultAccessType
    {
        None = 0,
        Public = 1,
        Private = 2,
        Protected = 3,
        Final = 4,
        Internal = 5
    }
    public enum DkmEvaluationResultStorageType
    {
        None = 0,
        Global = 1,
        Static = 2,
        Register = 3
    }
    [Flags]
    public enum DkmEvaluationResultTypeModifierFlags
    {
        None = 0,
        Virtual = 1,
        Constant = 2,
        Synchronized = 4,
        Volatile = 8
    }
}
namespace Microsoft.VisualStudio.Debugger.Evaluation.ClrCompilation
{
    [Flags]
    internal enum DkmClrCompilationResultFlags
    {
        None = 0,
        PotentialSideEffect = 1,
        ReadOnlyResult = 2,
        BoolResult = 4
    }
}
