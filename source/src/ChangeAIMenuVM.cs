using RTSCamera.View.Basic;
using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace ImprovedCombatAI
{
    public class ChangeAIMenuVM : MissionMenuVMBase
    {
        private readonly ChangeBodyProperties _changeBodyProperties = ChangeBodyProperties.Get();

        public string UseRealisticBlockingString { get; } = GameTexts.FindText("str_em_use_realistic_blocking").ToString();

        public string MeleeAIDifficultyString { get; } = GameTexts.FindText("str_em_melee_ai_difficulty").ToString();
        public string ChangeMeleeAIString { get; } = GameTexts.FindText("str_em_change_melee_ai").ToString();
        public string MeleeAIString { get; } = GameTexts.FindText("str_em_melee_ai").ToString();

        public string RangedAIDifficultyString { get; } = GameTexts.FindText("str_em_ranged_ai_difficulty").ToString();
        public string ChangeRangedAIString { get; } = GameTexts.FindText("str_em_change_ranged_ai").ToString();
        public string RangedAIString { get; } = GameTexts.FindText("str_em_ranged_ai").ToString();


        [DataSourceProperty] public bool EnableChangingBodyProperties => _changeBodyProperties != null;

        [DataSourceProperty]
        public bool UseRealisticBlocking
        {
            get => _changeBodyProperties?.UseRealisticBlocking ?? false;
            set
            {
                if (_changeBodyProperties == null || _changeBodyProperties.UseRealisticBlocking == value)
                    return;
                _changeBodyProperties.UseRealisticBlocking = value;
                this.OnPropertyChanged(nameof(UseRealisticBlocking));
            }
        }

        [DataSourceProperty]
        public NumericVM MeleeAIDifficulty { get; }

        [DataSourceProperty]
        public bool ChangeMeleeAI
        {
            get => _changeBodyProperties?.ChangeMeleeAI ?? false;
            set
            {
                if (_changeBodyProperties == null || _changeBodyProperties.ChangeMeleeAI == value)
                    return;
                _changeBodyProperties.ChangeMeleeAI = value;
                this.MeleeAIDifficulty.IsVisible = !value;
                this.MeleeAI.IsVisible = value;
                OnPropertyChanged(nameof(ChangeMeleeAI));
            }
        }

        [DataSourceProperty]
        public NumericVM MeleeAI { get; }

        [DataSourceProperty]
        public NumericVM RangedAIDifficulty { get; }

        [DataSourceProperty]
        public bool ChangeRangedAI
        {
            get => _changeBodyProperties?.ChangeRangedAI ?? false;
            set
            {
                if (_changeBodyProperties == null || _changeBodyProperties.ChangeRangedAI == value)
                    return;
                _changeBodyProperties.ChangeRangedAI = value;
                this.RangedAIDifficulty.IsVisible = !value;
                this.RangedAI.IsVisible = value;
                OnPropertyChanged(nameof(ChangeRangedAI));
            }
        }

        [DataSourceProperty]
        public NumericVM RangedAI { get; }

        [DataSourceProperty]
        public NumericVM RangedError { get; }

        public ChangeAIMenuVM(Action closeMenu)
            : base(closeMenu)
        {
            this.MeleeAIDifficulty = new NumericVM(MeleeAIDifficultyString, _changeBodyProperties?.MeleeAIDifficulty ?? 0, 0, 100, true,
                difficulty =>
                {
                    if (_changeBodyProperties == null)
                        return;
                    _changeBodyProperties.MeleeAIDifficulty = (int)difficulty;
                }, 1, !ChangeMeleeAI);
            this.MeleeAI = new NumericVM(MeleeAIString, _changeBodyProperties?.MeleeAI ?? 0, 0, 100, true,
                combatAI =>
                {
                    if (_changeBodyProperties == null)
                        return;
                    _changeBodyProperties.MeleeAI = (int)combatAI;
                }, 1, ChangeMeleeAI);
            this.RangedAIDifficulty = new NumericVM(RangedAIDifficultyString, _changeBodyProperties?.RangedAIDifficulty ?? 0, 0, 100, true,
                difficulty =>
                {
                    if (_changeBodyProperties == null)
                        return;
                    _changeBodyProperties.RangedAIDifficulty = (int) difficulty;
                }, 1, !ChangeRangedAI);
            this.RangedAI = new NumericVM(RangedAIString, _changeBodyProperties?.RangedAI ?? 0, 0, 100, true,
                combatAI =>
                {
                    if (_changeBodyProperties == null)
                        return;
                    _changeBodyProperties.RangedAI = (int)combatAI;
                }, 1, ChangeRangedAI);
            this.ChangeMeleeAI = _changeBodyProperties?.ChangeMeleeAI ?? false;
            this.ChangeRangedAI = _changeBodyProperties?.ChangeRangedAI ?? false;
            this.RangedError = new NumericVM(GameTexts.FindText("str_em_lead_error").ToString(), _changeBodyProperties?.RangedError ?? 0, 0, 0.5f, false,
                rangedError =>
                {
                    if (_changeBodyProperties == null)
                        return;
                    _changeBodyProperties.RangedError = rangedError;
                }, 100, true);
        }

        public override void CloseMenu()
        {
            _changeBodyProperties?.SaveConfig();
            base.CloseMenu();
        }
    }
}
