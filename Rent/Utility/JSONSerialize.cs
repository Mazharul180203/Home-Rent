using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Utility;

public class JSONSerialize
{
    public string ErrorMessage = "";
    public Exception Error = null;


    public JObject FormCollectionToJson(IFormCollection obj)
    {
        dynamic json = new JObject();
        if (obj.Keys.Any())
        {
            foreach (string key in obj.Keys)
            {   
                if (obj[key].Count > 1)
                {
                    JArray array = new JArray();
                    for (int i = 0; i < obj[key].Count; i++)
                    {
                        array.Add(obj[key][i]);
                    }
                    json.Add(key, array);
                }
                else
                {
                    var value = obj[key][0];
                    json.Add(key, value);
                }
            }
        }
        return json;
    }

    public List<T> GetListFromJSON<T>(string JSONString)
    {
        return JsonConvert.DeserializeObject<List<T>>(JSONString);
    }

    public T GetObjecFromJSON<T>(string JSONString)
    {
        return JsonConvert.DeserializeObject<T>(JSONString);
    }
    public string getJSONSFromObject(object obj, bool IgnoreReferenceLoopHandling)
    {
        try
        {
            ErrorMessage = "";

            if (IgnoreReferenceLoopHandling)
            {
                return JsonConvert.SerializeObject(obj, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
            }
            else return JsonConvert.SerializeObject(obj);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return null;
        }
    }
}