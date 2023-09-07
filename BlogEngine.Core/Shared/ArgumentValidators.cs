namespace BlogEngine.Core.Shared;

public static class ArgumentValidator
{
    public static void ThrowIfNullOrEmpty(string argument, string paramName = null)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            throw new System.ArgumentNullException($"{paramName ?? "Argument"} cannot be null or empty", paramName);
        }
    }

    public static void ThrowIfNegative<T>(T argument, string paramName = null) where T : INumber<T>
    {
        if (argument < T.Zero)
        {
            throw new System.ArgumentException($"{paramName ?? "Argument"} cannot be negative");
        }
    }

    public static void ThrowIfZeroOrNegative<T>(T argument, string paramName = null) where T : INumber<T>
    {
        if (argument <= T.Zero)
        {
            throw new System.ArgumentException($"{paramName ?? "Argument"} cannot be zero or negative");
        }
    }

    public static void ThrowIfZero<T>(T argument, string paramName = null) where T : INumber<T>
    {
        if (argument == T.Zero)
        {
            throw new System.ArgumentException($"{paramName ?? "Argument"} cannot be zero");
        }
    }

    public static void ThrowIfNullOrDefault<T>(T obj, string paramName = null) where T : new()
    {
        if (obj == null || obj.Equals(default(T)))
        {
            throw new System.ArgumentNullException($"{paramName ?? "Argument"} cannot be null or default");
        }
    }
}
