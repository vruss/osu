// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Game.Beatmaps;
using osu.Game.Rulesets;

namespace osu.Game.Screens
{
    public class OsuScreenDependencies : DependencyContainer
    {
        public Bindable<WorkingBeatmap> Beatmap { get; }

        public Bindable<RulesetInfo> Ruleset { get; }

        public OsuScreenDependencies(bool requireLease, IReadOnlyDependencyContainer parent)
            : base(parent)
        {
            if (requireLease)
            {
                Beatmap = parent.Get<LeasedBindable<WorkingBeatmap>>()?.GetBoundCopy();
                if (Beatmap == null)
                {
                    Cache(Beatmap = parent.Get<Bindable<WorkingBeatmap>>().BeginLease(false));
                }

                Ruleset = parent.Get<LeasedBindable<RulesetInfo>>()?.GetBoundCopy();
                if (Ruleset == null)
                {
                    Cache(Ruleset = parent.Get<Bindable<RulesetInfo>>().BeginLease(true));
                }
            }
            else
            {
                Beatmap = (parent.Get<LeasedBindable<WorkingBeatmap>>() ?? parent.Get<Bindable<WorkingBeatmap>>()).GetBoundCopy();
                Ruleset = (parent.Get<LeasedBindable<RulesetInfo>>() ?? parent.Get<Bindable<RulesetInfo>>()).GetBoundCopy();
            }
        }
    }
}