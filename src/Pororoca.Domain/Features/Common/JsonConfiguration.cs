using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Pororoca.Domain.Feature.Entities.Pororoca.Repetition;
using Pororoca.Domain.Features.Entities.Pororoca;
using Pororoca.Domain.Features.Entities.Pororoca.Http;
using Pororoca.Domain.Features.Entities.Pororoca.WebSockets;
using Pororoca.Domain.Features.Entities.Postman;

namespace Pororoca.Domain.Features.Common;

internal static class JsonConfiguration
{
    internal static readonly PororocaJsonSrcGenContext MainJsonCtxWithConverters =
        MakePororocaJsonContext(true);

    internal static readonly PororocaJsonSrcGenContext MainJsonCtx =
        MakePororocaJsonContext(false);

    internal static readonly JsonSerializerOptions MinifyingOptions = SetupMinifyingOptions();

    private static PororocaJsonSrcGenContext MakePororocaJsonContext(bool includeCustomConverters)
    {
        JsonSerializerOptions options = new();
        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        options.WriteIndented = true;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        if (includeCustomConverters)
        {
            options.Converters.Add(new PororocaRequestJsonConverter());
        }

        return new(options);
    }

    private static JsonSerializerOptions SetupMinifyingOptions()
    {
        JsonSerializerOptions options = new();
        options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        options.WriteIndented = false;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.AllowTrailingCommas = true;
        options.ReadCommentHandling = JsonCommentHandling.Skip;
        return options;
    }
}

[JsonSerializable(typeof(PororocaCollection))]
[JsonSerializable(typeof(PororocaEnvironment))]
[JsonSerializable(typeof(PororocaHttpRequest))]
[JsonSerializable(typeof(PororocaWebSocketConnection))]
[JsonSerializable(typeof(PororocaHttpRepetition))]
[JsonSerializable(typeof(PostmanCollectionV21))]
[JsonSerializable(typeof(PostmanEnvironment))]
[JsonSerializable(typeof(PostmanAuthType))]
[JsonSerializable(typeof(PostmanRequestBodyMode))]
[JsonSerializable(typeof(PostmanRequestBodyFormDataParamType))]
[JsonSerializable(typeof(PostmanRequestUrl))]
[JsonSerializable(typeof(PostmanAuthBasic))]
[JsonSerializable(typeof(PostmanAuthBearer))]
[JsonSerializable(typeof(PostmanAuthNtlm))]
[JsonSerializable(typeof(PostmanVariable[]))]
internal partial class PororocaJsonSrcGenContext : JsonSerializerContext
{
}