using System;
using System.Collections.Generic;
using System.Text;
using EnhancedMission;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EnhancedMissionChangeAI
{
    public class ChangeAIMenuVM : MissionMenuVMBase
    {
        private readonly ChangeBodyProperties _changeBodyProperties = ChangeBodyProperties.Get();

        public string UseRealisticBlockingString { get; } = GameTexts.FindText("str_use_realistic_blocking").ToString();

        public string ChangeMeleeAIString { get; } = GameTexts.FindText("str_change_melee_ai").ToString();
        public string MeleeAIString { get; } = GameTexts.FindText("str_melee_ai").ToString();

        public string ChangeRangedAIString { get; } = GameTexts.FindText("str_change_ranged_ai").ToString();
        public string RangedAIString { get; } = GameTexts.FindText("str_ranged_ai").ToString();


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
        public bool ChangeMeleeAI
        {
            get => _changeBodyProperties?.ChangeMeleeAI ?? false;
            set
            {
                if (_changeBodyProperties == null || _changeBodyProperties.ChangeMeleeAI == value)
                    return;
                _changeBodyProperties.ChangeMeleeAI = value;
                this.MeleeAI.IsVisible = value;
                OnPropertyChanged(nameof(ChangeMeleeAI));
            }
        }

        [DataSourceProperty]
        public NumericVM MeleeAI { get; }

        [DataSourceProperty]
        public bool ChangeRangedAI
        {
            get => _changeBodyProperties?.ChangeRangedAI ?? false;
            set
            {
                if (_changeBodyProperties == null || _changeBodyProperties.ChangeRangedAI == value)
                    return;
                _changeBodyProperties.ChangeRangedAI = value;
                this.RangedAI.IsVisible = value;
                OnPropertyChanged(nameof(ChangeRangedAI));
            }
        }

        [DataSourceProperty]
        public NumericVM RangedAI { get; }

        public ChangeAIMenuVM(Action closeMenu)
            : base(closeMenu)
        {

            this.MeleeAI = new NumericVM(MeleeAIString, _changeBodyProperties?.MeleeAI ?? 0, 0, 100, true,
                combatAI =>
                {
                    if (_changeBodyProperties == null)
                        return;
                    _changeBodyProperties.MeleeAI = (int)combatAI;
                }, 1, ChangeMeleeAI);

            this.RangedAI = new NumericVM(RangedAIString, _changeBodyProperties?.RangedAI ?? 0, 0, 100, true,
                combatAI =>
                {
                    if (_changeBodyProperties == null)
                        return;
                    _changeBodyProperties.RangedAI = (int)combatAI;
                }, 1, ChangeRangedAI);
        }

        public override void CloseMenu()
        {
            _changeBodyProperties?.SaveConfig();
            base.CloseMenu();
        }
    }
}
