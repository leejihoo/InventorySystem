using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[Serializable]
public class BaseItemModel
{
    public string Name { get; set; }
    public string Id { get; set; }
    public string Description { get; set; }
    public ItemType Type { get; set; }
}

[JsonConverter(typeof(StringEnumConverter)), Serializable]
public enum ItemType
{
    Equipment,
    Consumables,
    Misc
}

