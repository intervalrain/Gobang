using Application.Common;

namespace Server.Presenters;

public class DefaultPresenter<T> : IPresenter<T> where T : class
{
    public T Value { get; set; } = default!;

    public Task PresentTask(T response)
    {
        Value = response;
        return Task.CompletedTask;
    }
}

