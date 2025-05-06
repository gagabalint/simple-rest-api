using KEZDOCSAPATXI.Models;

namespace KEZDOCSAPATXI.Helpers
{
    public static class FormationExtension
    {
        public static Dictionary<string, int> ToPositionRequirements(this Formation f)
        {
            return new Dictionary<string, int>
        {
            { "GK", f.GK },
            { "DF", f.DF },
            { "MF", f.MF },
            { "FW", f.FW }
        };
        }
    }
}
