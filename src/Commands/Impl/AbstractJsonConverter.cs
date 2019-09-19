using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace MaskedEmails.Commands.Impl
{
    internal sealed class ClassHierarchyContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            // https://stackoverflow.com/questions/28561026/newtonsoft-json-base-class-serialization
            // by default, base class properties are ignored

            var collection = base.CreateProperties(type, memberSerialization);
            foreach (var property in collection)
                property.Ignored = false;

            return collection;
        }
    }
    public abstract class AbstractJsonConverter<T> : JsonConverter
    {
        protected abstract T Create(Type objectType, JObject jObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);

            var target = Create(objectType, jObject);
            if (target != null)
                serializer.Populate(jObject.CreateReader(), target);

            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
    }

    internal static class JObjectExtensions
    {
        public static bool ObjectExists(this JObject jObject, string name)
        {
            return FieldExists(jObject, name, JTokenType.Object);
        }

        public static bool ArrayExists(this JObject jObject, string name)
        {
            return FieldExists(jObject, name, JTokenType.Array);
        }

        public static bool StringExists(this JObject jObject, string name)
        {
            return FieldExists(jObject, name, JTokenType.String);
        }

        public static bool FieldExists(this JObject jObject, string name)
        {
            JToken token;

            return
                jObject.TryGetValue(name, out token)
                ;
        }

        public static bool FieldExists(this JObject jObject, string name, JTokenType type)
        {
            JToken token;

            return
                jObject.TryGetValue(name, out token)
                && token.Type == type
                ;
        }
    }
}