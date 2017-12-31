Imports System.Runtime.InteropServices
Imports Microsoft.CodeAnalysis.ExpressionEvaluator

Namespace Microsoft.CodeAnalysis.VisualBasic.ExpressionEvaluator
    Friend Class GeneratedNames2
        Friend Shared Function GetKind(name As String) As String
            Return CommonGeneratedNames.GetKind(name).ToGeneratedNameKind()
        End Function

        Friend Shared Function TryParseHoistedUserVariableName(proxyName As String, <Out> ByRef variableName As String) As Boolean
            Return CommonGeneratedNames.TryParseHoistedUserVariableName(proxyName, variableName)
        End Function

        Friend Shared Function TryParseStateMachineHoistedUserVariableName(proxyName As String, <Out> ByRef variableName As String, <Out()> ByRef index As Integer) As Boolean
            Return CommonGeneratedNames.TryParseStateMachineHoistedUserVariableName(proxyName, variableName, index)
        End Function

        Public Shared Function TryParseStateMachineTypeName(stateMachineTypeName As String, <Out> ByRef methodName As String) As Boolean
            Return CommonGeneratedNames.TryParseStateMachineTypeName(stateMachineTypeName, methodName)
        End Function

        Friend Shared Function TryParseStaticLocalFieldName(fieldName As String, <Out> ByRef methodName As String, <Out> ByRef methodSignature As String, <Out> ByRef localName As String) As Boolean
            Return CommonGeneratedNames.TryParseStaticLocalFieldName(fieldName, methodName, methodSignature, localName)
        End Function
    End Class
End Namespace
