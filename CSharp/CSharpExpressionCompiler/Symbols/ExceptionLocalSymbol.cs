// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.ExpressionEvaluator;

namespace Microsoft.CodeAnalysis.CSharp.ExpressionEvaluator
{
    internal sealed class ExceptionLocalSymbol : PlaceholderLocalSymbol
    {
        private readonly string _getExceptionMethodName;

        internal ExceptionLocalSymbol(MethodSymbol method, string name, string displayName, TypeSymbol type, string getExceptionMethodName, LocalAndMethodKind localAndMethodKind) :
            base(method, name, displayName, type)
        {
            _getExceptionMethodName = getExceptionMethodName;
            LocalAndMethodKind = localAndMethodKind;
        }

        internal override LocalAndMethodKind LocalAndMethodKind { get; }
        internal override int Index => -1;

        internal override bool IsWritableVariable
        {
            get { return false; }
        }

        internal override BoundExpression RewriteLocal(CSharpCompilation compilation, EENamedTypeSymbol container, SyntaxNode syntax, DiagnosticBag diagnostics)
        {
            var method = GetIntrinsicMethod(compilation, _getExceptionMethodName);
            var call = BoundCall.Synthesized(syntax, receiverOpt: null, method: method);
            return ConvertToLocalType(compilation, call, this.Type, diagnostics);
        }
    }
}
