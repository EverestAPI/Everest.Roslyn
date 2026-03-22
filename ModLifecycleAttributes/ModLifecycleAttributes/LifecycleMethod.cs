using System.Threading;
using Microsoft.CodeAnalysis;

namespace Celeste.Mod.Roslyn.ModLifecycleAttributes;

// a value-equatable model for our lifecycle methods
// (https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.cookbook.md#pipeline-model-design)
internal sealed record LifecycleMethod
{
    public readonly MethodKind Kind;
    public readonly string TypeFqn;
    public readonly string Identifier;

    private LifecycleMethod(IMethodSymbol declaration, MethodKind kind)
    {
        Kind = kind;
        Identifier = declaration.Name;
        // the FQN will include the global:: namespace alias
        TypeFqn = declaration.ContainingType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
    }

    public static LifecycleMethod AsLoad(GeneratorAttributeSyntaxContext syntaxContext, CancellationToken token)
        => new((IMethodSymbol)syntaxContext.TargetSymbol, MethodKind.Load);
    public static LifecycleMethod AsInitialize(GeneratorAttributeSyntaxContext syntaxContext, CancellationToken token)
        => new((IMethodSymbol)syntaxContext.TargetSymbol, MethodKind.Initialize);
    public static LifecycleMethod AsLoadContent(GeneratorAttributeSyntaxContext syntaxContext, CancellationToken token)
        => new((IMethodSymbol)syntaxContext.TargetSymbol, MethodKind.LoadContent);
    public static LifecycleMethod AsUnload(GeneratorAttributeSyntaxContext syntaxContext, CancellationToken token)
        => new((IMethodSymbol)syntaxContext.TargetSymbol, MethodKind.Unload);

    public enum MethodKind
    {
        Load,
        Initialize,
        LoadContent,
        Unload,
    }

    public override string ToString()
        => $"{nameof(LifecycleMethod)} ({Kind}): {TypeFqn}.{Identifier}";
}
