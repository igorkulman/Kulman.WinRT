namespace Kulman.WinRT.Data
{
    /// <summary>
    /// Universal result type containing status code, error message and the actual result
    /// </summary>
    /// <typeparam name="T">Result type</typeparam>
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
