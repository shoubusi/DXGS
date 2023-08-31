using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DXGS.Common;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DXGS.Modules.ViewModels
{
    public class ModuleAViewModel : ViewModelBase, IDocumentModule, ISupportState<ModuleAViewModel.Info>
    {
        public string Caption { get; private set; }
        public virtual bool IsActive { get; set; }
        

        public static ModuleAViewModel Create(string caption)
        {
            return ViewModelSource.Create(() => new ModuleAViewModel()
            {
                Caption = caption,
            });
        }

        public ModuleAViewModel() { }


        [Command]
        public void ShowDetail(string item)
        {
            IDialogService service = ServiceContainer.GetService<IDialogService>();
            service.ShowDialog(
                dialogButtons: MessageButton.OK,
                title: "Detail",
                documentType: null,
                parameter: item,
                parentViewModel: this);
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
