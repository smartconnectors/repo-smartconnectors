namespace SmartConnectors.Components.Excel
{
    public class ExcelResponse<T>
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public T Data { get; set; }

        public static ExcelResponse<T> GetResult(int code, string msg, T data = default(T))
        {
            return new ExcelResponse<T>
            {
                Code = code,
                Msg = msg,
                Data = data
            };
        }
    }
}
