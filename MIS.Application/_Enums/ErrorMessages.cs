namespace MIS.Application._Enums
{
    public static class ErrorMessages
    {
        public static string EntityNotFound(string module) => $"{module} not found."; 
        public static string GenericError => $"There is an error with your request.";
        public static string DuplicateRecordError(string field) => $"Existing record with same {field}. Can not proceed with the request.";
    }
}
