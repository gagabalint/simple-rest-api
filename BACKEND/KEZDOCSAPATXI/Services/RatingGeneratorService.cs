﻿using KEZDOCSAPATXI.Helpers;
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


        public List<Lineup> GenerateAllLineups(List<Player> originalPlayers)
        {
            List<Lineup> results = new List<Lineup>();

            foreach (var preset in FormationPresets.Presets)
            {
                string formationName = preset.Key;
                Dictionary<string, int> formation = preset.Value.ToPositionRequirements();

            
                List<Player> playersCopy = originalPlayers
                    .Select(p => new Player { Name = p.Name, Position = p.Position, isAssigned = false })
                    .ToList();

                List<AssignedPlayer> starters = new List<AssignedPlayer>();
                double totalScore = 0;


                Dictionary<string, int> formationCopy = formation.ToDictionary(entry => entry.Key, entry => entry.Value);

                foreach (var position in formationCopy.Keys.ToList())
                {
                    List<Player> natives = playersCopy
                        .Where(p => !p.isAssigned && p.Position == position)
                        .Take(formationCopy[position])
                        .ToList();

                    foreach (var player in natives)
                    {
                        player.isAssigned = true;
                        totalScore += ratingRuler(player.Position, position);
                        starters.Add(new AssignedPlayer { Player = player, AssignedPosition = position });
                    }

                    formationCopy[position] -= natives.Count;
                }

            
                foreach (var key in formationCopy.Keys.ToList())
                {
                    int needed = formationCopy[key];
                    for (int i = 0; i < needed; i++)
                    {
                        if (starters.Count >= 11) break;

                        Player best = FindBestPlayerForPosition(playersCopy, key);
                        if (best == null) break;  //a 2 foreachnel kinullazodhat a dictionary de a 2. nem latja

                        best.isAssigned = true;
                        totalScore += ratingRuler(best.Position, key);
                        starters.Add(new AssignedPlayer { Player = best, AssignedPosition = key });
                    }
                }
                if (starters.Count == 11)
                {
                    results.Add(new Lineup
                    {
                        Formation = formationName,
                        Rating = (int)Math.Round((totalScore / 11) * 100),
                        Players = starters
                    });
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
                if (formationPos == "MF") return 0.2;
                if (formationPos == "FW") return 0.1;
                return 0.1;
            }

            if (playerPos == "MF")
            {
                if (formationPos == "MF") return 1.0;
                if (formationPos == "DF" || formationPos == "FW") return 0.2;
                return 0.1;
            }

            if (playerPos == "FW")
            {
                if (formationPos == "FW") return 1.0;
                if (formationPos == "MF") return 0.2;
                if (formationPos == "DF") return 0.1;
                return 0.1;
            }
            return 0;
        }
    }


}
