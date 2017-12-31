using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.ExpressionEvaluator;

namespace Microsoft.CodeAnalysis.CSharp.ExpressionEvaluator
{
    static class GeneratedNames2
    {
        public static GeneratedNameKind GetKind(string name)
        {
            return CommonGeneratedNames.GetKind(name).ToGeneratedNameKind();
        }

        public static bool IsSynthesizedLocalName(string name)
        {
            return CommonGeneratedNames.IsSynthesizedLocalName(name);
        }

        public static bool TryParseSlotIndex(string fieldName, out int slotIndex)
        {
            return CommonGeneratedNames.TryParseSlotIndex(fieldName, out slotIndex);
        }

        public static bool TryParseSourceMethodNameFromGeneratedName(string generatedName, GeneratedNameKind requiredKind, out string methodName)
        {
            return CommonGeneratedNames.TryParseSourceMethodNameFromGeneratedName(generatedName, requiredKind.ToCommonGeneratedNameKind(), out methodName);
        }

        public static bool TryParseGeneratedName(string name, out GeneratedNameKind kind, out string part)
        {
            var res = CommonGeneratedNames.TryParseGeneratedName(name, out var commonKind, out part);
            kind = commonKind.ToGeneratedNameKind();
            return res;
        }
    }
}
