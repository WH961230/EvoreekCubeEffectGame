
public class BaseMgr<T>
{
    private BaseMgr<T> instance;

    private BaseMgr<T> Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            return null;
        }
    }
    
    public BaseMgr<T> GetInstance()
    {
        return Instance;
    }
}
