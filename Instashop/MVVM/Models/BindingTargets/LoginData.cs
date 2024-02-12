using Newtonsoft.Json;

namespace Instashop.MVVM.Models.BindingTargets;

public class LoginData
{
    [JsonProperty("token")]
    public string? Token { get; set; }
}
