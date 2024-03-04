namespace blazorpg.Data.Models;

public class Response<TEntity>
    {
        public string? StatusCode { get; set; }
        public bool Ok { get; set; }
        public string? Message { get; set; }
        public TEntity? Data { get; set; }


    }