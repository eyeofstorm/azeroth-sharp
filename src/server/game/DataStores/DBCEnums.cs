﻿/*
 * This file is part of the AzerothCore Project. See AUTHORS file for Copyright information
 *
 * This program is free software; you can redistribute it and/or modify it
 * under the terms of the GNU Affero General Public License as published by the
 * Free Software Foundation; either version 3 of the License, or (at your
 * option) any later version.
 *
 * This program is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for
 * more details.
 *
 * You should have received a copy of the GNU General Public License along
 * with this program. If not, see <http://www.gnu.org/licenses/>.
 */

namespace AzerothCore.DataStores;

public enum MapTypes : uint
{
    MAP_COMMON = 0,                                 // none
    MAP_INSTANCE = 1,                               // party
    MAP_RAID = 2,                                   // raid
    MAP_BATTLEGROUND = 3,                           // pvp
    MAP_ARENA = 4                                   // arena
}

[Flags]
public enum MapFlags : uint
{
    MAP_FLAG_DYNAMIC_DIFFICULTY = 0x100
}

public enum AchievementCriteriaTypes : uint
{
    ACHIEVEMENT_CRITERIA_TYPE_KILL_CREATURE = 0,
    ACHIEVEMENT_CRITERIA_TYPE_WIN_BG = 1,
    ACHIEVEMENT_CRITERIA_TYPE_REACH_LEVEL = 5,
    ACHIEVEMENT_CRITERIA_TYPE_REACH_SKILL_LEVEL = 7,
    ACHIEVEMENT_CRITERIA_TYPE_COMPLETE_ACHIEVEMENT = 8,
    ACHIEVEMENT_CRITERIA_TYPE_COMPLETE_QUEST_COUNT = 9,
    ACHIEVEMENT_CRITERIA_TYPE_COMPLETE_DAILY_QUEST_DAILY = 10, // you have to complete a daily quest x times in a row
    ACHIEVEMENT_CRITERIA_TYPE_COMPLETE_QUESTS_IN_ZONE = 11,
    ACHIEVEMENT_CRITERIA_TYPE_DAMAGE_DONE = 13,
    ACHIEVEMENT_CRITERIA_TYPE_COMPLETE_DAILY_QUEST = 14,
    ACHIEVEMENT_CRITERIA_TYPE_COMPLETE_BATTLEGROUND = 15,
    ACHIEVEMENT_CRITERIA_TYPE_DEATH_AT_MAP = 16,
    ACHIEVEMENT_CRITERIA_TYPE_DEATH = 17,
    ACHIEVEMENT_CRITERIA_TYPE_DEATH_IN_DUNGEON = 18,
    ACHIEVEMENT_CRITERIA_TYPE_COMPLETE_RAID = 19,
    ACHIEVEMENT_CRITERIA_TYPE_KILLED_BY_CREATURE = 20,
    ACHIEVEMENT_CRITERIA_TYPE_KILLED_BY_PLAYER = 23,
    ACHIEVEMENT_CRITERIA_TYPE_FALL_WITHOUT_DYING = 24,
    ACHIEVEMENT_CRITERIA_TYPE_DEATHS_FROM = 26,
    ACHIEVEMENT_CRITERIA_TYPE_COMPLETE_QUEST = 27,
    ACHIEVEMENT_CRITERIA_TYPE_BE_SPELL_TARGET = 28,
    ACHIEVEMENT_CRITERIA_TYPE_CAST_SPELL = 29,
    ACHIEVEMENT_CRITERIA_TYPE_BG_OBJECTIVE_CAPTURE = 30,
    ACHIEVEMENT_CRITERIA_TYPE_HONORABLE_KILL_AT_AREA = 31,
    ACHIEVEMENT_CRITERIA_TYPE_WIN_ARENA = 32,
    ACHIEVEMENT_CRITERIA_TYPE_PLAY_ARENA = 33,
    ACHIEVEMENT_CRITERIA_TYPE_LEARN_SPELL = 34,
    ACHIEVEMENT_CRITERIA_TYPE_HONORABLE_KILL = 35,
    ACHIEVEMENT_CRITERIA_TYPE_OWN_ITEM = 36,
    ACHIEVEMENT_CRITERIA_TYPE_WIN_RATED_ARENA = 37, /// @todo: the archievements 1162 and 1163 requires a special rating which can't be found in the dbc
    ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_TEAM_RATING = 38,
    ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_PERSONAL_RATING = 39,
    ACHIEVEMENT_CRITERIA_TYPE_LEARN_SKILL_LEVEL = 40,
    ACHIEVEMENT_CRITERIA_TYPE_USE_ITEM = 41,
    ACHIEVEMENT_CRITERIA_TYPE_LOOT_ITEM = 42,
    ACHIEVEMENT_CRITERIA_TYPE_EXPLORE_AREA = 43,
    ACHIEVEMENT_CRITERIA_TYPE_OWN_RANK = 44,
    ACHIEVEMENT_CRITERIA_TYPE_BUY_BANK_SLOT = 45,
    ACHIEVEMENT_CRITERIA_TYPE_GAIN_REPUTATION = 46,
    ACHIEVEMENT_CRITERIA_TYPE_GAIN_EXALTED_REPUTATION = 47,
    ACHIEVEMENT_CRITERIA_TYPE_VISIT_BARBER_SHOP = 48, // note: rewarded as soon as the player payed, not at taking place at the seat
    ACHIEVEMENT_CRITERIA_TYPE_EQUIP_EPIC_ITEM = 49,
    ACHIEVEMENT_CRITERIA_TYPE_ROLL_NEED_ON_LOOT = 50, /// @todo: itemlevel is mentioned in text but not present in dbc
    ACHIEVEMENT_CRITERIA_TYPE_ROLL_GREED_ON_LOOT = 51,
    ACHIEVEMENT_CRITERIA_TYPE_HK_CLASS = 52,
    ACHIEVEMENT_CRITERIA_TYPE_HK_RACE = 53,
    ACHIEVEMENT_CRITERIA_TYPE_DO_EMOTE = 54,
    ACHIEVEMENT_CRITERIA_TYPE_HEALING_DONE = 55,
    ACHIEVEMENT_CRITERIA_TYPE_GET_KILLING_BLOWS = 56, /// @todo: in some cases map not present, and in some cases need do without die
    ACHIEVEMENT_CRITERIA_TYPE_EQUIP_ITEM = 57,
    ACHIEVEMENT_CRITERIA_TYPE_MONEY_FROM_VENDORS = 59,
    ACHIEVEMENT_CRITERIA_TYPE_GOLD_SPENT_FOR_TALENTS = 60,
    ACHIEVEMENT_CRITERIA_TYPE_NUMBER_OF_TALENT_RESETS = 61,
    ACHIEVEMENT_CRITERIA_TYPE_MONEY_FROM_QUEST_REWARD = 62,
    ACHIEVEMENT_CRITERIA_TYPE_GOLD_SPENT_FOR_TRAVELLING = 63,
    ACHIEVEMENT_CRITERIA_TYPE_GOLD_SPENT_AT_BARBER = 65,
    ACHIEVEMENT_CRITERIA_TYPE_GOLD_SPENT_FOR_MAIL = 66,
    ACHIEVEMENT_CRITERIA_TYPE_LOOT_MONEY = 67,
    ACHIEVEMENT_CRITERIA_TYPE_USE_GAMEOBJECT = 68,
    ACHIEVEMENT_CRITERIA_TYPE_BE_SPELL_TARGET2 = 69,
    ACHIEVEMENT_CRITERIA_TYPE_SPECIAL_PVP_KILL = 70,
    ACHIEVEMENT_CRITERIA_TYPE_FISH_IN_GAMEOBJECT = 72,
    ACHIEVEMENT_CRITERIA_TYPE_ON_LOGIN = 74, /// @todo: title id is not mentioned in dbc
    ACHIEVEMENT_CRITERIA_TYPE_LEARN_SKILLLINE_SPELLS = 75,
    ACHIEVEMENT_CRITERIA_TYPE_WIN_DUEL = 76,
    ACHIEVEMENT_CRITERIA_TYPE_LOSE_DUEL = 77,
    ACHIEVEMENT_CRITERIA_TYPE_KILL_CREATURE_TYPE = 78, /// @todo: creature type (demon, undead etc.) is not stored in dbc
    ACHIEVEMENT_CRITERIA_TYPE_GOLD_EARNED_BY_AUCTIONS = 80,
    ACHIEVEMENT_CRITERIA_TYPE_CREATE_AUCTION = 82,
    ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_AUCTION_BID = 83,
    ACHIEVEMENT_CRITERIA_TYPE_WON_AUCTIONS = 84,
    ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_AUCTION_SOLD = 85,
    ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_GOLD_VALUE_OWNED = 86,
    ACHIEVEMENT_CRITERIA_TYPE_GAIN_REVERED_REPUTATION = 87,
    ACHIEVEMENT_CRITERIA_TYPE_GAIN_HONORED_REPUTATION = 88,
    ACHIEVEMENT_CRITERIA_TYPE_KNOWN_FACTIONS = 89,
    ACHIEVEMENT_CRITERIA_TYPE_LOOT_EPIC_ITEM = 90,
    ACHIEVEMENT_CRITERIA_TYPE_RECEIVE_EPIC_ITEM = 91,
    ACHIEVEMENT_CRITERIA_TYPE_ROLL_NEED = 93,
    ACHIEVEMENT_CRITERIA_TYPE_ROLL_GREED = 94,
    ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_HEALTH = 95,
    ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_POWER = 96,
    ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_STAT = 97,
    ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_SPELLPOWER = 98,
    ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_ARMOR = 99,
    ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_RATING = 100,
    ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_HIT_DEALT = 101,
    ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_HIT_RECEIVED = 102,
    ACHIEVEMENT_CRITERIA_TYPE_TOTAL_DAMAGE_RECEIVED = 103,
    ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_HEAL_CASTED = 104,
    ACHIEVEMENT_CRITERIA_TYPE_TOTAL_HEALING_RECEIVED = 105,
    ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_HEALING_RECEIVED = 106,
    ACHIEVEMENT_CRITERIA_TYPE_QUEST_ABANDONED = 107,
    ACHIEVEMENT_CRITERIA_TYPE_FLIGHT_PATHS_TAKEN = 108,
    ACHIEVEMENT_CRITERIA_TYPE_LOOT_TYPE = 109,
    ACHIEVEMENT_CRITERIA_TYPE_CAST_SPELL2 = 110, /// @todo: target entry is missing
    ACHIEVEMENT_CRITERIA_TYPE_LEARN_SKILL_LINE = 112,
    ACHIEVEMENT_CRITERIA_TYPE_EARN_HONORABLE_KILL = 113,
    ACHIEVEMENT_CRITERIA_TYPE_ACCEPTED_SUMMONINGS = 114,
    ACHIEVEMENT_CRITERIA_TYPE_EARN_ACHIEVEMENT_POINTS = 115,
    ACHIEVEMENT_CRITERIA_TYPE_USE_LFD_TO_GROUP_WITH_PLAYERS = 119,
    ACHIEVEMENT_CRITERIA_TYPE_TOTAL = 124, // 0..123 => 124 criteria types total
}

public static class DBCConst
{
    public static readonly int MAX_DUNGEON_DIFFICULTY = 3;
    public static readonly int MAX_RAID_DIFFICULTY = 4;
    public static readonly int MAX_DIFFICULTY = 4;

    // Client expected level limitation, like as used in DBC item max levels for "until max player level"
    // use as default max player level, must be fit max level for used client
    // also see MAX_LEVEL and STRONG_MAX_LEVEL define
    public static readonly int DEFAULT_MAX_LEVEL = 80;

    // client supported max level for player/pets/etc. Avoid overflow or client stability affected.
    // also see GT_MAX_LEVEL define
    public static readonly int MAX_LEVEL = 100;
}
