using System;

namespace Celeste.Mod.Roslyn.ModLifecycleAttributes.Samples;

public class Example
{
    [OnLoad]
    internal static void Load1()
    {
        Console.WriteLine($"hi im called on load");
    }

    [OnLoad]
    internal static void Load2()
    {
        Console.WriteLine($"hi im also called on load");
    }
}


public class Example2
{
    [OnLoad]
    internal static void Load1()
    {
        Console.WriteLine($"hi im called on load as well");
    }

    [OnLoad]
    internal static void Load2()
    {
        Console.WriteLine($"hi im definitely not called on load");
    }
}

public class MyModule
{
    public static void Load()
    {
        LifecycleMethods.OnLoad();
    }
}
