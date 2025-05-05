using KEZDOCSAPATXI.Models;

namespace KEZDOCSAPATXI.Helpers
{
    public static class FormationPresets
    {
        public static readonly Dictionary<string, Formation> Presets = new()
        {
            { "4-4-2", new Formation { GK = 1, DF = 4, MF = 4, FW = 2 } },
            { "4-3-3", new Formation { GK = 1, DF = 4, MF = 3, FW = 3 } },
            { "3-5-2", new Formation { GK = 1, DF = 3, MF = 5, FW = 2 } },
            { "4-5-1", new Formation { GK = 1, DF = 4, MF = 5, FW = 1 } }
        };

    }
}
