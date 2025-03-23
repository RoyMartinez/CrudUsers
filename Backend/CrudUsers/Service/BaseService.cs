namespace CrudUsers.Service
{
    public interface IBaseService<T>
    {
    }
    public class BaseService<T>: IBaseService<T> where T : class
    {
    }
}
