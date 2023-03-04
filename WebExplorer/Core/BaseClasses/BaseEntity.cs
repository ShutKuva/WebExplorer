using Newtonsoft.Json;

namespace Core.BaseClasses
{
    public abstract class BaseEntity
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}