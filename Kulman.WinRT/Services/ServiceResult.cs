namespace Kulman.WinRT.Services
{
    public class ServiceResult<T>
    {
        public int Status
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public T Result
        {
            get;
            set;
        }
    }

}
