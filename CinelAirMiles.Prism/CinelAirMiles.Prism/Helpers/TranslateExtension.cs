using System;
using System.Reflection;
using System.Resources;
using CinelAirMiles.Prism.Interfaces;

using Xamarin.Forms;

namespace CinelAirMiles.Prism.Helpers
{
    [Xamarin.Forms.ContentProperty("Text")]
    public class TranslateExtension : Xamarin.Forms.Xaml.IMarkupExtension
    {
        private readonly System.Globalization.CultureInfo ci;
        private const string ResourceId = "CinelAirMiles.Prism.Resources.Resource";
        private static readonly Lazy<ResourceManager> ResMgr =
            new Lazy<ResourceManager>(() => new ResourceManager(
                ResourceId,
                typeof(TranslateExtension).GetTypeInfo().Assembly));

        public TranslateExtension()
        {
            ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
            {
                return "";
            }

            var translation = ResMgr.Value.GetString(Text, ci);

            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    string.Format(
                        "Key '{0}' was not found in resources '{1}' for culture '{2}'.",
                        Text, ResourceId, ci.Name), "Text");
#else
        	translation = Text; // returns the key, which GETS DISPLAYED TO THE USER
#endif
            }

            return translation;
        }
    }
}
