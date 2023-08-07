namespace FCRA.Web
{
    public static class JqueryDataTableUtility
    {
        public static DataTableRequest GetDataTableRequest(HttpRequest httpRequest)
        {
            DataTableRequest request = new();

            request.Draw = Convert.ToInt32(httpRequest.Form["draw"].FirstOrDefault());
            request.Start = Convert.ToInt32(httpRequest.Form["start"].FirstOrDefault());
            request.Length = Convert.ToInt32(httpRequest.Form["length"].FirstOrDefault());
            request.Search = new DataTableSearch()
            {
                Value = httpRequest.Form["search[value]"].FirstOrDefault()
            };
            request.Order = new DataTableOrder[] {
            new DataTableOrder()
            {
                Dir = httpRequest.Form["order[0][dir]"].FirstOrDefault(),
                Column = Convert.ToInt32(httpRequest.Form["order[0][column]"].FirstOrDefault())
            }};
            return request;
        }
        public static string? GetRequestValue(this HttpRequest httpRequest, string key, bool decrypt = false)
        {
            var value = Convert.ToString(httpRequest.Form[key].FirstOrDefault());
            if (string.IsNullOrWhiteSpace(value))
                return null;
            if (decrypt)
                return value.Decrypt();
            return value;
        }
    }
}
