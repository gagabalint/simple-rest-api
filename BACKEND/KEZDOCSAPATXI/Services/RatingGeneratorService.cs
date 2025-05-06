using KEZDOCSAPATXI.Helpers;
using KEZDOCSAPATXI.Models;
using System.Reflection;

namespace KEZDOCSAPATXI.Services
{
    public interface IRatingGeneratorService
    {
        List<Lineup> GenerateAllLineups(List<Player> players);
    }

    public class RatingGeneratorService : IRatingGeneratorService
    {
        //Dictionary<string, int> squadPositionCounter = new Dictionary<string, int>
        //    {
        //        { "GK", 0 },
        //        { "DF", 0 },
        //        { "MF", 0 },
        //        { "FW", 0 }
        //     };


        //public List<Lineup> GenerateLineups(List<Player> inputPlayersList)
        //{

        //}
        //private void PositionProcesser(List<Player> input)
        //{

        //    foreach (Player item in input)
        //    {
        //        squadPositionCounter[item.Position]++;
        //    }
        //}
        public List<Lineup> GenerateAllLineups(List<Player> originalPlayers)
        {
            var results = new List<Lineup>();

            foreach (var preset in FormationPresets.Presets)
            {
                string formationName = preset.Key;
                var formation = preset.Value.ToPositionRequirements(); // Dictionary<string, int>

                // Deep copy a játékoslistára, hogy ne módosítsuk eredetit
                var players = originalPlayers
                    .Select(p => new Player { Name = p.Name, Position = p.Position, isAssigned = false }).ToList();


                var starters = new List<AssignedPlayer>();
                double totalScore = 0;

                foreach (var req in formation)
                {
                    string targetPos = req.Key;
                    int needed = req.Value;

                    for (int i = 0; i < needed; i++)
                    {
                        var best = FindBestPlayerForPosition(players, targetPos);
                        if (best == null) break;

                        best.isAssigned = true;
                        double score = ratingRuler(best.Position, targetPos);
                        totalScore += score;

                        starters.Add(new AssignedPlayer { Player=best,AssignedPosition=targetPos});
                    }
                }

                if (starters.Count == 11)
                {
                    var lineup = new Lineup
                    {
                        Formation = formationName,
                        Rating = (int)Math.Round((totalScore / 11) * 100),
                        Players = starters
                    };

                    results.Add(lineup);
                }
            }

            return results;
        }
        private Player FindBestPlayerForPosition(List<Player> players, string targetPos)
        {
            Player best = null;
            double bestScore = -1;

            foreach (var p in players.Where(p => !p.isAssigned))
            {
                double score = ratingRuler(p.Position, targetPos);
                if (score > bestScore)
                {
                    bestScore = score;
                    best = p;
                }
            }

            return best;
        }

        private double ratingRuler(string playerPos, string formationPos)
        {
            if (playerPos == "GK")
            {
                if (formationPos == "GK") return 1.0;
                return 0.1;
            }

            if (playerPos == "DF")
            {
                if (formationPos == "DF") return 1.0;
                if (formationPos == "MF") return 0.6;
                if (formationPos == "FW") return 0.3;
                return 0.1;
            }

            if (playerPos == "MF")
            {
                if (formationPos == "MF") return 1.0;
                if (formationPos == "DF" || formationPos == "FW") return 0.6;
                return 0.1;
            }

            if (playerPos == "FW")
            {
                if (formationPos == "FW") return 1.0;
                if (formationPos == "MF") return 0.6;
                if (formationPos == "DF") return 0.3;
                return 0.1;
            }
            return 0;
        }
    }


}
