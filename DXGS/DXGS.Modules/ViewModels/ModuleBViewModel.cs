using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DXGS.Common;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DXGS.Modules.ViewModels
{
    public class ModuleBViewModel : ViewModelBase, IDocumentModule, ISupportState<ModuleBViewModel.Info>
    {
        public string Caption { get; private set; }
        public virtual bool IsActive { get; set; }


        public static ModuleBViewModel Create(string caption)
        {
            return ViewModelSource.Create(() => new ModuleBViewModel()
            {
                Caption = caption,
            });
        }

        public ModuleBViewModel() { }

        public string DetailInfo
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        protected override void OnParameterChanged(object parameter)
        {
            base.OnParameterChanged(parameter);
            DetailInfo = string.Format("Detail for {0} item", parameter);
        }

        #region Serialization
        [Serializable]
        public class Info
        {
            public string Caption { get; set; }
        }
        Info ISupportState<Info>.SaveState()
        {
            return new Info()
            {
                Caption = this.Caption,
            };
        }
        void ISupportState<Info>.RestoreState(Info state)
        {
            this.Caption = state.Caption;
        }
        #endregion

    }
}
