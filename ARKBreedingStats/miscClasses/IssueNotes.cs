﻿using System;
using System.Collections.Generic;

namespace ARKBreedingStats.miscClasses
{
    /// <summary>
    /// Provides specific help for extraction-issues
    /// </summary>
    public static class IssueNotes
    {
        /// <summary>
        /// Returns a list of possible reasons that could cause the issues.
        /// </summary>
        /// <param name="issues"></param>
        /// <returns></returns>
        public static string getHelpTexts(Issue issues)
        {
            List<string> notes = new List<string>();
            int n = 1;
            int i = (int)issues;
            while (i >= n)
            {
                if ((i & n) != 0)
                    notes.Add($"{(notes.Count + 1)}. {GetHelpText((Issue)n)}");
                n <<= 1;
            }
            return string.Join("\n\n", notes.ToArray());
        }

        private static string GetHelpText(Issue issue)
        {
            switch (issue)
            {
                case Issue.Typo: return Loc.S("issueCauseTypo");
                case Issue.WildTamedBred: return Loc.S("issueCauseWildTamedBred");
                case Issue.TamingEffectivenessRange: return Loc.S("issueCauseTamingEffectivenessRange");
                case Issue.LockedDom: return Loc.S("issueCauseLockedDomLevel");
                case Issue.ImprintingLocked: return Loc.S("issueCauseImprintingLocked");
                case Issue.ImprintingNotUpdated: return Loc.S("issueCauseImprintingNotUpdated");
                case Issue.ImprintingNotPossible: return Loc.S("issueCauseImprintingNotPossible");
                case Issue.Singleplayer: return Loc.S("issueCauseSingleplayer");
                case Issue.WildLevelSteps: return Loc.S("issueCauseWildLevelSteps");
                case Issue.MaxWildLevel: return Loc.S("issueCauseMaxWildLevel");
                case Issue.StatMultipliers: return Loc.S("issueCauseStatMultipliers");
                case Issue.ArkStatIssue: return Loc.S("issueCauseArkStatIssue");
                case Issue.CreatureLevel: return Loc.S("issueCauseCreatureLevel");
                case Issue.OutdatedIngameValues: return Loc.S("issueCauseOutdatedIngameValues");
            }
            return string.Empty;
        }

        // order the enums according to their desired position in the issue-help, i.e. critical and common issues first
        [Flags]
        public enum Issue
        {
            None = 0,
            ImprintingNotPossible = 1,
            Typo = 2,
            CreatureLevel = 4,
            WildTamedBred = 8,
            LockedDom = 16,
            Singleplayer = 32,
            MaxWildLevel = 64,
            StatMultipliers = 128,
            ImprintingNotUpdated = 256,
            TamingEffectivenessRange = 512,
            ImprintingLocked = 1024,
            WildLevelSteps = 2048,
            ArkStatIssue = 4096,
            OutdatedIngameValues = 8192
        }
    }
}
