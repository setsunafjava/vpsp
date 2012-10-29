using System.Text;

namespace VP.Sharepoint.CQ.Core
{
    public static class JavaScriptBuilderExtension
    {
        public static void BeginFunction(this StringBuilder builder)
        {
            builder.Append("{");
        }

        public static void BeginFunction(this StringBuilder builder, string function)
        {
            builder.Append(function).Append("{");
        }

        public static void EndFunction(this StringBuilder builder)
        {
            builder.Append("}");
        }
    }
}
