using System.Resources;

namespace Shopping.Core
{
    public class CoreStrings
    {

        private static readonly ResourceManager _resourceManager
            = new ResourceManager("Shopping.Core.Properties.CoreStrings", typeof(CoreStrings).Assembly);
        public static string NotAnEFService(object? service)
            => string.Format(
                GetString("NotAnEFService", nameof(service)),
                service);


        private static string GetString(string name, params string[] formatterNames)
        {
            var value = _resourceManager.GetString(name)!;
            for (var i = 0; i < formatterNames.Length; i++)
            {
                value = value.Replace("{" + formatterNames[i] + "}", "{" + i + "}");
            }

            return value;
        }

    }
}