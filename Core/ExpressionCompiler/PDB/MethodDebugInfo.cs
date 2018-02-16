// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.ExpressionEvaluator
{
    internal partial class MethodDebugInfo<TTypeSymbol, TLocalSymbol>
        where TTypeSymbol : class, ITypeSymbol
        where TLocalSymbol : class
    {
        public static readonly MethodDebugInfo<TTypeSymbol, TLocalSymbol> None = new MethodDebugInfo<TTypeSymbol, TLocalSymbol>(
            ImmutableArray<HoistedLocalScopeRecord>.Empty,
            ImmutableDictionary<int, string>.Empty,
            ImmutableArray<ImmutableArray<ImportRecord>>.Empty,
            ImmutableArray<ExternAliasRecord>.Empty,
            null,
            null,
            "",
            ImmutableArray<string>.Empty,
            default,
            ImmutableArray<TLocalSymbol>.Empty,
            ILSpan.MaxValue,
            CompilerKind.Unknown);

        /// <summary>
        /// Hoisted local variable scopes.
        /// Null if the information should be decoded from local variable debug info (VB Windows PDBs).
        /// Empty if there are no hoisted user defined local variables.
        /// </summary>
        public readonly ImmutableArray<HoistedLocalScopeRecord> HoistedLocalScopeRecords;
        public readonly ImmutableDictionary<int, string> HoistedVarFieldTokenToNamesMap;

        public readonly ImmutableArray<ImmutableArray<ImportRecord>> ImportRecordGroups;
        public readonly ImmutableArray<ExternAliasRecord> ExternAliasRecords; // C# only.
        public readonly ImmutableDictionary<int, ImmutableArray<bool>> DynamicLocalMap; // C# only.
        public readonly ImmutableDictionary<int, ImmutableArray<string>> TupleLocalMap;
        public readonly string DefaultNamespaceName; // VB only.
        public readonly ImmutableArray<string> LocalVariableNames;
        public readonly ImmutableArray<string> ParameterNames;
        public readonly ImmutableArray<TLocalSymbol> LocalConstants;
        public readonly ILSpan ReuseSpan;
        public readonly CompilerKind Compiler;

        public MethodDebugInfo(
            ImmutableArray<HoistedLocalScopeRecord> hoistedLocalScopeRecords,
            ImmutableDictionary<int, string> hoistedVarFieldTokenToNamesMap,
            ImmutableArray<ImmutableArray<ImportRecord>> importRecordGroups,
            ImmutableArray<ExternAliasRecord> externAliasRecords,
            ImmutableDictionary<int, ImmutableArray<bool>> dynamicLocalMap,
            ImmutableDictionary<int, ImmutableArray<string>> tupleLocalMap,
            string defaultNamespaceName,
            ImmutableArray<string> localVariableNames,
            ImmutableArray<string> parameterNames,
            ImmutableArray<TLocalSymbol> localConstants,
            ILSpan reuseSpan,
            CompilerKind compiler)
        {
            Debug.Assert(!importRecordGroups.IsDefault);
            Debug.Assert(!externAliasRecords.IsDefault);
            Debug.Assert(defaultNamespaceName != null);

            HoistedLocalScopeRecords = hoistedLocalScopeRecords;
            HoistedVarFieldTokenToNamesMap = hoistedVarFieldTokenToNamesMap ?? ImmutableDictionary<int, string>.Empty;
            ImportRecordGroups = importRecordGroups;

            ExternAliasRecords = externAliasRecords;
            DynamicLocalMap = dynamicLocalMap;
            TupleLocalMap = tupleLocalMap;

            DefaultNamespaceName = defaultNamespaceName;

            LocalVariableNames = localVariableNames;
            ParameterNames = parameterNames;
            LocalConstants = localConstants;
            ReuseSpan = reuseSpan;
            Compiler = compiler;
        }

        public string GetParameterName(int index, IParameterSymbol parameter)
        {
            if (!ParameterNames.IsDefault)
            {
                Debug.Assert((uint)index < (uint)ParameterNames.Length);
                if ((uint)index < (uint)ParameterNames.Length)
                    return ParameterNames[index] ?? parameter.Name;
            }
            return parameter.Name;
        }

        public ImmutableSortedSet<int> GetInScopeHoistedLocalIndices(int ilOffset, ref ILSpan methodContextReuseSpan)
        {
            if (HoistedLocalScopeRecords.IsDefaultOrEmpty)
            {
                return ImmutableSortedSet<int>.Empty;
            }

            methodContextReuseSpan = MethodContextReuseConstraints.CalculateReuseSpan(
                ilOffset,
                methodContextReuseSpan,
                HoistedLocalScopeRecords.Select(record => new ILSpan((uint)record.StartOffset, (uint)(record.StartOffset + record.Length))));

            var scopesBuilder = ArrayBuilder<int>.GetInstance();
            int i = 0;
            foreach (var record in HoistedLocalScopeRecords)
            {
                var delta = ilOffset - record.StartOffset;
                if (0 <= delta && delta < record.Length)
                {
                    scopesBuilder.Add(i);
                }

                i++;
            }

            var result = scopesBuilder.ToImmutableSortedSet();
            scopesBuilder.Free();
            return result;
        }
    }
}
