namespace Northwind.WebApi.GlobalErrorHandler
{
    public class ErrorDetails
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
