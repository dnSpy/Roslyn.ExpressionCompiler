## Roslyn Expression Evaluator source code used by dnSpy

The `master` branch contains the original source code and a small number of changes so it compiles when referencing the latest Roslyn NuGet package. (The Roslyn EE code is from the Roslyn master branch, but the NuGet package was compiled using older Roslyn code).

The `dnSpy` branch contains the changes used by dnSpy. Eg. all references to the VS debugger and DiaSymReader NuGet packages have been removed. Only the core EE code is compiled.

Only the expression compiler is used by dnSpy.

### License

Same as Roslyn itself, see `License.txt` for more information.
