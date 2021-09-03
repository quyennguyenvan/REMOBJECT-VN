using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Coffe_Order.Infrastructures.Helpers
{
    public class JsonHelper
    {
        public static T Deserialize<T>(string json)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return default(T);
            }
        }

        public static string Serialize<T>(T obj)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static T GetValueFromKeyInJsonString<T>(string jsonString, string key) where T : class
        {
            List<String> keys = key.Split('.').ToList();
            JObject json = JObject.Parse(jsonString);

            int count = 0;
            string temp = null;
            T value = null;
            try
            {
                // loop to the last json key
                while (count < keys.Count)
                {
                    try
                    {
                        using (var sr = new StringReader(jsonString))
                        using (var jr = new JsonTextReader(sr) { DateParseHandling = DateParseHandling.None })
                        {
                            var j = JToken.ReadFrom(jr);
                            temp = j[keys[count]].ToString();
                        }
                        jsonString = temp;
                    }
                    catch (NullReferenceException ex)
                    {
                        return null;
                    }
                    count++;
                }
                // only a string
                if (typeof(T).Name.Equals("String"))
                {
                    return (T)Convert.ChangeType(temp, typeof(T));
                }

                // if still a json
                else
                {
                    value = JsonHelper.Deserialize<T>(temp);
                }

            }
            catch (NullReferenceException ex)
            {
                //log
                return null;
            }
            return value;
        }

        public static T GetValueFromKeyInJson<T>(string jsonString, string key) where T : class
        {
            List<String> keys = key.Split('.').ToList();
            JObject json = JObject.Parse(jsonString);

            int count = 0;
            string temp = null;
            T value = null;
            try
            {
                // loop to the last json key
                while (count < keys.Count)
                {
                    using (var sr = new StringReader(jsonString))
                    using (var jr = new JsonTextReader(sr) { DateParseHandling = DateParseHandling.None })
                    {
                        var j = JToken.ReadFrom(jr);
                        temp = j[keys[count]].ToString();
                    }
                    jsonString = temp;
                    count++;
                }
                // only a string
                if (typeof(T).Name.Equals("String"))
                {
                    return (T)Convert.ChangeType(temp, typeof(T));
                }

                // if still a json
                else
                {
                    value = JsonHelper.Deserialize<T>(temp);
                }

            }
            catch (NullReferenceException ex)
            {
                //log
                return null;
            }
            return value;
        }

        private static bool DoContainJsonCharacters(string str)
        {
            var specialCharacters = new String[] { "{", "}", "[", "]" };
            return specialCharacters.Any(s => str.Contains(s));
        }
    }
}
