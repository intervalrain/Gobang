namespace Application.Common;

public interface IPresenter<in TResponse>
{
    public Task PresentTask(TResponse response);
}