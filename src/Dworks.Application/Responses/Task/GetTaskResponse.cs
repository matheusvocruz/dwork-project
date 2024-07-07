namespace Dworks.Application.Responses.Task
{
    public class GetTaskResponse
    {
        public long Id { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
    }
}
