public static class GeneralExtensions
{
    public static Task<T> AsCompletedTask<T>(this T result) => Task.FromResult(result);
}