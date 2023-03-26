using Volo.Abp.Settings;

namespace MvcMovie.Settings;

public class MvcMovieSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(MvcMovieSettings.MySetting1));
    }
}
