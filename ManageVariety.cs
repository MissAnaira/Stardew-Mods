using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Extensions;
using StardewValley.Monsters;

namespace MonsterVariety;

internal static class ManageVariety

    internal static void Apply(IModHelper helper)
    {
        helper.Events.GameLoop.SaveLoaded += OnSaveLoaded;
        helper.Events.Player.Warped += OnWarped;
    }

    private static void OnSaveLoaded(object? sender, SaveLoadedEventArgs e)
    {
        Game1.currentLocation.characters.OnValueAdded += OnMonsterAdded;
    }

    private static void OnWarped(object? sender, WarpedEventArgs e)
    {
        if (e.OldLocation != null)
            e.OldLocation.characters.OnValueAdded -= OnMonsterAdded;
        if (e.NewLocation != null)
        {
            foreach (NPC npc in e.NewLocation.characters)
            {
                OnMonsterAdded(npc);
            }
            e.NewLocation.characters.OnValueAdded += OnMonsterAdded;
        }
    }

    private static void OnMonsterAdded(NPC value)
    {
        if (value is Monster monster)
            ApplyMonsterDrops(monster);
    }

    private static void ApplyMonsterDrops(NPC value)
    {
        if (monster.isHardModeMonster.Value&& Game1.random.GetDouble() < 0.001)
        {
            monster.objectsToDrop.Add("StarCrops_AncientStarfruitSeeds");
        }
        if (monster.isHardModeMonster.Value && Game1.random.GetDouble() < 0.0077)
        {
            monster.objectsToDrop.Add("StarCrops_StardropSeeds");
        }
    }
